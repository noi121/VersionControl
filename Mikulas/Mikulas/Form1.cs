using Mikulas.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikulas
{
    public partial class Form1 : Form
    {
        private List<Ball> _balls = new List<Ball>();
        private BallFactory _factory;

        public BallFactory Factory
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
            var b = Factory.CreateNew();
            _balls.Add(b);
            b.Left = b.Width * (-1);
            //_balls.Add(b);
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var maxPosition = 0;
            foreach (var b in _balls)
            {
                b.MoveBall();
                if (b.Left > maxPosition)
                {
                    maxPosition = b.Left;
                }
            }

            if (maxPosition >= 1000)
            {
                var oldest = _balls[0];
                mainPanel.Controls.Remove(oldest);
                _balls.Remove(oldest);
            }
        }
    }
}
