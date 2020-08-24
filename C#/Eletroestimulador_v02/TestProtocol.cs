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

namespace Eletroestimulador_v02
{
    public partial class TestProtocol : Form
    {
        SpikesForm telaSpikes;
        private int[] texSequence = new int[15];
        private int sequenceIndex = 0;
        private int countdown = 0;

        Thread th;

        private enum screen_states
        {
            COUNTDOWN,
            IMAGE
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
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if(testStep == test_step.STOPPED)
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
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    cont = 0;
                    positions[i, j] = rand.Next(0, 15);


                    foreach(int o in positions)
                    {
                        if(o == positions[i, j])
                            cont++;
                    }

                    if (cont > 1)
                        j--;
                }
            }


            Console.Write("Positions = [");
            foreach(int i in positions)
            {
                Console.Write(i.ToString() + ", ");
            }
            Console.WriteLine("]");

            texSequence[0] = rand.Next(1, 4);
            while ((texSequence[1] != 0) && (texSequence[1] != texSequence[0]))
                texSequence[1] = rand.Next(1, 4);
            while ((texSequence[2] != 0) && (texSequence[2] != texSequence[1]) &&
                (texSequence[2] != texSequence[0]))
                texSequence[2] = rand.Next(1, 4);
        }

        private void showCountDown(int count)
        {
            countdown = count;
            label_countDown.Visible = true;
            label_countDown.Text = countdown.ToString();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(screenState)
            {
                case screen_states.COUNTDOWN:
                    countdown--;
                    if (countdown == 0)
                    {
                        timer1.Stop();
                        label_countDown.Visible = false;
                        if(testStep == test_step.TEXTURE_IMAGES)
                        {
                            screenState = screen_states.IMAGE;
                            showTextures();
                        }
                        if(testStep == test_step.WAITING_FOR_START)
                        {

                        }

                        return;
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
                        showCountDown(10);
                        return;
                    }

                    timer1.Stop();
                    timer1.Enabled = false;
                    screenState = screen_states.COUNTDOWN;
                    pictureBox_textura.Image = null;
                    showCountDown(5);
                    sequenceIndex++;

                    break;
            }          
        }

        private void showTextures()
        {
            timer1.Enabled = false;
            timer1.Interval = 7000;
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

        private void TestProtocol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
