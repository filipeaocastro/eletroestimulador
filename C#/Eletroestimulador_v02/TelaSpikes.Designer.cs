namespace Eletroestimulador_v02
{
    partial class TelaSpikes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label_fileNameRO = new System.Windows.Forms.Label();
            this.label_sampleRO = new System.Windows.Forms.Label();
            this.label_spikesRO = new System.Windows.Forms.Label();
            this.trackBar_spikeWidth = new System.Windows.Forms.TrackBar();
            this.label_spikeWidth = new System.Windows.Forms.Label();
            this.label_spikeWidthValue = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.groupBox_tipoCorrente = new System.Windows.Forms.GroupBox();
            this.radioButton_tipoAlternada = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoAnodica = new System.Windows.Forms.RadioButton();
            this.radioButton_tipoCatodica = new System.Windows.Forms.RadioButton();
            this.button_connectUc = new System.Windows.Forms.Button();
            this.label_fileNameW = new System.Windows.Forms.Label();
            this.label_sampleW = new System.Windows.Forms.Label();
            this.label_spikesW = new System.Windows.Forms.Label();
            this.label_duration = new System.Windows.Forms.Label();
            this.textBox_duration = new System.Windows.Forms.TextBox();
            this.label_duration_ms = new System.Windows.Forms.Label();
            this.label_amplitudeValue = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.label_amplitude = new System.Windows.Forms.Label();
            this.timer_spk = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_spikeWidth)).BeginInit();
            this.groupBox_tipoCorrente.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_fileNameRO
            // 
            this.label_fileNameRO.AutoSize = true;
            this.label_fileNameRO.Enabled = false;
            this.label_fileNameRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fileNameRO.Location = new System.Drawing.Point(12, 9);
            this.label_fileNameRO.Name = "label_fileNameRO";
            this.label_fileNameRO.Size = new System.Drawing.Size(65, 15);
            this.label_fileNameRO.TabIndex = 0;
            this.label_fileNameRO.Text = "File name:";
            // 
            // label_sampleRO
            // 
            this.label_sampleRO.AutoSize = true;
            this.label_sampleRO.Enabled = false;
            this.label_sampleRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sampleRO.Location = new System.Drawing.Point(12, 34);
            this.label_sampleRO.Name = "label_sampleRO";
            this.label_sampleRO.Size = new System.Drawing.Size(59, 15);
            this.label_sampleRO.TabIndex = 1;
            this.label_sampleRO.Text = "Samples:";
            // 
            // label_spikesRO
            // 
            this.label_spikesRO.AutoSize = true;
            this.label_spikesRO.Enabled = false;
            this.label_spikesRO.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spikesRO.Location = new System.Drawing.Point(12, 60);
            this.label_spikesRO.Name = "label_spikesRO";
            this.label_spikesRO.Size = new System.Drawing.Size(47, 15);
            this.label_spikesRO.TabIndex = 2;
            this.label_spikesRO.Text = "Spikes:";
            // 
            // trackBar_spikeWidth
            // 
            this.trackBar_spikeWidth.Enabled = false;
            this.trackBar_spikeWidth.LargeChange = 2;
            this.trackBar_spikeWidth.Location = new System.Drawing.Point(15, 131);
            this.trackBar_spikeWidth.Name = "trackBar_spikeWidth";
            this.trackBar_spikeWidth.Size = new System.Drawing.Size(181, 45);
            this.trackBar_spikeWidth.TabIndex = 2;
            this.trackBar_spikeWidth.Scroll += new System.EventHandler(this.trackBar_spikeWidth_Scroll);
            // 
            // label_spikeWidth
            // 
            this.label_spikeWidth.AutoSize = true;
            this.label_spikeWidth.Enabled = false;
            this.label_spikeWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spikeWidth.Location = new System.Drawing.Point(12, 113);
            this.label_spikeWidth.Name = "label_spikeWidth";
            this.label_spikeWidth.Size = new System.Drawing.Size(73, 15);
            this.label_spikeWidth.TabIndex = 4;
            this.label_spikeWidth.Text = "Spike width:";
            // 
            // label_spikeWidthValue
            // 
            this.label_spikeWidthValue.AutoSize = true;
            this.label_spikeWidthValue.Enabled = false;
            this.label_spikeWidthValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spikeWidthValue.Location = new System.Drawing.Point(202, 131);
            this.label_spikeWidthValue.Name = "label_spikeWidthValue";
            this.label_spikeWidthValue.Size = new System.Drawing.Size(44, 15);
            this.label_spikeWidthValue.TabIndex = 5;
            this.label_spikeWidthValue.Text = "0.1 ms";
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(43, 396);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(145, 42);
            this.button_update.TabIndex = 6;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.TextChanged += new System.EventHandler(this.button_update_TextChanged);
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // groupBox_tipoCorrente
            // 
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoAlternada);
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoAnodica);
            this.groupBox_tipoCorrente.Controls.Add(this.radioButton_tipoCatodica);
            this.groupBox_tipoCorrente.Location = new System.Drawing.Point(12, 285);
            this.groupBox_tipoCorrente.Name = "groupBox_tipoCorrente";
            this.groupBox_tipoCorrente.Size = new System.Drawing.Size(239, 42);
            this.groupBox_tipoCorrente.TabIndex = 20;
            this.groupBox_tipoCorrente.TabStop = false;
            this.groupBox_tipoCorrente.Text = "Tipo de corrente";
            // 
            // radioButton_tipoAlternada
            // 
            this.radioButton_tipoAlternada.AutoSize = true;
            this.radioButton_tipoAlternada.Enabled = false;
            this.radioButton_tipoAlternada.Location = new System.Drawing.Point(149, 19);
            this.radioButton_tipoAlternada.Name = "radioButton_tipoAlternada";
            this.radioButton_tipoAlternada.Size = new System.Drawing.Size(70, 17);
            this.radioButton_tipoAlternada.TabIndex = 2;
            this.radioButton_tipoAlternada.TabStop = true;
            this.radioButton_tipoAlternada.Text = "Alternada";
            this.radioButton_tipoAlternada.UseVisualStyleBackColor = true;
            // 
            // radioButton_tipoAnodica
            // 
            this.radioButton_tipoAnodica.AutoSize = true;
            this.radioButton_tipoAnodica.Location = new System.Drawing.Point(76, 19);
            this.radioButton_tipoAnodica.Name = "radioButton_tipoAnodica";
            this.radioButton_tipoAnodica.Size = new System.Drawing.Size(64, 17);
            this.radioButton_tipoAnodica.TabIndex = 1;
            this.radioButton_tipoAnodica.TabStop = true;
            this.radioButton_tipoAnodica.Text = "Anódica";
            this.radioButton_tipoAnodica.UseVisualStyleBackColor = true;
            this.radioButton_tipoAnodica.CheckedChanged += new System.EventHandler(this.radioButton_tipoAnodica_CheckedChanged);
            // 
            // radioButton_tipoCatodica
            // 
            this.radioButton_tipoCatodica.AutoSize = true;
            this.radioButton_tipoCatodica.Checked = true;
            this.radioButton_tipoCatodica.Enabled = false;
            this.radioButton_tipoCatodica.Location = new System.Drawing.Point(6, 19);
            this.radioButton_tipoCatodica.Name = "radioButton_tipoCatodica";
            this.radioButton_tipoCatodica.Size = new System.Drawing.Size(67, 17);
            this.radioButton_tipoCatodica.TabIndex = 0;
            this.radioButton_tipoCatodica.TabStop = true;
            this.radioButton_tipoCatodica.Text = "Catódica";
            this.radioButton_tipoCatodica.UseVisualStyleBackColor = true;
            this.radioButton_tipoCatodica.CheckedChanged += new System.EventHandler(this.radioButton_tipoCatodica_CheckedChanged);
            // 
            // button_connectUc
            // 
            this.button_connectUc.Location = new System.Drawing.Point(61, 355);
            this.button_connectUc.Name = "button_connectUc";
            this.button_connectUc.Size = new System.Drawing.Size(107, 35);
            this.button_connectUc.TabIndex = 21;
            this.button_connectUc.Text = "Connect μC";
            this.button_connectUc.UseVisualStyleBackColor = true;
            this.button_connectUc.Click += new System.EventHandler(this.button_connectUc_Click);
            // 
            // label_fileNameW
            // 
            this.label_fileNameW.Enabled = false;
            this.label_fileNameW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_fileNameW.Location = new System.Drawing.Point(88, 9);
            this.label_fileNameW.Name = "label_fileNameW";
            this.label_fileNameW.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_fileNameW.Size = new System.Drawing.Size(163, 15);
            this.label_fileNameW.TabIndex = 22;
            this.label_fileNameW.Text = "(file name)";
            this.label_fileNameW.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_sampleW
            // 
            this.label_sampleW.Enabled = false;
            this.label_sampleW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_sampleW.Location = new System.Drawing.Point(88, 34);
            this.label_sampleW.Name = "label_sampleW";
            this.label_sampleW.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_sampleW.Size = new System.Drawing.Size(163, 15);
            this.label_sampleW.TabIndex = 23;
            this.label_sampleW.Text = "(samples)";
            this.label_sampleW.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_spikesW
            // 
            this.label_spikesW.Enabled = false;
            this.label_spikesW.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_spikesW.Location = new System.Drawing.Point(88, 60);
            this.label_spikesW.Name = "label_spikesW";
            this.label_spikesW.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_spikesW.Size = new System.Drawing.Size(163, 15);
            this.label_spikesW.TabIndex = 24;
            this.label_spikesW.Text = "(spikes)";
            this.label_spikesW.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_duration
            // 
            this.label_duration.AutoSize = true;
            this.label_duration.Enabled = false;
            this.label_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duration.Location = new System.Drawing.Point(14, 179);
            this.label_duration.Name = "label_duration";
            this.label_duration.Size = new System.Drawing.Size(57, 15);
            this.label_duration.TabIndex = 26;
            this.label_duration.Text = "Duration:";
            // 
            // textBox_duration
            // 
            this.textBox_duration.Enabled = false;
            this.textBox_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_duration.Location = new System.Drawing.Point(91, 176);
            this.textBox_duration.Name = "textBox_duration";
            this.textBox_duration.Size = new System.Drawing.Size(126, 21);
            this.textBox_duration.TabIndex = 27;
            this.textBox_duration.TextChanged += new System.EventHandler(this.textBox_duration_TextChanged);
            // 
            // label_duration_ms
            // 
            this.label_duration_ms.Enabled = false;
            this.label_duration_ms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duration_ms.Location = new System.Drawing.Point(223, 179);
            this.label_duration_ms.Name = "label_duration_ms";
            this.label_duration_ms.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_duration_ms.Size = new System.Drawing.Size(30, 15);
            this.label_duration_ms.TabIndex = 28;
            this.label_duration_ms.Text = "ms";
            this.label_duration_ms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_amplitudeValue
            // 
            this.label_amplitudeValue.Enabled = false;
            this.label_amplitudeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitudeValue.Location = new System.Drawing.Point(223, 205);
            this.label_amplitudeValue.Name = "label_amplitudeValue";
            this.label_amplitudeValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_amplitudeValue.Size = new System.Drawing.Size(30, 15);
            this.label_amplitudeValue.TabIndex = 31;
            this.label_amplitudeValue.Text = "μA";
            this.label_amplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Enabled = false;
            this.textBox_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_amplitude.Location = new System.Drawing.Point(91, 202);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(126, 21);
            this.textBox_amplitude.TabIndex = 30;
            this.textBox_amplitude.TextChanged += new System.EventHandler(this.textBox_amplitude_TextChanged);
            // 
            // label_amplitude
            // 
            this.label_amplitude.AutoSize = true;
            this.label_amplitude.Enabled = false;
            this.label_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitude.Location = new System.Drawing.Point(14, 205);
            this.label_amplitude.Name = "label_amplitude";
            this.label_amplitude.Size = new System.Drawing.Size(65, 15);
            this.label_amplitude.TabIndex = 29;
            this.label_amplitude.Text = "Amplitude:";
            // 
            // timer_spk
            // 
            this.timer_spk.Interval = 1;
            // 
            // TelaSpikes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 450);
            this.Controls.Add(this.label_amplitudeValue);
            this.Controls.Add(this.textBox_amplitude);
            this.Controls.Add(this.label_amplitude);
            this.Controls.Add(this.label_duration_ms);
            this.Controls.Add(this.textBox_duration);
            this.Controls.Add(this.label_duration);
            this.Controls.Add(this.label_spikesW);
            this.Controls.Add(this.label_sampleW);
            this.Controls.Add(this.label_fileNameW);
            this.Controls.Add(this.button_connectUc);
            this.Controls.Add(this.groupBox_tipoCorrente);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.label_spikeWidthValue);
            this.Controls.Add(this.label_spikeWidth);
            this.Controls.Add(this.trackBar_spikeWidth);
            this.Controls.Add(this.label_spikesRO);
            this.Controls.Add(this.label_sampleRO);
            this.Controls.Add(this.label_fileNameRO);
            this.Name = "TelaSpikes";
            this.Text = "TelaSpikes1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TelaSpikes_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_spikeWidth)).EndInit();
            this.groupBox_tipoCorrente.ResumeLayout(false);
            this.groupBox_tipoCorrente.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_fileNameRO;
        private System.Windows.Forms.Label label_sampleRO;
        private System.Windows.Forms.Label label_spikesRO;
        private System.Windows.Forms.TrackBar trackBar_spikeWidth;
        private System.Windows.Forms.Label label_spikeWidth;
        private System.Windows.Forms.Label label_spikeWidthValue;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.GroupBox groupBox_tipoCorrente;
        private System.Windows.Forms.RadioButton radioButton_tipoAlternada;
        private System.Windows.Forms.RadioButton radioButton_tipoAnodica;
        private System.Windows.Forms.RadioButton radioButton_tipoCatodica;
        private System.Windows.Forms.Button button_connectUc;
        private System.Windows.Forms.Label label_fileNameW;
        private System.Windows.Forms.Label label_sampleW;
        private System.Windows.Forms.Label label_spikesW;
        private System.Windows.Forms.Label label_duration;
        private System.Windows.Forms.TextBox textBox_duration;
        private System.Windows.Forms.Label label_duration_ms;
        private System.Windows.Forms.Label label_amplitudeValue;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.Label label_amplitude;
        private System.Windows.Forms.Timer timer_spk;
    }
}