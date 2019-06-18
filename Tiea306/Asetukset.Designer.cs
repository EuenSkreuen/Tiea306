namespace Tiea306
{
    partial class Asetukset
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
            this.aloita = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tiedostoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uusiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avaaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lopetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.apuaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.näytäOhjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tietojaSovelluksestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.metodi = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // aloita
            // 
            this.aloita.Location = new System.Drawing.Point(188, 265);
            this.aloita.Name = "aloita";
            this.aloita.Size = new System.Drawing.Size(135, 33);
            this.aloita.TabIndex = 0;
            this.aloita.Text = "Käynnistä simulaatio";
            this.aloita.UseVisualStyleBackColor = true;
            this.aloita.Click += new System.EventHandler(this.aloita_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tiedostoToolStripMenuItem,
            this.apuaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(326, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tiedostoToolStripMenuItem
            // 
            this.tiedostoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uusiToolStripMenuItem,
            this.avaaToolStripMenuItem,
            this.toolStripMenuItem1,
            this.lopetaToolStripMenuItem});
            this.tiedostoToolStripMenuItem.Name = "tiedostoToolStripMenuItem";
            this.tiedostoToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.tiedostoToolStripMenuItem.Text = "Tiedosto";
            // 
            // uusiToolStripMenuItem
            // 
            this.uusiToolStripMenuItem.Name = "uusiToolStripMenuItem";
            this.uusiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.uusiToolStripMenuItem.Text = "Uusi";
            // 
            // avaaToolStripMenuItem
            // 
            this.avaaToolStripMenuItem.Name = "avaaToolStripMenuItem";
            this.avaaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.avaaToolStripMenuItem.Text = "Avaa";
            this.avaaToolStripMenuItem.Click += new System.EventHandler(this.avaaToolStripMenuItem_Click);
            // 
            // lopetaToolStripMenuItem
            // 
            this.lopetaToolStripMenuItem.Name = "lopetaToolStripMenuItem";
            this.lopetaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lopetaToolStripMenuItem.Text = "Lopeta";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // apuaToolStripMenuItem
            // 
            this.apuaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tietojaSovelluksestaToolStripMenuItem,
            this.näytäOhjeToolStripMenuItem});
            this.apuaToolStripMenuItem.Name = "apuaToolStripMenuItem";
            this.apuaToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.apuaToolStripMenuItem.Text = "Apua";
            // 
            // näytäOhjeToolStripMenuItem
            // 
            this.näytäOhjeToolStripMenuItem.Name = "näytäOhjeToolStripMenuItem";
            this.näytäOhjeToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.näytäOhjeToolStripMenuItem.Text = "Näytä Ohje";
            // 
            // tietojaSovelluksestaToolStripMenuItem
            // 
            this.tietojaSovelluksestaToolStripMenuItem.Name = "tietojaSovelluksestaToolStripMenuItem";
            this.tietojaSovelluksestaToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.tietojaSovelluksestaToolStripMenuItem.Text = "Tietoja sovelluksesta";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tähtien määrä";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(96, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(124, 20);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Metodi";
            // 
            // metodi
            // 
            this.metodi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.metodi.FormattingEnabled = true;
            this.metodi.Items.AddRange(new object[] {
            "Barnes-Hut algoritmi",
            "Suora laskenta"});
            this.metodi.Location = new System.Drawing.Point(99, 72);
            this.metodi.Name = "metodi";
            this.metodi.Size = new System.Drawing.Size(121, 21);
            this.metodi.TabIndex = 5;
            // 
            // Asetukset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 300);
            this.Controls.Add(this.metodi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aloita);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Asetukset";
            this.Text = "Simulaation asetukset";
            this.Load += new System.EventHandler(this.Asetukset_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aloita;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tiedostoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uusiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avaaToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lopetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apuaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tietojaSovelluksestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem näytäOhjeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox metodi;
    }
}

