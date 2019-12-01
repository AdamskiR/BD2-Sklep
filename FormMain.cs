using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using IdentityModel.Client;
using System.Web;

namespace Sklep
{
 

    public partial class FormMain : Form
    {
        SqlConnection connection;
        string connectionString;
        public FormMain()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            producenci();

        }

        private void producenci()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Vendors", connection))
            {

                DataTable tabela_ven = new DataTable();
                adapter.Fill(tabela_ven);

                listProducts.DisplayMember = "Vendor";
                listProducts.ValueMember = "ID";
                listProducts.DataSource = tabela_ven;

                listProducts.DisplayMember = "Vendor";
                listProducts.ValueMember = "ID";
                listProducts.DataSource = tabela_ven;


            }

        }

        private void produkty()
        {
            string querry = "SELECT a.ProductName FROM Products a " + "INNER JOIN Vendors b ON b.ID = a.VendorID " + "WHERE a.VendorID = @VendorID";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@VendorID", listProducts.SelectedValue);


                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                listVendors1.DisplayMember = "ProductName";
                listVendors1.ValueMember = "ID";
                listVendors1.DataSource = tabela_prod;


            }

        }

        private void listProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(listProducts.SelectedValue.ToString());
            produkty();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttRejestracja_Click(object sender, EventArgs e)
        {
            Register registerform = new Register();
            registerform.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void buttLogin_Click(object sender, EventArgs e)
        {
            string querry = "SELECT Username, Password From Users Where Username = @login AND Password = @passwd";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                     
                        command.Parameters.Add("@login", SqlDbType.NChar).Value = textBoxlogin.Text;
                        command.Parameters.Add("@passwd", SqlDbType.NChar).Value = textBoxhaslo.Text;
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                                       
                        if (dt.Rows.Count == 1)
                        {
                            MessageBox.Show("Sukces. Zalogowano jako: " + textBoxlogin.Text.ToString());
                            //labelUsername.Text = textBoxlogin.Text;
                            globals.setLogin(GetUserID(textBoxlogin.Text), labelUsername);
                            clear();
                        }
                        else
                            MessageBox.Show("Porażka: Nie można znaleźć takiego użytkownika");


                        

            }
                }
             
  

        

        void clear()
        {
            textBoxlogin.Text = textBoxhaslo.Text = "";
        }



        int GetUserID(string user)
        {
            string querry = "SELECT ID From Users WHERE Username = @user";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            {
                command.Parameters.Add("@user", SqlDbType.NChar).Value = user;
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                               
                int ID;
                                             
                    while (dr.Read())
                    {
                        ID = dr.GetInt32(0);
                        //display retrieved record
                        Console.WriteLine("{0}", ID.ToString());
                        globals.userID = ID;
                        buttonWyloguj.Visible = true;
                        buttonIdzDoSklepu.Enabled = true;
                    return dr.GetInt32(0);
                    }
                return -1;
            }
       }



        private void buttPrzejdzDoOferty_Click(object sender, EventArgs e)
        {
            Alledrogo alledrogo = new Alledrogo();
            alledrogo.Show();
        }

        private void buttwyloguj_Click(object sender, EventArgs e)
        {
            globals.userID = default;
            buttonWyloguj.Visible = false;
            labelUsername.Text = "";
            buttonIdzDoSklepu.Enabled = false;
        }


    }
}