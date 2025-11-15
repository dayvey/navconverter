using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navconverter.Models
{
    public class AdoHivataliTetel
    {
        public string Tetel { get; set; }
        public string Mennyiseg { get; set; }
        public string Egysegar { get; set; }
        public string Afa { get; set; }
        public string Netto { get; set; }
        public string Brutto { get; set; }

        public string AfaOsszeg { get; set; }

        public string Cikkszam { get; set; } // ide kerül a faber cikkszám
        public string FaberMegnevezes { get; set; } // opcionális, ellenőrzéshez

        public string DisplayText => $"{Tetel} | {Cikkszam}";
    }
}
