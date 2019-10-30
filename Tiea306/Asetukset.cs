using System;
using System.Windows.Forms;
using System.IO;
using OpenGL;
using System.Collections.Generic;
using System.Linq;
/*
 * Tämä sisältää asetusikkunaan liittyvät koodit.
 * 
 * Tekijä: Aapo Peiponen
 */
namespace Tiea306
{
    //TODO: Virheenkäsittelijät, syötteiden validointi reaaliaikaisesti, näytä tallennetun simulaation tiedot kun se on valittuna
    //TODO: Lisää valinta simulaation askelten tallentamiseksi vain tietyin intervallein tilan säästämiseksi.
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
            //Hakee olemassa olevat simulaatiot listaan valmiiksi.
            try
            {
                List<string> simulaatiot = Directory.GetDirectories("/simulations").Select(Path.GetFileName).ToList();
                simulaatioLista.DataSource = simulaatiot;
                if(simulaatiot.Count == 0)
                {
                    button1.Enabled = false;
                }
            } catch (Exception s)
            {
                //
            }           
        }
        //Tapahtumankäsittelijä simulaation aloittavalle painikkeelle
        private void aloita_Click(object sender, EventArgs e)
        {
            int n = 500;
            try
            {
                n = Int32.Parse(maara.Text);
            } catch (Exception)
            {
                //
            }   
            if (tallenna.Checked)
            {
                Directory.CreateDirectory("/simulations/" + nimi.Text);
                string tiedot = nimi.Text + " " + maara.Text + " " + onko2D.Checked + " " + aikaAskel.Text + " " + metodi.SelectedIndex;
                File.WriteAllText("/simulations/" + nimi.Text + "/info.txt", tiedot);
            }
            new Simulaattori(Generoi(n), onko2D.Checked, metodi.SelectedIndex, Convert.ToDouble(aikaAskel.Text), tallenna.Checked, false, "/simulations/" + nimi.Text).Show();            
        }
        //Tapahtumankäsittelijä 2D valintalaatikolle
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !onko2D.Checked;
        }
        //Tapahtumankäsittelijä 3D valintalaatikolle
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            onko2D.Checked = !checkBox2.Checked;
        }
        //Tapahtumankäsittelijä simulaation tallentamisen valintalaatikolle
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //Ottaa tai poistaa käytöstä simulaation nimen 
            if (tallenna.Checked)
            {
                nimi.Enabled = true;
                nimiLaatikko_TextChanged(sender, e);
            } else
            {
                nimi.Enabled = false;
                kansioOlemassa.Text = "";
                aloita.Enabled = true;
            }
        }
        //Tapahtumankäsittelijä simulaation nimen laatikolle, joka tarkistaa että simuaation nimi on validi
        private void nimiLaatikko_TextChanged(object sender, EventArgs e)
        {           
            //Tarkistaa onko nimetty kansio jo olemassa.
            if (nimi.Text.Equals(""))
            {
                kansioOlemassa.Text = "Ei voi käyttää tyhjää nimenä.";
                aloita.Enabled = false;
                return;
            }
            if (Directory.Exists("/simulations/" + nimi.Text))
            {
                kansioOlemassa.Text = "On jo olemassa.";
                aloita.Enabled = false;
            } else if(nimi.Text.Contains(" ")) {
                kansioOlemassa.Text = "Ei välilyöntejä.";
                aloita.Enabled = false;
            }
            else {
                kansioOlemassa.Text = "";
                aloita.Enabled = true;
            }
        }
        //Tapahtumankäsittelijä simulaation toiston aloittamiselle
        private void button1_Click(object sender, EventArgs e)
        {
            
            //string tiedot = nimi.Text + " " + maara.Text + " " + onko2D.Checked + " " + aikaAskel.Text + " " + metodi.SelectedIndex;
            string[] tiedot = File.ReadAllText("/simulations/" + simulaatioLista.GetItemText(simulaatioLista.SelectedItem) + "/info.txt").Split(null);
            new Simulaattori(
                Generoi(Int32.Parse(tiedot[1])), 
                System.Boolean.Parse(tiedot[2]), 
                Int32.Parse(tiedot[4]), 
                Double.Parse(tiedot[3]), 
                false, 
                true,
                "/simulations/" + simulaatioLista.GetItemText(simulaatioLista.SelectedItem)).Show();
        }

        /// <summary>
        /// Generoi annetun määrän tähtiä
        /// </summary>
        /// <param name="kpl">Tähtien määrä</param>
        /// <returns></returns>
        private Kappale[] Generoi(int kpl)
        {
            double valovuosi = 63239.7263;
            Kappale[] kappaleet = new Kappale[kpl];
            Random r = new Random();
            for (int i = 0; i < kpl; i++)
            {
                Vertex3d sijainti;
                if (checkBox2.Checked)
                {
                    sijainti = (new Vertex3d(r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5)) * valovuosi;
                }
                else
                {
                    sijainti = (new Vertex3d(r.NextDouble() * 10 - 5, r.NextDouble() * 10 - 5, 0)) * valovuosi;
                }

                double massa = r.NextDouble() * 10;
                Vertex3d kiihtyvyys = (new Vertex3d(0, 0, 0));
                Vertex3d nopeus = (new Vertex3d(0, 0, 0));
                kappaleet[i] = new Kappale(sijainti, massa, kiihtyvyys, nopeus);
            }
            return kappaleet;
        }
    }
}
