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
                var dataSet = new DataSet();
                bool adminUser=false;
                bool managerUser = false;
                string querry2 = "SELECT RoleID FROM UserRole WHERE UserID='" + globals.userID + "'";
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString);
            SqlCommand cmd2 = new SqlCommand(querry2, cnn);
            cnn.Open();
            var dataAdapter = new SqlDataAdapter { SelectCommand = cmd2 };
                dataAdapter.Fill(dataSet);


                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(dataSet.Tables[0].Rows[0]["RoleID"]) == 1)
                    {
                    adminUser = true;
                    }
                    else if(Convert.ToInt32(dataSet.Tables[0].Rows[0]["RoleID"]) == 2)
                    {
                    managerUser = true;
                    }
                }
                else
                {
                adminUser = false;
                managerUser = false;
                }
            cnn.Close();

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
                if (adminUser == true)
                {
                    panelAdministracyjnyToolStripMenuItem.Visible = true;
                }
                if(managerUser == true)
                {
                    panelAdministracyjnyToolStripMenuItem.Visible = true;
                    ądzajUżytkownikamiToolStripMenuItem.Enabled = false;
                }
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
            wylistujUserow();
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
                listBoxNOProducts2.DisplayMember = "ProductName";
                listBoxNOProducts2.ValueMember = "ID";
                listBoxNOProducts2.DataSource = tabela_prod;

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
            var confirm = MessageBox.Show("Czy na pewno chcesz usunąć konto?", "Usuń konto", MessageBoxButtons.YesNo);
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

        private void buttonNOdodajDokoszyka_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    var dataSet = new DataSet();
                    string querry2 = "SELECT ProductStock FROM Products WHERE ID=" + "'" + listBoxNOProdukty.SelectedValue + "'";
                    SqlConnection cnn = new SqlConnection(connectionString);
                    cnn.Open();
                    SqlCommand cmd2 = new SqlCommand(querry2, cnn);
                    var dataAdapter = new SqlDataAdapter { SelectCommand = cmd2 };
                    dataAdapter.Fill(dataSet);
                    coIile kupno = new coIile();
                    int ids = Convert.ToInt32(listBoxNOProdukty.SelectedValue);
                    int ilosc = Convert.ToInt32(textBoxNOIleKupic.Text);
                    List<coIile> result = koszyk.FindAll(x => x.id == ids);

                    if (Convert.ToInt32(textBoxNOIleKupic.Text) > 0)
                    {
                        //Dodanie do koszyka jako nowej pozycji, nawet gdy jest juz na liscie
                        //coIile kupno = new coIile();
                        //kupno.id = Convert.ToInt32(listBoxNOProdukty.SelectedValue);
                        //kupno.ilosc = Convert.ToInt32(textBoxNOIleKupic.Text);
                        //koszyk.Add(kupno);

                        //Dodanie do koszyka z aktualizacja stanu gdy przedmiot jest już na liscie

                        if (Convert.ToInt32(dataSet.Tables[0].Rows[0]["ProductStock"]) >= Convert.ToInt32(textBoxNOIleKupic.Text))
                        {
                            if (result.Count() > 0)
                            {
                                for (int i = 0; i < koszyk.Count(); i++)
                                {
                                    if (koszyk[i].id == ids)
                                    {
                                        if (Convert.ToInt32(koszyk[i].ilosc + ilosc) <= Convert.ToInt32(dataSet.Tables[0].Rows[0]["ProductStock"]))
                                        {
                                            koszyk[i].ilosc += ilosc;
                                            MessageBox.Show("Dodano do koszyka");
                                        }
                                        else
                                        {
                                            MessageBox.Show("Suma towarów z koszyka i tego zamówienia przekracza ilość towaru w magazynie!");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                kupno.id = ids;
                                kupno.ilosc = ilosc;
                                koszyk.Add(kupno);
                                result.Clear();
                                MessageBox.Show("Dodano do koszyka");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Brak takiej ilości towaru w magazynie!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Musisz wprowadzić ilość!");
                    }
                    cnn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
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
            aktualizujZamowienia();
        }

        private void aktualizujZamowienia()
        {
            panelTwojeZamowienia.BringToFront();

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("ListUserOrders", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@userID", SqlDbType.Int);
                command.Parameters["@userID"].Value = globals.userID;

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

        private bool sprawdzstan()
        {
            bool zgadzasie = true;
            string message = "BŁĄD\n\nBrak żądanej ilości produktu na stanie, musisz zmienić ilość\n\nNazwa:\t\t\tMaMaxymalna możliwa ilość do kupienia: ";
            double por;
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("DisplaySpecificProduct", connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@id", SqlDbType.Int);
                DataTable spr_stan = new DataTable();

                for (int i = 0; i<koszyk.Count(); i++)
                {
                    command.Parameters["@id"].Value = koszyk[i].id;
                    adapter.Fill(spr_stan);
                    por = (double)spr_stan.Rows[i]["ProductStock"];
                    if (por < koszyk[i].ilosc)
                    {
                        zgadzasie = false;
                        message += "\n" + (string)spr_stan.Rows[0]["ProductName"] + "\t\t\t" + (double)spr_stan.Rows[0]["ProductStock"];
                    }
                }
            }
            if (!zgadzasie)
            {
                MessageBox.Show(message);
                return zgadzasie;
            } else return zgadzasie;
        }

        private void buttonPDKKup_Click(object sender, EventArgs e)
        {
            if(sprawdzstan())
            {
                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("DodajZamowienie", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    connection.Open();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@userID", SqlDbType.Int);
                    command.Parameters["@userID"].Value = globals.userID;
                    SqlDataReader reader = command.ExecuteReader();
                }

                using (connection = new SqlConnection(connectionString))
                using (SqlCommand command2 = new SqlCommand("DodajSzczegolyZamowienia", connection))
                using (SqlDataAdapter adapter2 = new SqlDataAdapter(command2))
                {
                    connection.Open();
                    command2.CommandType = CommandType.StoredProcedure;
                    command2.Parameters.Add("@user_id", SqlDbType.Int);
                    command2.Parameters.Add("@prod_id", SqlDbType.Int);
                    command2.Parameters.Add("@amount", SqlDbType.Int);
                    command2.Parameters["@user_id"].Value = globals.userID;

                    for (int i = 0; i < koszyk.Count(); i++)
                    {
                        command2.Parameters["@prod_id"].Value = koszyk[i].id;
                        command2.Parameters["@amount"].Value = koszyk[i].ilosc;
                        command2.ExecuteNonQuery();
                    }
                    MessageBox.Show("Gratulacje!\nTwoje zamówienie zostało złożone");
                    aktualizujZamowienia();
                    panelTwojeZamowienia.BringToFront();
                    koszyk.Clear();
                }
            }
        }


        private void dataGridViewWKoszyku_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell.ColumnIndex == 3)
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
            wylistujKategorie();
            wylistujVendorow();
            panelDodajProdukt.BringToFront();

        }

        private void buttonDodajProd_Click(object sender, EventArgs e)
        {
            string querry = "dbo.AddNewProductTransaction";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                bool productNameTaken;
                string querry2 = "SELECT ProductName, ID FROM Products WHERE ProductName=" + "'" + DodajNazwa.Text + "'";
                SqlConnection cnn = new SqlConnection(connectionString);
                cnn.Open();
                SqlCommand cmd2 = new SqlCommand(querry2, cnn);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();

                if (dr.HasRows == true)
                {
                    NazwaDodajAlert.Text = "Nazwa produktu jest zajęta!";
                    productNameTaken = true;
                }
                else
                {
                    NazwaDodajAlert.Text = " ";
                    productNameTaken = false;
                }
                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nazwa", SqlDbType.NChar).Value = DodajNazwa.Text;
                    cmd.Parameters.Add("@opis", SqlDbType.NChar).Value = DodajOpis.Text;
                    cmd.Parameters.Add("@cena", SqlDbType.Decimal).Value = Convert.ToDecimal(DodajCena.Text);
                    cmd.Parameters.Add("@ilosc", SqlDbType.Float).Value = Convert.ToDouble(DodajIlosc.Text);
                    cmd.Parameters.Add("@kategoria", SqlDbType.Int).Value = listBoxNOCategories.SelectedValue;
                    cmd.Parameters.Add("@vendor", SqlDbType.Int).Value = listBoxNOVendors.SelectedValue;
                    if (productNameTaken == false)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Produkt dodany!");
                    }
                    else
                    {
                        MessageBox.Show("Popraw błędne informacje!");
                    }
                    
                    cnn4.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
            }
        }
        private void wylistujKategorie()
        {
            string querry = "SELECT ID, CategoryName FROM Categories";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {



                DataTable tabela_cat = new DataTable();
                adapter.Fill(tabela_cat);

                listBoxNOCategories.DisplayMember = "CategoryName";
                listBoxNOCategories.ValueMember = "ID";
                listBoxNOCategories.DataSource = tabela_cat;
                listBoxNOCategories2.DisplayMember = "CategoryName";
                listBoxNOCategories2.ValueMember = "ID";
                listBoxNOCategories2.DataSource = tabela_cat;

            }
        }
        private void wylistujVendorow()
        {
            string querry = "SELECT ID, Vendor FROM Vendors";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {



                DataTable tabela_ven = new DataTable();
                adapter.Fill(tabela_ven);

                listBoxNOVendors.DisplayMember = "Vendor";
                listBoxNOVendors.ValueMember = "ID";
                listBoxNOVendors.DataSource = tabela_ven;

                listBoxNOVendors2.DisplayMember = "Vendor";
                listBoxNOVendors2.ValueMember = "ID";
                listBoxNOVendors2.DataSource = tabela_ven;

            }
        }
        private void wylistujUserow()
        {
            string querry = "SELECT ID, Username FROM Users";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {



                DataTable tabela_usr = new DataTable();
                adapter.Fill(tabela_usr);

                listBoxNOUsers.DisplayMember = "Username";
                listBoxNOUsers.ValueMember = "ID";
                listBoxNOUsers.DataSource = tabela_usr;

            }
        }
        private void modyfikujProduktToolStripMenuItem_Click(object sender, EventArgs e)
        {
            wylistujProdukty();
            wylistujKategorie();
            wylistujVendorow();
            panelModyfikujProdukt.BringToFront();
        }

        private void wczytajModyfikacje()
        {
            string querry = "SELECT ProductName, ProductPrice, ProductDesc, ProductStock, ProductCategoryID, VendorID FROM Products WHERE ID = @id";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@id", SqlDbType.NChar).Value = listBoxNOProducts2.SelectedValue;
                    SqlDataReader dr2 = cmd.ExecuteReader();

                    while (dr2.Read())
                    {
                        ModyfikujNazwa.Text = dr2.GetValue(0).ToString().TrimEnd();
                        ModyfikujCena.Text = dr2.GetValue(1).ToString().TrimEnd();
                        ModyfikujOpis.Text = dr2.GetValue(2).ToString().TrimEnd();
                        ModyfikujIlosc.Text = dr2.GetValue(3).ToString().TrimEnd();
                        listBoxNOCategories2.SelectedValue = dr2.GetValue(4);
                        listBoxNOVendors2.SelectedValue = dr2.GetValue(5);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
                cnn4.Close();
            }
        }
        private void wczytajDaneUzytkownikow()
        {
            string querry = "SELECT Username, Password, Email, FirstName, LastName,ZipCode, City, Street, TelephoneNumber FROM Users WHERE ID = @id";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@id", SqlDbType.NChar).Value = listBoxNOUsers.SelectedValue;
                    SqlDataReader dr2 = cmd.ExecuteReader();

                    while (dr2.Read())
                    {
                        adminEdytujLogin.Text = dr2.GetValue(0).ToString().TrimEnd();
                        adminEdytujHaslo.Text = dr2.GetValue(1).ToString().TrimEnd();
                        adminEdytujMail.Text = dr2.GetValue(2).ToString().TrimEnd();
                        adminEdytujImie.Text = dr2.GetValue(3).ToString().TrimEnd();
                        adminEdytujNazwisko.Text = dr2.GetValue(4).ToString().TrimEnd();
                        adminEdytujZip.Text = dr2.GetValue(5).ToString().TrimEnd();
                        adminEdytujMiasto.Text = dr2.GetValue(6).ToString().TrimEnd();
                        adminEdytujUlica.Text = dr2.GetValue(7).ToString().TrimEnd();
                        adminEdytujTel.Text = dr2.GetValue(8).ToString().TrimEnd();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }
                cnn4.Close();
            }
        }

        private void panelTwojeZamowienia_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBoxNOProducts2_SelectedIndexChanged(object sender, EventArgs e)
        {
            wczytajModyfikacje();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string querry = "UPDATE Products SET ProductName=@productname, ProductPrice=@productprice, ProductDesc=@productdesc, ProductStock=@productstock, ProductCategoryID=@categoryid, VendorID=@vendorid WHERE ID = @id";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                { 
                    var dataSet = new DataSet();
                    bool productNameTaken;
                    string querry2 = "SELECT ProductName, ID FROM Products WHERE ID=" + "'" + listBoxNOProducts2.SelectedValue + "'";
                    SqlConnection cnn = new SqlConnection(connectionString);
                    cnn.Open();
                    SqlCommand cmd2 = new SqlCommand(querry2, cnn);
                    var dataAdapter = new SqlDataAdapter { SelectCommand = cmd2};
                    dataAdapter.Fill(dataSet);


                    var dataSet2 = new DataSet();
                    string querry3 = "SELECT ProductName, ID FROM Products WHERE ProductName=" + "'" + ModyfikujNazwa.Text + "'";
                    SqlConnection cnn2 = new SqlConnection(connectionString);
                    cnn2.Open();
                    SqlCommand cmd3 = new SqlCommand(querry3, cnn2);
                    var dataAdapter2 = new SqlDataAdapter { SelectCommand = cmd3 };
                    dataAdapter2.Fill(dataSet2);

                    if (dataSet2.Tables[0].Rows.Count > 0)
                    {
                        if(dataSet2.Tables[0].Rows[0]["ProductName"].ToString() == dataSet.Tables[0].Rows[0]["ProductName"].ToString())
                        {
                            NazwaAlert.Text = " ";
                            productNameTaken = false;
                        }
                        else
                        {
                            NazwaAlert.Text = "Nazwa produktu jest zajęta!";
                            productNameTaken = true;
                        }
                    }
                    else
                    {
                        NazwaAlert.Text = " ";
                        productNameTaken = false;
                    }
                    cnn.Close();
                    cnn2.Close();
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@id", SqlDbType.NChar).Value = listBoxNOProducts2.SelectedValue;
                    cmd.Parameters.Add("@productname", SqlDbType.NChar).Value = ModyfikujNazwa.Text; //dodać sprawdzanie czy produkt o podanej nazwie istnieje
                    cmd.Parameters.Add("@productprice", SqlDbType.Decimal).Value = ModyfikujCena.Text;
                    cmd.Parameters.Add("@productdesc", SqlDbType.NChar).Value = ModyfikujOpis.Text;
                    cmd.Parameters.Add("@productstock", SqlDbType.NChar).Value = ModyfikujIlosc.Text;
                    cmd.Parameters.Add("@categoryid", SqlDbType.NChar).Value = listBoxNOCategories2.SelectedValue;
                    cmd.Parameters.Add("@vendorid", SqlDbType.NChar).Value = listBoxNOVendors2.SelectedValue;
                     if (productNameTaken == false)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Produkt zmodyfikowany!");
                    }
                    else
                    {
                        MessageBox.Show("Popraw błędne informacje!");
                    }
                    cnn4.Close();
                }

                   
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }

            }

        }

        private void listBoxNOUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            wczytajDaneUzytkownikow();
        }

        private void buttonAdminEdytujUzytkownika_Click(object sender, EventArgs e)
        {
            string querry = "UPDATE Users SET Username=@username, Password=@password, Email=@email, FirstName=@firstname, LastName=@lastname,ZipCode=@zipcode, City=@city, Street=@street, TelephoneNumber=@telephone WHERE ID = @id";
            using (SqlConnection cnn4 = new SqlConnection(connectionString))
            {

                try
                {
                    var dataSet = new DataSet();
                    bool loginTaken;
                    string querry2 = "SELECT Username, ID FROM Users WHERE ID=" + "'" + listBoxNOUsers.SelectedValue + "'";
                    SqlConnection cnn = new SqlConnection(connectionString);
                    cnn.Open();
                    SqlCommand cmd2 = new SqlCommand(querry2, cnn);
                    var dataAdapter = new SqlDataAdapter { SelectCommand = cmd2 };
                    dataAdapter.Fill(dataSet);


                    var dataSet2 = new DataSet();
                    string querry3 = "SELECT Username, ID FROM Users WHERE Username=" + "'" + adminEdytujLogin.Text + "'";
                    SqlConnection cnn2 = new SqlConnection(connectionString);
                    cnn2.Open();
                    SqlCommand cmd3 = new SqlCommand(querry3, cnn2);
                    var dataAdapter2 = new SqlDataAdapter { SelectCommand = cmd3 };
                    dataAdapter2.Fill(dataSet2);

                    if (dataSet2.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet2.Tables[0].Rows[0]["Username"].ToString() == dataSet.Tables[0].Rows[0]["Username"].ToString())
                        {
                            EditLoginValidation.Text = " ";
                            loginTaken = false;
                        }
                        else
                        {
                            EditLoginValidation.Text = "Login jest zajęty!";
                            loginTaken = true;
                        }
                    }
                    else
                    {
                        EditLoginValidation.Text = " ";
                        loginTaken = false;
                    }
                    cnn.Close();
                    cnn2.Close();
                    cnn4.Open();
                    SqlCommand cmd = new SqlCommand(querry, cnn4);
                    cmd.Parameters.Add("@id", SqlDbType.NChar).Value = listBoxNOUsers.SelectedValue;
                    cmd.Parameters.Add("@username", SqlDbType.NChar).Value = adminEdytujLogin.Text; //dodać sprawdzanie czy login jest zajęty
                    cmd.Parameters.Add("@password", SqlDbType.NChar).Value = adminEdytujHaslo.Text;
                    cmd.Parameters.Add("@email", SqlDbType.NChar).Value = adminEdytujMail.Text;
                    cmd.Parameters.Add("@firstname", SqlDbType.NChar).Value = adminEdytujImie.Text;
                    cmd.Parameters.Add("@lastname", SqlDbType.NChar).Value = adminEdytujNazwisko.Text;
                    cmd.Parameters.Add("@zipcode", SqlDbType.NChar).Value = adminEdytujZip.Text;
                    cmd.Parameters.Add("@city", SqlDbType.NChar).Value = adminEdytujMiasto.Text;
                    cmd.Parameters.Add("@street", SqlDbType.NChar).Value = adminEdytujUlica.Text;
                    cmd.Parameters.Add("@telephone", SqlDbType.Int).Value = Convert.ToInt32(adminEdytujTel.Text);
                    string querry5 = "dbo.AddRole";
                    SqlConnection cnn5 = new SqlConnection(connectionString);
                    cnn5.Open();
                    SqlCommand cmd5 = new SqlCommand(querry5, cnn5);
                    cmd5.CommandType = CommandType.StoredProcedure;
                    cmd5.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(listBoxNOUsers.SelectedValue);
                    if (administrator.Checked)
                    {
                        cmd5.Parameters.Add("@role", SqlDbType.Int).Value = 1;
                        cmd5.ExecuteNonQuery();
                    }
                    else if (manager.Checked)
                    {
                        cmd5.Parameters.Add("@role", SqlDbType.Int).Value = 2;
                        cmd5.ExecuteNonQuery();
                    }
                    else
                    {
                        //do nothing
                    }
                    cnn5.Close();
                    if (loginTaken == false)
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Użytkownik zmodyfikowany!");
                    }
                    else
                    {
                        MessageBox.Show("Popraw błędne informacje!");
                    }
                    cnn4.Close();
                
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR:" + ex.Message);
                }

            }
        }

        private void refreshList_Click(object sender, EventArgs e)
        {
            wylistujUserow();
        }

        private void OdswiezProdukty_Click(object sender, EventArgs e)
        {
            wylistujProdukty();
        }

        private void panelPrzejdzDoKasy_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
