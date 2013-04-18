using BarricadeSpel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Vak
    {
        //protected Stuk Stuk;            // speler of barricade
        public Stuk Stuk { get; set; }

        public Vak noord;               // noord vak in list
        public Vak oost;                // oost vak in list
        public Vak zuid;                // zuid vak in list
        public Vak west;                // west vak in list
        public bool LoopVak;            // als true kan er op de vak gelopen worden
        public bool InhetDorp;          // als true dan bevindt het stuk zich in het dorp
        public bool BarricadeMag;       // als true mag er een barricade geplaatst worden

        public Vak()
        {

        }
    }
}
