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
        private string busDescriptionArduino = "Arduino Due Prog. Port";
        private SerialPort ArduinoSerial;
        private bool serialConectada = false;
        List<TextBox> all_tbs = new List<TextBox>();
        Thread th;


        public Form1()
        {
            InitializeComponent();

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
        }

        private void button_conectarSerial_Click(object sender, EventArgs e)
        {
            try
            {
                ArduinoSerial = new SerialPort();
                ArduinoSerial.BaudRate = 115200;

                serialConectada = conectarArduino();
                if (serialConectada)
                {

                    label_statusConexao.Text = "Conectado";
                    label_statusConexao.ForeColor = System.Drawing.Color.Green;
                    ligaGroupBoxes();

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

        private void rotinaLerSerial()
        {
            while (ArduinoSerial.IsOpen)
            {
                if (ArduinoSerial.BytesToRead > 0)
                {
                    string txt = ArduinoSerial.ReadLine();
                    bool ativo = false;

                    if (txt.ToString().Equals("STOPED"))
                        ativo = false;

                    else if (txt.ToString().Equals("INITIATED"))
                        ativo = true;

                    changeState(ativo);
                }
            }
        }

        private delegate void changeStateDelegate(bool ativo);

        private void changeState(bool ativo)
        {
            if (this.button_parar.InvokeRequired)
            {
                object[] args = new object[] { ativo };
                changeStateDelegate changeState_Delegate = changeEnabled;
                this.Invoke(changeState_Delegate, args);
            }
            else
                changeEnabled(ativo);
        }

        void changeEnabled(bool ativo)
        {
            
            button_parar.Enabled = ativo;
        }


        private bool conectarArduino()
        {
            List<Win32DeviceMgmt.DeviceInfo> devices = new List<Win32DeviceMgmt.DeviceInfo>();
            foreach (Win32DeviceMgmt.DeviceInfo dev in Win32DeviceMgmt.GetAllCOMPorts())
            {
                devices.Add(dev);
            }
            //Localizando o canal USB (via CABO USB) para comunicação. 
            bool deviceFound = false;
            //Se o cabo USB estiver conectado (entre host e MIP), este será utilizado.
            foreach (Win32DeviceMgmt.DeviceInfo dev in devices)
            {
                if (dev.bus_description.ToLower().Equals(busDescriptionArduino.ToLower()))
                {
                    //Localizei um port COM para comunicação USB via CABO.
                    ArduinoSerial.PortName = dev.name;
                    //Abrir port encontrado com a descrição do Port associado ao canal USB-serial do MIP.
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
        private void abrirConexaoArduino()
        {
            try
            {
                ArduinoSerial.Open();
                ArduinoSerial.DiscardInBuffer();
                MessageBox.Show("Porta do Arduino aberta com sucesso!", "Porta serial aberta", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                serialConectada = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        private void button_atualizar_Click(object sender, EventArgs e)
        {
            if(th.IsAlive)
                th.Suspend();
            ArduinoSerial.WriteLine(Protocolos.atualizar);
            System.Threading.Thread.Sleep(10);
            while(ArduinoSerial.BytesToRead > 0)
            {
                string linha = ArduinoSerial.ReadLine();
                Console.WriteLine(linha);
                string[] dado = linha.Split('\t');
                TextBox tb = selecionaTB(dado[0]);
                if(tb != null)
                {
                    tb.Text = dado[1];
                }
            }
            if(th.IsAlive)
                th.Resume();
        }

        private void button_inciar_Click(object sender, EventArgs e)
        {
            string dados = coletaDados();
            dados += Protocolos.iniciar + "\n";
            ArduinoSerial.Write(dados);
        }

        private string coletaDados()
        {
            bool rnd_on = checkBox_intervaloTBaleat.Checked;
            string dados = "";

            List<TextBox> tbs = new List<TextBox>();
            for (int i = 0; i < (all_tbs.Count - 3); i++)
            {
                tbs.Add(all_tbs[i]);
            } 
            if (rnd_on)
            {
                tbs.Add(textBox_aleatTBmin);
                tbs.Add(textBox_aleatTBmax);
            }
            else
            {
                tbs.Add(textBox_intervaloTB);
                dados += Protocolos.intervaloRndOFF + "\n";
            }

            Protocolos protocolos = new Protocolos();
            List<string> codigos = protocolos.Cods(rnd_on);

            var comandos = tbs.Zip(codigos, (val, cod) => new { Valor = val, Codigos = cod });

            foreach(var ad in comandos)
            {
                dados += ad.Codigos + ad.Valor.Text + "\n";
            }

            return dados;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.Write(coletaDados());
        }

        private void ligaGroupBoxes()
        {
            groupBox_burst.Enabled = true;
            groupBox_estimulacao.Enabled = true;
            groupBox_TB.Enabled = true;
            button_inciar.Enabled = true;
            button_atualizar.Enabled = true;
        }

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

        private void button_parar_Click(object sender, EventArgs e)
        {
            ArduinoSerial.Write(Protocolos.parar + "\n");
        }

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

                default:
                    tb = null;
                    break;
            }

            return tb;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ArduinoSerial.IsOpen)
            {
                th.Abort();
                ArduinoSerial.Close();
            }
                
        }
    }
}
