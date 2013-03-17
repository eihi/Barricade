using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Stuk
    {
    }

    class Speler : Stuk
    {
        public char Kleur { get; set; }
        public Speler()
        {

        }
    }

    class Barricade : Stuk
    {
        public Barricade()
        {

        }
    }
}
