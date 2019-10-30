using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenGL;
/*
 * Tämä tiedosto sisältää itse simulaattorin ja sen tarvitsemat osat.
 * Sisältää myös simulaattorin käyttöliittymään tarvittavaa koodia.
 * 
 * Tekijä: Aapo Peiponen
 */
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
        private Vertex3d kameranSijainti = new Vertex3d(0, 0, 0); //Kameran sijainti (skaalaamaton).
        private Queue<Vertex3d>[] radat; //Taulukko jonoista joihin tallennetaan ratojen pisteet.
        private Kappale[] kappaleet; //Taulukko kappaleista joita simuloidaan.
        private Lehti2D juurisolmu2D; //Juurisolmu kaksiulotteista puuta varten
        private Lehti3D juurisolmu3D; //Juurisolmu kolmiulotteista puuta varten
        private int algoritmi = 0; //Mitä algoritmia käytetään. 0 = suora laskenta, 1 = Barnes-Hut algoritmi
        private double aikaAskel = 1; //Simulaation aika-askel vuosissa
        private double gravitaatiovakio = 4 * Math.Pow(Math.PI, 2); //Gravitaatiovakio
        private bool kaksiulotteinen = false; //Suoritetaanko simulaatio kaksiulotteisena
        private double rotationX = 0; //Kierto x-akselin suhteen asteissa
        private double rotationY = 0; //Kierto y-akselin suhteen asteissa
        private double rotationZ = 0; //Kierto z-akselin suhteen asteissa
        Matrix4x4d mvp; //Matriisi simulaation kameran toimintaa varten
        private String tiedostoSijainti = ""; //Tiedostosijainti jossa simulaatio on. Tyhjä jos ei tallenneta tai toisteta.
        private bool tallenna = false; //Tallennetaanko simulaatio
        private bool toista = false; //Toistetaanko simulaatiota tiedostosta

        /// <summary>
        /// Simulaattori. Laskee kappaleiden liikkeet, tai toistaa ne annetusta tiedostosijainnista.
        /// </summary>
        /// <param name="kappaleet">Lista simuloitavista kappaleista. Jätä tyhjäksi jos toistetaan.</param>
        /// <param name="kaksiulotteinen">Suoritetaanko simulaatio kaksiulotteisena.</param>
        /// <param name="algoritmi">Käytettävä algoritmi simulaatiossa. Suora voimien laskenta = 0, ja Barnes-Hut algoritmi = 1.</param>
        /// <param name="aikaAskel">Simulaatiossa käytettävä aika-askel.</param>
        /// <param name="tallennetaanko">Tallennetaanko simulaatio.</param>
        /// <param name="toistetaanko">Käynnistetäänkö simulaattori toistotilassa. Tällöin jotkin ominaisuudet ovat pois käytöstä.</param>
        /// <param name="polku">Tiedostopolku johon simulaatio tallennetaan, tai josta sitä luetaan.</param>
        public Simulaattori(Kappale[] kappaleet, bool kaksiulotteinen, int algoritmi, double aikaAskel, bool tallennetaanko, bool toistetaanko, String polku)
        {
            //Alustetaan simulaatio annettujen tietojen pohjalta
            if (toistetaanko)
            {
                try
                {
                    //Alustetaan simulaatio toistoa varten
                    this.kappaleet = kappaleet;
                    this.kaksiulotteinen = kaksiulotteinen;
                    this.algoritmi = algoritmi;
                    this.aikaAskel = aikaAskel;
                    toista = toistetaanko;
                    tallenna = tallennetaanko;
                    tiedostoSijainti = polku;                    
                    InitializeComponent();
                    this.Text = polku;
                    BHViivat.Enabled = false;
                    nopeusviivat.Enabled = false;
                    kiihtyvyysviivat.Enabled = false;
                    rataviivat.Enabled = false;
                    rataViivojenPituus.Enabled = false;
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Virhe", MessageBoxButtons.OK);
                    this.Close();
                }                
            }
            else {
                //Alustetaan simulaatio normaalisti
                this.kappaleet = kappaleet;
                this.kaksiulotteinen = kaksiulotteinen;
                this.algoritmi = algoritmi;
                this.aikaAskel = aikaAskel;
                toista = toistetaanko;
                tallenna = tallennetaanko;
                tiedostoSijainti = polku;
                this.radat = new Queue<Vertex3d>[kappaleet.Length];
                for (int i = 0; i < radat.Length; i++)
                {
                    radat[i] = new Queue<Vertex3d>();
                }
                InitializeComponent();
                if (algoritmi == 0)
                {
                    BHViivat.Enabled = false;
                }
            }

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
                if(e.Button == MouseButtons.Left)
                {
                    kameranSijainti = new Vertex3d(edellinenSijainti.x + e.Location.X - aloitusPiste.X, edellinenSijainti.y + aloitusPiste.Y - e.Location.Y, 0.0);
                    rotationX = edellinenSijainti.y + aloitusPiste.Y - e.Location.Y;
                    rotationY = edellinenSijainti.x + e.Location.X - aloitusPiste.X;
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

        //Tapahtumankäsittelijä simulaation ikkunan luontihetkelle
        private void glControl1_ContextCreated(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Enable(EnableCap.DepthTest);
            Gl.DepthFunc(DepthFunction.Less);
            Gl.MatrixMode(MatrixMode.Projection);
            if (toista)
            {
                kulunutAika = aikaAskel;
                glControl1.AnimationTime = 34;
            }
        }

        //Tapahtumankäsittelijä simulaation ikkunan päivitykselle. Käytännössä sisältää pääsilmukan.
        private void glControl1_Render(object sender, GlControlEventArgs e)
        {
            Control senderControl = (Control)sender;
            Gl.Viewport(0, 0, senderControl.ClientSize.Width, senderControl.ClientSize.Height);            
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);

            //Asetetaan matriisi vektoreiden siirtämiseksi paikoilleen
            Matrix4x4d perspective = Matrix4x4d.Perspective(45.0, 1.0, 0.1, 1000.0);
            Matrix4x4d translate = Matrix4x4d.Translated(0, 0, 0 - 1);
            Matrix4x4d rotateX = Matrix4x4d.RotatedX(rotationX);
            Matrix4x4d rotateY = Matrix4x4d.RotatedY(rotationY);
            Matrix4x4d rotateZ = Matrix4x4d.RotatedZ(rotationZ);
            Matrix4x4d scale = Matrix4x4d.Scaled(1.0 / kerroin, 1.0 / kerroin, 1.0 / kerroin);
            Matrix4x4d m = Matrix4x4d.Identity;
            Matrix4x4d v = translate * rotateX * rotateY * rotateZ * scale;
            mvp = perspective * v * m;

            //Kappalelistan päivitys, jos ollaan toistotilassa
            if (toista)
            {
                try
                {
                    haeKappaleet(tiedostoSijainti + "/" + kulunutAika.ToString() + ".txt", kappaleet);
                    kulunutAika += aikaAskel;
                }
                catch (Exception)
                {
                    //Oletetaan että saavuttiin tallennettujen askelten loppuun, joten aloitetaan alusta.
                    try
                    {
                        kulunutAika = aikaAskel;
                        haeKappaleet(tiedostoSijainti + "/" + kulunutAika.ToString() + ".txt", kappaleet);
                    }
                    catch (Exception a)
                    {
                        //Virhe johtuu jostain muusta. Keskeytetään suoritus.
                        MessageBox.Show(a.Message, "Virhe", MessageBoxButtons.OK);
                        this.Close();
                    }
                }

            }
            //Kappaleiden piirto
            foreach (Kappale k in kappaleet)
            {
                Vertex3d s = k.Sijainti;
                drawStar(project((norm(s, -valovuosi, valovuosi, -1, 1))));
            }

            //Nopeusviivat
            if(nopeusviivat.Checked == true)
            {
                Gl.Begin(PrimitiveType.Lines);
                Gl.Color3(0.0f, 0.0f, 1.0f);
                foreach (Kappale k in kappaleet)
                {

                    if (Pituus(k.Nopeus) > 0)
                    {
                        Vertex3d vv = k.Nopeus / Pituus(k.Nopeus);
                        Gl.Vertex3(project(norm(k.Sijainti, -valovuosi, valovuosi, -1, 1) + vv * 0.01));
                        Gl.Vertex3(project(norm(k.Sijainti, -valovuosi, valovuosi, -1, 1) + vv * 0.1 + vv * 0.02));
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
                        Gl.Vertex3(project(norm(k.Sijainti, -valovuosi, valovuosi, -1, 1) + s * 0.01));
                        Gl.Vertex3(project(norm(k.Sijainti, -valovuosi, valovuosi, -1, 1) + s * 0.1 + s * 0.02));
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
                        Gl.Vertex3(project(norm(radat[i].ElementAt(j - 1), -valovuosi, valovuosi, -1, 1)));
                        Gl.Vertex3(project(norm(radat[i].ElementAt(j), -valovuosi, valovuosi, -1, 1)));
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

            //Voimien laskenta            
            if (algoritmi == 0 && !toista)
            {
                //Suora laskenta
                Suora_Laskenta sl = new Suora_Laskenta();
                sl.asetaAikaAskel(aikaAskel);
                sl.päivitä(kappaleet);
                kulunutAika += sl.aika;
            }
            if (algoritmi == 1 && !toista)
            {
                //Barnes-Hut algoritmi
                //Rakennetaan puu (kaksi tai kolmiulotteinen versio
                if (kaksiulotteinen)
                {
                    //Määritetään juurisolmun koko, sijainti on origossa.
                    juurisolmu2D = new Lehti2D();
                    juurisolmu2D.Sijainti = new Vertex3d(0, 0, 0);
                    juurisolmu2D.koko = juurenKoko(kappaleet);
                    //Sijoitetaan jokainen kappale puuhun
                    foreach (Kappale k in kappaleet)
                    {
                        Sijoita2D(k, juurisolmu2D);
                    }
                    //Varsinainen laskenta
                    //Silmukka joka valitsee kappaleen kerrallaan ja laskee puun avulla siihen kohdistuvat voimat
                    foreach (Kappale k in kappaleet)
                    {
                        Vertex3d kiihtyvyys = BarnesHut2D(juurisolmu2D, k);
                        //Lopulliseen kiihtyvyyteen otettava huomioon gravitaatiovakio.
                        k.Kiihtyvyys = kiihtyvyys * -gravitaatiovakio;
                        //Lasketaan kappaleen sijainti nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                        k.Sijainti += k.Nopeus * aikaAskel + k.Kiihtyvyys * 0.5 * Math.Pow(aikaAskel, 2);
                        //Lasketaan kappaleelle uusi nopeus vanhan nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                        k.Nopeus = k.Nopeus + k.Kiihtyvyys * aikaAskel;
                    }
                    kulunutAika += aikaAskel;
                    if (BHViivat.Checked) { piirrä2DPuu(juurisolmu2D, kerroin); }
                }
                else
                {
                    //Määritetään juurisolmun koko, sijainti on origossa.
                    juurisolmu3D = new Lehti3D();
                    juurisolmu3D.Sijainti = new Vertex3d(0, 0, 0);
                    juurisolmu3D.koko = juurenKoko(kappaleet);
                    //Sijoitetaan jokainen kappale puuhun
                    foreach (Kappale k in kappaleet)
                    {
                        Sijoita3D(k, juurisolmu3D);
                    }
                    //Varsinainen laskenta
                    //Silmukka joka valitsee kappaleen kerrallaan ja laskee puun avulla siihen kohdistuvat voimat
                    foreach (Kappale k in kappaleet)
                    {
                        Vertex3d kiihtyvyys = BarnesHut3D(juurisolmu3D, k);
                        //Lopulliseen kiihtyvyyteen otettava huomioon gravitaatiovakio.
                        k.Kiihtyvyys = kiihtyvyys * -gravitaatiovakio;
                        //Lasketaan kappaleen sijainti nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                        k.Sijainti += k.Nopeus * aikaAskel + k.Kiihtyvyys * 0.5 * Math.Pow(aikaAskel, 2);
                        //Lasketaan kappaleelle uusi nopeus vanhan nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                        k.Nopeus = k.Nopeus + k.Kiihtyvyys * aikaAskel;
                    }
                    kulunutAika += aikaAskel;
                    if (BHViivat.Checked) { piirrä3DPuu(juurisolmu3D, kerroin); }
                }

            }

            //Päivitetään käyttöliittymän tekstit.
            if(kerroin == 1)
            {
                label3.Text = "1 valovuosi";
            } else
            {
                label3.Text = kerroin.ToString() + " valovuotta";
            }            
            label2.Text = kulunutAika.ToString("#,0") + " vuotta";

            //Tallennetaan simulaation askelet, mikäli käyttäjä on niin valinnut
            //TODO: Laske kulutettu tila ja näytä se käyttäjälle.
            if (tallenna)
            {
                try
                {
                    tallennaKappaleet(kappaleet, tiedostoSijainti + "/" + kulunutAika.ToString() + ".txt");
                } catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Virhe", MessageBoxButtons.OK);
                    this.Close();
                }                
            }
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

        /// <summary>
        /// Laskee annettujen kappaleiden kokonaisenergian massakeskipisteen avulla.
        /// </summary>
        /// <param name="kappaleet"></param>
        /// <returns></returns>
        private double Energia(Kappale[] kappaleet, Vertex3d massakeskipiste)
        {
            double gravitaatiovakio = 4 * Math.Pow(Math.PI, 2);
            double kineettinenEnergia = 0;
            double potentiaaliEnergia = 0;
            foreach (Kappale k in kappaleet)
            {
                //Kineettinen energia
                kineettinenEnergia += 0.5 * k.Massa * Math.Pow(Pituus(k.Sijainti), 2);
                //Uusi versio, kirjasta tähtitieteen perusteet
                double potentiaalienergia = 0;
                for(int i = 0; i < kappaleet.Length; i++)
                {
                    for(int s = i + 1; s < kappaleet.Length; s++)
                    {
                        potentiaalienergia += (kappaleet[i].Massa * kappaleet[s].Massa) / Pituus(kappaleet[i].Sijainti - kappaleet[s].Sijainti);
                    }
                }
                potentiaalienergia = -gravitaatiovakio * potentiaalienergia;
            }
            return kineettinenEnergia + potentiaaliEnergia;            
        }

        /// <summary>
        /// Palauttaa juurisolmun koon jonka sisälle sijoittuu kaikki kappaleet. 
        /// </summary>
        /// <param name="kappalelista"></param>
        /// <returns></returns>
        public double juurenKoko(Kappale[] kappalelista)
        {
            double maksimi = 0;
            foreach(Kappale k in kappalelista)
            {
                if(Math.Abs(k.Sijainti.x) > maksimi)
                {
                    maksimi = Math.Abs(k.Sijainti.x);
                }
                if(Math.Abs(k.Sijainti.y) > maksimi)
                {
                    maksimi = Math.Abs(k.Sijainti.y);
                }
                if(Math.Abs(k.Sijainti.z) > maksimi)
                {
                    maksimi = Math.Abs(k.Sijainti.z);
                }
            }
            return maksimi*2+2;
        }

        /// <summary>
        /// Sijoittaa annetun kappaleen Barnes-Hut algoritmin kaksiulotteisessa avaruudessa vaatimaan nelipuuhun.
        /// Toimii rekursiivisesti, luoden tarvittaessa uusia lehtiä puuhun kappaletta varten.
        /// </summary>
        /// <param name="kappale">Kappale jota ollaan sijoittamassa.</param>
        /// <param name="juuri">Solmu johon sijoitus halutaan tehdä.</param>
        public void Sijoita2D(Kappale kappale, Lehti2D juuri)
        {
            //Tapaus jossa tämä solmu on tyhjä
            if (juuri.OnkoTyhjä)
            {
                juuri.OnkoTyhjä = false;
                juuri.kappale = kappale;
                juuri.Massakeskipiste = kappale.Sijainti;
                juuri.Massa = kappale.Massa;
                return;
            }
            //Tapaus jossa tämä solmu ei ole tyhjä, eikä omaa lapsisolmuja.
            if ((juuri.OnkoTyhjä == false) && (juuri.OnkoJuuriSolmu == false))
            {
                //Muuttaa solmun tyypin juurisolmuksi, luo uudet lapsisolmut, ja siirtää olemassaolevan kappaleen yhteen uusista solmuista.
                juuri.OnkoJuuriSolmu = true;
                juuri.oikeaYlä = new Lehti2D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, 0)
                };
                juuri.oikeaAla = new Lehti2D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, 0)
                };
                juuri.vasenYlä = new Lehti2D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, 0)
                };
                juuri.vasenAla = new Lehti2D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, 0)
                };
                SijoitaLaatikkoon2D(juuri.kappale, juuri);
                SijoitaLaatikkoon2D(kappale, juuri);
                //Päivitetään nykyisen solmun massa ja massakeskipiste
                double juurenMassa = 0;
                Vertex3d juurenMassaKeskiPiste = new Vertex3d(0, 0, 0);
                if (!juuri.oikeaYlä.OnkoTyhjä)
                {
                    juurenMassa += juuri.oikeaYlä.Massa;
                    juurenMassaKeskiPiste += juuri.oikeaYlä.Massakeskipiste * juuri.oikeaYlä.Massa;
                }
                if (!juuri.oikeaAla.OnkoTyhjä)
                {
                    juurenMassa += juuri.oikeaAla.Massa;
                    juurenMassaKeskiPiste += juuri.oikeaAla.Massakeskipiste * juuri.oikeaAla.Massa;
                }
                if (!juuri.vasenYlä.OnkoTyhjä)
                {
                    juurenMassa += juuri.vasenYlä.Massa;
                    juurenMassaKeskiPiste += juuri.vasenYlä.Massakeskipiste * juuri.vasenYlä.Massa;
                }
                if (!juuri.vasenAla.OnkoTyhjä)
                {
                    juurenMassa += juuri.vasenAla.Massa;
                    juurenMassaKeskiPiste += juuri.vasenAla.Massakeskipiste * juuri.vasenAla.Massa;
                }
                juuri.Massa = juurenMassa;
                juuri.Massakeskipiste = juurenMassaKeskiPiste / juurenMassa;
                return;
            }
            //Tapaus jossa tämä solmu on juurisolmu jolla on lapsisolmuja.
            if (juuri.OnkoJuuriSolmu)
            {
                SijoitaLaatikkoon2D(kappale, juuri);
                //Päivitetään nykyisen solmun massa ja massakeskipiste
                double juurenMassa = 0;
                Vertex3d juurenMassaKeskiPiste = new Vertex3d(0, 0, 0);
                if (!juuri.oikeaYlä.OnkoTyhjä)
                {
                    juurenMassa += juuri.oikeaYlä.Massa;
                    juurenMassaKeskiPiste += juuri.oikeaYlä.Massakeskipiste * juuri.oikeaYlä.Massa;
                }
                if (!juuri.oikeaAla.OnkoTyhjä)
                {
                    juurenMassa += juuri.oikeaAla.Massa;
                    juurenMassaKeskiPiste += juuri.oikeaAla.Massakeskipiste * juuri.oikeaAla.Massa;
                }
                if (!juuri.vasenYlä.OnkoTyhjä)
                {
                    juurenMassa += juuri.vasenYlä.Massa;
                    juurenMassaKeskiPiste += juuri.vasenYlä.Massakeskipiste * juuri.vasenYlä.Massa;
                }
                if (!juuri.vasenAla.OnkoTyhjä)
                {
                    juurenMassa += juuri.vasenAla.Massa;
                    juurenMassaKeskiPiste += juuri.vasenAla.Massakeskipiste * juuri.vasenAla.Massa;
                }
                juuri.Massa = juurenMassa;
                juuri.Massakeskipiste = juurenMassaKeskiPiste / juurenMassa;
                return;
            }
        }

        /// <summary>
        /// Sijoittaa annetun kappaleen nelipuun lehtiin.
        /// </summary>
        /// <param name="kappale">Sijoitettava kappale.</param>
        /// <param name="juuri">Solmu jonka lehtiin sijoitus halutaan tehdä.</param>
        private void SijoitaLaatikkoon2D(Kappale kappale, Lehti2D juuri)
        {
            if (kappale.Sijainti.x >= juuri.Sijainti.x)
            {
                //Kappaleen uusi sijainti on oikealla puolella
                if (kappale.Sijainti.y >= juuri.Sijainti.y)
                {
                    //Kappale on oikeassa ylänurkassa
                    Sijoita2D(kappale, juuri.oikeaYlä);
                }
                else
                {
                    //Kappale on oikeassa alanurkassa
                    Sijoita2D(kappale, juuri.oikeaAla);
                }
            }
            else
            {
                //Kappaleen uusi sijainti on vasemmalla puolella
                if (kappale.Sijainti.y >= juuri.Sijainti.y)
                {
                    //Kappale on vasemmassa ylänurkassa
                    Sijoita2D(kappale, juuri.vasenYlä);
                }
                else
                {
                    //Kappale on vasemmassa alanurkassa
                    Sijoita2D(kappale, juuri.vasenAla);
                }
            }
        }

        /// <summary>
        /// Sijoittaa kappaleen kolmiulotteisessa avaruudessa Barnes-Hut algoritmin vaatimaan kahdeksanpuuhun.
        /// Toimii rekursiivisesti, luoden tarvittaessa uusia lehtiä kappaletta varten.
        /// </summary>
        /// <param name="kappale">Kappale jota ollaan sijoittamassa.</param>
        /// <param name="juuri">Solmu johon halutaan sijoittaa kappale.</param>
        public void Sijoita3D(Kappale kappale, Lehti3D juuri)
        {
            //Tapaus jossa tämä solmu on tyhjä
            if (juuri.OnkoTyhjä)
            {
                juuri.OnkoTyhjä = false;
                juuri.kappale = kappale;
                juuri.Massakeskipiste = kappale.Sijainti;
                juuri.Massa = kappale.Massa;
                return;
            }
            //Tapaus jossa tämä solmu ei ole tyhjä, eikä omaa lapsisolmuja.
            if ((juuri.OnkoTyhjä == false) && (juuri.OnkoJuuriSolmu == false))
            {
                //Muuttaa solmun tyypin juurisolmuksi, luo uudet lapsisolmut, ja siirtää olemassaolevan kappaleen yhteen uusista solmuista.
                /*
                 * A1 = Vasen taka-ylänurkka
                 * A2 = Oikea taka-ylänurkka
                 * A3 = Vasen etu-ylänurkka
                 * A4 = Oikea etu-ylänurkka
                 * B1 = Vasen taka-alanurkka
                 * B2 = Oikea taka-alanurkka
                 * B3 = Vasen etu-alanurkka
                 * B4 = Oikea etu-alanurkka
                 */
                juuri.OnkoJuuriSolmu = true;
                juuri.A3 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, juuri.Sijainti.z + juuri.koko / 4)
                };
                juuri.A4 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, juuri.Sijainti.z + juuri.koko / 4)
                };
                juuri.B3 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, juuri.Sijainti.z + juuri.koko / 4)
                };
                juuri.B4 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, juuri.Sijainti.z + juuri.koko / 4)
                };
                juuri.A1 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, juuri.Sijainti.z - juuri.koko / 4)
                };
                juuri.A2 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y + juuri.koko / 4, juuri.Sijainti.z - juuri.koko / 4)
                };
                juuri.B1 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x + juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, juuri.Sijainti.z - juuri.koko / 4)
                };
                juuri.B2 = new Lehti3D
                {
                    koko = juuri.koko / 2,
                    Sijainti = new Vertex3d(juuri.Sijainti.x - juuri.koko / 4, juuri.Sijainti.y - juuri.koko / 4, juuri.Sijainti.z - juuri.koko / 4)
                };
                SijoitaLaatikkoon3D(juuri.kappale, juuri);
                SijoitaLaatikkoon3D(kappale, juuri);
                //Päivitetään nykyisen solmun massa ja massakeskipiste
                double juurenMassa = 0;
                Vertex3d juurenMassaKeskiPiste = new Vertex3d(0, 0, 0);
                if (!juuri.A1.OnkoTyhjä)
                {
                    juurenMassa += juuri.A1.Massa;
                    juurenMassaKeskiPiste += juuri.A1.Massakeskipiste * juuri.A1.Massa;
                }
                if (!juuri.A2.OnkoTyhjä)
                {
                    juurenMassa += juuri.A2.Massa;
                    juurenMassaKeskiPiste += juuri.A2.Massakeskipiste * juuri.A2.Massa;
                }
                if (!juuri.A3.OnkoTyhjä)
                {
                    juurenMassa += juuri.A3.Massa;
                    juurenMassaKeskiPiste += juuri.A3.Massakeskipiste * juuri.A3.Massa;
                }
                if (!juuri.A4.OnkoTyhjä)
                {
                    juurenMassa += juuri.A4.Massa;
                    juurenMassaKeskiPiste += juuri.A4.Massakeskipiste * juuri.A4.Massa;
                }
                if (!juuri.B1.OnkoTyhjä)
                {
                    juurenMassa += juuri.B1.Massa;
                    juurenMassaKeskiPiste += juuri.B1.Massakeskipiste * juuri.B1.Massa;
                }
                if (!juuri.B2.OnkoTyhjä)
                {
                    juurenMassa += juuri.B2.Massa;
                    juurenMassaKeskiPiste += juuri.B2.Massakeskipiste * juuri.B2.Massa;
                }
                if (!juuri.B3.OnkoTyhjä)
                {
                    juurenMassa += juuri.B3.Massa;
                    juurenMassaKeskiPiste += juuri.B3.Massakeskipiste * juuri.B3.Massa;
                }
                if (!juuri.B4.OnkoTyhjä)
                {
                    juurenMassa += juuri.B4.Massa;
                    juurenMassaKeskiPiste += juuri.B4.Massakeskipiste * juuri.B4.Massa;
                }
                juuri.Massa = juurenMassa;
                juuri.Massakeskipiste = juurenMassaKeskiPiste / juurenMassa;
                return;
            }
            //Tapaus jossa tämä solmu on juurisolmu jolla on lapsisolmuja.
            if (juuri.OnkoJuuriSolmu)
            {
                SijoitaLaatikkoon3D(kappale, juuri);
                //Päivitetään nykyisen solmun massa ja massakeskipiste
                double juurenMassa = 0;
                Vertex3d juurenMassaKeskiPiste = new Vertex3d(0, 0, 0);
                if (!juuri.A1.OnkoTyhjä)
                {
                    juurenMassa += juuri.A1.Massa;
                    juurenMassaKeskiPiste += juuri.A1.Massakeskipiste * juuri.A1.Massa;
                }
                if (!juuri.A2.OnkoTyhjä)
                {
                    juurenMassa += juuri.A2.Massa;
                    juurenMassaKeskiPiste += juuri.A2.Massakeskipiste * juuri.A2.Massa;
                }
                if (!juuri.A3.OnkoTyhjä)
                {
                    juurenMassa += juuri.A3.Massa;
                    juurenMassaKeskiPiste += juuri.A3.Massakeskipiste * juuri.A3.Massa;
                }
                if (!juuri.A4.OnkoTyhjä)
                {
                    juurenMassa += juuri.A4.Massa;
                    juurenMassaKeskiPiste += juuri.A4.Massakeskipiste * juuri.A4.Massa;
                }
                if (!juuri.B1.OnkoTyhjä)
                {
                    juurenMassa += juuri.B1.Massa;
                    juurenMassaKeskiPiste += juuri.B1.Massakeskipiste * juuri.B1.Massa;
                }
                if (!juuri.B2.OnkoTyhjä)
                {
                    juurenMassa += juuri.B2.Massa;
                    juurenMassaKeskiPiste += juuri.B2.Massakeskipiste * juuri.B2.Massa;
                }
                if (!juuri.B3.OnkoTyhjä)
                {
                    juurenMassa += juuri.B3.Massa;
                    juurenMassaKeskiPiste += juuri.B3.Massakeskipiste * juuri.B3.Massa;
                }
                if (!juuri.B4.OnkoTyhjä)
                {
                    juurenMassa += juuri.B4.Massa;
                    juurenMassaKeskiPiste += juuri.B4.Massakeskipiste * juuri.B4.Massa;
                }
                juuri.Massa = juurenMassa;
                juuri.Massakeskipiste = juurenMassaKeskiPiste / juurenMassa;
                return;
            }
        }

        /// <summary>
        /// Sijoittaa kappaleen johonkin solmun kahdeksasta lehdestä.
        /// </summary>
        /// <param name="kappale">Kappale jota ollaan sijoittamassa.</param>
        /// <param name="juuri">Solmu johon halutaan sijoittaa.</param>
        private void SijoitaLaatikkoon3D(Kappale kappale, Lehti3D juuri)
        {
            /*
             * A1 = Vasen taka-ylänurkka
             * A2 = Oikea taka-ylänurkka
             * A3 = Vasen etu-ylänurkka
             * A4 = Oikea etu-ylänurkka
             * B1 = Vasen taka-alanurkka
             * B2 = Oikea taka-alanurkka
             * B3 = Vasen etu-alanurkka
             * B4 = Oikea etu-alanurkka
             */            
            if (kappale.Sijainti.x >= juuri.Sijainti.x)
            {
                //Kappale on oikealla puolella
                if (kappale.Sijainti.y >= juuri.Sijainti.y)
                {
                    //Kappale on oikeassa ylänurkassa
                    if(kappale.Sijainti.z >= juuri.Sijainti.z)
                    {
                        //Kappale on oikealla edessä ylänurkassa.
                        Sijoita3D(kappale, juuri.A4);
                    }
                    else
                    {
                        //Kappale on oikealla takana ylänurkassa.
                        Sijoita3D(kappale, juuri.A2);
                    }                    
                }
                else
                {
                    //Kappale on oikeassa alanurkassa
                    if (kappale.Sijainti.z >= juuri.Sijainti.z)
                    {
                        //Kappale on oikealla edessä alanurkassa.
                        Sijoita3D(kappale, juuri.B4);
                    }
                    else
                    {
                        //Kappale on oikealla takana alanurkassa.
                        Sijoita3D(kappale, juuri.B2);
                    }
                }
            }
            else
            {
                //Kappale on vasemmalla puolella
                if (kappale.Sijainti.y >= juuri.Sijainti.y)
                {
                    //Kappale on vasemmassa ylänurkassa
                    if (kappale.Sijainti.z >= juuri.Sijainti.z)
                    {
                        //Kappale on vasemmalla edessä ylänurkassa.
                        Sijoita3D(kappale, juuri.A3);
                    }
                    else
                    {
                        //Kappale on vasemmalla ylänurkassa takana
                        Sijoita3D(kappale, juuri.A1);
                    }
                }
                else
                {
                    //Kappale on vasemmassa alanurkassa
                    if (kappale.Sijainti.z >= juuri.Sijainti.z)
                    {
                        //Kappale on vasemmassa alanurkassa edessä
                        Sijoita3D(kappale, juuri.B3);
                    }
                    else
                    {
                        //Kappale on vasemmassa alanurkassa takana
                        Sijoita3D(kappale, juuri.B1);
                    }
                }
            }
        }

        /// <summary>
        /// Piirtää Barnes-Hut algoritmin kaksiulotteisessa avaruudessa vaatiman nelipuun.
        /// </summary>
        /// <param name="juuri">Solmu joka halutaan piirtää</param>
        /// <param name="k">kerroin laatikoiden koon skaalaamiseen</param>
        public void piirrä2DPuu(Lehti2D juuri, double k)
        {
            //Piirtää laatikon annetun solmun sijaintiin.            
            Vertex3d vasenAlanurkka = new Vertex3d(juuri.Sijainti.x - juuri.koko / 2, juuri.Sijainti.y - juuri.koko / 2, 0);
            double koko = juuri.koko;
            Gl.Begin(PrimitiveType.Lines);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            //Viiva vasemmasta alanurkasta ylös.
            Gl.Vertex3(project(norm(vasenAlanurkka, -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(0, koko, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva vasemmasta ylänurkasta oikeaan ylänurkkaan.
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(0, koko, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(koko, koko, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta ylänurkasta oikeaan alanurkkaan.
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(koko, koko, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(koko, 0, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta alanurkasta vasempaan alanurkkaan.
            Gl.Vertex3(project(norm(vasenAlanurkka + new Vertex3d(koko, 0, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenAlanurkka, -valovuosi, valovuosi, -1, 1)));
            Gl.End();
            //Piirtää tämän solmun sisäiset solmut, jos sellaisia on.
            if (juuri.OnkoJuuriSolmu)
            {
                piirrä2DPuu(juuri.oikeaYlä,k);
                piirrä2DPuu(juuri.oikeaAla,k);
                piirrä2DPuu(juuri.vasenYlä,k);
                piirrä2DPuu(juuri.vasenAla,k);
            }            
        }

        /// <summary>
        /// Piirtää Barnes-Hut algoritmin kolmiulotteisessa avaruudessa vaatiman kahdeksanpuun.
        /// </summary>
        /// <param name="juuri">Solmu joka halutaan piirtää</param>
        /// <param name="k">kerroin laatikoiden koon skaalaamiseen</param>
        public void piirrä3DPuu(Lehti3D juuri, double k)
        {
            
            //Piirtää laatikon annetun solmun sijaintiin.  
            
            Vertex3d vasenEtuAlanurkka = new Vertex3d(juuri.Sijainti.x - juuri.koko / 2, juuri.Sijainti.y - juuri.koko / 2, juuri.Sijainti.z + juuri.koko / 2);
            double koko = juuri.koko;
            Gl.Begin(PrimitiveType.Lines);
            Gl.Color3(0.0f, 1.0f, 0.0f);
            //Viiva vasemmasta etualanurkasta vasempaan etuylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka, -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva vasemmasta etuylänurkasta oikeaan etuylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta etuylänurkasta oikeaan etualanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, 0), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta etualanurkasta vasempaan etualanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka, -valovuosi, valovuosi, -1, 1)));

            //Viiva vasemmasta etualanurkasta vasempaan taka-alanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, 0, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva vasemmasta taka-alanurkasta vasempaan taka-ylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva vasemmasta takaylänurkasta vasempaan etuylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, 0), -valovuosi, valovuosi, -1, 1)));

            //Viiva vasemmasta taka-alanurkasta oikeaan taka-alanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva vasemmasta takaylänurkasta oikeaan takaylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(0, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta etuylänurkasta oikeaan takaylänurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta takaylänurkasta oikeaan taka-alanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, koko, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            //Viiva oikeasta taka-alanurkasta oikeaan etualanurkkaan.
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, -koko), -valovuosi, valovuosi, -1, 1)));
            Gl.Vertex3(project(norm(vasenEtuAlanurkka + new Vertex3d(koko, 0, 0), -valovuosi, valovuosi, -1, 1)));
            Gl.End();
            //Piirtää tämän solmun sisäiset solmut, jos sellaisia on.
            if (juuri.OnkoJuuriSolmu)
            {
                piirrä3DPuu(juuri.A1, k);
                piirrä3DPuu(juuri.A2, k);
                piirrä3DPuu(juuri.A3, k);
                piirrä3DPuu(juuri.A4, k);
                piirrä3DPuu(juuri.B1, k);
                piirrä3DPuu(juuri.B2, k);
                piirrä3DPuu(juuri.B3, k);
                piirrä3DPuu(juuri.B4, k);
            }
            
        }

        /// <summary>
        /// Laskee kaksiulotteisessa avaruudessa kappaleeseen kohdistuvan kiihtyvyyden.
        /// </summary>
        /// <param name="juurisolmu">Puun aloitussolmu, tai alipuun aloitussolmu.</param>
        /// <param name="kappale">Kappale johon kohdistuvia voimia halutaan laskea.</param>
        /// <returns></returns>
        public Vertex3d BarnesHut2D(Lehti2D juurisolmu, Kappale kappale)
        {
            if(juurisolmu.kappale == kappale||juurisolmu.OnkoTyhjä)
            {
                return new Vertex3d(0, 0, 0);
            }
            double etäisyys = Etäisyys(juurisolmu.Massakeskipiste, kappale.Sijainti);
            if (juurisolmu.OnkoJuuriSolmu)
            {
                //Kyseessä on solmu jolla on lapsisolmuja, siirrytään sisempiin solmuihin ellei olla riittävän kaukana.
                //Mikäli solmun koko / etäisyydellä < kynnysarvo (0.5), kohdellaan tämän solmun sisäisiä kappaleita yhtenä kappaleena.
                
                if (juurisolmu.koko / etäisyys < 0.5)
                {
                    //Solmu on riittävän kaukana, kohdellaan yhtenä kappaleena.
                    return (kappale.Sijainti - juurisolmu.Massakeskipiste) * juurisolmu.Massa / (Math.Pow(Etäisyys(kappale.Sijainti, juurisolmu.Massakeskipiste), 3));
                }
                else
                {
                    //Käydään läpi sisemmät solmut
                    return BarnesHut2D(juurisolmu.oikeaYlä, kappale) +
                        BarnesHut2D(juurisolmu.oikeaAla, kappale) +
                        BarnesHut2D(juurisolmu.vasenAla, kappale) +
                        BarnesHut2D(juurisolmu.vasenYlä, kappale);
                }
            }
            else
            {
                //Kyseessä on lehtisolmu. Lasketaan normaalisti.
                //Estää laskennallista singulariteettia (mutta vähentää tarkkuutta
                if (etäisyys < valovuosi * 0.01) { return new Vertex3d(0, 0, 0); }
                return (kappale.Sijainti - juurisolmu.Massakeskipiste) * juurisolmu.Massa / (Math.Pow(Etäisyys(kappale.Sijainti, juurisolmu.Massakeskipiste), 3));
            }
        }

        /// <summary>
        /// Laskee kappaleeseen kohdistuvan kiihtyvyyden kolmiulotteisessa avaruudessa.
        /// </summary>
        /// <param name="juurisolmu">Puun aloitussolmu, tai alipuun aloitussolmu.</param>
        /// <param name="kappale">Kappale jonka kiihtyvyys halutaan selvittää.</param>
        /// <returns></returns>
        public Vertex3d BarnesHut3D(Lehti3D juurisolmu, Kappale kappale)
        {
            if (juurisolmu.kappale == kappale || juurisolmu.OnkoTyhjä)
            {
                return new Vertex3d(0, 0, 0);
            }
            double etäisyys = Etäisyys(juurisolmu.Massakeskipiste, kappale.Sijainti);
            if (juurisolmu.OnkoJuuriSolmu)
            {
                //Kyseessä on solmu jolla on lapsisolmuja, siirrytään sisempiin solmuihin ellei olla riittävän kaukana.
                //Mikäli solmun koko / etäisyydellä < kynnysarvo (0.5), kohdellaan tämän solmun sisäisiä kappaleita yhtenä kappaleena.

                if (juurisolmu.koko / etäisyys < 0.5)
                {
                    //Solmu on riittävän kaukana, kohdellaan yhtenä kappaleena.
                    return (kappale.Sijainti - juurisolmu.Massakeskipiste) * juurisolmu.Massa / (Math.Pow(Etäisyys(kappale.Sijainti, juurisolmu.Massakeskipiste), 3));
                }
                else
                {
                    //Käydään läpi sisemmät solmut
                    return BarnesHut3D(juurisolmu.A1, kappale) +
                        BarnesHut3D(juurisolmu.A2, kappale) +
                        BarnesHut3D(juurisolmu.A3, kappale) +
                        BarnesHut3D(juurisolmu.A4, kappale) +
                        BarnesHut3D(juurisolmu.B1, kappale) +
                        BarnesHut3D(juurisolmu.B2, kappale) +
                        BarnesHut3D(juurisolmu.B3, kappale) +
                        BarnesHut3D(juurisolmu.B4, kappale);
                }
            }
            else
            {
                //Kyseessä on lehtisolmu. Lasketaan normaalisti.
                //Estää laskennallista singulariteettia (mutta vähentää tarkkuutta
                if (etäisyys < valovuosi * 0.01) { return new Vertex3d(0, 0, 0); }
                return (kappale.Sijainti - juurisolmu.Massakeskipiste) * juurisolmu.Massa / (Math.Pow(Etäisyys(kappale.Sijainti, juurisolmu.Massakeskipiste), 3));
            }
        }

        /// <summary>
        /// Laskee etäisyyden kahden pisteen välillä
        /// </summary>
        /// <param name="a">Piste a</param>
        /// <param name="b">Piste b</param>
        /// <returns></returns>
        private double Etäisyys(Vertex3d a, Vertex3d b)
        {
            double x = Math.Pow(b.x - a.x, 2);
            double y = Math.Pow(b.y - a.y, 2);
            double z = Math.Pow(b.z - a.z, 2);
            return Math.Sqrt(x + y + z);
        }
        /// <summary>
        /// Siirtää vektorin kameran sijainnin ja rotaation perusteella.
        /// </summary>
        /// <param name="a">Vektori jota siirretään</param>
        /// <returns></returns>
        private Vertex3d project(Vertex3d a)
        {
            Vertex4d vv = new Vertex4d(a.x, a.y, a.z);   
            Vertex3d b = (Vertex3d)(mvp * vv);              
            return b;
        }

        /// <summary>
        /// Piirtää tähden annettuun sijaintiin. Tähti piirretään kolmella risteävällä viivalla.
        /// Tämä funktio on tarpeen koska pisteet näkyy vain tietyn matkan päähän
        /// </summary>
        /// <param name="center">Tähden sijainti</param>
        private void drawStar(Vertex3d center)
        {

            Gl.Begin(PrimitiveType.Lines);
            Gl.Color3(1.0f, 1.0f, 1.0f);
            Gl.Vertex3(center + new Vertex3d(0, 0, -0.005));
            Gl.Vertex3(center + new Vertex3d(0, 0, 0.005));
            Gl.Vertex3(center + new Vertex3d(0, -0.005, 0));
            Gl.Vertex3(center + new Vertex3d(0, 0.005, 0));
            Gl.Vertex3(center + new Vertex3d(-0.005, 0, 0));
            Gl.Vertex3(center + new Vertex3d(0.005, 0, 0));
            Gl.End();            
        }

        /// <summary>
        /// Tallentaa annettujen kappaleiden sijainnit tekstitiedostoon.
        /// </summary>
        /// <param name="kappaleet">Lista kappaleista.</param>
        /// <param name="polku">Tallennussijainti tiedostopäätteineen.</param>
        public static void tallennaKappaleet(Kappale[] kappaleet, String polku)
        {            
            try
            {
                StreamWriter s = new StreamWriter(polku);
                foreach(Kappale k in kappaleet)
                {
                    s.WriteLine(                        
                        k.Sijainti.x.ToString("R") + " " + k.Sijainti.y.ToString("R") + " " + k.Sijainti.z.ToString("R"));
                }
                s.Close();
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Päivittää kappaleet annetusta tekstitiedostosta
        /// </summary>
        /// <param name="polku">Tiedostopolku tekstitiedostoon</param>
        /// <param name="kappaleet">Kappaleet joiden sijainnit päivitetään</param>
        public void haeKappaleet(String polku, Kappale[] kappaleet)
        {
            try
            {
                StreamReader s = new StreamReader(polku);
                foreach(Kappale k in kappaleet)
                {
                    string[] tiedot = s.ReadLine().Split(null);
                    k.Sijainti = new Vertex3d(Double.Parse(tiedot[0]), Double.Parse(tiedot[1]), Double.Parse(tiedot[2]));
                }
            } catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
