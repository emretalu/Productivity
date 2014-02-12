using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace Productivity
{
    public partial class Statistics : Form
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void Statistics_Load(object sender, EventArgs e)
        {
            //select * from records where startTime between '2013-09-27' and '2013-09-28' and finishTime between '2013-09-27' and '2013-09-28'
            
            SqlConnection con = new SqlConnection("Server=.; Database=productivity; uid=sa; pwd=12345");           

            for (int i = 6; i >= 0; i--)
            {
                int dd = 0;
                int total = 0;
                
                string baslangic = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");
                string bitis = DateTime.Now.AddDays(-i+1).ToString("yyyy-MM-dd");
                string[] day = baslangic.ToString().Split('-');
                dd = Convert.ToInt32(day[2]);

                SqlCommand cmd = new SqlCommand("select * from records where startTime between '" + baslangic + "' and '" + bitis + "' and finishTime between '" + baslangic + "' and '" + bitis + "'", con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {                    
                    total += Convert.ToInt32(dr[5].ToString());
                }
                chart1.Series["Çalışma Süresi"].Points.AddXY(dd, total);
                chart1.Series["Çalışma Süresi"].ChartType = SeriesChartType.Spline;
                chart1.Series["Çalışma Süresi"].Color = Color.Red;
                con.Close();                
            }                        
        }
    }
}
