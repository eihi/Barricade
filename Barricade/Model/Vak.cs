using BarricadeSpel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel
{
    class Vak
    {
        //protected Stuk Stuk;            // speler of barricade
        public Stuk Stuk { get; set; }

        public Vak noord;               // noord vak in list
        public Vak oost;                // oost vak in list
        public Vak zuid;                // zuid vak in list
        public Vak west;                // west vak in list
        public bool LoopVak = true;     // als true kan er op de vak gelopen worden
        public bool InhetDorp = false;  // als true dan bevindt het stuk zich in het dorp

        public Vak()
        {

        }
    }
}
