using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon
{
    internal class DAL
    {
        private string connectionString = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection _connection;

        // MySql 
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

        // People

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
                    //string secret_code = reader.GetString("secret_code");
                    //string type_role = reader.GetString("type_role");
                    //int Num_reports = reader.GetInt32("Num_reports");
                    //int Num_mentions = reader.GetInt32("Num_mentions");

                    People people = new People(id, first_name, last_name);
                    peopleList.Add(people);
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while geting people: {ex.Message}");
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

        public bool GetNameIfFound(string newFullName)
        {
            
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string firstName = People.FirstNameAndLast(newFullName)[0];
            string lastName = People.FirstNameAndLast(newFullName)[1];
            try
            {
                OpenConnection();
                cmd = new MySqlCommand($"SELECT first_name, last_name FROM people WHERE first_name = '{firstName}' AND last_name = '{lastName}'", _connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string first_name = reader.GetString("first_name");
                    string last_name = reader.GetString("last_name");
                    if (first_name != null && lastName != null)
                    {
                        return true;
                    }
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while geting people: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                CloseConnection();
            }
            return false;
        }

        public bool GetSecretCodeIfFound(string newSecretCode)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                OpenConnection();
                cmd = new MySqlCommand($"SELECT secret_code FROM people WHERE secret_code = '{newSecretCode}'", _connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string secretCode = reader.GetString("secret_code");
                    if (secretCode != null)
                    {
                        return true;
                    }
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while geting people: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                CloseConnection();
            }
            return false;
        }

        public string GetSecretCode(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;
            string firstName = People.FirstNameAndLast(fullName)[0];
            string lastName = People.FirstNameAndLast(fullName)[1];

            try
            {
                OpenConnection();
                cmd = new MySqlCommand($"SELECT secret_code FROM people WHERE first_name = '{firstName}' AND last_name = '{lastName}'", _connection);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string secretCode = reader.GetString("secret_code");
                    if (secretCode != null)
                    {
                        return secretCode;
                    }
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while geting people: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                CloseConnection();
            }
            return "The program could not find the secret code.";
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

        public void InsertPeople(People people)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();

                //if (!GetNameIfFound(people.Full_name))
                //{
                string query = "INSERT INTO people (id, first_name, last_name, secret_code, type_role) VALUES (@id, @first_name, @last_name, @secret_code, @type_role);";
                    cmd = new MySqlCommand(query, _connection);
                    cmd.Parameters.AddWithValue("@id", people.Id);
                    cmd.Parameters.AddWithValue("@first_name", people.First_name);
                    cmd.Parameters.AddWithValue("@last_name", people.Last_name);
                    cmd.Parameters.AddWithValue("@secret_code", people.Secret_code);
                    cmd.Parameters.AddWithValue("@type_role", people.Type_role);
                    cmd.Parameters.AddWithValue("@num_reports", people.Num_reports);
                    cmd.Parameters.AddWithValue("@num_mentions", people.Num_mentions);

                    cmd.ExecuteNonQuery();
                    Console.WriteLine($"{people.First_name} {people.Last_name} inserted successfully.");
                    Console.WriteLine($"your secret code is: {GetSecretCode(people.Full_name)}");
                //}
                //else
                //{
                //    Console.WriteLine("The name is already found.");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting people: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void PersonIdentificationFlow(string peopleFullName)
        {
            try
            {
                if (!GetNameIfFound(peopleFullName))
                {
                    People people = new People(peopleFullName);
                    //Console.WriteLine($"people.First_name: {people.First_name}, people.Last_name: {people.Last_name}");
                    InsertPeople(people);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting people: {ex.Message}");
            }
        }

        //public void IntelSubmissionFlow(string peopleFullName)
        //{

        //}

        public int GetIdByName(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                string firstName = People.FirstNameAndLast(fullName)[0];
                string lastName = People.FirstNameAndLast(fullName)[1];

                OpenConnection();
                cmd = new MySqlCommand($"SELECT id FROM people WHERE first_name = '{firstName}' AND last_name = '{lastName}'", _connection);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    return id;
                }
                Console.WriteLine("People read successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while geting people: {ex.Message}");
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                CloseConnection();
            }
            return 0;
        }

        // Report

        public void InsertReport(IntelReports report)
        {
            MySqlCommand cmd = null;
            try
            {
                OpenConnection();

                //if (!GetNameIfFound(people.Full_name))
                //{
                string query = "INSERT INTO intel_reports (id, reporter_id, target_id, text, timestamp) VALUES (@id, @reporter_id, @target_id, @text, @timestamp);";
                cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", report.Id);
                cmd.Parameters.AddWithValue("@reporter_id", report.Reporter_id);
                cmd.Parameters.AddWithValue("@target_id", report.Target_id);
                cmd.Parameters.AddWithValue("@text", report.Text);
                cmd.Parameters.AddWithValue("@timestamp", report.Timestamp);

                cmd.ExecuteNonQuery();
                Console.WriteLine($"{report.Id} inserted successfully.");
                Console.WriteLine($"{report.Reporter_id} reported about {report.Target_id} ({report.Timestamp})");
                //}
                //else
                //{
                //    Console.WriteLine("The name is already found.");
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting report: {ex.Message}");
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ReportIdentificationFlow(int reporterId, int targetId, string text)
        {
            try
            {
                //if (!GetNameIfFound(people.Full_name))
                //{
                IntelReports report = new IntelReports(reporterId, targetId, text);
                InsertReport(report);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while inserting report: {ex.Message}");
            }
        }
    }
}
