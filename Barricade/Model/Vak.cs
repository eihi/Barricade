using BarricadeSpel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel
{
    abstract class Vak
    {
        public Stuk Stuk;               // speler of barricade
        public Vak next;                // next vak in list
        public Vak previous;            // previous vak in list
        public bool LoopVak = true;     // als true kan er op de vak gelopen worden
        public bool InhetDorp = false;  // als true dan bevindt het stuk zich in het dorp

        public Vak()
        {

        }
    }
}
