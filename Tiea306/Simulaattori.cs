using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using OpenGL;

namespace Tiea306
{
    public partial class Simulaattori : Form
    {
        private double valovuosi = 63239.7263; //Valovuosi astronomisissa yksiköissä
        private double kerroin = 1; //Kerroin zoomausta varten
        private double kulunutAika = 0; //Kuinka pitkä ajanjakso on simuloitu
        private int radanPituus = 20; //Piirrettävien ratojen pituus. Vastaa tallennettavien pisteiden määrää, joista sitten rata piirretään.
        private Point aloitusPiste; //Piste josta hiirellä raahaus aloitettiin.
        private Vertex3d edellinenSijainti = new Vertex3d(0, 0, 0); //Aiempi sijainti johon hiirellä raahaus lopetettiin.
        private Vertex3d kameranSijainti = new Vertex3d(0, 0, 0); //Kameran sijainti (skaalaamaton). Ei koske oikeaa kameraa, vaan kappaleiden sijaintia siirretään tämän perusteella.
        private Queue<Vertex3d>[] radat; //Taulukko jonoista joihin tallennetaan ratojen pisteet.
        private Kappale[] kappaleet; //Taulukko kappaleista joita simuloidaan.
        public Simulaattori(Kappale[] kappaleet)
        {
            //Alustetaan kappalelista ja ratalista
            this.kappaleet = kappaleet;
            this.radat = new Queue<Vertex3d>[kappaleet.Length];
            for(int i = 0; i < radat.Length;i++)
            {
                radat[i] = new Queue<Vertex3d>();
            }
            InitializeComponent(); 
            
            //Tallennetaan piste josta kuvakulman raahaus hiirellä alkoi.
            this.glControl1.MouseDown += (sender, e) => { aloitusPiste = e.Location; };
            //Tallennetaan piste johon kuvakulman raahaus loppui.
            this.glControl1.MouseUp += (sender, e) => { edellinenSijainti = kameranSijainti; };
            //Muutetaan ratojen piirtämiseen tallennettavien pisteiden määrää.
            this.rataViivojenPituus.ValueChanged += (sender, e) => { radanPituus = (int)rataViivojenPituus.Value; };
            //Tapahtumankäsittelijä zoomausta varten hiiren rullalle.
            this.glControl1.MouseWheel += (sender, e) =>
            {
                if (e.Delta > 0) { if (kerroin > 1) { kerroin -= 1; } }
                else { kerroin += 1; }
            };
            //Siirretään kuvakulmaa hiiren liikkeen perusteella, jos vasen näppäin on pohjassa.
            this.glControl1.MouseMove += (sender, e) => 
            {
                if (e.Button == MouseButtons.Left)
                {
                    kameranSijainti = new Vertex3d(edellinenSijainti.x + e.Location.X - aloitusPiste.X, edellinenSijainti.y + aloitusPiste.Y - e.Location.Y, 0);
                }
            };   
            //Tyhjennetään tallennetut ratojen pisteet. Tällä tavoin entinen rata ei tule uudelleen näkyville kun piirtäminen otetaan uudelleen käyttöön.
            this.rataviivat.CheckStateChanged += (sender, e) => 
            {
                if (rataviivat.Checked == false)
                {
                    foreach (Queue<Vertex3d> l in radat) { l.Clear(); }
                }
            };            
        }

        private void glControl1_ContextCreated(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Enable(EnableCap.DepthTest);
            Gl.DepthFunc(DepthFunction.Less);
            Gl.MatrixMode(MatrixMode.Projection);
        }

        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);            
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);            

            //Kappaleiden piirto
            Gl.Begin(PrimitiveType.Points);            
            Gl.Color3(1.0f, 1.0f, 1.0f);
            Vertex3d skaalattuKameranSijaintiX = norm(kameranSijainti, -senderControl.ClientSize.Width / 2, senderControl.ClientSize.Width / 2, -1, 1);
            Vertex3d skaalattuKameranSijaintiY = norm(kameranSijainti, -senderControl.ClientSize.Height / 2, senderControl.ClientSize.Height / 2, -1, 1);
            Vertex3d skaalattuKameranSijainti = new Vertex3d(skaalattuKameranSijaintiX.x, skaalattuKameranSijaintiY.y, 0);
            foreach (Kappale k in kappaleet)
            {
                Vertex3d s = k.Sijainti;
                Gl.Vertex3(skaalattuKameranSijainti + (norm(s, -valovuosi * kerroin, valovuosi * kerroin, -1, 1)));
            }
            Gl.End();

            //Nopeusviivat
            if(nopeusviivat.Checked == true)
            {
                Gl.Begin(PrimitiveType.Lines);
                Gl.Color3(0.0f, 0.0f, 1.0f);
                foreach (Kappale k in kappaleet)
                {

                    if (Pituus(k.Nopeus) > 0)
                    {
                        Vertex3d v = k.Nopeus / Pituus(k.Nopeus);
                        Gl.Vertex3(skaalattuKameranSijainti + norm(k.Sijainti, -valovuosi * kerroin, valovuosi * kerroin, -1, 1) + v * 0.01);
                        Gl.Vertex3(skaalattuKameranSijainti + norm(k.Sijainti, -valovuosi * kerroin, valovuosi * kerroin, -1, 1) + v * 0.1 / kerroin + v * 0.02);
                    }
                }
                Gl.End();
            }

            //Kiihtyvyysviivat
            if(kiihtyvyysviivat.Checked == true)
            {
                Gl.Begin(PrimitiveType.Lines);
                Gl.Color3(1.0f, 0.0f, 0.0f);
                foreach (Kappale k in kappaleet)
                {
                    if (Pituus(k.Kiihtyvyys) > 0)
                    {
                        Vertex3d s = k.Kiihtyvyys / Pituus(k.Kiihtyvyys);
                        Gl.Vertex3(skaalattuKameranSijainti + norm(k.Sijainti,-valovuosi * kerroin, valovuosi * kerroin,-1,1) + s * 0.01);
                        Gl.Vertex3(skaalattuKameranSijainti + norm(k.Sijainti, -valovuosi * kerroin, valovuosi * kerroin, -1, 1) + s * 0.1 / kerroin + s * 0.02);
                    }
                }
                Gl.End();
            }

            //Rataviivat
            if(rataviivat.Checked == true)
            {
                Gl.Begin(PrimitiveType.Lines);
                Gl.Color3(0.0f, 1.0f, 0.0);
                for(int i=0;i<radat.Length;i++)
                {                    
                    //Piirretään rata. Piirto ennen päivitystä niin rata ei peitä tähteä.
                    for (int j = 1; j < radat[j].Count-1; j++)
                    {
                        Gl.Vertex3(skaalattuKameranSijainti + norm(radat[i].ElementAt(j-1), -valovuosi * kerroin, valovuosi * kerroin, -1, 1));
                        Gl.Vertex3(skaalattuKameranSijainti + norm(radat[i].ElementAt(j), -valovuosi * kerroin, valovuosi * kerroin, -1, 1));
                    }
                    //Tallennetaan listaan viimeisin sijainti, ja tyhjennetään toisesta päästä jos on tarvis.
                    if (radat[i].Count == radanPituus)
                    {
                        radat[i].Dequeue();
                        radat[i].Enqueue(kappaleet[i].Sijainti);
                    } else
                    {
                        radat[i].Enqueue(kappaleet[i].Sijainti);
                    }
                }
                Gl.End();
            }            

            //Varsinainen laskenta.
            Suora_Laskenta sl = new Suora_Laskenta();
            sl.päivitä(kappaleet);

            //Päivitetään käyttöliittymän tekstit.
            if(kerroin == 1)
            {
                label3.Text = "1 valovuosi";
            } else
            {
                label3.Text = kerroin.ToString() + " valovuotta";
            }            
            label2.Text = kulunutAika.ToString("#,0") + " vuotta";
            kulunutAika += sl.aika;
        }

        /// <summary>
        /// Skaalaa annetun pisteen niin että se sijoittuu annetulle alueelle.
        /// </summary>
        /// <param name="a">Piste jota skaalataan</param>
        /// <returns></returns>
        private Vertex3d norm(Vertex3d muunnettava, double vanhaMinimi, double vanhaMaksimi, double uusiMinimi, double uusiMaksimi)
        {            
            double x = (((muunnettava.x - vanhaMinimi) * (uusiMaksimi - uusiMinimi)) / (vanhaMaksimi - vanhaMinimi)) + uusiMinimi;
            double y = (((muunnettava.y - vanhaMinimi) * (uusiMaksimi - uusiMinimi)) / (vanhaMaksimi - vanhaMinimi)) + uusiMinimi;
            double z = (((muunnettava.z - vanhaMinimi) * (uusiMaksimi - uusiMinimi)) / (vanhaMaksimi - vanhaMinimi)) + uusiMinimi;
            return new Vertex3d(x, y, z);
        }

        /// <summary>
        /// Antaa vektorin pituuden
        /// </summary>
        /// <param name="a">Vektori jonka pituus halutaan selvittää</param>
        /// <returns></returns>
        private double Pituus(Vertex3d a)
        {
            double x = Math.Pow(a.x, 2);
            double y = Math.Pow(a.y, 2);
            double z = Math.Pow(a.z, 2);
            return Math.Sqrt(x + y + z);
        }
    }
}
