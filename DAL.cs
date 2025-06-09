using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        public List<People> GetPeople(string query = "SELECT * FROM people")
        {
            List<People> peopleList = new List<People>();
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                OpenConnection();
                cmd = new MySqlCommand(query, _connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string first_name = reader.GetString("first_name");
                    string last_name = reader.GetString("last_name");
                    string secret_code = reader.GetString("secret_code");
                    string type_role = reader.GetString("type_role");
                    int Num_reports = reader.GetInt32("Num_reports");
                    int Num_mentions = reader.GetInt32("Num_mentions");

                    People people = new People(id, first_name, last_name, secret_code, type_role, Num_reports, Num_mentions);
                    peopleList.Add(people);
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while fetching agents: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                CloseConnection();
            }
            return peopleList;
        }

        public void PrintPeople()
        {
            DAL dAL = new DAL();
            List<People> peoples = dAL.GetPeople();
            foreach (var people in peoples)
            {
                Console.WriteLine(people.ToString());
            }
        }

        public void PersonIdentificationFlow(string people)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();
                List<People> peopleList = GetPeople();
                //Console.WriteLine("Enter your name:");
                //string input = Console.ReadLine();
                //if (peopleList) {
                string query = "INSERT INTO people (id, codeName, realName, location, status, missionsCompleted) VALUES (@id, @codeName, @realName, @location, @status, @missionsCompleted);";

                //cmd = new MySqlCommand(query, _connection);
                //cmd.Parameters.AddWithValue("@Id", people.Id);
                //cmd.Parameters.AddWithValue("@First_name", people.First_name);
                //cmd.Parameters.AddWithValue("@Last_name", people.Last_name);
                //cmd.Parameters.AddWithValue("@Secret_code", people.Secret_code);
                //cmd.Parameters.AddWithValue("@Type_role", people.Type_role);
                //cmd.Parameters.AddWithValue("@Num_reports", people.Num_reports);
                //cmd.Parameters.AddWithValue("@Num_mentions", people.Num_mentions);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Agent inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting agent: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
