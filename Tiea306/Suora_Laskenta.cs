using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiea306
{
    static class Suora_Laskenta
    {
        //TODO: Käytä mieluummin astronomista yksikköä?
        static double gravitaatiovakio = 6.672e-11;
        static double auringon_massa_kg = 1.9891e30;
        static double valovuosi_m = 9.4605284e15;
        static double vuosi_sekunneissa = 31556926;

        static public void päivitä(Kappale[] kappaleet)
        {
            /*
             * Ulompi silmukka määrää kappaleen joka päivitetään, sisempi silmukka
             * on kaikkien muiden kappaleiden läpikäyntiä varten vuorovaikutusten 
             * laskemiseksi.
             */
            for (int i = 0; i < kappaleet.Length; i++)
            {
                for(int k = 0; k < kappaleet.Length; k++)
                {
                    if (i == k) { continue; }
                }
            }
        }
    }
}
