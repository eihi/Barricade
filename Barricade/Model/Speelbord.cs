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
        private int bordHeight { get; set; }
        private int bordWidth { get; set; }

        private string ihd, bm;

        public Speelbord(List<string> b)
        {
            // b opslaan in bord List
            bord = b;

            // Bepaal dimensies van het bord
            bordHeight = bord.Count;
            bordWidth = bord.OrderByDescending(s => s.Length).First().Length;

            // Maak SaveState 2d array van Vak objecten
            State = new Vak[bordHeight, bordWidth];
            
            for (int y = 0; y < bord.Count; y++)
            {
                for (int x = 0; x < bord[y].Length; x++)
                {
                    if (x % 4 == 0)
                        State[y,x/4] = BepaalVak(y, x);
                }
            }

            KoppelVak(); // Connecties maken
            SaveState();
        }

        private void SaveState()
        {
            string blok;

            for (int i = 0; i < bordHeight; i++)
            {
                for (int j = 0; j < bordWidth; j++)
                {
                    if (State[i, j] == null)
                    {
                        if (j == 0)
                        {
                            
                            int k = j;
                            while (k < bordWidth && State[i, k] == null)
                                k++;
                            if (k < bordWidth && State[i, k].InhetDorp == true)
                                ihd = "1";
                            else
                                ihd = "0";
                            if (k < bordWidth && State[i, k].BarricadeMag == true)
                                bm = "B";
                            else
                                bm = "-";
                            if (k < bordWidth && State[i, k].BarricadeMag == true && State[i, k].InhetDorp == true)
                                blok = ihd + bm + "  ";
                        }
                        else
                            blok = "    ";

                        if (i - 1 >= 0 && State[i - 1, j] != null && State[i - 1, j].zuid != null) // Eerste verticale connectie
                        {
                            blok = " |  ";
                        }
                        else if (i - 1 >= 0 && State[i - 1, j] == null) // Daarop volgende verticale connecties
                        {
                            if (j == 0)
                                blok = ihd + bm + "  ";
                            else
                                blok = "    ";

                            // Is erboven ergens een Vak met een zuid-connectie?
                            int t = 1;

                            while (i - t >= 0 && State[i - t, j] == null)
                            {
                                t++;
                            }

                            if (t > 1) // Is de while-loop een vak tegen gekomen?
                            {
                                if (i - t >= 0 && State[i - t, j] != null && State[i - t, j].zuid != null) // Is dit vak het begin van de connectie?
                                {
                                    blok = " |  ";
                                }
                            }
                        }
                        else
                        {
                            if (j == 0)
                                blok = ihd + bm + "  ";
                            else
                                blok = "    ";
                        }
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
                                    blok = BepaalStateStuk(State[i, j], "[ ]-");
                                else
                                    blok = BepaalStateStuk(State[i, j], "[ ] ");
                                break;
                            case "BarricadeSpel.Model.Rust":
                                if (State[i, j].oost != null)
                                    blok = BepaalStateStuk(State[i, j], "{ }-");
                                else
                                    blok = BepaalStateStuk(State[i, j], "{ } ");
                                break;
                            case "BarricadeSpel.Model.Vak":
                                if (State[i, j].oost != null)
                                    blok = BepaalStateStuk(State[i, j], "( )-");
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
                                vak = new Doel();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
                                return vak;
                            case '1':
                                vak = new Bos();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
                                return vak;
                            case 'R':
                                vak = new Start();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
                                vak.Stuk = new SpelerStuk('R');
                                return vak;
                            case 'G':
                                vak = new Start();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
                                vak.Stuk = new SpelerStuk('G');
                                return vak;
                            case 'Y':
                                vak = new Start();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
                                vak.Stuk = new SpelerStuk('Y');
                                return vak;
                            case 'B':
                                vak = new Start();
                                vak.BarricadeMag = BepaalBarricadeMag(y);
                                vak.InhetDorp = BepaalInHetDorp(y);
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

        private bool BepaalInHetDorp(int y)
        {
            if (bord[y][0] == '1')
                return true;
            return false;
        }

        private bool BepaalBarricadeMag(int y)
        {
            if (bord[y][1] == 'B')
                return true;
            return false;
        }

        private Vak BepaalStuk(Vak v, int y, int x)
        {
            Vak vak = v;
            switch (bord[y][x + 1])
            {
                case ' ':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    return vak;
                case '*':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    vak.Stuk = new BarricadeStuk();
                    return vak;
                case 'R':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'G':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'Y':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
                case 'B':
                    vak.BarricadeMag = BepaalBarricadeMag(y);
                    vak.InhetDorp = BepaalInHetDorp(y);
                    vak.Stuk = new SpelerStuk(bord[y][x + 1]);
                    return vak;
            }
            return vak;
        }

        private void KoppelVak() // Controleren op connecties
        {

            for (int i = 0 ; i < bordHeight; i++)
            {
                for (int j = 0 ; j < bord[i].Length; j++)
                {
                    // Horizontaal
                    if (State[i, j] != null && State[i, j + 1] != null)         // controleren of de vakken die gekoppeld moeten worden niet leeg zijn
                    {
                        if ((j * 4) + 3 > 0 && (j * 4) + 3 < bord[i].Length)    // controleren of de waarden binnen de bounds blijft
                        {
                            if (bord[i][(j * 4) + 3] == '-')                    // controleren op horizontale link
                            {
                                State[i, j].oost = State[i, j + 1];
                                State[i, j + 1].west = State[i, j];
                            }
                        }
                    }

                    // Verticaal
                    if (i >= 0 && i < bordHeight) // Verticale bounds
                    {
                        if ((j * 4) + 1 >= 0 && (j * 4) + 1 < bord[i].Length) // Horizontale bounds
                        {
                            // Is er een verticale connectie?
                            if ((i + 1) < bordHeight)
                            {
                                if (bord[i + 1][(j * 4) + 1] == '|') // controleren op verticale link
                                {
                                    if (State[i, j] != null && State[i + 2, j] != null)
                                    {
                                        State[i, j].zuid = State[i + 2, j];
                                        State[i + 2, j].noord = State[i, j];
                                        //Console.WriteLine(i + 1 + " " + ((j * 4) + 1) + "|");
                                    }
                                    else // Zijn er meerdere connecties (verlengd / bos)
                                    {
                                        int t = 1;

                                        do
                                        {
                                            t++;
                                        }
                                        while (i + t < bordHeight && bord[i + t][(j * 4) + 1] == '|');

                                        if (t > 2)
                                        {
                                            if (State[i + t, j].noord == null)
                                            {
                                                State[i, j].zuid = State[i + t, j];
                                                State[i + t, j].noord = State[i, j];
                                                //Console.WriteLine(i + t + " " + ((j * 4) + 1) + "|*");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }            


        }
    }
}
