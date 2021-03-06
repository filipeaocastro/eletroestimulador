﻿namespace Eletroestimulador
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_freq = new System.Windows.Forms.TextBox();
            this.groupBox_estimulacao = new System.Windows.Forms.GroupBox();
            this.groupBox_iDirection = new System.Windows.Forms.GroupBox();
            this.radioButton_iPos = new System.Windows.Forms.RadioButton();
            this.radioButton_iNeg = new System.Windows.Forms.RadioButton();
            this.label_msDuracaoTotal = new System.Windows.Forms.Label();
            this.trackBar_iamp = new System.Windows.Forms.TrackBar();
            this.label_duracaoTotal = new System.Windows.Forms.Label();
            this.textBox_duracaoTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_porcentagem = new System.Windows.Forms.Label();
            this.label_larguraPulso = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_larguraPulso = new System.Windows.Forms.TextBox();
            this.label_hz = new System.Windows.Forms.Label();
            this.label_frequencia = new System.Windows.Forms.Label();
            this.groupBox_burst = new System.Windows.Forms.GroupBox();
            this.label_us2 = new System.Windows.Forms.Label();
            this.label_intervaloBurst = new System.Windows.Forms.Label();
            this.textBox_intervaloBurst = new System.Windows.Forms.TextBox();
            this.label_us1 = new System.Windows.Forms.Label();
            this.label_duracaoBurst = new System.Windows.Forms.Label();
            this.textBox_duracaoBurst = new System.Windows.Forms.TextBox();
            this.groupBox_TB = new System.Windows.Forms.GroupBox();
            this.label_aleatTBms2 = new System.Windows.Forms.Label();
            this.checkBox_intervaloTBaleat = new System.Windows.Forms.CheckBox();
            this.label_aleatTBmax = new System.Windows.Forms.Label();
            this.label_ms2 = new System.Windows.Forms.Label();
            this.textBox_aleatTBmax = new System.Windows.Forms.TextBox();
            this.label_aleatTBms1 = new System.Windows.Forms.Label();
            this.label_intervaloTB = new System.Windows.Forms.Label();
            this.textBox_intervaloTB = new System.Windows.Forms.TextBox();
            this.label_aleatTBmin = new System.Windows.Forms.Label();
            this.label_ms1 = new System.Windows.Forms.Label();
            this.label_duracaoTB = new System.Windows.Forms.Label();
            this.textBox_aleatTBmin = new System.Windows.Forms.TextBox();
            this.textBox_duracaoTB = new System.Windows.Forms.TextBox();
            this.button_atualizar = new System.Windows.Forms.Button();
            this.button_inciar = new System.Windows.Forms.Button();
            this.button_parar = new System.Windows.Forms.Button();
            this.label_statusStatic = new System.Windows.Forms.Label();
            this.label_status = new System.Windows.Forms.Label();
            this.button_conectarSerial = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label_statusConexao = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox_estimulacao.SuspendLayout();
            this.groupBox_iDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_iamp)).BeginInit();
            this.groupBox_burst.SuspendLayout();
            this.groupBox_TB.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_freq
            // 
            this.textBox_freq.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_freq.Location = new System.Drawing.Point(104, 150);
            this.textBox_freq.Name = "textBox_freq";
            this.textBox_freq.Size = new System.Drawing.Size(72, 22);
            this.textBox_freq.TabIndex = 1;
            // 
            // groupBox_estimulacao
            // 
            this.groupBox_estimulacao.Controls.Add(this.groupBox_iDirection);
            this.groupBox_estimulacao.Controls.Add(this.label_msDuracaoTotal);
            this.groupBox_estimulacao.Controls.Add(this.trackBar_iamp);
            this.groupBox_estimulacao.Controls.Add(this.label_duracaoTotal);
            this.groupBox_estimulacao.Controls.Add(this.textBox_duracaoTotal);
            this.groupBox_estimulacao.Controls.Add(this.label1);
            this.groupBox_estimulacao.Controls.Add(this.label_porcentagem);
            this.groupBox_estimulacao.Controls.Add(this.label_larguraPulso);
            this.groupBox_estimulacao.Controls.Add(this.textBox_amplitude);
            this.groupBox_estimulacao.Controls.Add(this.label2);
            this.groupBox_estimulacao.Controls.Add(this.textBox_larguraPulso);
            this.groupBox_estimulacao.Controls.Add(this.label_hz);
            this.groupBox_estimulacao.Controls.Add(this.label_frequencia);
            this.groupBox_estimulacao.Controls.Add(this.textBox_freq);
            this.groupBox_estimulacao.Enabled = false;
            this.groupBox_estimulacao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_estimulacao.Location = new System.Drawing.Point(6, 6);
            this.groupBox_estimulacao.Name = "groupBox_estimulacao";
            this.groupBox_estimulacao.Size = new System.Drawing.Size(211, 239);
            this.groupBox_estimulacao.TabIndex = 1;
            this.groupBox_estimulacao.TabStop = false;
            this.groupBox_estimulacao.Text = "Estimulação";
            // 
            // groupBox_iDirection
            // 
            this.groupBox_iDirection.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox_iDirection.Controls.Add(this.radioButton_iPos);
            this.groupBox_iDirection.Controls.Add(this.radioButton_iNeg);
            this.groupBox_iDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_iDirection.Location = new System.Drawing.Point(0, 87);
            this.groupBox_iDirection.Name = "groupBox_iDirection";
            this.groupBox_iDirection.Size = new System.Drawing.Size(211, 51);
            this.groupBox_iDirection.TabIndex = 22;
            this.groupBox_iDirection.TabStop = false;
            this.groupBox_iDirection.Text = "Direção da corrente";
            // 
            // radioButton_iPos
            // 
            this.radioButton_iPos.AutoSize = true;
            this.radioButton_iPos.Checked = true;
            this.radioButton_iPos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_iPos.Location = new System.Drawing.Point(6, 20);
            this.radioButton_iPos.Name = "radioButton_iPos";
            this.radioButton_iPos.Size = new System.Drawing.Size(74, 20);
            this.radioButton_iPos.TabIndex = 19;
            this.radioButton_iPos.TabStop = true;
            this.radioButton_iPos.Text = "Positiva";
            this.radioButton_iPos.UseVisualStyleBackColor = true;
            // 
            // radioButton_iNeg
            // 
            this.radioButton_iNeg.AutoSize = true;
            this.radioButton_iNeg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton_iNeg.Location = new System.Drawing.Point(124, 20);
            this.radioButton_iNeg.Name = "radioButton_iNeg";
            this.radioButton_iNeg.Size = new System.Drawing.Size(81, 20);
            this.radioButton_iNeg.TabIndex = 20;
            this.radioButton_iNeg.Text = "Negativa";
            this.radioButton_iNeg.UseVisualStyleBackColor = true;
            // 
            // label_msDuracaoTotal
            // 
            this.label_msDuracaoTotal.AutoSize = true;
            this.label_msDuracaoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_msDuracaoTotal.Location = new System.Drawing.Point(180, 209);
            this.label_msDuracaoTotal.Name = "label_msDuracaoTotal";
            this.label_msDuracaoTotal.Size = new System.Drawing.Size(26, 16);
            this.label_msDuracaoTotal.TabIndex = 11;
            this.label_msDuracaoTotal.Text = "ms";
            // 
            // trackBar_iamp
            // 
            this.trackBar_iamp.Location = new System.Drawing.Point(6, 48);
            this.trackBar_iamp.Maximum = 1823;
            this.trackBar_iamp.Minimum = 360;
            this.trackBar_iamp.Name = "trackBar_iamp";
            this.trackBar_iamp.Size = new System.Drawing.Size(123, 45);
            this.trackBar_iamp.TabIndex = 18;
            this.trackBar_iamp.TickFrequency = 100;
            this.trackBar_iamp.Value = 360;
            this.trackBar_iamp.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label_duracaoTotal
            // 
            this.label_duracaoTotal.AutoSize = true;
            this.label_duracaoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duracaoTotal.Location = new System.Drawing.Point(7, 209);
            this.label_duracaoTotal.Name = "label_duracaoTotal";
            this.label_duracaoTotal.Size = new System.Drawing.Size(91, 16);
            this.label_duracaoTotal.TabIndex = 10;
            this.label_duracaoTotal.Text = "Duração total:";
            // 
            // textBox_duracaoTotal
            // 
            this.textBox_duracaoTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_duracaoTotal.Location = new System.Drawing.Point(104, 206);
            this.textBox_duracaoTotal.Name = "textBox_duracaoTotal";
            this.textBox_duracaoTotal.Size = new System.Drawing.Size(72, 22);
            this.textBox_duracaoTotal.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(179, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "μA";
            // 
            // label_porcentagem
            // 
            this.label_porcentagem.AutoSize = true;
            this.label_porcentagem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_porcentagem.Location = new System.Drawing.Point(184, 181);
            this.label_porcentagem.Name = "label_porcentagem";
            this.label_porcentagem.Size = new System.Drawing.Size(22, 16);
            this.label_porcentagem.TabIndex = 5;
            this.label_porcentagem.Text = "μs";
            // 
            // label_larguraPulso
            // 
            this.label_larguraPulso.AutoSize = true;
            this.label_larguraPulso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_larguraPulso.Location = new System.Drawing.Point(6, 181);
            this.label_larguraPulso.Name = "label_larguraPulso";
            this.label_larguraPulso.Size = new System.Drawing.Size(112, 16);
            this.label_larguraPulso.TabIndex = 4;
            this.label_larguraPulso.Text = "Largura de pulso:";
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_amplitude.Location = new System.Drawing.Point(135, 48);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(41, 22);
            this.textBox_amplitude.TabIndex = 0;
            this.textBox_amplitude.Leave += new System.EventHandler(this.textBox_amplitude_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Amplitude:";
            // 
            // textBox_larguraPulso
            // 
            this.textBox_larguraPulso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_larguraPulso.Location = new System.Drawing.Point(124, 178);
            this.textBox_larguraPulso.Name = "textBox_larguraPulso";
            this.textBox_larguraPulso.Size = new System.Drawing.Size(50, 22);
            this.textBox_larguraPulso.TabIndex = 2;
            // 
            // label_hz
            // 
            this.label_hz.AutoSize = true;
            this.label_hz.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_hz.Location = new System.Drawing.Point(182, 153);
            this.label_hz.Name = "label_hz";
            this.label_hz.Size = new System.Drawing.Size(24, 16);
            this.label_hz.TabIndex = 2;
            this.label_hz.Text = "Hz";
            // 
            // label_frequencia
            // 
            this.label_frequencia.AutoSize = true;
            this.label_frequencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_frequencia.Location = new System.Drawing.Point(19, 153);
            this.label_frequencia.Name = "label_frequencia";
            this.label_frequencia.Size = new System.Drawing.Size(79, 16);
            this.label_frequencia.TabIndex = 1;
            this.label_frequencia.Text = "Frequência:";
            // 
            // groupBox_burst
            // 
            this.groupBox_burst.Controls.Add(this.label_us2);
            this.groupBox_burst.Controls.Add(this.label_intervaloBurst);
            this.groupBox_burst.Controls.Add(this.textBox_intervaloBurst);
            this.groupBox_burst.Controls.Add(this.label_us1);
            this.groupBox_burst.Controls.Add(this.label_duracaoBurst);
            this.groupBox_burst.Controls.Add(this.textBox_duracaoBurst);
            this.groupBox_burst.Enabled = false;
            this.groupBox_burst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_burst.Location = new System.Drawing.Point(223, 6);
            this.groupBox_burst.Name = "groupBox_burst";
            this.groupBox_burst.Size = new System.Drawing.Size(211, 78);
            this.groupBox_burst.TabIndex = 9;
            this.groupBox_burst.TabStop = false;
            this.groupBox_burst.Text = "Burst";
            // 
            // label_us2
            // 
            this.label_us2.AutoSize = true;
            this.label_us2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_us2.Location = new System.Drawing.Point(184, 51);
            this.label_us2.Name = "label_us2";
            this.label_us2.Size = new System.Drawing.Size(22, 16);
            this.label_us2.TabIndex = 8;
            this.label_us2.Text = "μs";
            // 
            // label_intervaloBurst
            // 
            this.label_intervaloBurst.AutoSize = true;
            this.label_intervaloBurst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_intervaloBurst.Location = new System.Drawing.Point(7, 51);
            this.label_intervaloBurst.Name = "label_intervaloBurst";
            this.label_intervaloBurst.Size = new System.Drawing.Size(62, 16);
            this.label_intervaloBurst.TabIndex = 7;
            this.label_intervaloBurst.Text = "Intervalo:";
            // 
            // textBox_intervaloBurst
            // 
            this.textBox_intervaloBurst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_intervaloBurst.Location = new System.Drawing.Point(76, 48);
            this.textBox_intervaloBurst.Name = "textBox_intervaloBurst";
            this.textBox_intervaloBurst.Size = new System.Drawing.Size(102, 22);
            this.textBox_intervaloBurst.TabIndex = 5;
            // 
            // label_us1
            // 
            this.label_us1.AutoSize = true;
            this.label_us1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_us1.Location = new System.Drawing.Point(184, 23);
            this.label_us1.Name = "label_us1";
            this.label_us1.Size = new System.Drawing.Size(22, 16);
            this.label_us1.TabIndex = 2;
            this.label_us1.Text = "μs";
            // 
            // label_duracaoBurst
            // 
            this.label_duracaoBurst.AutoSize = true;
            this.label_duracaoBurst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duracaoBurst.Location = new System.Drawing.Point(7, 23);
            this.label_duracaoBurst.Name = "label_duracaoBurst";
            this.label_duracaoBurst.Size = new System.Drawing.Size(63, 16);
            this.label_duracaoBurst.TabIndex = 1;
            this.label_duracaoBurst.Text = "Duração:";
            // 
            // textBox_duracaoBurst
            // 
            this.textBox_duracaoBurst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_duracaoBurst.Location = new System.Drawing.Point(76, 20);
            this.textBox_duracaoBurst.Name = "textBox_duracaoBurst";
            this.textBox_duracaoBurst.Size = new System.Drawing.Size(102, 22);
            this.textBox_duracaoBurst.TabIndex = 4;
            // 
            // groupBox_TB
            // 
            this.groupBox_TB.Controls.Add(this.label_aleatTBms2);
            this.groupBox_TB.Controls.Add(this.checkBox_intervaloTBaleat);
            this.groupBox_TB.Controls.Add(this.label_aleatTBmax);
            this.groupBox_TB.Controls.Add(this.label_ms2);
            this.groupBox_TB.Controls.Add(this.textBox_aleatTBmax);
            this.groupBox_TB.Controls.Add(this.label_aleatTBms1);
            this.groupBox_TB.Controls.Add(this.label_intervaloTB);
            this.groupBox_TB.Controls.Add(this.textBox_intervaloTB);
            this.groupBox_TB.Controls.Add(this.label_aleatTBmin);
            this.groupBox_TB.Controls.Add(this.label_ms1);
            this.groupBox_TB.Controls.Add(this.label_duracaoTB);
            this.groupBox_TB.Controls.Add(this.textBox_aleatTBmin);
            this.groupBox_TB.Controls.Add(this.textBox_duracaoTB);
            this.groupBox_TB.Enabled = false;
            this.groupBox_TB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox_TB.Location = new System.Drawing.Point(223, 90);
            this.groupBox_TB.Name = "groupBox_TB";
            this.groupBox_TB.Size = new System.Drawing.Size(211, 196);
            this.groupBox_TB.TabIndex = 10;
            this.groupBox_TB.TabStop = false;
            this.groupBox_TB.Text = "Trem de Burst";
            // 
            // label_aleatTBms2
            // 
            this.label_aleatTBms2.AutoSize = true;
            this.label_aleatTBms2.Enabled = false;
            this.label_aleatTBms2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aleatTBms2.Location = new System.Drawing.Point(184, 167);
            this.label_aleatTBms2.Name = "label_aleatTBms2";
            this.label_aleatTBms2.Size = new System.Drawing.Size(26, 16);
            this.label_aleatTBms2.TabIndex = 15;
            this.label_aleatTBms2.Text = "ms";
            // 
            // checkBox_intervaloTBaleat
            // 
            this.checkBox_intervaloTBaleat.AutoSize = true;
            this.checkBox_intervaloTBaleat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox_intervaloTBaleat.Location = new System.Drawing.Point(44, 101);
            this.checkBox_intervaloTBaleat.Name = "checkBox_intervaloTBaleat";
            this.checkBox_intervaloTBaleat.Size = new System.Drawing.Size(134, 20);
            this.checkBox_intervaloTBaleat.TabIndex = 8;
            this.checkBox_intervaloTBaleat.Text = "Intervalo aleatório";
            this.checkBox_intervaloTBaleat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_intervaloTBaleat.UseVisualStyleBackColor = true;
            this.checkBox_intervaloTBaleat.CheckedChanged += new System.EventHandler(this.checkBox_intervaloTBaleat_CheckedChanged);
            // 
            // label_aleatTBmax
            // 
            this.label_aleatTBmax.AutoSize = true;
            this.label_aleatTBmax.Enabled = false;
            this.label_aleatTBmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aleatTBmax.Location = new System.Drawing.Point(12, 167);
            this.label_aleatTBmax.Name = "label_aleatTBmax";
            this.label_aleatTBmax.Size = new System.Drawing.Size(58, 16);
            this.label_aleatTBmax.TabIndex = 14;
            this.label_aleatTBmax.Text = "Máximo:";
            // 
            // label_ms2
            // 
            this.label_ms2.AutoSize = true;
            this.label_ms2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ms2.Location = new System.Drawing.Point(184, 66);
            this.label_ms2.Name = "label_ms2";
            this.label_ms2.Size = new System.Drawing.Size(26, 16);
            this.label_ms2.TabIndex = 8;
            this.label_ms2.Text = "ms";
            // 
            // textBox_aleatTBmax
            // 
            this.textBox_aleatTBmax.Enabled = false;
            this.textBox_aleatTBmax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_aleatTBmax.Location = new System.Drawing.Point(76, 164);
            this.textBox_aleatTBmax.Name = "textBox_aleatTBmax";
            this.textBox_aleatTBmax.Size = new System.Drawing.Size(102, 22);
            this.textBox_aleatTBmax.TabIndex = 10;
            // 
            // label_aleatTBms1
            // 
            this.label_aleatTBms1.AutoSize = true;
            this.label_aleatTBms1.Enabled = false;
            this.label_aleatTBms1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aleatTBms1.Location = new System.Drawing.Point(184, 139);
            this.label_aleatTBms1.Name = "label_aleatTBms1";
            this.label_aleatTBms1.Size = new System.Drawing.Size(26, 16);
            this.label_aleatTBms1.TabIndex = 12;
            this.label_aleatTBms1.Text = "ms";
            // 
            // label_intervaloTB
            // 
            this.label_intervaloTB.AutoSize = true;
            this.label_intervaloTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_intervaloTB.Location = new System.Drawing.Point(7, 66);
            this.label_intervaloTB.Name = "label_intervaloTB";
            this.label_intervaloTB.Size = new System.Drawing.Size(62, 16);
            this.label_intervaloTB.TabIndex = 7;
            this.label_intervaloTB.Text = "Intervalo:";
            // 
            // textBox_intervaloTB
            // 
            this.textBox_intervaloTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_intervaloTB.Location = new System.Drawing.Point(76, 63);
            this.textBox_intervaloTB.Name = "textBox_intervaloTB";
            this.textBox_intervaloTB.Size = new System.Drawing.Size(102, 22);
            this.textBox_intervaloTB.TabIndex = 7;
            // 
            // label_aleatTBmin
            // 
            this.label_aleatTBmin.AutoSize = true;
            this.label_aleatTBmin.Enabled = false;
            this.label_aleatTBmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_aleatTBmin.Location = new System.Drawing.Point(16, 139);
            this.label_aleatTBmin.Name = "label_aleatTBmin";
            this.label_aleatTBmin.Size = new System.Drawing.Size(54, 16);
            this.label_aleatTBmin.TabIndex = 11;
            this.label_aleatTBmin.Text = "Mínimo:";
            // 
            // label_ms1
            // 
            this.label_ms1.AutoSize = true;
            this.label_ms1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_ms1.Location = new System.Drawing.Point(184, 35);
            this.label_ms1.Name = "label_ms1";
            this.label_ms1.Size = new System.Drawing.Size(26, 16);
            this.label_ms1.TabIndex = 2;
            this.label_ms1.Text = "ms";
            // 
            // label_duracaoTB
            // 
            this.label_duracaoTB.AutoSize = true;
            this.label_duracaoTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duracaoTB.Location = new System.Drawing.Point(7, 35);
            this.label_duracaoTB.Name = "label_duracaoTB";
            this.label_duracaoTB.Size = new System.Drawing.Size(63, 16);
            this.label_duracaoTB.TabIndex = 1;
            this.label_duracaoTB.Text = "Duração:";
            // 
            // textBox_aleatTBmin
            // 
            this.textBox_aleatTBmin.Enabled = false;
            this.textBox_aleatTBmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_aleatTBmin.Location = new System.Drawing.Point(76, 136);
            this.textBox_aleatTBmin.Name = "textBox_aleatTBmin";
            this.textBox_aleatTBmin.Size = new System.Drawing.Size(102, 22);
            this.textBox_aleatTBmin.TabIndex = 9;
            // 
            // textBox_duracaoTB
            // 
            this.textBox_duracaoTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_duracaoTB.Location = new System.Drawing.Point(76, 32);
            this.textBox_duracaoTB.Name = "textBox_duracaoTB";
            this.textBox_duracaoTB.Size = new System.Drawing.Size(102, 22);
            this.textBox_duracaoTB.TabIndex = 6;
            // 
            // button_atualizar
            // 
            this.button_atualizar.Enabled = false;
            this.button_atualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_atualizar.Location = new System.Drawing.Point(16, 252);
            this.button_atualizar.Name = "button_atualizar";
            this.button_atualizar.Size = new System.Drawing.Size(190, 34);
            this.button_atualizar.TabIndex = 11;
            this.button_atualizar.TabStop = false;
            this.button_atualizar.Text = "Atualizar";
            this.button_atualizar.UseVisualStyleBackColor = true;
            this.button_atualizar.Click += new System.EventHandler(this.button_atualizar_Click);
            // 
            // button_inciar
            // 
            this.button_inciar.Enabled = false;
            this.button_inciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_inciar.ForeColor = System.Drawing.Color.Green;
            this.button_inciar.Location = new System.Drawing.Point(14, 402);
            this.button_inciar.Name = "button_inciar";
            this.button_inciar.Size = new System.Drawing.Size(211, 58);
            this.button_inciar.TabIndex = 11;
            this.button_inciar.Text = "Iniciar \r\neletroestimulação";
            this.button_inciar.UseVisualStyleBackColor = true;
            this.button_inciar.Click += new System.EventHandler(this.button_inciar_Click);
            // 
            // button_parar
            // 
            this.button_parar.Enabled = false;
            this.button_parar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_parar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button_parar.Location = new System.Drawing.Point(230, 402);
            this.button_parar.Name = "button_parar";
            this.button_parar.Size = new System.Drawing.Size(211, 58);
            this.button_parar.TabIndex = 12;
            this.button_parar.Text = "Parar";
            this.button_parar.UseVisualStyleBackColor = true;
            this.button_parar.EnabledChanged += new System.EventHandler(this.button_parar_EnabledChanged);
            this.button_parar.Click += new System.EventHandler(this.button_parar_Click);
            // 
            // label_statusStatic
            // 
            this.label_statusStatic.AutoSize = true;
            this.label_statusStatic.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_statusStatic.Location = new System.Drawing.Point(164, 463);
            this.label_statusStatic.Name = "label_statusStatic";
            this.label_statusStatic.Size = new System.Drawing.Size(60, 20);
            this.label_statusStatic.TabIndex = 9;
            this.label_statusStatic.Text = "Status:";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_status.Location = new System.Drawing.Point(230, 463);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(83, 20);
            this.label_status.TabIndex = 14;
            this.label_status.Text = "PARADO";
            // 
            // button_conectarSerial
            // 
            this.button_conectarSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_conectarSerial.Location = new System.Drawing.Point(250, 22);
            this.button_conectarSerial.Name = "button_conectarSerial";
            this.button_conectarSerial.Size = new System.Drawing.Size(190, 34);
            this.button_conectarSerial.TabIndex = 0;
            this.button_conectarSerial.Text = "Conectar";
            this.button_conectarSerial.UseVisualStyleBackColor = true;
            this.button_conectarSerial.Click += new System.EventHandler(this.button_conectarSerial_Click);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Status de conexão:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_statusConexao
            // 
            this.label_statusConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_statusConexao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label_statusConexao.Location = new System.Drawing.Point(12, 38);
            this.label_statusConexao.Name = "label_statusConexao";
            this.label_statusConexao.Size = new System.Drawing.Size(211, 18);
            this.label_statusConexao.TabIndex = 17;
            this.label_statusConexao.Text = "DESCONECTADO";
            this.label_statusConexao.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(449, 319);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox_estimulacao);
            this.tabPage1.Controls.Add(this.groupBox_burst);
            this.tabPage1.Controls.Add(this.groupBox_TB);
            this.tabPage1.Controls.Add(this.button_atualizar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(441, 293);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(441, 299);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 576);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label_statusConexao);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_conectarSerial);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.label_statusStatic);
            this.Controls.Add(this.button_parar);
            this.Controls.Add(this.button_inciar);
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "Form1";
            this.Text = "Eletroestimulador";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox_estimulacao.ResumeLayout(false);
            this.groupBox_estimulacao.PerformLayout();
            this.groupBox_iDirection.ResumeLayout(false);
            this.groupBox_iDirection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_iamp)).EndInit();
            this.groupBox_burst.ResumeLayout(false);
            this.groupBox_burst.PerformLayout();
            this.groupBox_TB.ResumeLayout(false);
            this.groupBox_TB.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_freq;
        private System.Windows.Forms.GroupBox groupBox_estimulacao;
        private System.Windows.Forms.Label label_hz;
        private System.Windows.Forms.Label label_frequencia;
        private System.Windows.Forms.Label label_porcentagem;
        private System.Windows.Forms.Label label_larguraPulso;
        private System.Windows.Forms.TextBox textBox_larguraPulso;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.GroupBox groupBox_burst;
        private System.Windows.Forms.Label label_us1;
        private System.Windows.Forms.Label label_duracaoBurst;
        private System.Windows.Forms.TextBox textBox_duracaoBurst;
        private System.Windows.Forms.Label label_us2;
        private System.Windows.Forms.Label label_intervaloBurst;
        private System.Windows.Forms.TextBox textBox_intervaloBurst;
        private System.Windows.Forms.GroupBox groupBox_TB;
        private System.Windows.Forms.Label label_ms2;
        private System.Windows.Forms.Label label_intervaloTB;
        private System.Windows.Forms.TextBox textBox_intervaloTB;
        private System.Windows.Forms.Label label_ms1;
        private System.Windows.Forms.Label label_duracaoTB;
        private System.Windows.Forms.TextBox textBox_duracaoTB;
        private System.Windows.Forms.CheckBox checkBox_intervaloTBaleat;
        private System.Windows.Forms.Label label_aleatTBms2;
        private System.Windows.Forms.Label label_aleatTBmax;
        private System.Windows.Forms.TextBox textBox_aleatTBmax;
        private System.Windows.Forms.Label label_aleatTBms1;
        private System.Windows.Forms.Label label_aleatTBmin;
        private System.Windows.Forms.TextBox textBox_aleatTBmin;
        private System.Windows.Forms.Label label_msDuracaoTotal;
        private System.Windows.Forms.Label label_duracaoTotal;
        private System.Windows.Forms.TextBox textBox_duracaoTotal;
        private System.Windows.Forms.Button button_atualizar;
        private System.Windows.Forms.Button button_inciar;
        private System.Windows.Forms.Button button_parar;
        private System.Windows.Forms.Label label_statusStatic;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Button button_conectarSerial;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_statusConexao;
        private System.Windows.Forms.TrackBar trackBar_iamp;
        private System.Windows.Forms.RadioButton radioButton_iNeg;
        private System.Windows.Forms.RadioButton radioButton_iPos;
        private System.Windows.Forms.GroupBox groupBox_iDirection;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

