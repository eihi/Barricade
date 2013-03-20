using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BarricadeSpel.Model
{
    class Spel
    {
        public Spel(List<string> bestand)
        {
            Speelbord bord = new Speelbord(bestand);
            Dobbelsteen dobbelsteen = new Dobbelsteen();
        }
    }
}
