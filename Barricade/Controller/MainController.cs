using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace BarricadeSpel.Controller
{
    public class MainController
    {
        public MainController()
        {
            
        }

        public string OpenFile()
        {
            // Vraag om een spelbestand
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".bord";
            dialog.Filter = "Barricade bord (.bord)|*.bord";
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Barricade";
            if (dialog.ShowDialog() != true)
                Environment.Exit(0);

            return dialog.FileName;
        }

        public List<string> FileReader(string file)
        {
            // Stap 1: lees alle regels
            StreamReader f = new StreamReader(file);
            List<string> regels = new List<string>();
            while (!f.EndOfStream)
            {
                string regel = f.ReadLine().TrimEnd();
                if (regel == "" || regel[0] == ';') continue;
                regels.Add(regel);
            }

            return regels;
        }
    }
}
