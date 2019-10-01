using OpenGL;
using System;
using System.Windows.Forms;

namespace Tiea306
{
    class Suora_Laskenta
    {
        //Gravitaatiovakio, kun massan yksikkö on auringon massa, matkassa käytetään astronomista yksikköä, ja ajan yksikkö on vuosi
        double gravitaatiovakio = 4*Math.Pow(Math.PI,2);
        //Valovuosi astronomisissa yksiköissä.
        double valovuosi = 63239.7263;
        //Aika-askel
        public double aika = 1000;
        public void asetaAikaAskel(double aikaAskel)
        {
            aika = aikaAskel;
        }
        /// <summary>
        /// Metodi kappaleiden sijaintien päivittämiseen.
        /// </summary>
        /// <param name="kappaleet"></param>
        public void päivitä(Kappale[] kappaleet)
        {
            /*
             * Ulompi silmukka määrää kappaleen joka päivitetään, sisempi silmukka
             * on kaikkien muiden kappaleiden läpikäyntiä varten vuorovaikutusten 
             * laskemiseksi.
             */
            for (int i = 0; i < kappaleet.Length; i++)
            {
                Vertex3d kiihtyvyys = new Vertex3d(0, 0, 0);
                for (int j = 0; j < kappaleet.Length; j++)
                {
                    //Kappale ei vaikuta omaan liikkeeseensä.
                    if (i == j) { continue; }
                    //Pehmennysparametri, poistaa laskennallisen singulariteetin mahdollisuuden.
                    if (Etäisyys(kappaleet[i].Sijainti, kappaleet[j].Sijainti) < valovuosi * 0.01){ continue; }  
                    double massa_j = kappaleet[j].Massa;
                    Vertex3d sijainti_i = kappaleet[i].Sijainti;
                    Vertex3d sijainti_j = kappaleet[j].Sijainti;
                    //Lasketaan kappaleeseen kohdistuva kiihtyvyys kappaleesta j, ja summataan se yhteen aiempien kanssa.
                    kiihtyvyys += (sijainti_i - sijainti_j) * massa_j / (Math.Pow(Etäisyys(sijainti_i, sijainti_j), 3) );  
                }
                //Lopulliseen kiihtyvyyteen otettava huomioon gravitaatiovakio.
                kappaleet[i].Kiihtyvyys = kiihtyvyys * -gravitaatiovakio;
                //Lasketaan kappaleen sijainti nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                kappaleet[i].Sijainti += kappaleet[i].Nopeus * aika + kappaleet[i].Kiihtyvyys * 0.5 * Math.Pow(aika, 2);
                //Lasketaan kappaleelle uusi nopeus vanhan nopeuden, kiihtyvyyden ja aika-askeleen perusteella.
                kappaleet[i].Nopeus = kappaleet[i].Nopeus + kappaleet[i].Kiihtyvyys * aika;
            }
        }

        /// <summary>
        /// Apumetodi etäisyyksien laskemiseen.
        /// </summary>
        /// <param name="a">Ensimmäinen piste</param>
        /// <param name="b">Toinen piste</param>
        /// <returns></returns>
        private double Etäisyys(Vertex3d a, Vertex3d b)
        {
            double x = Math.Pow(b.x - a.x, 2);
            double y = Math.Pow(b.y - a.y, 2);
            double z = Math.Pow(b.z - a.z, 2);
            return Math.Sqrt(x + y + z);
        }
    }
}
