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
            o.Filter = "simulation files (*.siminfo)|*.siminfo";
            o.RestoreDirectory = true;
            o.ShowDialog();
        }

        private void aloita_Click(object sender, EventArgs e)
        {
            double valovuosi_pc = 0.306594845;
            //Väliaikainen systeemi tähtien generointiin
            Kappale[] kappaleet = new Kappale[100];
            Random r = new Random();
            for(int i = 0; i < 100; i++)
            {
                Vertex3d sijainti = (new Vertex3d(r.NextDouble()*2-1, r.NextDouble()*2-1, 0))*valovuosi_pc;
                double massa = r.NextDouble();
                Vertex3d kiihtyvyys = (new Vertex3d(r.NextDouble(), r.NextDouble(), 0))*valovuosi_pc;
                Vertex3d nopeus = (new Vertex3d(r.NextDouble(), r.NextDouble(), 0))*valovuosi_pc;
                kappaleet[i] = new Kappale(sijainti, massa, kiihtyvyys, nopeus);
            }
            new Simulaattori(kappaleet).Show();
        }
    }
}
