using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace week04
{
    public partial class Form1 : Form
    {
        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp;
        Excel.Workbook xlWb;
        Excel.Worksheet xlWs;
        public Form1()
        {
                       
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();   //Excel elindítása és az applikáció objektum betöltése
                xlWb = xlApp.Workbooks.Add(Missing.Value);   //új munkafüzet
                xlWs = xlWb.ActiveSheet;   //új munkalap
                //CreateTable();   //új tábla létrehozása

                xlApp.Visible = true;
                xlApp.UserControl = true;   //control átadása a felhasználónak

            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                //hiba esetén az excel bezárása automatikusan
                xlWb.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWb = null;
                xlApp = null;
            }
        }
    }
}
