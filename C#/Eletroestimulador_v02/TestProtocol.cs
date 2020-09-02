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
using System.IO;


namespace Eletroestimulador_v02
{
    public partial class TestProtocol : Form
    {
        SpikesForm telaSpikes;
        private int[] texSequence = new int[15];
        private long[] elapedTime = new long[15];
        private string[] arrowPressedArr = new string[15];
        private int textureSeqIndex = 0;    // Controls the 15 texture sequence
        private int sequenceIndex = 0;      // Controls the screen sequence during the protocol
        private int countdown = 0;
        private string pressedKey;
        

        
        Stopwatch interval = new Stopwatch();
        private long nanosecPerTick = (1000L * 1000L * 1000L) / Stopwatch.Frequency;
        private long tempoDecorrido = 0;

        Thread increment_progBar;

        StreamWriter myStream;
        SaveFileDialog saveFileDialog1;

        #region enums

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

        #endregion

        #region Constructors
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
            increment_progBar.Start();
            increment_progBar.Suspend();
        }
        #endregion

        private void button_start_Click(object sender, EventArgs e)
        {
            if (testStep == test_step.STOPPED)
            {
                button_start.Visible = false;
                testStep = test_step.TEXTURE_IMAGES;
                screenState = screen_states.COUNTDOWN;
                sequenceIndex = 1;
                showCountDown(5);
                button_status.Focus();
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

        #region Countdown and timer functions

        private void showCountDown(int count)
        {
            countdown = count;
            screenState = screen_states.COUNTDOWN;
            label_countDown.Visible = true;
            label_countDown.Text = countdown.ToString();
            timer1.Interval = 1000; // voltar pra 1000
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
                        else if (testStep == test_step.WAITING_FOR_START)
                        {
                            testStep = test_step.STIMULATION_ON;
                            Console.WriteLine("Before start");
                            startStimulation();
                            Console.WriteLine("Started");
                        }
                        else if (testStep == test_step.STIMULATION_ON)
                        {
                            startStimulation();
                        }

                        break;
                    }
                    label_countDown.Text = countdown.ToString();
                    break;

                case screen_states.IMAGE:

                    if (sequenceIndex == 3)
                    {
                        sequenceIndex = 0;
                        timer1.Stop();
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

                    endStimulation(false);
                    
                    break;

            }
        }

        #endregion

        #region Show textures images

        private void showTextures()
        {
            timer1.Enabled = false;
            timer1.Interval = 7000; // voltar pra 7000
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

        #endregion

        #region start/update/end/finish stimulation

        private void startStimulation()
        {
            timer1.Stop();
            checkSerialState();
            if (serial_States != serial_states.UPDATED)
            {
                MessageBox.Show("Something is wrong with the connection\nEstado: " + serial_States.ToString(), 
                    "Erro", MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);

                // ************************** COLOCAR QUE FUNCÃO FINALIZA O PROTOCOLO E FECHA A TELA **********

                return;
            }

            telaSpikes.ESPSerial.WriteLine(Protocolos.iniciar);
            
            timer1.Interval = 7300;
            do
            {
                checkSerialState();
            } while (serial_States != serial_states.INITIATED);

            interval.Start();
            
            screenState = screen_states.CROSS;
            label_countDown.Visible = true;
            label_countDown.Text = "+";

            progressBar_cross.Visible = true;
            progressBar_cross.Value = 0;
            increment_progBar.Resume();  
            timer1.Start(); // Inicia o timer de 7.3 s

            label_counter.Text = (textureSeqIndex + 1).ToString();
            button_status.BackColor = Color.Red;

        }

        private void updateTexture()
        {
            telaSpikes.updateTexture(texSequence[textureSeqIndex]);
            telaSpikes.sendData();
            telaSpikes.spikeTransfer();
            telaSpikes.ESPSerial.WriteLine(Protocolos.wf_spike);
            
            Console.WriteLine("Updated");
        }

        private void endStimulation(bool arrowPressed)
        {
            elapedTime[textureSeqIndex] = interval.ElapsedMilliseconds; // Save the elapsed time on this texture
            telaSpikes.ESPSerial.WriteLine(Protocolos.parar);   // Send the command to stop the stimulation
            serial_States = serial_states.STOPPED;  // Chante de serial state to STOPPED

            // Stop the timer and the Stopwatch
            interval.Stop();
            interval.Reset();
            timer1.Stop();

            button_status.BackColor = Color.Green;

            label_countDown.Text = "STOP";
            if (arrowPressed)
                arrowPressedArr[textureSeqIndex] = pressedKey;
            else
                arrowPressedArr[textureSeqIndex] = "NULL";

            increment_progBar.Suspend();
            progressBar_cross.Visible = false;


            if (textureSeqIndex >= (texSequence.Length - 1))
            {
                finishProtocol();
            }
            else
            {
                textureSeqIndex++;
                showCountDown(5);
                updateTexture();
            }
            

        }

        private void finishProtocol()
        {
            saveOutput();
            this.Close();
        }

        #endregion

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

        private void button_status_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("Tecla apertada");

            if (serial_States == serial_states.INITIATED)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        pressedKey = "LEFT";
                        break;

                    case Keys.Up:
                        pressedKey = "UP";
                        break;

                    case Keys.Right:
                        pressedKey = "RIGHT";
                        break;

                    default:
                        return;
                }

                endStimulation(true);
            }
        }

        #region file output related functions

        private void choosePath()
        {
            try
            {
                saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    myStream = new StreamWriter(saveFileDialog1.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void saveOutput()
        {
            string[] doc = new string[16];
            doc[0] = "Trial\tTexture\tPressed Arrow\tElapsed Time (ms)";
            
            for(int i = 1; i < 16; i ++)
            {
                doc[i] = i.ToString() + "\t" + texSequence[i - 1].ToString() + "\t" 
                    + arrowPressedArr[i - 1] + "\t\t" + elapedTime[i - 1].ToString();
            }

            try
            {
                choosePath();
                if (myStream  != null)
                {
                    foreach(string s in doc)
                    {
                        myStream.WriteLine(s);
                    }
                        
                    myStream.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void TestProtocol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}