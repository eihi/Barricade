using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Dobbelsteen
    {
        private int waarde;
        private Random random = new Random();

        public Dobbelsteen()
        {
        }

        public int Waarde
        {
            get { return waarde; }
        }

        public void Gooi()
        {
            waarde = random.Next(1, 7);
        }
    }
}
