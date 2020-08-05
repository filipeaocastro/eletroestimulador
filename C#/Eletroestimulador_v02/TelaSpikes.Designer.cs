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
            this.button_update = new System.Windows.Forms.Button();
            this.groupBox_currentDirection = new System.Windows.Forms.GroupBox();
            this.radioButton_alternated = new System.Windows.Forms.RadioButton();
            this.radioButton_anodic = new System.Windows.Forms.RadioButton();
            this.radioButton_cathodic = new System.Windows.Forms.RadioButton();
            this.button_connectUc = new System.Windows.Forms.Button();
            this.label_duration = new System.Windows.Forms.Label();
            this.textBox_duration = new System.Windows.Forms.TextBox();
            this.label_duration_ms = new System.Windows.Forms.Label();
            this.label_amplitudeValue = new System.Windows.Forms.Label();
            this.textBox_amplitude = new System.Windows.Forms.TextBox();
            this.label_amplitude = new System.Windows.Forms.Label();
            this.timer_spk = new System.Windows.Forms.Timer(this.components);
            this.button_toggleVisible = new System.Windows.Forms.Button();
            this.numericUpDown_textureNumber = new System.Windows.Forms.NumericUpDown();
            this.label_textureNumber = new System.Windows.Forms.Label();
            this.checkBox_applyParameters = new System.Windows.Forms.CheckBox();
            this.panel_toggle = new System.Windows.Forms.Panel();
            this.button_loadTex = new System.Windows.Forms.Button();
            this.textBox_fileName = new System.Windows.Forms.TextBox();
            this.groupBox_currentDirection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).BeginInit();
            this.panel_toggle.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_update
            // 
            this.button_update.Enabled = false;
            this.button_update.Location = new System.Drawing.Point(66, 385);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(145, 42);
            this.button_update.TabIndex = 6;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.TextChanged += new System.EventHandler(this.button_update_TextChanged);
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // groupBox_currentDirection
            // 
            this.groupBox_currentDirection.Controls.Add(this.radioButton_alternated);
            this.groupBox_currentDirection.Controls.Add(this.radioButton_anodic);
            this.groupBox_currentDirection.Controls.Add(this.radioButton_cathodic);
            this.groupBox_currentDirection.Location = new System.Drawing.Point(15, 139);
            this.groupBox_currentDirection.Name = "groupBox_currentDirection";
            this.groupBox_currentDirection.Size = new System.Drawing.Size(239, 42);
            this.groupBox_currentDirection.TabIndex = 20;
            this.groupBox_currentDirection.TabStop = false;
            this.groupBox_currentDirection.Text = "Current direction";
            // 
            // radioButton_alternated
            // 
            this.radioButton_alternated.AutoSize = true;
            this.radioButton_alternated.Enabled = false;
            this.radioButton_alternated.Location = new System.Drawing.Point(149, 19);
            this.radioButton_alternated.Name = "radioButton_alternated";
            this.radioButton_alternated.Size = new System.Drawing.Size(70, 17);
            this.radioButton_alternated.TabIndex = 2;
            this.radioButton_alternated.TabStop = true;
            this.radioButton_alternated.Text = "Alternada";
            this.radioButton_alternated.UseVisualStyleBackColor = true;
            // 
            // radioButton_anodic
            // 
            this.radioButton_anodic.AutoSize = true;
            this.radioButton_anodic.Location = new System.Drawing.Point(76, 19);
            this.radioButton_anodic.Name = "radioButton_anodic";
            this.radioButton_anodic.Size = new System.Drawing.Size(64, 17);
            this.radioButton_anodic.TabIndex = 1;
            this.radioButton_anodic.TabStop = true;
            this.radioButton_anodic.Text = "Anódica";
            this.radioButton_anodic.UseVisualStyleBackColor = true;
            this.radioButton_anodic.CheckedChanged += new System.EventHandler(this.radioButton_tipoAnodica_CheckedChanged);
            // 
            // radioButton_cathodic
            // 
            this.radioButton_cathodic.AutoSize = true;
            this.radioButton_cathodic.Checked = true;
            this.radioButton_cathodic.Enabled = false;
            this.radioButton_cathodic.Location = new System.Drawing.Point(6, 19);
            this.radioButton_cathodic.Name = "radioButton_cathodic";
            this.radioButton_cathodic.Size = new System.Drawing.Size(67, 17);
            this.radioButton_cathodic.TabIndex = 0;
            this.radioButton_cathodic.TabStop = true;
            this.radioButton_cathodic.Text = "Catódica";
            this.radioButton_cathodic.UseVisualStyleBackColor = true;
            this.radioButton_cathodic.CheckedChanged += new System.EventHandler(this.radioButton_tipoCatodica_CheckedChanged);
            // 
            // button_connectUc
            // 
            this.button_connectUc.Enabled = false;
            this.button_connectUc.Location = new System.Drawing.Point(83, 344);
            this.button_connectUc.Name = "button_connectUc";
            this.button_connectUc.Size = new System.Drawing.Size(107, 35);
            this.button_connectUc.TabIndex = 21;
            this.button_connectUc.Text = "Connect μC";
            this.button_connectUc.UseVisualStyleBackColor = true;
            this.button_connectUc.Click += new System.EventHandler(this.button_connectUc_Click);
            // 
            // label_duration
            // 
            this.label_duration.AutoSize = true;
            this.label_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duration.Location = new System.Drawing.Point(16, 34);
            this.label_duration.Name = "label_duration";
            this.label_duration.Size = new System.Drawing.Size(57, 15);
            this.label_duration.TabIndex = 26;
            this.label_duration.Text = "Duration:";
            // 
            // textBox_duration
            // 
            this.textBox_duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_duration.Location = new System.Drawing.Point(79, 31);
            this.textBox_duration.Name = "textBox_duration";
            this.textBox_duration.Size = new System.Drawing.Size(126, 21);
            this.textBox_duration.TabIndex = 27;
            this.textBox_duration.TextChanged += new System.EventHandler(this.textBox_duration_TextChanged);
            // 
            // label_duration_ms
            // 
            this.label_duration_ms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_duration_ms.Location = new System.Drawing.Point(224, 34);
            this.label_duration_ms.Name = "label_duration_ms";
            this.label_duration_ms.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_duration_ms.Size = new System.Drawing.Size(30, 15);
            this.label_duration_ms.TabIndex = 28;
            this.label_duration_ms.Text = "ms";
            this.label_duration_ms.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_amplitudeValue
            // 
            this.label_amplitudeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitudeValue.Location = new System.Drawing.Point(224, 69);
            this.label_amplitudeValue.Name = "label_amplitudeValue";
            this.label_amplitudeValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_amplitudeValue.Size = new System.Drawing.Size(30, 15);
            this.label_amplitudeValue.TabIndex = 31;
            this.label_amplitudeValue.Text = "μA";
            this.label_amplitudeValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_amplitude
            // 
            this.textBox_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_amplitude.Location = new System.Drawing.Point(79, 66);
            this.textBox_amplitude.Name = "textBox_amplitude";
            this.textBox_amplitude.Size = new System.Drawing.Size(126, 21);
            this.textBox_amplitude.TabIndex = 30;
            this.textBox_amplitude.TextChanged += new System.EventHandler(this.textBox_amplitude_TextChanged);
            // 
            // label_amplitude
            // 
            this.label_amplitude.AutoSize = true;
            this.label_amplitude.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_amplitude.Location = new System.Drawing.Point(8, 69);
            this.label_amplitude.Name = "label_amplitude";
            this.label_amplitude.Size = new System.Drawing.Size(65, 15);
            this.label_amplitude.TabIndex = 29;
            this.label_amplitude.Text = "Amplitude:";
            // 
            // timer_spk
            // 
            this.timer_spk.Interval = 1;
            // 
            // button_toggleVisible
            // 
            this.button_toggleVisible.Enabled = false;
            this.button_toggleVisible.Location = new System.Drawing.Point(243, 12);
            this.button_toggleVisible.Name = "button_toggleVisible";
            this.button_toggleVisible.Size = new System.Drawing.Size(27, 26);
            this.button_toggleVisible.TabIndex = 32;
            this.button_toggleVisible.UseVisualStyleBackColor = true;
            this.button_toggleVisible.Click += new System.EventHandler(this.button_toggleVisible_Click);
            // 
            // numericUpDown_textureNumber
            // 
            this.numericUpDown_textureNumber.Location = new System.Drawing.Point(110, 104);
            this.numericUpDown_textureNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_textureNumber.Name = "numericUpDown_textureNumber";
            this.numericUpDown_textureNumber.Size = new System.Drawing.Size(43, 20);
            this.numericUpDown_textureNumber.TabIndex = 34;
            this.numericUpDown_textureNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_textureNumber.ValueChanged += new System.EventHandler(this.numericUpDown_textureNumber_ValueChanged);
            // 
            // label_textureNumber
            // 
            this.label_textureNumber.AutoSize = true;
            this.label_textureNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_textureNumber.Location = new System.Drawing.Point(8, 104);
            this.label_textureNumber.Name = "label_textureNumber";
            this.label_textureNumber.Size = new System.Drawing.Size(97, 15);
            this.label_textureNumber.TabIndex = 35;
            this.label_textureNumber.Text = "Texture number:";
            // 
            // checkBox_applyParameters
            // 
            this.checkBox_applyParameters.AutoSize = true;
            this.checkBox_applyParameters.Location = new System.Drawing.Point(57, 213);
            this.checkBox_applyParameters.Name = "checkBox_applyParameters";
            this.checkBox_applyParameters.Size = new System.Drawing.Size(136, 17);
            this.checkBox_applyParameters.TabIndex = 36;
            this.checkBox_applyParameters.Text = "Apply these parameters";
            this.checkBox_applyParameters.UseVisualStyleBackColor = true;
            this.checkBox_applyParameters.CheckedChanged += new System.EventHandler(this.checkBox_applyParameters_CheckedChanged);
            // 
            // panel_toggle
            // 
            this.panel_toggle.Controls.Add(this.checkBox_applyParameters);
            this.panel_toggle.Controls.Add(this.groupBox_currentDirection);
            this.panel_toggle.Controls.Add(this.label_textureNumber);
            this.panel_toggle.Controls.Add(this.label_duration);
            this.panel_toggle.Controls.Add(this.numericUpDown_textureNumber);
            this.panel_toggle.Controls.Add(this.textBox_duration);
            this.panel_toggle.Controls.Add(this.label_duration_ms);
            this.panel_toggle.Controls.Add(this.label_amplitudeValue);
            this.panel_toggle.Controls.Add(this.label_amplitude);
            this.panel_toggle.Controls.Add(this.textBox_amplitude);
            this.panel_toggle.Location = new System.Drawing.Point(4, 44);
            this.panel_toggle.Name = "panel_toggle";
            this.panel_toggle.Size = new System.Drawing.Size(266, 242);
            this.panel_toggle.TabIndex = 37;
            this.panel_toggle.Visible = false;
            // 
            // button_loadTex
            // 
            this.button_loadTex.Location = new System.Drawing.Point(12, 12);
            this.button_loadTex.Name = "button_loadTex";
            this.button_loadTex.Size = new System.Drawing.Size(97, 26);
            this.button_loadTex.TabIndex = 38;
            this.button_loadTex.Text = "Load Textures";
            this.button_loadTex.UseVisualStyleBackColor = true;
            this.button_loadTex.Click += new System.EventHandler(this.button_loadTex_Click);
            // 
            // textBox_fileName
            // 
            this.textBox_fileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_fileName.Location = new System.Drawing.Point(95, 306);
            this.textBox_fileName.Name = "textBox_fileName";
            this.textBox_fileName.Size = new System.Drawing.Size(94, 21);
            this.textBox_fileName.TabIndex = 37;
            this.textBox_fileName.Text = "File";
            // 
            // TelaSpikes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 432);
            this.Controls.Add(this.textBox_fileName);
            this.Controls.Add(this.button_loadTex);
            this.Controls.Add(this.button_toggleVisible);
            this.Controls.Add(this.button_connectUc);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.panel_toggle);
            this.Name = "TelaSpikes";
            this.Text = "TelaSpikes1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TelaSpikes_FormClosed);
            this.Click += new System.EventHandler(this.TelaSpikes_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TelaSpikes_KeyDown);
            this.groupBox_currentDirection.ResumeLayout(false);
            this.groupBox_currentDirection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_textureNumber)).EndInit();
            this.panel_toggle.ResumeLayout(false);
            this.panel_toggle.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.GroupBox groupBox_currentDirection;
        private System.Windows.Forms.RadioButton radioButton_alternated;
        private System.Windows.Forms.RadioButton radioButton_anodic;
        private System.Windows.Forms.RadioButton radioButton_cathodic;
        private System.Windows.Forms.Button button_connectUc;
        private System.Windows.Forms.Label label_duration;
        private System.Windows.Forms.TextBox textBox_duration;
        private System.Windows.Forms.Label label_duration_ms;
        private System.Windows.Forms.Label label_amplitudeValue;
        private System.Windows.Forms.TextBox textBox_amplitude;
        private System.Windows.Forms.Label label_amplitude;
        private System.Windows.Forms.Timer timer_spk;
        private System.Windows.Forms.Button button_toggleVisible;
        private System.Windows.Forms.NumericUpDown numericUpDown_textureNumber;
        private System.Windows.Forms.Label label_textureNumber;
        private System.Windows.Forms.CheckBox checkBox_applyParameters;
        private System.Windows.Forms.Panel panel_toggle;
        private System.Windows.Forms.Button button_loadTex;
        private System.Windows.Forms.TextBox textBox_fileName;
    }
}