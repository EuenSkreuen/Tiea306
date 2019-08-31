using System;
using System.Windows.Forms;
using System.IO;
using OpenGL;

namespace Tiea306
{
    public partial class Asetukset : Form
    {
        public Asetukset()
        {
            InitializeComponent();
            metodi.SelectedIndex = 0;
        }

        private void Asetukset_Load(object sender, EventArgs e)
        {
            //Luo ohjelmaa käynnistettäessä tallennukseen tarvittavan kansion ellei sitä ole jo olemassa.
            if (!(Directory.Exists("/simulations")))
            {
                Directory.CreateDirectory("/simulations");
            }
        }

        private void avaaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: kesken
            OpenFileDialog o = new OpenFileDialog();
            o.InitialDirectory = "simulations/";
            o.RestoreDirectory = true;
            o.ShowDialog();
        }

        private void aloita_Click(object sender, EventArgs e)
        {
            double valovuosi = 63239.7263;
            //TODO: Väliaikainen systeemi tähtien generointiin
            int n = 500;
            try
            {
                n = Int32.Parse(textBox1.Text);
            }
            catch (FormatException)
            {
                //
            }
            Kappale[] kappaleet = new Kappale[n];
            Random r = new Random();
            for(int i = 0; i < n; i++)
            {
                Vertex3d sijainti;
                if (checkBox2.Checked)
                {
                    sijainti = (new Vertex3d(r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5)) * valovuosi;
                } else
                {
                    sijainti = (new Vertex3d(r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5, 0)) * valovuosi;
                }
                
                double massa = r.NextDouble()*10;
                Vertex3d kiihtyvyys = (new Vertex3d(0, 0, 0));
                Vertex3d nopeus = (new Vertex3d(0 ,0, 0));
                kappaleet[i] = new Kappale(sijainti, massa, kiihtyvyys, nopeus);
            }
            new Simulaattori(kappaleet, checkBox1.Checked, metodi.SelectedIndex, Convert.ToDouble(aikaAskel.Text)).Show();
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
        }
    }
}
