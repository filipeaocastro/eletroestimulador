using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.IO.Ports;
using System.Threading;

namespace Eletroestimulador_v02
{
    public partial class TelaSpikes : Form
    {
        // Dados da comunicação serial
        private string busDescriptionESP = "CP2102 USB to UART Bridge Controller";
        private SerialPort ESPSerial;
        private bool serialConectada = false;
        Thread th;

        OpenFileDialog openFileDialog1;
        private string spikesTxt = "";
        private bool[] spikes;
        private UInt16 index_spk = 0;
        private int duration = 0;
        private int samples = 0;

        private enum estados_estimulacao
        {
            ATIVO,
            ATUALIZADO,
            DESATUALIZADO
        }
        estados_estimulacao estadoAtual = estados_estimulacao.DESATUALIZADO;

        public TelaSpikes()
        {
            InitializeComponent();
            textBox_duration.Text = "";
            textBox_amplitude.Text = "";
            setTimer();

            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "Selecione o arquivo de spikes";
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string reading;
                try
                {
                    var sr = new StreamReader(openFileDialog1.FileName);
                    //label_fileNameRO.Text = sr.ReadToEnd();


                    reading = sr.ReadToEnd();
                    reading = reading.Trim();
                    for (int i = 0; i < reading.Length; i++)
                    {
                        if (reading[i] != ',')
                            spikesTxt += reading[i];
                    }

                    spikes = new bool[spikesTxt.Length];



                    setParameterLabels(openFileDialog1);


                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }

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

                    // STOPPED quer dizer que a estimulação chegou ao fim, então a label do botão muda 
                    //  pra "Iniciar"
                    if (txt.ToString().Equals("STOPPED"))
                    {
                        newLabel = "Start";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }


                    // Caso for INITIATED, a label muda pra "Parar"
                    else if (txt.ToString().Equals("INITIATED"))
                    {
                        newLabel = "Stop";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }


                    // Ele retorna OK! quando atualizamos os dados no ESP e o botão vira "Iniciar"
                    else if (txt.ToString().Equals("OK!"))
                    {
                        newLabel = "Start";
                        changeState(newLabel);  // Função que muda o estado dos botões
                    }
                }
            }
        }

        // Como a thread de leitura tenta alterar um parâmetro da thread principal (que rege o form) ela
        //  precisa de um delegado que solicita essa mudança para a thread principal, evitando uma exceção
        private delegate void changeStateDelegate(string newLabel);

        private void changeState(string newLabel)
        {
            // Caso a thread principal esteja usando o botão, ele invoca o delegado para que ele seja alterado
            if (this.button_update.InvokeRequired)
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
        // >> Ressaltando que existe um evento ligado à mudança da label do botão, que altera a 
        //      máquina de estados do sistema
        void changeLabel(string newLabel)
        {
            button_update.Text = newLabel;
        }

        /**
        * Essa função procura nos dispositivos do Windows um que possua o nome definido pelo barramento
         "CP2102 USB to UART Bridge Controller", identifica a porta serial deste e abre a porta.
        * 
        * Retorna se a conexão ocorreu com ou sem sucesso.
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

        private void button_connectUc_Click(object sender, EventArgs e)
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

                    button_update.Enabled = true;  // Ativa o botão iniciar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            // Proteção contra valores nulos nas textboxes
            // Caso uma delas não possua um valor inserido, o sistema não permite que dados sejam
            // enviados ao ESP.
            //foreach (TextBox tb in textBoxes)
            ///{
            ///
            /*
            if (textBox_duration.Text == "" || textBox_amplitude.Text == "")
            {
                MessageBox.Show("Algum campo não foi preenchido!",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }*/
            // }

            /*
             * Switch (case) que define quais comandos serão enviados ao ESP de acordo com o estado
             * do sistema quando ocorrer o clique no botão Iniciar/Atualizar/Parar
             * 
             * Se o sistema estiver ATIVO (eletroestimulação acontecendo) o botão estará com a label 
             * "Parar", então o comando de parar a eletroestimulação é enviado ao ESP.
             * 
             * Se o sistema estiver ATUALIZADO (eletroestimulação pronta parar acontecer) o botão 
             * estará com a label "Iniciar", então o comando de iniciar a eletroestimulação é 
             * enviado ao ESP.
             * 
             * Se o sistema estiver DESATUALIZADO (dados presentes no ESP diferentes dos presentes nas
             * textboxes) o botão estará com a label Atualizar, então os parâmetros atualizados serão
             * enviados ao ESP.
             */
            try
            {
                switch (estadoAtual)
                {
                    case estados_estimulacao.ATIVO:
                        // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                        ESPSerial.WriteLine("2");
                        initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
                        break;

                    case estados_estimulacao.ATUALIZADO:
                        // Enviar comando p/ INICIAR e só mudar o estado quando o ESP avisar que iniciou
                        ESPSerial.WriteLine(Protocolos.iniciar);
                        break;

                    case estados_estimulacao.DESATUALIZADO:
                        // Enviar comandos p/ atualizar os dados do ESP e só mudar o estado quando o ESP der OK!
                        
                        //string dados = coletaDados();
                        //duration = Convert.ToInt32(textBox_duration.Text);
                        //if (duration == 0)
                        //    duration = spikesTxt.Length;
                        ESPSerial.WriteLine(Protocolos.wf_spike);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button_update_TextChanged(object sender, EventArgs e)
        {
            string texto = button_update.Text;

            // Switch (case) que relaciona a label com a alteração de estado do sistema
            switch (texto)
            {
                case "Update":
                    estadoAtual = estados_estimulacao.DESATUALIZADO;
                    //enableTBs(true);
                    break;

                case "Stop":
                    estadoAtual = estados_estimulacao.ATIVO;
                    initTimer(true);
                    //enableTBs(false);
                    break;

                case "Start":
                    ESPSerial.WriteLine("2");
                    initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
                    estadoAtual = estados_estimulacao.ATUALIZADO;
                    //enableTBs(true);
                    break;
            }
        }

        private string coletaDados()
        {
            //bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            dados += Protocolos.larguraPulso + (trackBar_spikeWidth.Value * 100).ToString() + "\n";   // Largura do spike
            dados += Protocolos.amplitude + textBox_amplitude.Text + "\n"; // Amplitude do spike
            dados += verificaDirecao(); // Verifica os radiobuttons de direção da corrente
            dados += Protocolos.wf_spike + "\n";    // Indica que a forma de onda é por spikes

            // Escreve os dados a serem enviado no console, para fins de depuração
            Console.Write(dados);

            return dados;
        }

        private void trackBar_spikeWidth_Scroll(object sender, EventArgs e)
        {
            label_spikeWidthValue.Text = (Convert.ToDouble(trackBar_spikeWidth.Value) / 10.0).ToString() + " ms";
            mudaLabelAtualizar();
        }

        private void setParameterLabels(OpenFileDialog ofd)
        {
            int nSpikes = 0;
            label_fileNameW.Text = ofd.SafeFileName;
            samples = spikesTxt.Length;
            label_sampleW.Text = samples.ToString();
            for (int i = 0; i < spikesTxt.Length; i++)
                if (spikesTxt[i] == 1)
                    nSpikes++;
            
        }

        private void setTimer()
        {
            timer_spk.Tick += new EventHandler(timer_spk_Tick);
        }

        private void timer_spk_Tick(object Sender, EventArgs e)
        {
            ESPSerial.WriteLine(spikesTxt[index_spk].ToString());
            index_spk++;
            if (index_spk >= samples)
                index_spk = 0;
            if (index_spk > duration)
                button_update.Text = "Start";
        }

        private void initTimer(bool timerOn)
        {
            if (timerOn)
            {
                index_spk = 0;
                if (th.ThreadState == ThreadState.Running)
                    th.Suspend();

                timer_spk.Enabled = true;
            }
            else
            {
                timer_spk.Enabled = false;
                if(th.ThreadState == ThreadState.Suspended)
                    th.Resume();
            }
            
        }

        /*
         * Confere os radiobuttons relacionados à direção da corrente e retorna o código de protocolo 
         * relacionado à mesma
         */
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
            button_update.Text = "Update";
        }

        private void textBox_duration_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void textBox_amplitude_TextChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void radioButton_tipoCatodica_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void radioButton_tipoAnodica_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void TelaSpikes_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
