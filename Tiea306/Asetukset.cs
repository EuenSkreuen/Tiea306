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
            o.RestoreDirectory = true;
            o.ShowDialog();
        }

        private void aloita_Click(object sender, EventArgs e)
        {
            double valovuosi = 63239.7263;
            //TODO: Väliaikainen systeemi tähtien generointiin
            Kappale[] kappaleet = new Kappale[500];
            Random r = new Random();
            for(int i = 0; i < 500; i++)
            {
                Vertex3d sijainti = (new Vertex3d(r.NextDouble()*2-1, r.NextDouble()*2-1, 0))*valovuosi;
                double massa = r.NextDouble()*10;
                Vertex3d kiihtyvyys = (new Vertex3d(0, 0, 0));
                Vertex3d nopeus = (new Vertex3d(0 ,0, 0));
                kappaleet[i] = new Kappale(sijainti, massa, kiihtyvyys, nopeus);
            }
            new Simulaattori(kappaleet).Show();
        }
    }
}
