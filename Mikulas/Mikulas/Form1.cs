using Mikulas.Abstraction;
using Mikulas.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Mikulas
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();
        private IToyFactory _factory;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }


        public Form1()
        {
            InitializeComponent();
            Factory = new BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var t = Factory.CreateNew();
            _toys.Add(t);
            t.Left = t.Width * (-1);
            //_balls.Add(b);
            mainPanel.Controls.Add(t);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var t in _toys)
            {
                t.MoveToy();
                if (t.Left > maxPosition)
                {
                    maxPosition = t.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                var oldest = _toys[0];
                mainPanel.Controls.Remove(oldest);
                _toys.Remove(oldest);
            }
        }
    }
}
