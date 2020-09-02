using Eletroestimulador_v02.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Eletroestimulador_v02
{
    public partial class TestProtocol : Form
    {
        SpikesForm telaSpikes;
        private int[] texSequence = new int[15];
        private int textureSeqIndex = 0;    // Controls the 15 texture sequence
        private int sequenceIndex = 0;      // Controls the screen sequence during the protocol
        private int countdown = 0;
        

        
        Stopwatch interval;
        private long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        private long tempoDecorrido = 0;

        Thread increment_progBar;

        private enum screen_states
        {
            COUNTDOWN,
            IMAGE,
            CROSS
        }
        screen_states screenState;

        private enum test_step
        {
            STOPPED,
            TEXTURE_IMAGES,
            WAITING_FOR_START,
            STIMULATION_ON
        }
        test_step testStep;

        private enum serial_states
        {
            STOPPED = 0,
            INITIATED,
            UPDATED,
            IDLE
        }
        serial_states serial_States = serial_states.IDLE;

        public TestProtocol()
        {
            InitializeComponent();

        }

        public TestProtocol(SpikesForm _telaSpikes)
        {
            InitializeComponent();
            telaSpikes = _telaSpikes;

            generateSequence();

            testStep = test_step.STOPPED;
            Console.WriteLine(telaSpikes.th.IsAlive.ToString());
            telaSpikes.testProtocolOn = true;
            initThreadProgBar();
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (testStep == test_step.STOPPED)
            {
                button_start.Visible = false;
                testStep = test_step.TEXTURE_IMAGES;
                screenState = screen_states.COUNTDOWN;
                sequenceIndex = 1;
                showCountDown(5);
            }

        }

        private void generateSequence()
        {
            int[,] positions = new int[3, 5];
            int cont;

            // Positions matrix:
            // Texture 1: 0 0 0 0 0
            // Texture 2: 0 0 0 0 0
            // Texture 3: 0 0 0 0 0

            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cont = 0;
                    positions[i, j] = rand.Next(0, 15);


                    foreach (int o in positions)
                    {
                        if (o == positions[i, j])
                            cont++;
                    }

                    if (cont > 1)
                        j--;
                }
            }


            Console.Write("Positions = [");
            foreach (int i in positions)
            {
                Console.Write(i.ToString() + ", ");
            }
            Console.WriteLine("]");

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    texSequence[positions[i, j]] = i + 1;
                }
            }

            Console.Write("texSequence = [");
            foreach (int i in texSequence)
            {
                Console.Write(i.ToString() + ", ");
            }
            Console.WriteLine("]");



        }

        private void showCountDown(int count)
        {
            countdown = count;
            screenState = screen_states.COUNTDOWN;
            label_countDown.Visible = true;
            label_countDown.Text = countdown.ToString();
            timer1.Interval = 200; // voltar pra 1000
            timer1.Enabled = true;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (screenState)
            {
                case screen_states.COUNTDOWN:
                    countdown--;
                    if (countdown == 0)
                    {
                        timer1.Stop();

                        if (testStep == test_step.TEXTURE_IMAGES)
                        {
                            label_countDown.Visible = false;
                            screenState = screen_states.IMAGE;
                            showTextures();
                        }
                        if (testStep == test_step.WAITING_FOR_START)
                        {
                            testStep = test_step.STIMULATION_ON;
                            Console.WriteLine("Before start");
                            startStimulation();
                            Console.WriteLine("Started");
                        }
                        if (testStep == test_step.STIMULATION_ON)
                        {

                        }

                        break;
                    }
                    label_countDown.Text = countdown.ToString();
                    break;

                case screen_states.IMAGE:

                    if (sequenceIndex == 3)
                    {
                        sequenceIndex = 0;
                        timer1.Enabled = false;
                        testStep = test_step.WAITING_FOR_START;
                        pictureBox_textura.Image = null;
                        screenState = screen_states.COUNTDOWN;
                        showCountDown(10);
                        sequenceIndex = 0;
                        updateTexture();

                        break;
                    }

                    timer1.Stop();
                    timer1.Enabled = false;
                    screenState = screen_states.COUNTDOWN;
                    pictureBox_textura.Image = null;
                    showCountDown(5);
                    sequenceIndex++;
                    break;

                case screen_states.CROSS:

                    endStimulation();
                    
                    break;

            }
        }

        private void showTextures()
        {
            timer1.Enabled = false;
            timer1.Interval = 1000; // voltar pra 7000
            showImage(sequenceIndex);
            timer1.Enabled = true;
            timer1.Start();
        }

        private void showImage(int texNum)
        {
            pictureBox_textura.Visible = true;
            switch (texNum)
            {
                case 1:
                    pictureBox_textura.Image = Resources.tex1;
                    break;

                case 2:
                    pictureBox_textura.Image = Resources.tex2;
                    break;

                case 3:
                    pictureBox_textura.Image = Resources.tex3;
                    break;
            }
        }

        private void startStimulation()
        {
            checkSerialState();
            if (serial_States != serial_states.UPDATED)
            {
                MessageBox.Show("Something is wrong with the connection", "Erro", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                // ************************** COLOCAR QUE FUNCÃO FINALIZA O PROTOCOLO E FECHA A TELA **********

                return;
            }

            telaSpikes.ESPSerial.WriteLine(Protocolos.iniciar);
            timer1.Stop();
            timer1.Interval = 7300;
            do
            {
                checkSerialState();
            } while (serial_States != serial_states.INITIATED);
            screenState = screen_states.CROSS;

            label_countDown.Visible = true;
            label_countDown.Text = "+";

            progressBar_cross.Visible = true;
            progressBar_cross.Value = 0;
            increment_progBar.Start();
            timer1.Start();

        }

        private void updateTexture()
        {
            telaSpikes.updateTexture(texSequence[textureSeqIndex]);
            telaSpikes.sendData();
            telaSpikes.spikeTransfer();
            telaSpikes.ESPSerial.WriteLine(Protocolos.wf_spike);
            
            Console.WriteLine("Updated");
        }

        private void endStimulation()
        {
            label_countDown.Text = "STOP";
            telaSpikes.ESPSerial.WriteLine(Protocolos.parar);
            if(textureSeqIndex >= (texSequence.Length - 1))
            {
                // COLOCAR AQUI FUNÇÃO QUE SALVA OS DADOS DA ESTIMULAÇÃO NUMA MATRIZ
            }
            else
            {
                textureSeqIndex++;
                showCountDown(5);
            }
            

        }

        private void finishProtocol()
        {
            // ******************** COLOCAR AQUI FUNÇÃO QUE SALVA OS DADOS NO ARQUIVO ****************
        }

        private void checkSerialState()
        {
            switch (telaSpikes.stateProtocol)
            {
                case 0:
                    serial_States = serial_states.STOPPED;
                    break;

                case 1:
                    serial_States = serial_states.INITIATED;
                    break;

                case 2:
                    serial_States = serial_states.UPDATED;
                    break;

                case 3:
                    serial_States = serial_states.IDLE;
                    break;
            }
        }

        private void TestProtocol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Functions relating to the Progress Bar

        private void initThreadProgBar()
        {
            increment_progBar = new Thread(incrementProgBarRoutine);
            increment_progBar.IsBackground = true;
        }

        private void incrementProgBarRoutine()
        {
            Stopwatch interval_progBar = new Stopwatch();
            long nanosecPerTick_progBar = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
            long elapsed = 0;

            interval_progBar.Start();
            while(true)
            {
                if (interval_progBar.ElapsedMilliseconds >= 100)
                {
                    interval_progBar.Reset();
                    interval_progBar.Start();
                    incrementProgBar(110);
                }
            }
            
        }

        private delegate void incrementProgBarDelegate(int increment);

        private void incrementProgBar(int increment)
        {
            // Caso a thread principal esteja usando a trackbar, ele invoca o delegado para que ele seja alterado
            if (this.progressBar_cross.InvokeRequired)
            {
                object[] args = new object[] { increment };
                incrementProgBarDelegate incrementProgBarDelegate = incPB;
                this.Invoke(incrementProgBarDelegate, args);
            }
            // Caso contrário, apenas muda o botão
            else
                incPB(increment);
        }

        private void incPB(int increment)
        {
            progressBar_cross.Increment(increment);
        }

        #endregion

        private void button_status_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:

                    break;

                case Keys.Up:

                    break;

                case Keys.Right:

                    break;

                default:

                    break;
            }
        }
    }
}

/*  TO DO:
 * 
 *  Colocar uma trackbar ao invés de label pra fazer o countdown
 */ 
