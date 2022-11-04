using Mikulas.Abstraction;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikulas.Entities
{
    class Present : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            
            g.FillRectangle(BoxColor, 0, 0, Width, Height);
            g.FillRectangle(RibbonColor, 20, 0, Width / 5, Height);
            g.FillRectangle(RibbonColor, 0, 20, Width, Height / 5);
        }

        public Present(Color ribbon, Color box)
        {
            RibbonColor = new SolidBrush(ribbon);
            BoxColor = new SolidBrush(box);
        }

        public SolidBrush RibbonColor { get; private set; }
        public SolidBrush BoxColor { get; private set; }
    }
}
