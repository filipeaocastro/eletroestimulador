﻿namespace Eletroestimulador_v02
{
    partial class TelaInicial
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
            this.label_eletroestimulador = new System.Windows.Forms.Label();
            this.button_carregar = new System.Windows.Forms.Button();
            this.button_novo = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_eletroestimulador
            // 
            this.label_eletroestimulador.AutoSize = true;
            this.label_eletroestimulador.Font = new System.Drawing.Font("Arial Narrow", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_eletroestimulador.Location = new System.Drawing.Point(103, 41);
            this.label_eletroestimulador.Name = "label_eletroestimulador";
            this.label_eletroestimulador.Size = new System.Drawing.Size(268, 31);
            this.label_eletroestimulador.TabIndex = 4;
            this.label_eletroestimulador.Text = "ELETROESTIMULADOR";
            // 
            // button_carregar
            // 
            this.button_carregar.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_carregar.Location = new System.Drawing.Point(274, 107);
            this.button_carregar.Name = "button_carregar";
            this.button_carregar.Size = new System.Drawing.Size(157, 61);
            this.button_carregar.TabIndex = 3;
            this.button_carregar.Text = "CARREGAR\r\nPARÂMETROS";
            this.button_carregar.UseVisualStyleBackColor = true;
            // 
            // button_novo
            // 
            this.button_novo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_novo.Location = new System.Drawing.Point(49, 107);
            this.button_novo.Name = "button_novo";
            this.button_novo.Size = new System.Drawing.Size(157, 61);
            this.button_novo.TabIndex = 5;
            this.button_novo.Text = "NOVO\r\nPROJETO";
            this.button_novo.UseVisualStyleBackColor = true;
            this.button_novo.Click += new System.EventHandler(this.button_novo_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(134, 188);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 33);
            this.button1.TabIndex = 6;
            this.button1.Text = "ESTIMULAÇÃO VIA SPIKES";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // TelaInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 233);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_novo);
            this.Controls.Add(this.button_carregar);
            this.Controls.Add(this.label_eletroestimulador);
            this.Name = "TelaInicial";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_eletroestimulador;
        private System.Windows.Forms.Button button_carregar;
        private System.Windows.Forms.Button button_novo;
        private System.Windows.Forms.Button button1;
    }
}
