using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sklep
{
    public class globals
    {
               
        public static int userID;

        public static void GetUserID(string user)
        {
            SqlConnection connection;
            string connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;

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
                    //Console.WriteLine("{0}", ID.ToString());
                    globals.userID = ID;
                }
            }
        }

        public static void setLogin(int id, System.Windows.Forms.Label label)
        {
            SqlConnection connection;
            string connectionString = ConfigurationManager.ConnectionStrings["Sklep.Properties.Settings.ShopConnectionString"].ConnectionString;

            string querry = "SELECT Username From Users WHERE ID = @id";

            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(querry, connection))
            {
                command.Parameters.Add("@id", SqlDbType.NChar).Value = id;
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    label.Text = dr.GetString(0);
                    //Console.WriteLine("{0}", username.ToString());
                }
            }
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());

        }
    }
}
