using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Speelbord
    {
        public List<string> bord { get; set; }
        //private int posy, posx;

        public Speelbord(List<string> b)
        {
            // Stap 1: bordyx opslaan in yx List
            bord = b;
            
            // Test: uitlezen van char test print "<" uit originele bord in output venster
            //Console.WriteLine(bord[0][36]); 
            
            // Stap 2: maak een linkedlist van Vak objecten
            for (int y = 0; y < bord.Count; y++)
            {
                for (int x = 0; x < bord[y].Length; x++)
                {
                    BepaalVak(y, x);
                }
                Console.WriteLine();
            }
        }

        private Vak BepaalVak(int y, int x)
        {
            Vak vak;
            switch (bord[y][x])
            {
                case '<':
                    if (bord[y][x + 2] == '>')
                    {
                        switch (bord[y][x + 1])
                        {
                            case ' ':
                                vak = new Doel();
                                Console.Write("< >");
                                return vak;
                            case '1':
                                vak = new Bos();
                                Console.Write("<1>");
                                return vak;
                            case 'R':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('R');
                                Console.Write("<R>");
                                return vak;
                            case 'G':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('G');
                                Console.Write("<G>");
                                return vak;
                            case 'Y':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('Y');
                                Console.Write("<Y>");
                                return vak;
                            case 'B':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('B');
                                Console.Write("<B>");
                                return vak;
                            case 'r':
                                vak = new Start();
                                return vak;
                            case 'g':
                                vak = new Start();
                                return vak;
                            case 'y':
                                vak = new Start();
                                return vak;
                            case 'b':
                                vak = new Start();
                                return vak;
                        }   
                    }
                    break;
                case '(':
                    if (bord[y][x + 2] == ')')
                    {
                        switch (bord[y][x + 1])
                        {
                            case ' ':
                                vak = new Vak();
                                Console.Write("( )");
                                return vak;
                            case '*':
                                vak = new Vak();
                                vak.Stuk = new BarricadeStuk();
                                Console.Write("(*)");
                                return vak;
                            case 'R':
                                vak = new Vak();
                                vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                                Console.Write("(R)");
                                return vak;
                            case 'G':
                                vak = new Vak();
                                vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                                Console.Write("(G)");
                                return vak;
                            case 'Y':
                                vak = new Vak();
                                vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                                Console.Write("(Y)");
                                return vak;
                            case 'B':
                                vak = new Vak();
                                vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                                Console.Write("(B)");
                                return vak;
                        }
                    }
                    break;
                case '{':
                    if (bord[y][x + 2] == '}')
                    {
                        vak = new Rust();
                        Console.Write("{ }");
                        return vak;
                    }
                    break;
                case '[':
                    if (bord[y][x + 2] == ']')
                    {
                        vak = new Barricade();
                        Console.Write("[ ]");
                        return vak;
                    }
                    break;
                default:
                    break;
            }
            return null;//throw new ArgumentException("Parameter cannot be null", "bord[y][x]");
        }

        public void KoppelVak(int y, int x)
        {
            if (bord[y-1][x + 1] != null)
            {
                switch (bord[y - 1][x + 1])
                {
                    case '|':

                        break;
                }
            }
        }
    }
}
