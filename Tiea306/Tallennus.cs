using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tiea306
{
    /// <summary>
    /// Tämän luokan tarkoitus on tallentaa simulaation askeleet, ja lukea niitä tarvittaessa.
    /// </summary>
    class Tallennus
    {   
        // TODO: Korvaa tallennettava teksti suoraan simulaatio-oliolla tms, mikä ikinä tuleekin vastaamaan tähtien sijainteja.
        /// <summary>
        /// Tämä metodi tallentaa simulaation yhden askeleen tiedot. 
        /// </summary>
        /// <param name="data">Tähtien sijainnit ja muut tiedot</param>
        /// <param name="name">Simulaation nimi</param>
        public static void tallenna(String data, String name)
        {
            try
            {
                if (!(Directory.Exists("simulations/" + name)))
                {
                    Directory.CreateDirectory("simulations/" + name);
                }
                BinaryWriter kirjoittaja = new BinaryWriter(new FileStream("simulations/" + name+"/"+name, FileMode.Create));
                kirjoittaja.Write(data);
                kirjoittaja.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine("Virhe: " + e.Message);
                return;
            }            
        }

        // TODO: Sama kuin tallennuksessa, tosin tässä palauta tekstin sijaan suoraan härpäke jonka pohjalta tähdet voidaan piirtää, tai seuraava askel laskea.
        /// <summary>
        /// Tämä metodi lukee annetusta tiedostosta simulaation yhden askeleen tiedot.
        /// </summary>
        /// <param name="name">Simulaation nimi</param>
        /// <returns></returns>
        public static string lue(String name)
        {
            try
            {
                BinaryReader lukija = new BinaryReader(new FileStream("simulations/" + name + "/" + name, FileMode.Open));
                String s = lukija.ReadString();
                lukija.Close();
                return s;
            }
            catch (IOException e)
            {
                Console.WriteLine("Virhe: " + e.Message);
                return "";
            }
        }
    }
}
