using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Авторизация_WPF
{
    class DB
    {        
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-F8M2CVU;Initial Catalog=registrar;Integrated Security=True;Encrypt=False");

        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
