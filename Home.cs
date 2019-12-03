using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklep
{
    public partial class Home : Form
    {
        SqlConnection connection;
        string connectionString;
        bool isUsernameTaken;
        bool isUserAuthenticated = false;

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
        private void buttonZaloguj_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn3 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn3.Open();

                    string querry3 = "SELECT Username,Password FROM [Users] where Username = @Username AND Password = @Password";
                    SqlCommand cmd3 = new SqlCommand(querry3, cnn3);
                    SqlParameter uName = new SqlParameter("@Username", SqlDbType.VarChar);
                    SqlParameter uPassword = new SqlParameter("@Password", SqlDbType.VarChar);

                    uName.Value = textBoxLogUzytkownik.Text;
                    uPassword.Value = textBoxLogHaslo.Text;

                    cmd3.Parameters.Add(uName);
                    cmd3.Parameters.Add(uPassword);


                    SqlDataReader dr2 = cmd3.ExecuteReader();

                    if (dr2.Read() == true && isUserAuthenticated == false)
                    {
                        MessageBox.Show("Witaj ponownie " + textBoxLogUzytkownik.Text.ToString());
                        labelZalogowanyJako.Text = "Zalogowany jako: " + textBoxLogUzytkownik.Text;
                        isUserAuthenticated = true;
                        buttonZaloguj.Visible = false;
                        wylogujToolStripMenuItem.Visible = true;
                        edytujSwojeDaneToolStripMenuItem.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowy login lub hasło");
                    }
                    cnn3.Close();
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
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
        private void buttonRejestracja_Click(object sender, EventArgs e)
        {
            string querry = "INSERT INTO [Users] ([Username], [Password], [Email]) VALUES(@login, @pass, @email)";


            using (SqlConnection cnn = new SqlConnection(connectionString))
            {

                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(querry, cnn))
                    {

                        cmd.Parameters.Add("@login", SqlDbType.NChar).Value = textBoxNazwaUzytkownika.Text;
                        using (SqlConnection cnn2 = new SqlConnection(connectionString))
                        {

                            try
                            {
                                cnn2.Open();

                                string querry2 = "SELECT * from [Users] where Username= @Username";
                                SqlCommand cmd2 = new SqlCommand(querry2, cnn2);
                                cmd2.Parameters.AddWithValue("@Username", this.textBoxNazwaUzytkownika.Text);
                                SqlDataReader dr = cmd2.ExecuteReader();
                                dr.Read();

                                if (dr.HasRows == true)
                                {
                                    LoginValidation.Text = "Login jest zajęty!";
                                    isUsernameTaken = true;
                                }
                                else
                                {
                                    isUsernameTaken = false;
                                    LoginValidation.Text = " ";
                                }
                                cnn2.Close();
                            }
                            catch (Exception ex)
                            {
                                // We should log the error somewhere, 
                                // for this example let's just show a message
                                MessageBox.Show("ERROR:" + ex.Message);
                            }
                        }
                        cmd.Parameters.Add("@pass", SqlDbType.NChar).Value = textBoxHaslo.Text;
                        cmd.Parameters.Add("@email", SqlDbType.NChar).Value = textBoxEmail.Text;

                        string pattern = "^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$)";

                        
                        if (!string.IsNullOrWhiteSpace(textBoxNazwaUzytkownika.Text) && !string.IsNullOrWhiteSpace(textBoxHaslo.Text) && !string.IsNullOrWhiteSpace(textBoxEmail.Text) && Regex.IsMatch(textBoxEmail.Text, pattern) && isUsernameTaken == false && textBoxHaslo.Text == textHaslo2.Text)
                        {
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Twoje konto zostało założone. Witaj " + textBoxNazwaUzytkownika.Text.ToString());
                        }
                        else
                        {
                            MessageBox.Show("Wprowadzone dane są nieprawidłowe lub brakuje wymaganych informacji, spróbuj ponownie!");
                        }
                        cnn.Close();
                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }



        private void textBoxEmail_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^([a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$)";
            if (!Regex.IsMatch(textBoxEmail.Text, pattern))
            {
                EmailValidation.Text = "Adres email jest nieprawidłowy";
            }
            else
            {
                EmailValidation.Text = "Adres email jest poprawny ";
                return;
            }
        }

        private void textHaslo2_Validating(object sender, CancelEventArgs e)
        {
            if (textBoxHaslo.Text == textHaslo2.Text)
            {
                PasswordValidation.Text = "Hasła są zgodne :)";
            }
            else
            {
                PasswordValidation.Text = "Hasła muszą być identyczne!";
            }
        }

        private void wylogujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isUserAuthenticated = false;
            labelZalogowanyJako.Text = " ";
            buttonZaloguj.Visible = true;
            wylogujToolStripMenuItem.Visible = false;
            edytujSwojeDaneToolStripMenuItem.Visible = false;
            MessageBox.Show("Do zobaczenia!");
        }

        private void naszaOfertaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelNaszaOferta.BringToFront();
            wylistujProdukty();
        }

        private void ListBoxNOProdukty_Load(object sender, EventArgs e)
        {


        }


        private void panelNaszaOferta_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 220, 228, 415, 228);
        }

        private void listBoxNOProdukty_SelectedIndexChanged(object sender, EventArgs e)
        {
            wyswDaneOProdukcie();
        }

    

        private void wylistujProdukty()
        {
            string querry = "SELECT ID, ProductName FROM Products ";// + "INNER JOIN Vendors b ON b.ID = a.VendorID " + "WHERE a.VendorID = @VendorID";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                // command.Parameters.AddWithValue("@VendorID", listBoxNOProdukty.SelectedValue);


                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                listBoxNOProdukty.DisplayMember = "ProductName";
                listBoxNOProdukty.ValueMember = "ID";
                listBoxNOProdukty.DataSource = tabela_prod;

            }
        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void wyswDaneOProdukcie()
        {
            string querry = "SELECT " +
                            "dbo.Products.ProductName, dbo.Products.ProductDesc, dbo.Products.ProductPrice, dbo.Products.ProductImage, dbo.Products.ProductStock, dbo.Vendors.Vendor, dbo.Categories.CategoryName, dbo.Products.ID " +
                            "FROM " +
                            "dbo.Categories INNER JOIN " +
                            "dbo.ProdCat ON dbo.Categories.ID = dbo.ProdCat.IDCat INNER JOIN " +
                            "dbo.Products ON dbo.ProdCat.IDProd = dbo.Products.ID INNER JOIN " +
                            "dbo.Vendors ON dbo.Products.VendorID = dbo.Vendors.ID " +
                            "WHERE " +
                            "dbo.Products.ID = @id";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@id", listBoxNOProdukty.SelectedValue);


                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                labelNOopisProd.Text = "Opis Produktu: " + (string)tabela_prod.Rows[0]["ProductDesc"];
                labelNOProducent.Text = "Producent: " + (string)tabela_prod.Rows[0]["Vendor"];
                labelNOCena.Text = "Cena: " + (decimal)tabela_prod.Rows[0]["ProductPrice"];
                labelNOwMagazynie.Text = "Ilośc w magzynie: " + (double)tabela_prod.Rows[0]["ProductStock"];
                string temp = "Kategorie: | ";

                foreach (DataRow row in tabela_prod.Rows)
                {
                    temp += row["CategoryName"].ToString() + " | ";
                }

                labelNOkategorie.Text = temp;

                labelKupteraz.Text = "Wybierz ilość jaką chcesz kupić";
                textBoxNOIleKupic.Visible = true;
                buttonNOdodajDokoszyka.Visible = true;
            }
        }

        private void labelNOopisProd_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        //TODO 04.12 

        //private void buttonNOdodajDokoszyka_Click(object sender, EventArgs e)
        //{
        //    bool czystarczy = CzyWystarczajacoTowaru()
        //    //wczytaniie wartosci z pola
        //    //sprawdzenie czy nie jest wieksza niz stan agazynu
        //    // dodanie wartosci do tablicy globals.IDkupionych wraz  z id
        //}

        //private bool CzyWystarczajacoTowaru(double kupione)
        //{
        //    if (int.Parse(textBoxNOIleKupic.Text) != 0 || double.Parse(textBoxNOIleKupic.Text) <= kupione)
        //        return true;
        //    else
        //        return false;
        //}

        private void textBoxNOIleKupic_Leave(object sender, EventArgs e)
        {

        }

        private void textBoxNOIleKupic_TextChanged(object sender, EventArgs e)
        {
            string pattern = "^([1-9][0-9]+|[1-9])$";
            if (Regex.IsMatch(textBoxNOIleKupic.Text, pattern))
            {
                buttonNOdodajDokoszyka.Enabled = true;
            }
            else
            {
                buttonNOdodajDokoszyka.Enabled = false;
                MessageBox.Show("Wprowadź dane w postaci liczbowej", "Błąd",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void naszaOfertaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            panelNaszaOferta.BringToFront();
            wylistujProdukty();

        }
    }
}
