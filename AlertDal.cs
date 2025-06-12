using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon
{
    internal class AlertDal
    {
        private string connectionString = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection _connection;
        DAL dal = new DAL();

        public void InsertAlert(Alert alert)
        {

            MySqlCommand cmd = null;
            try
            {
                _connection = dal.OpenConnection();

                string query = "INSERT INTO alerts (id, target_id, created_at, reason) VALUES (@id, @target_id, @created_at, @reason);";
                cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", alert.Id);
                cmd.Parameters.AddWithValue("@target_id", alert.Target_id);
                cmd.Parameters.AddWithValue("@created_at", alert.Created_at);
                cmd.Parameters.AddWithValue("@reason", alert.Reason);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"{alert.Id} inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting report: {ex.Message}");
            }
            finally
            {
                dal.CloseConnection();
            }
        }
    }
}
