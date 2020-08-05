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
using System.Diagnostics;


namespace Eletroestimulador_v02
{
    public partial class TelaSpikes : Form
    {
        // Dados da comunicação serial
        private string busDescriptionESP = "CP2102 USB to UART Bridge Controller";
        private SerialPort ESPSerial;
        private bool serialConectada = false;
        Thread th;

        Stopwatch intervalo_echo;
        private long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        private long tempoDecorrido = 0;



        OpenFileDialog openFileDialog1;
        private FolderBrowserDialog FolderBrowserDialog;
        private string spikesTxt = "";
        private bool[] spikes;
        private UInt16 index_spk = 0;
        private int duration = 0;
        private int samples = 0;

        private string txtPath;

        private int totalTextures = 0;
        private int aleatTexture = 1;
        Random rand;

        // Variáveis para salvar no arquivo txt
        private int nTextura = 0;   // Numero da textura

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

        #region Buttons events

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
                    button_connectUc.Enabled = false;   // Desativa o botão de conectar
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
                        ESPSerial.WriteLine(Protocolos.parar);
                        //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
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
                        if(checkBox_applyParameters.Checked == false)
                        {
                            ESPSerial.WriteLine(Protocolos.amplitude + "2000");
                            ESPSerial.WriteLine(Protocolos.iDirection_anodic);
                        }
                        spikeTransfer();
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
                    //initTimer(true);
                    intervalo_echo = new Stopwatch();
                    intervalo_echo.Start();
                    //enableTBs(false);
                    break;

                case "Start":
                   // ESPSerial.WriteLine(Protocolos.);
                    //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode
                    estadoAtual = estados_estimulacao.ATUALIZADO;
                    //enableTBs(true);
                    break;
            }
        }

        #endregion

        private string coletaDados()
        {
            //bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            //dados += Protocolos.larguraPulso + (trackBar_spikeWidth.Value * 100).ToString() + "\n";   // Largura do spike
            dados += Protocolos.amplitude + textBox_amplitude.Text + "\n"; // Amplitude do spike
            dados += verificaDirecao(); // Verifica os radiobuttons de direção da corrente
            dados += Protocolos.wf_spike + "\n";    // Indica que a forma de onda é por spikes

            // Escreve os dados a serem enviado no console, para fins de depuração
            Console.Write(dados);

            return dados;
        }

        private void trackBar_spikeWidth_Scroll(object sender, EventArgs e)
        {
            //label_spikeWidthValue.Text = (Convert.ToDouble(trackBar_spikeWidth.Value) / 10.0).ToString() + " ms";
            mudaLabelAtualizar();
        }

        private void setParameterLabels(OpenFileDialog ofd)
        {
            int nSpikes = 0;
            //label_fileNameW.Text = ofd.SafeFileName;
            samples = spikesTxt.Length;
            //label_sampleW.Text = samples.ToString();
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
            /*
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
            }*/
            
        }

        /*
         * Confere os radiobuttons relacionados à direção da corrente e retorna o código de protocolo 
         * relacionado à mesma
         */
        private string verificaDirecao()
        {
            if (radioButton_anodic.Checked == true)
                return Protocolos.iDirection_anodic + "\n";
            else if (radioButton_cathodic.Checked == true)
                return Protocolos.iDirection_cathodic + "\n";
            else
                return Protocolos.iDirection_biDirectional + "\n";
        }

        #region Interface functions

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

        private void button_toggleVisible_Click(object sender, EventArgs e)
        {
            componentsVisible();
        }

        private void componentsVisible()
        {
            panel_toggle.Visible = !panel_toggle.Visible;
        }

        private void numericUpDown_textureNumber_ValueChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        private void checkBox_applyParameters_CheckedChanged(object sender, EventArgs e)
        {
            mudaLabelAtualizar();
        }

        #endregion

        private void button_loadTex_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog = new FolderBrowserDialog();
            if (Environment.UserName == "Filipe Augusto")
                FolderBrowserDialog.SelectedPath = "C:\\Users\\Filipe Augusto\\Google Drive\\UFU\\BioLab\\TCC\\texturas";
            //FolderBrowserDialog.Title = "Selecione a pasta";
            //openFileDialog1.Filter = "Text files(*.txt)|*.txt";
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                string reading;
                try
                {
                    var folderPath = FolderBrowserDialog.SelectedPath;
                    Console.WriteLine(folderPath.ToString());
                    string[] files = Directory.GetFiles(FolderBrowserDialog.SelectedPath);
                    foreach (string str in files)
                    {
                        Console.WriteLine(str);
                    }

                    totalTextures = files.Length;
                    numericUpDown_textureNumber.Maximum = totalTextures;
                    rand = new Random();
                    aleatTexture = rand.Next(1, totalTextures + 1);
                    //MessageBox.Show(aleatTexture.ToString());
                    var sr = new StreamReader(files[aleatTexture - 1]);
                    txtPath = files[aleatTexture - 1];
                    
                    Console.WriteLine(files[aleatTexture - 1]);
                    //MessageBox.Show(sr.ReadToEnd());


                    reading = sr.ReadToEnd();
                    reading = reading.Trim();
                    for (int i = 0; i < reading.Length; i++)
                    {
                        if (reading[i] != ',')
                            spikesTxt += reading[i];
                    }

                    spikes = new bool[spikesTxt.Length];

                    button_toggleVisible.Enabled = true;
                    button_connectUc.Enabled = true;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void spikeTransfer()
        {
            int count = 0;
            ESPSerial.WriteLine(Protocolos.init_spk_transfer + spikesTxt.Length.ToString());
            Console.WriteLine(Protocolos.init_spk_transfer + spikesTxt.Length.ToString());
            int dif = 0;
            while(count < spikesTxt.Length)
            {
                dif = spikesTxt.Length - count;
                if (dif >= 60)
                {
                    ESPSerial.WriteLine(spikesTxt.Substring(count, 60));
                    //Console.WriteLine(spikesTxt.Substring(count, 60));
                    count += 60;
                }
                else
                {
                    ESPSerial.WriteLine(spikesTxt.Substring(count));
                    count += dif;
                }
                
            }
            ESPSerial.WriteLine(Protocolos.end_spk_transfer);
        }

        private void TelaSpikes_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Bateu a tecla");
            if( (e.KeyCode == Keys.Up) && (estadoAtual == estados_estimulacao.ATIVO))
            {
                // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                ESPSerial.WriteLine(Protocolos.parar);
                if (intervalo_echo.IsRunning)
                    intervalo_echo.Stop();
                tempoDecorrido = intervalo_echo.ElapsedTicks * nanosecPerTick;
                tempoDecorrido /= 100000;

                string[] lines = {"Caminho do arquivo: " + txtPath, "Tempo decorrido: "
                        + tempoDecorrido.ToString() + " ms"};

                string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "saves",  "WriteLines.txt")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }

                //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode

            }
        }

        private void TelaSpikes_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Bateu a tecla");
            if (estadoAtual == estados_estimulacao.ATIVO)
            {
                // Enviar comando p/ PARAR e o estado só muda quando o ESP avisar que parou
                ESPSerial.WriteLine(Protocolos.parar);
                if (intervalo_echo.IsRunning)
                    intervalo_echo.Stop();
                tempoDecorrido = intervalo_echo.ElapsedTicks * nanosecPerTick;
                tempoDecorrido /= 1000000;

                string[] lines = {"Caminho do arquivo: " + txtPath, "Tempo decorrido: "
                        + tempoDecorrido.ToString() + " ms"};

                string docPath =
          Environment.GetFolderPath(Environment.SpecialFolder.Desktop);


                using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "saves txt", textBox_fileName.Text + ".txt")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }

                //initTimer(false);   // Para o timer, só inicia quando o ESP avisa que pode

            }
        }
    }
}


/* TO DO:
 * 
 *  Criar protocolos para enviar a quantidade total de spikes e carregar eles no ESP.
 * 
 */ 
