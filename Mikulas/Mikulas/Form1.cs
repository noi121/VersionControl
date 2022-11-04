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
        private Toy next;

        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value;
                DisplayNext();
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Factory = new BallFactory()
            {
                BallColor = button3.BackColor
            };
        }

        private void DisplayNext()
        {
            if (next != null)
            {
                Controls.Remove(next);
            }
            next = Factory.CreateNew();
            next.Left = label1.Left;
            next.Top = label1.Top + 20;
            Controls.Add(next);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var picker = new ColorDialog();
            picker.Color = button.BackColor;

            if (picker.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            button.BackColor = picker.Color;
        }
    }
}
