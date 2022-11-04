﻿using Mikulas.Abstraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas.Entities
{
    public class Ball : Toy
    {
        
        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }

        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
        }

        public SolidBrush BallColor { get; private set; }

    }
}
