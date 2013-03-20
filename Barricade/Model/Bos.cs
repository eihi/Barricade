using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Bos : Vak
    {
        public List<SpelerStuk> Spelers = new List<SpelerStuk>(); // om spelers op te slaan die naar het bos gestuurd worden

        public Bos()
        {
            LoopVak = false; // staat standaard op true in superklasse dus moet op false gezet worden
        }
    }
}
