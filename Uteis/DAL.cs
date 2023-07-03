using MySql.Data.MySqlClient;
using System.Data;

namespace MyCommerce.Uteis
{
   //DAL = Data Acces Layer
    public class DAL
    {
        private static string Server = "localhost";
        private static string Database = "sistema_venda";
        private static string User = "root";
        private static string Password = "";
        private static string ConnectionString = $"Server={Server};Database={Database};Uid={User};Pwd={Password};Sslmode=none;Charset=utf8;";
        private static MySqlConnection Connection;

        public DAL()
        {
            Connection = new MySqlConnection(ConnectionString);
            Connection.Open();
        }

        //Recebe como Argumento uma String c o comando SELECT
        public DataTable ReturnDataTable(string query)
        {
            DataTable dt = new DataTable();
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
            adapt.Fill(dt);
            return dt;
        }

        public DataTable ReturnDataTableCMD(MySqlCommand cmd)
        {
            DataTable dt = new DataTable();
            cmd.Connection = Connection;
            MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);
            adapt.Fill(dt);
            return dt;
        }

        //Recebe como Argumento uma String c o comando INSERT, UPDATE ou DELETE
        public void CRUD(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, Connection);
            cmd.ExecuteNonQuery();
        }
    }
}
