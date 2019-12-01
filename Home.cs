using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklep
{
    public partial class Home : Form
    {
        SqlConnection connection;
        string connectionString;

        public Home()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;


        }


        private void Home_Load(object sender, EventArgs e)
        {
            panelLogo.BackgroundImage = imageListlogo.Images[0];
            panelWelcome.BringToFront();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panelWelcome_Paint(object sender, PaintEventArgs e)
        {

        }

        private void zarejestrujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelRejestracja.BringToFront();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void panelRejestracja_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 250, 225, 500, 225);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void zamknijProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void zalogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelLogin.BringToFront();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelEdytujSwojeDane_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 400, 100, 400, 350);
        }

        private void edytujSwojeDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelEdytujSwojeDane.BringToFront();
        }

        private void ądzajUżytkownikamiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
