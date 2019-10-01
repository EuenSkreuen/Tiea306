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
            this.lopetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apuaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tietojaSovelluksestaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.näytäOhjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.maara = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.metodi = new System.Windows.Forms.ComboBox();
            this.onko2D = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.koko = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.aikaAskel = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.tallenna = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nimi = new System.Windows.Forms.TextBox();
            this.kansioOlemassa = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.simulaatioLista = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // aloita
            // 
            this.aloita.Location = new System.Drawing.Point(113, 279);
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
            this.menuStrip1.Size = new System.Drawing.Size(432, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tiedostoToolStripMenuItem
            // 
            this.tiedostoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lopetaToolStripMenuItem});
            this.tiedostoToolStripMenuItem.Name = "tiedostoToolStripMenuItem";
            this.tiedostoToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.tiedostoToolStripMenuItem.Text = "Tiedosto";
            // 
            // lopetaToolStripMenuItem
            // 
            this.lopetaToolStripMenuItem.Name = "lopetaToolStripMenuItem";
            this.lopetaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lopetaToolStripMenuItem.Text = "Lopeta";
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
            // tietojaSovelluksestaToolStripMenuItem
            // 
            this.tietojaSovelluksestaToolStripMenuItem.Name = "tietojaSovelluksestaToolStripMenuItem";
            this.tietojaSovelluksestaToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.tietojaSovelluksestaToolStripMenuItem.Text = "Tietoja sovelluksesta";
            // 
            // näytäOhjeToolStripMenuItem
            // 
            this.näytäOhjeToolStripMenuItem.Name = "näytäOhjeToolStripMenuItem";
            this.näytäOhjeToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.näytäOhjeToolStripMenuItem.Text = "Näytä Ohje";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tähtien määrä";
            // 
            // maara
            // 
            this.maara.Location = new System.Drawing.Point(118, 7);
            this.maara.Name = "maara";
            this.maara.Size = new System.Drawing.Size(124, 20);
            this.maara.TabIndex = 3;
            this.maara.Text = "500";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 180);
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
            "Suora laskenta",
            "Barnes-Hut algoritmi"});
            this.metodi.Location = new System.Drawing.Point(118, 177);
            this.metodi.Name = "metodi";
            this.metodi.Size = new System.Drawing.Size(121, 21);
            this.metodi.TabIndex = 5;
            // 
            // onko2D
            // 
            this.onko2D.AutoSize = true;
            this.onko2D.Checked = true;
            this.onko2D.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onko2D.Location = new System.Drawing.Point(118, 32);
            this.onko2D.Name = "onko2D";
            this.onko2D.Size = new System.Drawing.Size(40, 17);
            this.onko2D.TabIndex = 6;
            this.onko2D.Text = "2D";
            this.onko2D.UseVisualStyleBackColor = true;
            this.onko2D.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(118, 55);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(40, 17);
            this.checkBox2.TabIndex = 7;
            this.checkBox2.Text = "3D";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Simulaation koko";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Aika-askel";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Enabled = false;
            this.checkBox3.Location = new System.Drawing.Point(118, 127);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(136, 17);
            this.checkBox3.TabIndex = 10;
            this.checkBox3.Text = "Dynaaminen aika-askel";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // koko
            // 
            this.koko.Enabled = false;
            this.koko.Location = new System.Drawing.Point(118, 75);
            this.koko.Name = "koko";
            this.koko.Size = new System.Drawing.Size(124, 20);
            this.koko.TabIndex = 11;
            this.koko.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(248, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "(valovuotta)";
            // 
            // aikaAskel
            // 
            this.aikaAskel.Location = new System.Drawing.Point(118, 101);
            this.aikaAskel.Name = "aikaAskel";
            this.aikaAskel.Size = new System.Drawing.Size(124, 20);
            this.aikaAskel.TabIndex = 13;
            this.aikaAskel.Text = "1000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(248, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "(vuotta)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 153);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Aloitusjakauma";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(118, 150);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 207);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Pehmennysparametri";
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(118, 204);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(121, 20);
            this.textBox4.TabIndex = 18;
            this.textBox4.Text = "0.01";
            // 
            // tallenna
            // 
            this.tallenna.AutoSize = true;
            this.tallenna.Location = new System.Drawing.Point(118, 230);
            this.tallenna.Name = "tallenna";
            this.tallenna.Size = new System.Drawing.Size(116, 17);
            this.tallenna.TabIndex = 20;
            this.tallenna.Text = "Tallenna simulaatio";
            this.tallenna.UseVisualStyleBackColor = true;
            this.tallenna.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 256);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Simulaation nimi";
            // 
            // nimi
            // 
            this.nimi.Enabled = false;
            this.nimi.Location = new System.Drawing.Point(113, 253);
            this.nimi.Name = "nimi";
            this.nimi.Size = new System.Drawing.Size(121, 20);
            this.nimi.TabIndex = 22;
            this.nimi.TextChanged += new System.EventHandler(this.nimiLaatikko_TextChanged);
            // 
            // kansioOlemassa
            // 
            this.kansioOlemassa.AutoSize = true;
            this.kansioOlemassa.ForeColor = System.Drawing.Color.DarkRed;
            this.kansioOlemassa.Location = new System.Drawing.Point(242, 256);
            this.kansioOlemassa.Name = "kansioOlemassa";
            this.kansioOlemassa.Size = new System.Drawing.Size(0, 13);
            this.kansioOlemassa.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(432, 453);
            this.tabControl1.TabIndex = 24;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.kansioOlemassa);
            this.tabPage1.Controls.Add(this.maara);
            this.tabPage1.Controls.Add(this.aloita);
            this.tabPage1.Controls.Add(this.nimi);
            this.tabPage1.Controls.Add(this.onko2D);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.checkBox2);
            this.tabPage1.Controls.Add(this.tallenna);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.textBox4);
            this.tabPage1.Controls.Add(this.koko);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.metodi);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.aikaAskel);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(424, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Uusi simulaatio";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.simulaatioLista);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(424, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Toista simulaatio";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // simulaatioLista
            // 
            this.simulaatioLista.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simulaatioLista.FormattingEnabled = true;
            this.simulaatioLista.Location = new System.Drawing.Point(9, 34);
            this.simulaatioLista.Name = "simulaatioLista";
            this.simulaatioLista.Size = new System.Drawing.Size(121, 21);
            this.simulaatioLista.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 18);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(142, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Valitse toistettava simulaatio:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(9, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 37);
            this.button1.TabIndex = 2;
            this.button1.Text = "Toista simulaatio";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Asetukset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 477);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Asetukset";
            this.Text = "Simulaation asetukset";
            this.Load += new System.EventHandler(this.Asetukset_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aloita;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tiedostoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lopetaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apuaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tietojaSovelluksestaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem näytäOhjeToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox maara;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox metodi;
        private System.Windows.Forms.CheckBox onko2D;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TextBox koko;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox aikaAskel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.CheckBox tallenna;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox nimi;
        private System.Windows.Forms.Label kansioOlemassa;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox simulaatioLista;
    }
}

