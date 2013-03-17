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
        public Stuk Stuk;       // speler of barricade
        public Vak next;        // next vak in list
        public Vak previous;    // previous vak in list
        public bool InhetDorp;
    }

    class Start : Vak
    {
        public Start()
        {
        }
    }

    class Doel : Vak
    {
        public Doel()
        {
        }
    }

    class Splitsing : Vak
    {
        public Vak split;   // split vak in list
        public Splitsing()
        {
        }
    }

    class Bos : Vak
    {
        public List<Speler> Spelers;

        public Bos()
        {
        }
    }

    class Rust : Vak
    {
        public Rust()
        {
        }
    }
}
