using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiea306
{
    public class Lehti2D
    {
        /*
         * Solmun sisältämät lehtisolmut. Luodaan vasta erikseen tarvittaessa.
         * Sijoittuu nimiensä mukaisesti neliöön.
         * _____________________
         *| vasenYlä | oikeaYlä |
         *|__________|__________|
         *| vasenAla | oikeaAla |
         *|__________|__________|
         * 
         */
        public Lehti2D oikeaYlä;
        public Lehti2D oikeaAla;
        public Lehti2D vasenYlä;
        public Lehti2D vasenAla;
        public Lehti2D()
        {
            OnkoJuuriSolmu = false;
            OnkoTyhjä = true;
            Massa = 0;
            Massakeskipiste = new Vertex3d(0, 0, 0);
        }

        //Solmun sijainti.
        public Vertex3d Sijainti { get; set; }
        //Solmun koko. (Yhden sivun pituus)
        public double koko { get; set; }
        //Solmun kokonaismassa.
        public double Massa { get; set; }
        //Solmun massakeskipiste.
        public Vertex3d Massakeskipiste { get; set; }
        //Solun sisältämä kappale.
        public Kappale kappale { get; set; }
        //Onko tällä solmulla lehtisolmuja
        public bool OnkoJuuriSolmu { get; set; }
        //Onko tämä solmu tyhjä (ei lehtisolmuja eikä kappaletta).
        public bool OnkoTyhjä { get; set; }
    }
}
