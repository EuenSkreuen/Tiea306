using OpenGL;
/*
 * Tämä luokka soveltuu tähtien tai planeettojen kuvaamiseen. Sisältää simulaation kannalta
 * oleelliset tiedot.
 * 
 * Tekijä: Aapo Peiponen
 */
namespace Tiea306
{
    public class Kappale
    {
        public Kappale(Vertex3d sijainti, double massa, Vertex3d kiihtyvyys, Vertex3d nopeus)
        {
            Sijainti = sijainti;
            Massa = massa;
            Kiihtyvyys = kiihtyvyys;
            Nopeus = nopeus;
        }
        public Vertex3d Sijainti { get; set; }
        public double Massa { get; set; }
        public Vertex3d Kiihtyvyys { get; set; }
        public Vertex3d Nopeus { get; set; }

    }
}
