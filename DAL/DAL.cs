using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using static Mysqlx.Expect.Open.Types;

namespace Malshinon
{
    internal class DAL
    {
        private string connectionString = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection _connection;

        public MySqlConnection OpenConnection()
        {
            if (_connection == null)
            {
                _connection = new MySqlConnection(connectionString);
            }

            if (_connection.State != System.Data.ConnectionState.Open)
            {
                _connection.Open();
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public DAL()
        {
            try
            {
                OpenConnection();
                Console.WriteLine("Connection successful.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"MySQL Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }
    }
}
