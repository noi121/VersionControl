﻿using Mikulas.Abstraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikulas.Entities
{
    public class BallFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }

        public Color BallColor { get; set; }
        public BallFactory()
        {

        }   
    }
}
