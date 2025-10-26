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

        public string DisplayText => $"{Cikkszam} | {Megnevezes}";
    }
}
