using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace navconverter.Models
{
    public class FaberTetel
    {
        public string Cikkszam { get; set; }
        public string Megnevezes { get; set; }

        public int Chosen = 0;
        public string DisplayText => $"{Cikkszam} | {Megnevezes}";
    }
}
