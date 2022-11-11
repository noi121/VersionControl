using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VAR.Entities;

namespace VAR
{
    public partial class Form1 : Form
    {
        PortfolioEntities context = new PortfolioEntities();
        List<Tick> Ticks;

        List<PortfolioItem> Portfolio = new List<PortfolioItem>();
        List<decimal> Rendezve = new List<decimal>();
        public Form1()
        {
            InitializeComponent();
            Ticks = context.Tick.ToList();
            dataGridView1.DataSource = Ticks;
            CreatePortfolio();

            List<decimal> Nyereségek = new List<decimal>();
            int intervallum = 30;
            DateTime kezdoDatum = (from x in Ticks select x.TradingDay).Min();
            DateTime vegDatum = new DateTime(2016, 12, 30);
            TimeSpan z = vegDatum - kezdoDatum;
            for (int i = 0; i < z.Days - intervallum; i++)
            {
                decimal ny = GetPortfolioValue(kezdoDatum.AddDays(i + intervallum)) - GetPortfolioValue(kezdoDatum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + " " + ny);
            }
            var nyeresegRendezve = (from x in Nyereségek orderby x select x).ToList();
            Rendezve = nyeresegRendezve;
            MessageBox.Show(nyeresegRendezve[nyeresegRendezve.Count() / 5].ToString());   
        }

        private void CreatePortfolio()
        {
            Portfolio.Add(new PortfolioItem() { Index = "OTP", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolio.Add(new PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolio;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var p in Portfolio)
            {
                var last = (from x in Ticks where p.Index == x.Index.Trim() && date <= x.TradingDay select x).First();
                value += (decimal)last.Price * p.Volume;
            }
            return value;
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                sw.WriteLine("Időszak" + ";" + "Nyereség");
                int counter = 1;
                foreach (var n in Rendezve)
                {
                    sw.WriteLine(counter + ";" + n.ToString());
                    counter++;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFile();
        }
    }
}
