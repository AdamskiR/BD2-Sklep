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
    public partial class Alledrogo : Form
    {
        SqlConnection connection;
        string connectionString;

        public Alledrogo()
        {
            InitializeComponent();
            globals.setLogin(globals.userID, labelUsername);
            connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;
        }

        private void Alledrogo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'shopDataSet.SubCategories' table. You can move, or remove it, as needed.
            this.subCategoriesTableAdapter.Fill(this.shopDataSet.SubCategories);
            kategorie();

        }

        private void kategorie()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Categories", connection))
            {

                DataTable tabela_kat = new DataTable();
                adapter.Fill(tabela_kat);

                listBoxKategorie.DisplayMember = "CategoryName";
                listBoxKategorie.ValueMember = "ID";
                listBoxKategorie.DataSource = tabela_kat;

                listBoxKategorie.DisplayMember = "Vendor";
                listBoxKategorie.ValueMember = "ID";
                listBoxKategorie.DataSource = tabela_kat;


            }

        }

    }
}
