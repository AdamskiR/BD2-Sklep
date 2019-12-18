﻿using System;
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

        public class coIile
        {
            public int id, ilosc;

            public override string ToString()
            {
                return "ID: " + this.id + ", Ilosc: " + this.ilosc;
            }

        }

        public List<coIile> koszyk = new List<coIile>();


        public Home()
        {
            InitializeComponent();
            isUserAuthenticatedView();
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

        private void isUserAuthenticatedView()
        {
            if (isUserAuthenticated == false)
            {
                labelZalogowanyJako.ResetText();
                buttonZaloguj.Visible = true;
                wylogujToolStripMenuItem.Visible = false;
                edytujSwojeDaneToolStripMenuItem.Visible = false;
                panelAdministracyjnyToolStripMenuItem.Visible = false;
                zalogujToolStripMenuItem.Visible = true;
                koszykToolStripMenuItem.Visible = false;
                twojeZamówieniaToolStripMenuItem.Visible = false;
                textBoxNOIleKupic.Visible = false;
                buttonNOdodajDokoszyka.Visible = false;
                zarejestrujToolStripMenuItem.Visible = true;

            }
            else
            {
                panelAdministracyjnyToolStripMenuItem.Visible = true;
                buttonZaloguj.Visible = false;
                zalogujToolStripMenuItem.Visible = false;
                wylogujToolStripMenuItem.Visible = true;
                edytujSwojeDaneToolStripMenuItem.Visible = true;
                twojeZamówieniaToolStripMenuItem.Visible = true;
                twojeZamówieniaToolStripMenuItem.Enabled = true;
                currentUsername = textBoxLogUzytkownik.Text;
                koszykToolStripMenuItem.Visible = true;
                twojeZamówieniaToolStripMenuItem.Visible = true;
                labelZalogowanyJako.Text = "Zalogowany jako: " + textBoxLogUzytkownik.Text;
                zarejestrujToolStripMenuItem.Visible = false;
                textBoxNOIleKupic.Visible = true;
                buttonNOdodajDokoszyka.Visible = true;
                labelKupteraz.Text = "Wybierz ilość jaką chcesz kupić";
            }
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
                        globals.GetUserID(textBoxLogUzytkownik.Text);
                        isUserAuthenticated = true;
                        isUserAuthenticatedView();
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
            panelEdytujUzytkownikow.BringToFront();
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
                            panelLogin.BringToFront();
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
            isUserAuthenticatedView();
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
            }
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
            connection.Close();
        }

        private void Wyswietlprodukt(int id)
        {
            panelWyswProdukt.BringToFront();

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("dbo.DisplaySpecificProduct", connection))
            using (SqlDataAdapter adapter1 = new SqlDataAdapter(command))

            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;

                DataTable tabela_prod = new DataTable();
                adapter1.Fill(tabela_prod);

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
            string querry = "dbo.UserDataUpdate";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@login", SqlDbType.NChar).Value = currentUsername;
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
                        string querry2 = "dbo.DeleteAccount";

                        SqlCommand cmd2 = new SqlCommand(querry2, cnn2);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@login", SqlDbType.NChar).Value = currentUsername;
                        cmd2.ExecuteNonQuery();
                        MessageBox.Show("Konto usunięte!");
                        currentUsername = null;
                        isUserAuthenticated = false;
                        isUserAuthenticatedView();
                        panelWelcome.BringToFront();
                        
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
        //Poprawic dodawanie tych samych elemnetów do listy
        private void buttonNOdodajDokoszyka_Click(object sender, EventArgs e)
        {
            {
                if (Convert.ToInt32(textBoxNOIleKupic.Text) > 0)
                {
                    //Dodanie do koszyka jako nowej pozycji, nawet gdy jest juz na liscie
                    //coIile kupno = new coIile();
                    //kupno.id = Convert.ToInt32(listBoxNOProdukty.SelectedValue);
                    //kupno.ilosc = Convert.ToInt32(textBoxNOIleKupic.Text);
                    //koszyk.Add(kupno);
                   
                    //Dodanie do koszyka z aktualizacja stanu gdy przedmiot jest już na liscie
                     coIile kupno = new coIile();
                     int ids = Convert.ToInt32(listBoxNOProdukty.SelectedValue);
                     int ilosc = Convert.ToInt32(textBoxNOIleKupic.Text);
                     List<coIile> result = koszyk.FindAll(x => x.id == ids);
                     if (result.Count() > 0)
                     {
                         for (int i = 0; i < koszyk.Count(); i++)
                         {
                             if (koszyk[i].id == ids)
                             {
                                 koszyk[i].ilosc += ilosc;
                             }

                         }

                     } 
                     else
                     {
                         kupno.id = ids;
                         kupno.ilosc = ilosc;
                         koszyk.Add(kupno);
                     }
                     result.Clear();
                     MessageBox.Show("Dodano do koszyka");
                }
                else
                  MessageBox.Show("Musisz wprowadzić ilość!");
            }
        }

        private void usuńProduktyZKoszykaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            koszyk.Clear();
            sprawdzKoszyk();
            panelPrzejdzDoKasy.Refresh();
        }

        private void twojeZamówieniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelTwojeZamowienia.BringToFront();

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("ListUserOrders", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@userID", SqlDbType.Int);
                command.Parameters["@userID"].Value = globals.userID;                       //Do zmiany

                DataTable zam_uz = new DataTable();
                adapter.Fill(zam_uz);
                
                dataGridViewTZZamowienia.AutoGenerateColumns = false;
                dataGridViewTZZamowienia.DataSource = zam_uz;
            }

        }


        private void dataGridViewTZZamowienia_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                int orderno = 0;
                float totprice = 1;
                Int32.TryParse(dataGridViewTZZamowienia.Rows[e.RowIndex].Cells[0].Value.ToString(), out orderno);
                float.TryParse(dataGridViewTZZamowienia.Rows[e.RowIndex].Cells[1].Value.ToString(), out totprice);
                labelTZCenaZaWszystko.Text = "Cena za całość: " + totprice.ToString();

                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("DisplayOrder", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@orderID", SqlDbType.Int);
                    command.Parameters["@orderID"].Value = orderno;

                    DataTable konkretneZamowienie = new DataTable();
                    adapter.Fill(konkretneZamowienie);

                    dataGridViewOrder.AutoGenerateColumns = false;
                    dataGridViewOrder.DataSource = konkretneZamowienie;
                }
            }
            catch
            {
            }
        }
        private void przejdźDoKasyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sprawdzKoszyk();
            dataGridViewWKoszyku.AutoGenerateColumns = false;

            DataTable produktywkoszyku = new DataTable();
            if (koszyk.Count() > 0)
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("DisplaySPecificProductWOCategories", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@id", SqlDbType.Int);

                    for (int i = 0; i < koszyk.Count(); i++)
                    {
                        command.Parameters["@id"].Value = koszyk[i].id;
                        adapter.Fill(produktywkoszyku);
                    }
                    dataGridViewWKoszyku.DataSource = produktywkoszyku;
                    
                }
            }
            DGVwKoszykuAktualizuj();
            panelPrzejdzDoKasy.BringToFront();

            
        }
        //jebie sie
        private void DGVwKoszykuAktualizuj()
        {
            try
            {
                float ileZaZakupy = 0;
                for (int i = 0; i < koszyk.Count(); i++)
                {
                    dataGridViewWKoszyku.Rows[i].Cells[3].Value = koszyk[i].ilosc;
                    float temp;
                    float.TryParse(dataGridViewWKoszyku.Rows[i].Cells[2].Value.ToString(), out temp);
                    ileZaZakupy += temp * koszyk[i].ilosc;
                    dataGridViewWKoszyku.Rows[i].Cells[4].Value = koszyk[i].ilosc * temp;
                    labelPDKsumaZaZakupy.Text = "Cena za wszystko: " + ileZaZakupy.ToString() + " zł";
                }
            }
            catch
            {
                //Z pewnych nieznanych przyczyn podczas edycji ilości w koszyku i nie "odkliknieciu" pola, chcac dodac nową rzecz do koszyka wyskakuje błąd System.ArgumentOutOfRangeException 
                Console.WriteLine("ERROR");
            }

        }

        private void sprawdzKoszyk()
        {
            if (koszyk.Count() == 0)
            {
                labelPDKInformacja.Visible = false;
                labelPDKsumaZaZakupy.Visible = false;
                labelBrakWKoszyku.Visible = true;
                buttonPDKKup.Visible = false;
                labelOTK.Visible = false;
                dataGridViewWKoszyku.Visible = false;
            }
            else
            {
                labelPDKInformacja.Visible = true;
                labelPDKsumaZaZakupy.Visible = true;
                labelBrakWKoszyku.Visible = false;
                labelOTK.Visible = true;
                buttonPDKKup.Visible = true;
                dataGridViewWKoszyku.Visible = true;
            }
        }


        private void buttonPDKKup_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet");
        }


        private void dataGridViewWKoszyku_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if(e.Cell.ColumnIndex == 3)
            {
                if (e.Cell.Value != null)
                {
                    int.TryParse(e.Cell.Value.ToString(), out int temp);

                    // int.TryParse(dataGridViewWKoszyku.Rows[e.Cell. .Value.ToString(), out temp);

                    koszyk[e.Cell.RowIndex].ilosc = temp;
                    DGVwKoszykuAktualizuj();
                }
            }
        }


        private void wyszukajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not implemented yet");
        }

        private void dodajProduktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelDodajProdukt.BringToFront();
        }

        private void buttonDodajProd_Click(object sender, EventArgs e)
        {
            string querry = "INSERT INTO Products(ProductName, ProductDesc, ProductPrice, ProductStock, ProductCategoryID, VendorID) Values(@nazwa, @opis,@cena,@ilosc,@kategoria,@vendor)";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@nazwa", SqlDbType.NChar).Value = DodajNazwa.Text;
                    cmd.Parameters.Add("@opis", SqlDbType.NChar).Value = DodajOpis.Text;
                    cmd.Parameters.Add("@cena", SqlDbType.Decimal).Value = Convert.ToDecimal(DodajCena.Text);
                    cmd.Parameters.Add("@ilosc", SqlDbType.Float).Value = Convert.ToDouble(DodajIlosc.Text);
                    cmd.Parameters.Add("@kategoria", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@vendor", SqlDbType.Int).Value = 1;
                    cmd.ExecuteNonQuery();
                    cnn4.Close();
                    MessageBox.Show("Produkt dodany!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }
        private void modyfikujProduktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelModyfikujProdukt.BringToFront();
        }


       
    }
    
}
