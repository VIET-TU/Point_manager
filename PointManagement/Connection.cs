using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointManagement
{
    internal class Connection
    {
        string connectionString = @"Data Source=DESKTOP-VK77CN3\SQLEXPRESS;Initial Catalog=PointManagement;Integrated Security=True";

        SqlConnection connection;

        public void OpenConnection()
        {
            connection = new SqlConnection(connectionString);
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            connection.Dispose();
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dataTable = new DataTable();
            OpenConnection();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            dataAdapter.Fill(dataTable);
            CloseConnection();
            return dataTable;
        }

        public void ExecuteNonQuery(string query)
        {
            OpenConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = query;
            command.Connection = connection;
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
