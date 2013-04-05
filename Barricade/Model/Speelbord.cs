using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Speelbord
    {
        private Vak[,] State;
        public List<string> bord { get; set; }

        public Speelbord(List<string> b)
        {
            State = new Vak[100, 100]; // 2d array maken voor savestate
            // Stap 1: b opslaan in bord List
            bord = b;
            
            // Stap 2: maak een SaveState 2d array van Vak objecten
            for (int y = 0; y < bord.Count; y++)
            {
                for (int x = 0; x < bord[y].Length; x++)
                {
                    if (x % 4 == 0)
                        State[y,x/4] = BepaalVak(y, x);
                }
                Console.WriteLine();
            }
            KoppelVak();
            SaveState();
        }

        private void SaveState()
        {
            string blok;
            for (int i = 0; i < 40; i++)
            {
                //Console.Write(i + " ");
                for (int j = 0; j < 100; j++)
                {
                    if (State[i, j] == null)
                    {
                        
                            blok = "    ";
                    }
                    else
                    {
                        switch (State[i, j].ToString())
                        {
                            case "BarricadeSpel.Model.Doel":
                                if (State[i, j].oost != null)
                                    blok = "< >-";
                                else
                                    blok = "< > ";
                                break;
                            case "BarricadeSpel.Model.Start":
                                blok = BepaalStateStuk(State[i, j], "<R> ");
                                break;
                            case "BarricadeSpel.Model.Bos":
                                blok = "<1> ";
                                break;
                            case "BarricadeSpel.Model.Barricade":
                                if (State[i, j].oost != null)
                                    blok = "[ ]-";
                                else
                                    blok = BepaalStateStuk(State[i, j], "[ ] ");
                                break;
                            case "BarricadeSpel.Model.Rust":
                                if (State[i, j].oost != null)
                                    blok = "{ }-";
                                else
                                    blok = BepaalStateStuk(State[i, j], "{ } ");
                                break;
                            case "BarricadeSpel.Model.Vak":
                                if (State[i, j].oost != null)
                                    blok = "( )-";
                                else
                                    blok = BepaalStateStuk(State[i, j], "( ) ");
                                break;
                            default:
                                blok = "    ";
                                break;
                        }
                    }
                    Console.Write(blok);
                }
                Console.Write("\n");
            }
        }

        private string BepaalStateStuk(Vak v, string b)
        {
            string vak = b;
            if (v.Stuk != null)
            {
                switch (v.Stuk.ToString())
                {
                    case "BarricadeSpel.Model.BarricadeStuk":
                        if (v.oost != null)
                            return vak[0] + "*" + vak[2] + "-";
                        else
                            return vak[0] + "*" + vak[2] + " ";
                    case "BarricadeSpel.Model.SpelerStuk":
                        if (v.oost != null)
                            return vak[0] + ((SpelerStuk)v.Stuk).Kleur.ToString() + vak[2] + "-";
                        else
                            return vak[0] + ((SpelerStuk)v.Stuk).Kleur.ToString() + vak[2] + " ";
                }
            }
            return vak;
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
                                return new Doel();
                            case '1':
                                return new Bos();;
                            case 'R':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('R');
                                return vak;
                            case 'G':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('G');
                                return vak;
                            case 'Y':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('Y');
                                return vak;
                            case 'B':
                                vak = new Start();
                                vak.Stuk = new SpelerStuk('B');
                                return vak;
                            case 'r':
                                return new Start();
                            case 'g':
                                return new Start();
                            case 'y':
                                return new Start();
                            case 'b':
                                return new Start();
                        }   
                    }
                    break;
                case '(':
                    if (bord[y][x + 2] == ')')
                        return BepaalStuk(new Vak(), y, x);
                    break;
                case '{':
                    if (bord[y][x + 2] == '}')
                        return BepaalStuk(new Rust(), y, x);
                    break;
                case '[':
                    if (bord[y][x + 2] == ']')
                        return BepaalStuk(new Barricade(), y, x);
                    break;
                default:
                    break;
            }
            return null;
        }

        private Vak BepaalStuk(Vak v, int y, int x)
        {
            Vak vak = v;
            switch (bord[y][x + 1])
            {
                case ' ':
                    return vak;
                case '*':
                    vak.Stuk = new BarricadeStuk();
                    return vak;
                case 'R':
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'G':
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'Y':
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'B':
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
            }
            return vak;
        }

        public void KoppelVak()
        {
            for (int i = 0 ; i < bord.Count; i++)
            {
                for (int j = 0 ; j < bord[i].Length; j++)
                {
                    if (State[i, j] != null && State[i, j + 1] != null)         // controleren of de vakken die gekoppeld moeten worden niet leeg zijn
                    {
                        if ((j * 4) + 3 > 0 && (j * 4) + 3 < bord[i].Length)    // controleren of de waarden binnen de bounds blijft
                        {
                            if (bord[i][(j * 4) + 3] == '-')                    // controleren op horizontale link
                            {
                                State[i, j].oost = State[i, j + 1];
                                State[i, j + 1].west = State[i, j];
                                //Console.Write(i + " " + j + " " + State[i, j] + " --> ");
                            }
                        }
                    }
                    if (State[i, j] != null && State[i + 2, j] != null)          // controleren of vak onder huidige vak niet null is
                    {
                        if (((j * 4) + 1 > 0 && (j * 4) + 1 < bord[i].Length) && (i > 0 && i < bord.Count-3)) // controleren of de waarden binnen de bounds blijven
                        {
                            Console.WriteLine(bord[i][(j * 4) + 1]);    // print alle | characters
                            if (bord[i][(j * 4) + 2] == '|')                    // controleren op horizontale link
                            {
                                State[i, j].zuid = State[i + 2, j];
                                State[i + 2, j].noord = State[i, j];
                                Console.WriteLine("werkt");
                            }
                        }
                    }
                }
                Console.WriteLine();
            }
            
        }
    }
}
