using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eletroestimulador_v02
{
    public partial class TelaMain : Form
    {
        public TelaMain()
        {
            InitializeComponent();
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
    }
}
