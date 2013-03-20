using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace BarricadeSpel.Model
{
    class Speelbord
    {
        public List<string> yx { get; set; }

        public Speelbord(List<string> bordyx)
        {
            // Stap 1: bordyx opslaan in yx List
            yx = bordyx;
            
            // Test: uitlezen van char test print "<" uit originele bord in output venster
            Console.WriteLine(yx[0][36]); 
            
            // Stap 2: maak een linkedlist van Vak objecten

        }
    }
}
