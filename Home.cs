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
        string currentUsername;
        int[] Top3id = new int[3];


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

        private void zarejestrujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelRejestracja.BringToFront();
        }

        private void panelRejestracja_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 250, 225, 500, 225);
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

                    string querry3 = "dbo.LoginProcedure";
                    SqlCommand cmd3 = new SqlCommand(querry3, cnn3);

                    SqlParameter uName = new SqlParameter("@login", SqlDbType.VarChar);
                    SqlParameter uPassword = new SqlParameter("@password", SqlDbType.VarChar);

                    uName.Value = textBoxLogUzytkownik.Text;
                    uPassword.Value = textBoxLogHaslo.Text;
                    cmd3.CommandType = CommandType.StoredProcedure;
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
                        currentUsername = textBoxLogUzytkownik.Text;
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowy login lub hasło");
                    }
                    cnn3.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }


        private void panelEdytujSwojeDane_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
            e.Graphics.DrawLine(pen, 400, 100, 400, 350);
        }

        private void edytujSwojeDaneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelEdytujSwojeDane.BringToFront();
            string querry = "dbo.GetUserData";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@login", SqlDbType.NChar).Value = currentUsername;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr2 = cmd.ExecuteReader();

                    while (dr2.Read())
                    {
                        textBox10.Text = dr2.GetValue(0).ToString().TrimEnd();
                        textBox9.Text = dr2.GetValue(1).ToString().TrimEnd();
                        textBox1.Text = dr2.GetValue(2).ToString().TrimEnd();
                        textBox8.Text = dr2.GetValue(3).ToString().TrimEnd();
                        textBox7.Text = dr2.GetValue(4).ToString().TrimEnd();
                        textBox6.Text = dr2.GetValue(5).ToString().TrimEnd();
                    }
                    cnn4.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }

        private void ądzajUżytkownikamiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void buttonRejestracja_Click(object sender, EventArgs e)
        {
            string querry = "dbo.DodajUzytkownika";


            using (SqlConnection cnn = new SqlConnection(connectionString))
            {

                try
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(querry, cnn))
                    {


                        using (SqlConnection cnn2 = new SqlConnection(connectionString))
                        {

                            try
                            {
                                cnn2.Open();

                                string querry2 = "dbo.LoginTaken";
                                SqlCommand cmd2 = new SqlCommand(querry2, cnn2);
                                cmd2.CommandType = CommandType.StoredProcedure;
                                cmd2.Parameters.AddWithValue("@login", this.textBoxNazwaUzytkownika.Text);
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
                                MessageBox.Show("ERROR:" + ex.Message);
                            }
                        }
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@login", SqlDbType.NChar).Value = textBoxNazwaUzytkownika.Text;
                        cmd.Parameters.Add("@password", SqlDbType.NChar).Value = textBoxHaslo.Text;
                        cmd.Parameters.Add("@email", SqlDbType.NChar).Value = textBoxEmail.Text;

                        if (string.IsNullOrEmpty(textBoxImie.Text))
                            cmd.Parameters.Add("@imie", SqlDbType.NChar).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@imie", SqlDbType.NChar).Value = textBoxImie.Text;
                        }
                        if (string.IsNullOrEmpty(textBoxNazwisko.Text))
                            cmd.Parameters.Add("@nazwisko", SqlDbType.NChar).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@nazwisko", SqlDbType.NChar).Value = textBoxNazwisko.Text;
                        }
                        if (string.IsNullOrEmpty(textBoxKod.Text))
                            cmd.Parameters.Add("@zipcode", SqlDbType.NChar).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@zipcode", SqlDbType.NChar).Value = textBoxKod.Text;
                        }
                        if (string.IsNullOrEmpty(textBoxMiasto.Text))
                            cmd.Parameters.Add("@miasto", SqlDbType.NChar).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@miasto", SqlDbType.NChar).Value = textBoxMiasto.Text;
                        }
                        if (string.IsNullOrEmpty(textBoxUlica.Text))
                            cmd.Parameters.Add("@ulica", SqlDbType.NChar).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@ulica", SqlDbType.NChar).Value = textBoxUlica.Text;
                        }
                        if (string.IsNullOrEmpty(textBoxNrTelefonu.Text))
                            cmd.Parameters.Add("@telefon", SqlDbType.Int).Value = DBNull.Value;
                        else
                        {
                            cmd.Parameters.Add("@telefon", SqlDbType.Int).Value = Convert.ToInt32(textBoxNrTelefonu.Text);
                        }

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
            labelZalogowanyJako.ResetText();
            buttonZaloguj.Visible = true;
            wylogujToolStripMenuItem.Visible = false;
            edytujSwojeDaneToolStripMenuItem.Visible = false;
            MessageBox.Show("Do zobaczenia!");
            panelWelcome.BringToFront();
        }

        private void naszaOfertaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelNaszaOferta.BringToFront();
            wylistujProdukty();
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

        private void wyswDaneOProdukcie()
        {

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DisplaySpecificProduct", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = listBoxNOProdukty.SelectedValue;

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


        private void zobaczWszystkoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelNaszaOferta.BringToFront();
            wylistujProdukty();
        }

        private void tOP3ZamawianeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelNowosci.BringToFront();

            string querry = "Select * from Get3RecentProdID";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                int temp = 0;
                while (dr.Read())
                {
                    Top3id[temp] = dr.GetInt32(0);
                    temp++;
                }
            }
            Wyswietlprodukt(Top3id[0]);

        }

        private void Wyswietlprodukt(int id)
        {
            panelWyswProdukt.BringToFront();

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DisplaySpecificProduct", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))

            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = id;

                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                labelIloscSztuk.Text = "Pozostało: " + (double)tabela_prod.Rows[0]["ProductStock"] + " sztuk";
                labelOpisProd.Text = "Opis Produktu: " + (string)tabela_prod.Rows[0]["ProductDesc"];
                labelProducent.Text = "Producent: " + (string)tabela_prod.Rows[0]["Vendor"];
                labelCena.Text = "Cena: " + (decimal)tabela_prod.Rows[0]["ProductPrice"];
                labelNazwaProd.Text = "Nazwa produktu: " + (string)tabela_prod.Rows[0]["ProductName"];
                string temp = "Kategorie: | ";

                foreach (DataRow row in tabela_prod.Rows)
                {
                    temp += row["CategoryName"].ToString() + " | ";
                }

                labelKategorie.Text = temp;
            }
        }


        private void najpopularniejszyZOstatnich5ZakupówToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 idTop1 = 0;
            panelTop1.BringToFront();

            string querry = "Select * from Top1OutOfRecent5";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);
                connection.Open();

                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    idTop1 = dr.GetInt32(0);
                }
            }

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DisplaySpecificProduct", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int);
                command.Parameters["@id"].Value = idTop1;

                DataTable tabela_prod = new DataTable();
                adapter.Fill(tabela_prod);

                labelNOpisProd.Text = "Opis Produktu: " + (string)tabela_prod.Rows[0]["ProductDesc"];
                labelNProducent.Text = "Producent: " + (string)tabela_prod.Rows[0]["Vendor"];
                labelNCena.Text = "Cena: " + (decimal)tabela_prod.Rows[0]["ProductPrice"];
                labelNNazwaProd.Text = "Nazwa produktu: " + (string)tabela_prod.Rows[0]["ProductName"];
                string temp = "Kategorie: | ";

                foreach (DataRow row in tabela_prod.Rows)
                {
                    temp += row["CategoryName"].ToString() + " | ";
                }

                labelNKategorie.Text = temp;
            }
        }

        private void buttonN1_Click(object sender, EventArgs e)
        {
            Wyswietlprodukt(Top3id[0]);
        }

        private void buttonN2_Click(object sender, EventArgs e)
        {
            Wyswietlprodukt(Top3id[1]);
        }

        private void buttonN3_Click(object sender, EventArgs e)
        {
            Wyswietlprodukt(Top3id[2]);
        }

        private void buttonZmienDane_Click(object sender, EventArgs e)
        {
            string querry = "UPDATE Users SET FirstName=@imie, LastName=@nazwisko, ZipCode=@zipcode, City=@miasto, Street=@ulica, TelephoneNumber=@telefon WHERE Username=" + "'" + currentUsername + "'";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    if (string.IsNullOrEmpty(textBox10.Text))
                        cmd.Parameters.Add("@imie", SqlDbType.NChar).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@imie", SqlDbType.NChar).Value = textBox10.Text;
                    }
                    if (string.IsNullOrEmpty(textBox9.Text))
                        cmd.Parameters.Add("@nazwisko", SqlDbType.NChar).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@nazwisko", SqlDbType.NChar).Value = textBox9.Text;
                    }
                    if (string.IsNullOrEmpty(textBox1.Text))
                        cmd.Parameters.Add("@zipcode", SqlDbType.NChar).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@zipcode", SqlDbType.NChar).Value = textBox1.Text;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text))
                        cmd.Parameters.Add("@miasto", SqlDbType.NChar).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@miasto", SqlDbType.NChar).Value = textBox8.Text;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text))
                        cmd.Parameters.Add("@ulica", SqlDbType.NChar).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@ulica", SqlDbType.NChar).Value = textBox7.Text;
                    }
                    if (string.IsNullOrEmpty(textBox6.Text))
                        cmd.Parameters.Add("@telefon", SqlDbType.Int).Value = DBNull.Value;
                    else
                    {
                        cmd.Parameters.Add("@telefon", SqlDbType.Int).Value = Convert.ToInt32(textBox6.Text);
                    }

                    cmd.ExecuteNonQuery();
                    cnn4.Close();
                    MessageBox.Show("Dane zostały zmienione!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }

        private void ZmianaHasla_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn2 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn2.Open();

                    string querry2 = "dbo.VerifyPassword";
                    SqlCommand cmd2 = new SqlCommand(querry2, cnn2);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@login", currentUsername);
                    string value = cmd2.ExecuteScalar().ToString().TrimEnd();

                    if (aktualneHaslo.Text.Equals(value) && potwierdzHaslo.Text.Equals(value))
                    {
                        string query = "dbo.PasswordChange";
                        SqlCommand cmd3 = new SqlCommand(query, cnn2);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@login", SqlDbType.NChar).Value = currentUsername;
                        cmd3.Parameters.Add("@password", SqlDbType.NChar).Value = noweHaslo.Text;
                        cmd3.ExecuteNonQuery();
                        MessageBox.Show("Hasło zostało zmienione!");
                    }
                    else
                    {
                        MessageBox.Show("Podane hasło jest niepoprawne!");
                    }
                    cnn2.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }

        private void buttonUsunKonto_Click(object sender, EventArgs e)
        {
           var confirm = MessageBox.Show("Czy na pewno chcesz usunąć konto?", "Usuń konto",MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                using (SqlConnection cnn2 = new SqlConnection(connectionString))
                {
                    try
                    {
                        cnn2.Open();
                        string querry2 = "DELETE from [Users] where Username="+"'"+currentUsername+"'";
                        SqlCommand cmd2 = new SqlCommand(querry2, cnn2);
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Konto usunięte!");
                        currentUsername = null;
                        isUserAuthenticated = false;
                        labelZalogowanyJako.ResetText();
                        panelWelcome.BringToFront();
                        wylogujToolStripMenuItem.Visible = false;
                        edytujSwojeDaneToolStripMenuItem.Visible = false;
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR:" + ex.Message);
                    }
                }
            }
            else
            {
                //do nothing
            }
             
        }    
    }
}
