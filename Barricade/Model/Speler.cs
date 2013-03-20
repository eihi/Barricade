using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class SpelerStuk : Stuk
    {
        public char Kleur { get; set; }     // speler kleur
        public string Naam { get; set; }    // speler naam

        public SpelerStuk(char _kleur)
        {
            Kleur = _kleur;
        }
    }
}
