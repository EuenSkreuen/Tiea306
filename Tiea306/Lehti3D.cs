using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * 3-ulotteisen Barnes-Hut puun lehtisolmu
 * 
 * Tekijä: Aapo Peiponen
 */
namespace Tiea306
{
    public class Lehti3D
    {
        /*
         * Selitys merkinnöille:
         * Kyseessä on samanlainen rakenne kuin Lehti2D, mutta tässä tapauksessa puhutaan neliöiden sijaan kuutioista
         * Samalla tavalla kuin kaksiulotteisessa versiossa neliö jakautuu neljään yhtään suureen neliöön,
         * tässä kuutio jakautuu kahdeksaan yhtä suureen kuutioon.
         * A on ylempi kerros ja B on alempi kerros, numerot menee ylhäältäpäin katsottuna näin:
         * A1 A2
         * A3 A4
         * Ja edestäpäin katsottuna näyttää tältä
         * A3 A4
         * B3 B4
         *  
         */ 
        public Lehti3D A1; //Vasen taka-ylänurkka
        public Lehti3D A2; //Oikea taka-ylänurkka
        public Lehti3D A3; //Vasen etu-ylänurkka
        public Lehti3D A4; //Oikea etu-ylänurkka
        public Lehti3D B1; //Vasen taka-alanurkka
        public Lehti3D B2; //Oikea taka-alanurkka
        public Lehti3D B3; //Vasen etu-alanurkka
        public Lehti3D B4; //Oikea etu-alanurkka
        public Lehti3D()
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
