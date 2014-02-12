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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }                

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            SqlConnection con = new SqlConnection("Server=.; Database=productivity; uid=sa; pwd=12345");
            SqlCommand cmd = new SqlCommand("select * from users where username=@username and password=@password", con);
            
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            con.Open();

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                this.Hide();
                Record rec = new Record(dr[0].ToString());
                rec.Show();
            }
            else
            {
                MessageBox.Show("Lütfen doğru bilgilerle giriş yapmayı deneyiniz.");
                con.Close();
            }
          
        }
    }
}
