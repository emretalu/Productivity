using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Productivity
{
    public partial class Record : Form
    {
        int time = 0;
        int saniye = 0;
        int saat = 0;
        int dakika = 0;
        int total = 0;
        public string name;
        private string uid;
        private string wtime;
        private int insert_id;        
        PictureBox clickedPictureBox;

        public Record()
        {
            InitializeComponent();
        }

        public Record(string id)
        {
            InitializeComponent();
            uid = id;
        }

        private void Record_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1;            
        }
        
        private void btnPlay_Click(object sender, EventArgs e)
        {            
            clickedPictureBox = sender as PictureBox;
            
            this.btnPlay.Image = imgList1.Images["play2.png"];
            this.btnPause.Image = imgList1.Images["pause.png"];
            this.btnStop.Image = imgList1.Images["stop.png"];

            SqlConnection con = new SqlConnection("Server=.; Database=productivity; uid=sa; pwd=12345");
            SqlCommand com = new SqlCommand();

            if (time == 0)
            {
                timer1.Start();
                com.CommandText = "insert into records(startTime,finishTime,uId,total) OUTPUT Inserted.recordId values(@stime,@ftime,@id,@ttime)";
                com.Connection = con;

                com.Parameters.AddWithValue("@id", Convert.ToInt32(uid));
                com.Parameters.AddWithValue("@stime", DateTime.Now);
                com.Parameters.AddWithValue("@ftime", DateTime.Now);
                com.Parameters.AddWithValue("@ttime", 0);

                con.Open();
                insert_id = Convert.ToInt32(com.ExecuteScalar());
            }
            else
            {
                this.timer1.Stop();
                this.timer1.Start();

                com.CommandText = "update records set finishTime=@ftimea, total=@ttimea where uId=@ida and recordId='" + insert_id + "'";
                com.Connection = con;

                com.Parameters.AddWithValue("@ida", Convert.ToInt32(uid));
                com.Parameters.AddWithValue("@ftimea", DateTime.Now);
                com.Parameters.AddWithValue("@ttimea", total);

                con.Open();
            }
            
            con.Close();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            clickedPictureBox = sender as PictureBox;

            this.btnPlay.Image = imgList1.Images["play.png"];
            this.btnPause.Image = imgList1.Images["pause2.png"];
            this.btnStop.Image = imgList1.Images["stop.png"];

            SqlConnection con = new SqlConnection("Server=.; Database=productivity; uid=sa; pwd=12345");
            SqlCommand com = new SqlCommand();
            com.CommandText = "update records set finishTime=@ftime2, total=@ttime2 where uId=@id2 and recordId='" + insert_id + "'";
            com.Connection = con;

            com.Parameters.AddWithValue("@id2", Convert.ToInt32(uid));
            com.Parameters.AddWithValue("@ftime2", DateTime.Now);
            com.Parameters.AddWithValue("@ttime2", total);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            clickedPictureBox = sender as PictureBox;

            this.btnPlay.Image = imgList1.Images["play.png"];
            this.btnPause.Image = imgList1.Images["pause.png"];
            this.btnStop.Image = imgList1.Images["stop2.png"];

            SqlConnection con = new SqlConnection("Server=.; Database=productivity; uid=sa; pwd=12345");
            SqlCommand com = new SqlCommand();
            com.CommandText = "update records set workingTime=@wtime3, finishTime=@ftime3, total=@ttime3 where uId=@id3 and recordId='" + insert_id + "'";
            com.Connection = con;

            com.Parameters.AddWithValue("@id3", Convert.ToInt32(uid));
            com.Parameters.AddWithValue("@wtime3", wtime);
            com.Parameters.AddWithValue("@ftime3", DateTime.Now);
            com.Parameters.AddWithValue("@ttime3", total);

            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            time = 0;
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            saniye = (time/10) % 60;
            dakika = (time / 600) % 60;
            saat = time / 36000;
            total = saniye + (dakika * 60) + (saat * 60 * 60);

            wtime = saat.ToString() + ":" + dakika.ToString() + ":" + saniye.ToString();
            txtTime.Text = wtime;
        }

        private void Record_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void istatistikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics stat = new Statistics();
            stat.Show();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About abt = new About();
            abt.Show();
        }
    }
}
