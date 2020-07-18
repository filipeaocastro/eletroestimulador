﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Management;
using System.Diagnostics;
using System.Security.Permissions;

namespace Eletroestimulador_v02
{
    public partial class TelaMain : Form
    {
        // Dados da comunicação serial
        private string busDescriptionESP = "CP2102 USB to UART Bridge Controller";
        private SerialPort ESPSerial;
        private bool serialConectada = false;

        private List<TextBox> textBoxes = new List<TextBox>();

        private enum estados_estimulacao
        {
            ATIVO,
            ATUALIZADO,
            DESATUALIZADO
        }

        estados_estimulacao estadoAtual = estados_estimulacao.DESATUALIZADO;

        Thread th;

        public TelaMain()
        {
            InitializeComponent();

            if (radioButton_tipoQuadrada.Checked)
                ativaOndaQ(true);
            else
                ativaOndaQ(false);

            adicionaTBs();
        }

        private void fechaApp(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void radioButton_tipoQuadrada_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_tipoQuadrada.Checked)
                ativaOndaQ(true);
            else
                ativaOndaQ(false);
        }

        private void ativaOndaQ(bool ativa)
        {
            label_pulseWd.Enabled = ativa;
            label_usPulseWd.Enabled = ativa;
            textBox_pulseWd.Enabled = ativa;

        }

        #region Funções de configuração da serial e thread de leitura

        /**
         * Função de leitura da porta serial do ESP que roda como uma Thread em background
         */
        private void rotinaLerSerial()
        {
            // Loop que funciona enquanto a porta com o ESP estiver aberta
            while (ESPSerial.IsOpen)
            {
                // Toda vez que chega um dado em forma de linha (terminado com '\n') ele é lido
                //  caso o dado seja igual a "STOPED" ou "INITIATED" ele atualiza o form
                if (ESPSerial.BytesToRead > 0)
                {
                    string txt = ESPSerial.ReadLine();
                    Console.WriteLine(txt);
                    string newLabel = "";

                    // STOPED quer dizer que a estimulação chegou ao fim, então a label do botão muda 
                    //  pra "Iniciar"
                    if (txt.ToString().Equals("STOPPED"))
                        newLabel = "Iniciar";

                    // Caso for INITIATED, a label muda pra "Parar"
                    else if (txt.ToString().Equals("INITIATED"))
                        newLabel = "Parar";

                    // Ele retorna OK! quando atualizamos os dados no ESP e o botão vira "Iniciar"
                    else if (txt.ToString().Equals("OK!"))
                        newLabel = "Iniciar";


                    // Chama a função que muda o estado dos botões
                    changeState(newLabel);
                }
            }
        }

        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeStateDelegate(string newLabel);

        private void changeState(string newLabel)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.button_iniciar.InvokeRequired)
            {
                object[] args = new object[] { newLabel };
                changeStateDelegate changeState_Delegate = changeLabel;
                this.Invoke(changeState_Delegate, args);
            }
            // Caso contrário, apenas muda o botão
            else
                changeLabel(newLabel);
        }

        // Função que ativa e desativa o botão de 'Parar'
        // >> Ressaltando que existe um evento ligado à mudança de estado do botão, que ativa e desativa 
        //      outros componentes do form
        void changeLabel(string newLabel)
        {
            button_iniciar.Text = newLabel;
        }

        /**
        * Essa função procura nos dispositivos do Windows um que possua o nome definido pelo barramento
         "CP2102 USB to UART Bridge Controller", identifica a porta serial deste e abre a porta.
        * 
        * Retorna se a conexão ocorreu com sucesso.
        */
        private bool conectarESP()
        {
            List<Win32DeviceMgmt.DeviceInfo> devices = new List<Win32DeviceMgmt.DeviceInfo>();
            // Adiciona todas as portas COM conectadas ao Windows à lista
            foreach (Win32DeviceMgmt.DeviceInfo dev in Win32DeviceMgmt.GetAllCOMPorts())
            {
                devices.Add(dev);
            }
            bool deviceFound = false;

            // Compara o nome descrito pelo barramento de todos os devices
            foreach (Win32DeviceMgmt.DeviceInfo dev in devices)
            {
                if (dev.bus_description.ToLower().Equals(busDescriptionESP.ToLower()))
                {
                    //Localizei um port COM para comunicação USB via CABO.
                    ESPSerial.PortName = dev.name;
                    //Abrir port encontrado
                    abrirConexaoESP();
                    deviceFound = true;
                }
            }

            if (!deviceFound)
            {
                MessageBox.Show("Dispositivo " + busDescriptionESP + " não encontrado!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return deviceFound;
        }

        /**
         *  Função que abre a porta serial com o ESP e deixa ela pronta para ser utilizada
         */
        private void abrirConexaoESP()
        {
            try
            {
                ESPSerial.Open();   // Abre a porta
                ESPSerial.DiscardInBuffer();    // Descarta os dados do buffer de entrada
                MessageBox.Show("Porta serial aberta com sucesso!", "Porta serial aberta", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                serialConectada = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Eventos dos botões de Atualizar/Iniciar e Conectar

        /*
         *  Fluxo de funcionamento do envio de dados e mudança de estados
         *  
         *  info: O botão Iniciar/Parar/Atualizar são o mesmo botão, alterando apenas a label.
         *  
         *  Existem 3 estados do sistema: 
         *  => DESATUALIZADO: Quando o ESP não recebeu os dados mais recentes das textboxes, assim o
         *  botão Atualizar/Iniciar fica com a label "Atualizar".
         *  => ATUALIZADO: Quando o ESP está configurado com as informações atuais das textboxes. Assim
         *  o botão Atualizar/Iniciar fica com a label "Iniciar".
         *  => ATIVO: Quando o ESP está fazendo a eletroestimulação, assim o botão Atualizar/Iniciar
         *  fica com a label "Parar".
         *  
         *  O sistema inicia no estado DESATUALIZADO. Ao preencher as textboxes, o usuário pode clicar
         *  em Atualizar e enviar os dados ao ESP. Quando isso acontece, o ESP retorna um "OK!" e só
         *  então o botão Iniciar torna-se disponível. Ao clicar nele o sistema envia o ESP o comando
         *  para iniciar a eletroestimulação, e o mesmo retorna o comando "INITIATED", e só então
         *  o botão Parar torna-se disponível. Ao clicar nele, o sistema envia ao ESP o comando de parar
         *  e o mesmo retorna "STOPPED", e só então o botão Iniciar torna-se disponível.
         *  Caso o botão Iniciar esteja ativo e ocorrer qualquer alteração nas textboxes o mesmo volta
         *  para Atualizar, já que os dados enviados pelo ESP da última vez são diferentes dos presentes
         *  nas textboxes.
         *  
         *  Note que o estado do botão Iniciar/Parar/Atualizar apenas é atualizado quando o ESP responde
         *  ao comando que o sistema enviou. Logo, o sistema e o ESP são codependentes quanto ao seu 
         *  funcionamento.
         * 
         */ 

        private void button_conectar_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria o objeto de porta serial e inicia com velocidade de 115200 bps
                ESPSerial = new SerialPort();
                ESPSerial.BaudRate = 115200;

                serialConectada = conectarESP();    // Inicia a conexão
               

                // Se a conexão inicar com sucesso o programa habilita os controles e inicia a thread
                //  de leitura da serial
                if (serialConectada)
                {
                    // Inicia a thread de leitura da serial
                    th = new Thread(rotinaLerSerial);
                    th.IsBackground = true;
                    th.Start();

                    button_iniciar.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_iniciar_Click(object sender, EventArgs e)
        {
            foreach (TextBox tb in textBoxes)
            {
                if ((tb.Text == null) || (tb.Text == ""))
                {
                    MessageBox.Show("Algum campo não foi preenchido!",
                        "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                switch (estadoAtual)
                {
                    case estados_estimulacao.ATIVO:
                        // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                        ESPSerial.WriteLine(Protocolos.parar);
                        break;

                    case estados_estimulacao.ATUALIZADO:
                        // Enviar comando p/ INICIAR e só mudar o estado quando o ESP avisar que iniciou
                        ESPSerial.WriteLine(Protocolos.iniciar);
                        break;

                    case estados_estimulacao.DESATUALIZADO:
                        // Enviar comandos p/ atualizar os dados do ESP e só mudar o estado quando o ESP der OK!
                        string dados = coletaDados();
                        ESPSerial.Write(dados);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_iniciar_TextChanged(object sender, EventArgs e)
        {
            string texto = button_iniciar.Text;

            switch (texto)
            {
                case "Atualizar":
                    estadoAtual = estados_estimulacao.DESATUALIZADO;
                    break;

                case "Parar":
                    estadoAtual = estados_estimulacao.ATIVO;
                    break;

                case "Iniciar":
                    estadoAtual = estados_estimulacao.ATUALIZADO;
                    break;
            }
        }

        #endregion

        #region Funções complementares

        /**
        * Função que pega os dados de todas as text boxes e outros campos do form e forma uma string que 
        * compila todos os códigos de protocolo com seus respectivos valores para que sejam enviados ao 
        * Arduino de forma correta.
        */
        private string coletaDados()
        {
            //bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            Protocolos protocolos = new Protocolos();
            List<string> codigos = protocolos.Cods();

            // Engloba os códigos e seus respectivos valores (duas listas) num só objeto iterável
            var comandos = textBoxes.Zip(codigos, (val, cod) => new { Valor = val, Codigos = cod });

            // Coloca os protocolos em sequência com o valor
            foreach (var ad in comandos)
            {
                dados += ad.Codigos + ad.Valor.Text + "\n";
            }

            dados += verificaDirecao();
            dados += verificaOnda();

            return dados;
        }

        private void adicionaTBs()
        {
            textBoxes.Add(textBox_amplitude);
            textBoxes.Add(textBox_freq);
            textBoxes.Add(textBox_duracao);
            textBoxes.Add(textBox_pulseWd);
        }

        private string verificaOnda()
        {
            if (radioButton_tipoSenoide.Checked == true)
                return Protocolos.wf_sin + "\n";
            else if (radioButton_tipoQuadrada.Checked == true)
                return Protocolos.wf_square + "\n";
            else if (radioButton_tipoTriangular.Checked == true)
                return Protocolos.wf_triangular + "\n";
            else //(radioButton_tipoDenteDeSerra.Checked == true)
                return Protocolos.wf_sawtooth + "\n";
        }

        private string verificaDirecao()
        {
            if (radioButton_tipoAnodica.Checked == true)
                return Protocolos.iDirection_anodic + "\n";
            else if (radioButton_tipoCatodica.Checked == true)
                return Protocolos.iDirection_cathodic + "\n";
            else
                return Protocolos.iDirection_biDirectional + "\n";
        }

        private void mudaLabelAtualizar()
        {
            estadoAtual = estados_estimulacao.DESATUALIZADO;
            button_iniciar.Text = "Atualizar";
        }

        #endregion

        

        #region Eventos das textboxes para avisar que houve alterações

        private void textBox_amplitude_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_freq_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_duracao_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_pulseWd_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        #endregion
    }
}
