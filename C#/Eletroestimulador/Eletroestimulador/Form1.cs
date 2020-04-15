using System;
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
using System.Management;
using System.Threading;
using System.Diagnostics;

namespace Eletroestimulador
{
    public partial class Form1 : Form
    {
        // Dados da comunicação serial
        private string busDescriptionArduino = "Arduino Due Prog. Port";
        private SerialPort ArduinoSerial;
        private bool serialConectada = false;

        // Lista que guarda todas as text boxes do form
        List<TextBox> all_tbs = new List<TextBox>();
        Thread th;


        public Form1()
        {
            InitializeComponent();

            // Adiciona todas as text boxes do form na lista
            all_tbs.Add(textBox_amplitude);
            all_tbs.Add(textBox_freq);
            all_tbs.Add(textBox_larguraPulso);
            all_tbs.Add(textBox_duracaoTotal);
            all_tbs.Add(textBox_duracaoBurst);
            all_tbs.Add(textBox_intervaloBurst);
            all_tbs.Add(textBox_duracaoTB);
            all_tbs.Add(textBox_intervaloTB);
            all_tbs.Add(textBox_aleatTBmin);
            all_tbs.Add(textBox_aleatTBmax);

            // Inicia o valor da text box que guarda o valor da trackbar
            textBox_amplitude.Text = trackBar_iamp.Value.ToString();
        }

        private void button_conectarSerial_Click(object sender, EventArgs e)
        {
            try
            {
                // Cria o objeto de porta serial e inicia com velocidade de 115200 bps
                ArduinoSerial = new SerialPort();
                ArduinoSerial.BaudRate = 115200;

                serialConectada = conectarArduino();    // Inicia a conexão

                // Se a conexão inicar com sucesso o programa habilita os controles e inicia a thread
                //  de leitura da serial
                if (serialConectada)
                {
                    // Muda o estado das labels e ativa os comandos
                    label_statusConexao.Text = "Conectado";
                    label_statusConexao.ForeColor = System.Drawing.Color.Green;
                    ligaGroupBoxes();

                    // Inicia a thread de leitura da serial
                    th = new Thread(rotinaLerSerial);
                    th.IsBackground = true;
                    th.Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        /**
         * Função de leitura da porta serial do Arduino que roda como uma Thread em background
         */ 
        private void rotinaLerSerial()
        {
            // Loop que funciona enquanto a porta com o Arduino estiver aberta
            while (ArduinoSerial.IsOpen)
            {   
                // Toda vez que chega um dado em forma de linha (terminado com '\n') ele é lido
                //  caso o dado seja igual a "STOPED" ou "INITIATED" ele atualiza o form
                if (ArduinoSerial.BytesToRead > 0)
                {
                    string txt = ArduinoSerial.ReadLine();
                    bool ativo = false;
                    // STOPED quer dizer que a estimulação chegou ao fim, então o botão de 'Parar' é desligado
                    //  e o de ativar é ligado
                    if (txt.ToString().Equals("STOPED"))
                        ativo = false;
                    // Caso for INITIATED acontece o inverso do STOPED
                    else if (txt.ToString().Equals("INITIATED"))
                        ativo = true;

                    // Chama a função que muda o estado dos botões
                    changeState(ativo);
                }
            }
        }

        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeStateDelegate(bool ativo);

        private void changeState(bool ativo)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.button_parar.InvokeRequired)
            {
                object[] args = new object[] { ativo };
                changeStateDelegate changeState_Delegate = changeEnabled;
                this.Invoke(changeState_Delegate, args);
            }
            // Caso contrário, apenas muda o botão
            else
                changeEnabled(ativo);
        }

        // Função que ativa e desativa o botão de 'Parar'
        // >> Ressaltando que existe um evento ligado à mudança de estado do botão, que ativa e desativa 
        //      outros componentes do form
        void changeEnabled(bool ativo)
        {
            button_parar.Enabled = ativo;
        }

        /**
         * Essa função procura nos dispositivos do Windows um que possua o nome definido pelo barramento
          "Arduino Due Prog. Port", identifica a porta serial deste e abre a porta.
         * 
         * Retorna se a conexão ocorreu com sucesso.
         */
        private bool conectarArduino()
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
                if (dev.bus_description.ToLower().Equals(busDescriptionArduino.ToLower()))
                {
                    //Localizei um port COM para comunicação USB via CABO.
                    ArduinoSerial.PortName = dev.name;
                    //Abrir port encontrado
                    abrirConexaoArduino();
                    deviceFound = true;
                }
            }

            if (!deviceFound)
            {
                MessageBox.Show("Dispositivo " + busDescriptionArduino + " não encontrado!", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return deviceFound;
        }

        /**
         *  Função que abre a porta serial com o Arduino e deixa ela pronta para ser utilizada
         */ 
        private void abrirConexaoArduino()
        {
            try
            {
                ArduinoSerial.Open();   // Abre a porta
                ArduinoSerial.DiscardInBuffer();    // Descarta os dados do buffer de entrada
                MessageBox.Show("Porta do Arduino aberta com sucesso!", "Porta serial aberta", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                serialConectada = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /**
         * Evento que faz com que toda vez que o botão de 'Parar' é ativado ou desativado o botão de 'Iniciar'
            e a label de 'Status' seja alterada conforme o estado da aquisição
         */ 
        private void button_parar_EnabledChanged(object sender, EventArgs e)
        {
            if (button_parar.Enabled == false)
            {
                button_inciar.Enabled = true;
                label_status.Text = "PARADO";
            }
            else
            {
                button_inciar.Enabled = false;
                label_status.Text = "ATIVO";
            }
        }

        /**
         *  Quando o botão de 'Atualizar' é clicado, o sistema solicita ao Arduino todas as variáveis referentes
             à estimulação, que são recebidas e alocadas nas text boxes do form
         */ 
        private void button_atualizar_Click(object sender, EventArgs e)
        {
            // Suspende a thread de leitura da serial, para evitar conflitos a leitura das variáveis
            if(th.IsAlive)
                th.Suspend();

            ArduinoSerial.WriteLine(Protocolos.atualizar); // Envia solicitação das variáveis
            System.Threading.Thread.Sleep(10);  // Espera o Arduino enviar
            while(ArduinoSerial.BytesToRead > 0)
            {
                string linha = ArduinoSerial.ReadLine();    // Lê o dado recebido

                // Separa o nome da variável do seu valor
                Console.WriteLine(linha);
                string[] dado = linha.Split('\t');

                // Associa a text box do form à variável correspondente do Arduino e grava nela o valor recebido
                TextBox tb = selecionaTB(dado[0]);
                if(tb != null)
                {
                    tb.Text = dado[1];
                }
            }
            // Uma vez que todos os dados recebidos do Arduino são lidos a thread de leitura da serial é retomada
            if(th.IsAlive)
                th.Resume();
        }

        /**
         * Evento relacionado ao início da eletroestimulação
         * Ele coleta todos os dados informados no form acerca da estimulação, atualiza os parâmetros no Arduino
         * de acordo e envia o código de início de estimulação.
         */ 
        private void button_inciar_Click(object sender, EventArgs e)
        {
            string dados = coletaDados();
            dados += Protocolos.iniciar + "\n";
            ArduinoSerial.Write(dados);
        }

        /**
         * Função que pega os dados de todas as text boxes e outros campos do form e forma uma string que 
         * compila todos os códigos de protocolo com seus respectivos valores para que sejam enviados ao 
         * Arduino de forma correta.
         */ 
        private string coletaDados()
        {
            bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            List<TextBox> tbs = new List<TextBox>();
            // Coloca todas as text boxes do form (menos as de aleatoriedade) numa lista
            for (int i = 0; i < (all_tbs.Count - 3); i++)
            {
                tbs.Add(all_tbs[i]);
            } 
            // Se a checkbox de intervalo aleatório estiver marcada, ele considera as text boxes respectivas
            if (rnd_on)
            {
                tbs.Add(textBox_aleatTBmin);
                tbs.Add(textBox_aleatTBmax);
            }
            // Caso contrário ele utiliza apenas a textbox de intervalo fixo e adiciona o protocolo de intervalo
            // aleatório desligado
            else
            {
                tbs.Add(textBox_intervaloTB);
                dados += Protocolos.intervaloRndOFF + "\n";
            }

            Protocolos protocolos = new Protocolos();
            List<string> codigos = protocolos.Cods(rnd_on);

            // Engloba os códigos e seus respectivos valores (duas listas) num só objeto iterável
            var comandos = tbs.Zip(codigos, (val, cod) => new { Valor = val, Codigos = cod });

            // Coloca os protocolos em sequência com o valor
            foreach(var ad in comandos)
            {
                dados += ad.Codigos + ad.Valor.Text + "\n";
            }

            if (radioButton_iPos.Checked)
                dados += Protocolos.correntePositiva + "\n";
            else
                dados += Protocolos.correnteNegativa + "\n";

            return dados;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.Write(coletaDados());
        }

        /**
         *  Função que ativa as group boxes do form
         */ 
        private void ligaGroupBoxes()
        {
            groupBox_burst.Enabled = true;
            groupBox_estimulacao.Enabled = true;
            groupBox_TB.Enabled = true;
            button_inciar.Enabled = true;
            button_atualizar.Enabled = true;
        }

        /**
         * Evento que ativa e desativa as text boxes de aleatoriedade de acordo com a checkbox de 
         * intervalo aleatório
         */ 
        private void checkBox_intervaloTBaleat_CheckedChanged(object sender, EventArgs e)
        {
            bool liga = checkBox_intervaloTBaleat.Checked;

            label_aleatTBmin.Enabled = liga;
            label_aleatTBmax.Enabled = liga;
            textBox_aleatTBmin.Enabled = liga;
            textBox_aleatTBmax.Enabled = liga;
            label_aleatTBms1.Enabled = liga;
            label_aleatTBms2.Enabled = liga;

            label_intervaloTB.Enabled = !liga;
            textBox_intervaloTB.Enabled = !liga;
            label_ms2.Enabled = !liga;

        }

        /**
         * Caso o botão de parar seja clicado, o sistema envia o comando de parar para o Arduino
         */ 
        private void button_parar_Click(object sender, EventArgs e)
        {
            ArduinoSerial.Write(Protocolos.parar + "\n");
        }

        /**
         * Função que recebe o código e associa ele a uma text box do form
         * 
         * @param codigo String recebida do Arduino que corresponde a alguma variável da eletroestimulação
         * @return tb TextBox correspondente à variável recebida do Arduino
         */ 
        private TextBox selecionaTB(string codigo)
        {
            TextBox tb;

            switch(codigo)
            {
                case Protocolos.amplitude:
                    tb = textBox_amplitude;
                    break;

                case Protocolos.frequencia:
                    tb = textBox_freq;
                    break;

                case Protocolos.larguraPulso:
                    tb = textBox_larguraPulso;
                    break;

                case Protocolos.duracaoTotal:
                    tb = textBox_duracaoTotal;
                    break;

                case Protocolos.duracaoBurst:
                    tb = textBox_duracaoBurst;
                    break;

                case Protocolos.intervaloBurst:
                    tb = textBox_intervaloBurst;
                    break;

                case Protocolos.duracaoTB:
                    tb = textBox_duracaoTB;
                    break;

                case Protocolos.intervaloTB:
                    tb = textBox_intervaloTB;
                    break;

                case Protocolos.intervaloTBmax:
                    tb = textBox_aleatTBmax;
                    break;

                case Protocolos.intervaloTBmin:
                    tb = textBox_aleatTBmin;
                    checkBox_intervaloTBaleat.Checked = true;
                    break;

                case Protocolos.intervaloRndOFF:
                    tb = null;
                    checkBox_intervaloTBaleat.Checked = false;
                    break;

                case Protocolos.correntePositiva:
                    tb = null;
                    radioButton_iPos.Checked = true;
                    break;

                case Protocolos.correnteNegativa:
                    tb = null;
                    radioButton_iNeg.Checked = true;
                    break;

                default:
                    tb = null;
                    break;
            }

            return tb;
        }

        /**
         * Finaliza a thread de leitura da serial e fecha a porta serial quando o form é fechado para
         * evitar exceções
         */ 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(ArduinoSerial != null)
                if (ArduinoSerial.IsOpen)
                {
                    th.Abort();
                    ArduinoSerial.Close();
                }
                
        }

        /**
         * Evento que atualiza o valor da text box de amplitude de acordo com a trackbar
         */
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox_amplitude.Text = trackBar_iamp.Value.ToString();
        }

        /**
         *  Evento que atualiza o valor da trackbar de acordo com a textbox de amplitude
         */ 
        private void textBox_amplitude_Leave(object sender, EventArgs e)
        {
            try
            {
                if (textBox_amplitude.Text == "")
                    return;

                int tb_value = Convert.ToInt32(textBox_amplitude.Text.ToString());

                if (tb_value < trackBar_iamp.Minimum)
                    tb_value = trackBar_iamp.Minimum;
                if (tb_value > trackBar_iamp.Maximum)
                    tb_value = trackBar_iamp.Maximum;

                textBox_amplitude.Text = tb_value.ToString();


                trackBar_iamp.Value = Convert.ToInt32(textBox_amplitude.Text.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
