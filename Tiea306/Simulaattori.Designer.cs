using System;
using System.Windows.Forms;
using System.Text;
using OpenGL;

namespace Tiea306
{
    partial class Simulaattori
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.glControl1 = new OpenGL.GlControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.rataViivojenPituus = new System.Windows.Forms.NumericUpDown();
            this.rataviivat = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.kiihtyvyysviivat = new System.Windows.Forms.CheckBox();
            this.nopeusviivat = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BHViivat = new System.Windows.Forms.CheckBox();
            this.Aikateksti = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rataViivojenPituus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.glControl1);
            this.panel1.Location = new System.Drawing.Point(139, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(647, 527);
            this.panel1.TabIndex = 0;
            // 
            // glControl1
            // 
            this.glControl1.Animation = true;
            this.glControl1.AnimationTimer = false;
            this.glControl1.BackColor = System.Drawing.Color.DimGray;
            this.glControl1.ColorBits = ((uint)(24u));
            this.glControl1.DepthBits = ((uint)(24u));
            this.glControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.glControl1.Location = new System.Drawing.Point(0, 0);
            this.glControl1.MultisampleBits = ((uint)(0u));
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(647, 527);
            this.glControl1.StencilBits = ((uint)(0u));
            this.glControl1.TabIndex = 0;
            this.glControl1.ContextCreated += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_ContextCreated);
            this.glControl1.Render += new System.EventHandler<OpenGL.GlControlEventArgs>(this.glControl1_Render);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.Controls.Add(this.Aikateksti);
            this.panel2.Controls.Add(this.BHViivat);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.rataViivojenPituus);
            this.panel2.Controls.Add(this.rataviivat);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.kiihtyvyysviivat);
            this.panel2.Controls.Add(this.nopeusviivat);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(121, 527);
            this.panel2.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 154);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Rataviivojen pituus";
            // 
            // rataViivojenPituus
            // 
            this.rataViivojenPituus.Location = new System.Drawing.Point(3, 170);
            this.rataViivojenPituus.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.rataViivojenPituus.Name = "rataViivojenPituus";
            this.rataViivojenPituus.Size = new System.Drawing.Size(81, 20);
            this.rataViivojenPituus.TabIndex = 11;
            this.rataViivojenPituus.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // rataviivat
            // 
            this.rataviivat.AutoSize = true;
            this.rataviivat.Location = new System.Drawing.Point(3, 134);
            this.rataviivat.Name = "rataviivat";
            this.rataviivat.Size = new System.Drawing.Size(74, 17);
            this.rataviivat.TabIndex = 10;
            this.rataviivat.Text = "Rataviivat";
            this.rataviivat.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "(Liiku hiirellä)";
            // 
            // kiihtyvyysviivat
            // 
            this.kiihtyvyysviivat.AutoSize = true;
            this.kiihtyvyysviivat.Location = new System.Drawing.Point(3, 111);
            this.kiihtyvyysviivat.Name = "kiihtyvyysviivat";
            this.kiihtyvyysviivat.Size = new System.Drawing.Size(97, 17);
            this.kiihtyvyysviivat.TabIndex = 8;
            this.kiihtyvyysviivat.Text = "Kiihtyvyysviivat";
            this.kiihtyvyysviivat.UseVisualStyleBackColor = true;
            // 
            // nopeusviivat
            // 
            this.nopeusviivat.AutoSize = true;
            this.nopeusviivat.Location = new System.Drawing.Point(3, 88);
            this.nopeusviivat.Name = "nopeusviivat";
            this.nopeusviivat.Size = new System.Drawing.Size(88, 17);
            this.nopeusviivat.TabIndex = 7;
            this.nopeusviivat.Text = "Nopeusviivat";
            this.nopeusviivat.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 277);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Zoomaustaso:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "1 valovuosi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "0 vuotta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 264);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Simulaation tarkkuus:";
            // 
            // BHViivat
            // 
            this.BHViivat.AutoSize = true;
            this.BHViivat.Location = new System.Drawing.Point(3, 65);
            this.BHViivat.Name = "BHViivat";
            this.BHViivat.Size = new System.Drawing.Size(107, 17);
            this.BHViivat.TabIndex = 17;
            this.BHViivat.Text = "Barnes-Hut viivat";
            this.BHViivat.UseVisualStyleBackColor = true;
            // 
            // Aikateksti
            // 
            this.Aikateksti.AutoSize = true;
            this.Aikateksti.Location = new System.Drawing.Point(3, 212);
            this.Aikateksti.Name = "Aikateksti";
            this.Aikateksti.Size = new System.Drawing.Size(69, 13);
            this.Aikateksti.TabIndex = 18;
            this.Aikateksti.Text = "Kulunut aika:";
            // 
            // Simulaattori
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 551);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Simulaattori";
            this.Text = "Simulaattori";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rataViivojenPituus)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion        

        
        
        private System.Windows.Forms.Panel panel1;
        private OpenGL.GlControl glControl1;
        private System.Windows.Forms.Panel panel2;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckBox kiihtyvyysviivat;
        private CheckBox nopeusviivat;
        private Label label5;
        private Label label4;
        private Label label6;
        private CheckBox rataviivat;
        private Label label7;
        private NumericUpDown rataViivojenPituus;
        private CheckBox BHViivat;
        private Label Aikateksti;
    }
}