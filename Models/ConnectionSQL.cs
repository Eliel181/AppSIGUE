using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ConnectionSQL
    {
        private readonly string connectionString;
        public ConnectionSQL()
        {
            connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\LPOO II\\Proyetos\\DataBases\\DB_AppSIGUE.mdf\";Integrated Security=True;Connect Timeout=30";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
