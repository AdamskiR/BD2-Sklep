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
    public partial class Register : Form
    {
        SqlConnection connection;
        string connectionString;
        public Register()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttRegister_Click(object sender, EventArgs e)
        {
            Zarejestruj();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            wypiszuserow();
        }

        private void wypiszuserow()
        {
            
                using (connection = new SqlConnection(connectionString))
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Users", connection))
                {

                    DataTable tabela_ven = new DataTable();
                    adapter.Fill(tabela_ven);

                    listBox1.DisplayMember = "Username";
                    listBox1.ValueMember = "ID";
                    listBox1.DataSource = tabela_ven;
                                               
            }
            
        }

        void Zarejestruj()
        {



            string querry = "INSERT INTO [Users] ([Username], [Password], [Email]) VALUES(@login, @pass, @email)";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {

                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    cnn.Open();

                    // Prepare the command to be executed on the db
                    using (SqlCommand cmd = new SqlCommand(querry, cnn))
                    {
                        // Create and set the parameters values 
                        cmd.Parameters.Add("@login", SqlDbType.NChar).Value = textBoxlogin.Text;
                        cmd.Parameters.Add("@pass", SqlDbType.NChar).Value = textBoxHaslo.Text;
                        cmd.Parameters.Add("@email", SqlDbType.NChar).Value = textBoxEmail.Text;

                        // Let's ask the db to execute the query
                        int rowsAdded = cmd.ExecuteNonQuery();
                        if (rowsAdded >= 3)
                        {
                            MessageBox.Show("Twoje konto zostało założone. Witaj " + textBoxlogin.Text.ToString());
                            wypiszuserow();
                            clear();
                        }
                        else
                            // Well this should never really happen
                            MessageBox.Show("Wprowadzone dane są nieprawidłowe lub brakuje wymaganych informacji, spróbuj ponownie!");

                       
                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }

           
            

            //using (connection = new SqlConnection(connectionString))
            //using (SqlCommand command = new SqlCommand(querry, connection))
            //using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            //{

            //        command.Parameters.AddWithValue("@username", textBoxlogin.Text.Trim());
            //        command.Parameters.AddWithValue("@password", textBoxHaslo.Text.Trim());
            //        command.Parameters.AddWithValue("@email", textBoxEmail.Text.Trim());

            //        connection.Open();

            //        command.ExecuteNonQuery();
            //        int result = command.ExecuteNonQuery();
            //        connection.Close();


            //        MessageBox.Show(result.ToString());
            //    clear();

            //}
        }

        void clear()
        {
            textBoxlogin.Text = textBoxHaslo.Text = textBoxEmail.Text = "";

        }

  
    }

}
