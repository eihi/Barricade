﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarricadeSpel.Model
{
    class Start : Vak
    {
        public Start()
        {
            LoopVak = false; // staat standaard op true in superklasse dus moet op false gezet worden
        }
    }
}
