﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BarricadeSpel.Model;
using BarricadeSpel.Controller;

namespace BarricadeSpel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Spel Spel;
        public MainController mc = new MainController();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NieuwSpel(object sender, RoutedEventArgs e)
        {
            Spel = new Spel(mc.FileReader(mc.OpenFile(".bord", "Barricade bord (.bord)|*.bord", "\\Barricade")));
        }

        private void LaadSpel(object sender, RoutedEventArgs e)
        {
            Spel = new Spel(mc.FileReader(mc.OpenFile(".save", "Barricade save (.save)|*.save", "\\Savestates"))); // werkt niet opent borden folder ipv Savestates :(
        }
    }
}
