using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace EstateManager
{
    public static class ConnectDB
    {
        public static SqlConnection GetDBConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\AplicatieLicenta\\EstateManager\\Database.mdf;Integrated Security=True");
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public static DataTable GetDataTable(string SQL_Text)
        {
            SqlConnection cn_connection = GetDBConnection();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(SQL_Text, cn_connection);
            adapter.Fill(table);
            return table;
        }

        public static void ExecuteSQLQuery(string SQLQuery)
        {
            SqlCommand cmd_Command = new SqlCommand(SQLQuery, GetDBConnection());
            cmd_Command.ExecuteNonQuery();
        }

        public static void CloseDBConnection()
        {
            SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\AplicatieLicenta\\EstateManager\\Database.mdf;Integrated Security=True");
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }
    }
}
