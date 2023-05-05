using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Novel
{
    internal class Utility
    {
        public static string connection = @"Data Source=DESKTOP-HUJGH1E\SQLEXPRESS;Initial Catalog=Novel;Integrated Security=True";

        public static SqlConnection conn = new SqlConnection(connection);

        public static DataTable getData(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            return dt;
        }

        public static string enc(string pass)
        {
            using(SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] password = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                string getPass = Convert.ToBase64String(password);

                return getPass;
            }
        }
    }
}
