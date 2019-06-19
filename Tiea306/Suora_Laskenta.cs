using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiea306
{
    class Suora_Laskenta
    {
        //TODO: Käytä mieluummin astronomista yksikköä?
        //double gravitaatiovakio = 6.672e-11;
        //double auringon_massa_kg = 1.9891e30;
        //double valovuosi_m = 9.4605284e15;
        //double vuosi_sekunneissa = 31556926;
        double gravitaatiovakio = 0.00430091;
        double auringon_massa = 1;
        double valovuosi_pc = 0.306594845;
        double vuosi_sekunneissa = 1;

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
             * TODO: Dynaaminen aika-askel
             */
            for (int i = 0; i < kappaleet.Length; i++)
            {
                Vertex3d kiihtyvyys = new Vertex3d(0, 0, 0);
                for (int j = 0; j < kappaleet.Length; j++)
                {
                    if (i == j) { continue; }
                    double massa_j = kappaleet[j].Massa;
                    Vertex3d sijainti_i = kappaleet[i].Sijainti;
                    Vertex3d sijainti_j = kappaleet[j].Sijainti;
                    kiihtyvyys += (sijainti_i - sijainti_j) * massa_j / Math.Pow(Etäisyys(sijainti_i, sijainti_j), 3);
                }
                kappaleet[i].Kiihtyvyys = kiihtyvyys * -gravitaatiovakio;
                Vertex3d vanha_nopeus = kappaleet[i].Nopeus;
                kappaleet[i].Nopeus = kappaleet[i].Nopeus + kappaleet[i].Kiihtyvyys * vuosi_sekunneissa;
                kappaleet[i].Sijainti = vanha_nopeus*vuosi_sekunneissa + (kappaleet[i].Kiihtyvyys * 0.5) * vuosi_sekunneissa * vuosi_sekunneissa;
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
