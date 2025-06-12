using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon
{
    internal class ReportDal
    {
        private string connectionString = "server=localhost;user=root;password=;database=malshinon";
        private MySqlConnection _connection;
        DAL dal = new DAL();
        PeopleDal PeopleDal = new PeopleDal();

        public void InsertReport(IntelReports report)
        {
            MySqlCommand cmd = null;
            try
            {
                dal.OpenConnection();

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

        public int GetNumReportByName(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                string firstName = People.FirstNameAndLast(fullName)[0];
                string lastName = People.FirstNameAndLast(fullName)[1];

                dal.OpenConnection();
                cmd = new MySqlCommand($"SELECT num_reports FROM people WHERE first_name = @firstName AND last_name = @lastName", _connection);
                cmd.Parameters.AddWithValue("firstName", firstName);
                cmd.Parameters.AddWithValue("lastName", lastName);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int numReport = reader.GetInt32("num_reports");
                    return numReport;
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
                dal.CloseConnection();
            }
            return 0;
        }

        public int GetNumMentionByName(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                string firstName = People.FirstNameAndLast(fullName)[0];
                string lastName = People.FirstNameAndLast(fullName)[1];

                dal.OpenConnection();
                cmd = new MySqlCommand($"SELECT num_mentions FROM people WHERE first_name = @firstName AND last_name = @lastName", _connection);
                cmd.Parameters.AddWithValue("firstName", firstName);
                cmd.Parameters.AddWithValue("lastName", lastName);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int numMentions = reader.GetInt32("num_mentions");
                    return numMentions;
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
                dal.CloseConnection();
            }
            return 0;
        }

        public void ChangeTypeRole(int peopleId, string newType)
        {
            MySqlCommand cmd = null;
            try
            {
                dal.OpenConnection();
                string query = "UPDATE people SET type_role = @newType WHERE id = @peopleId;";
                cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@newType", newType);
                cmd.Parameters.AddWithValue("@peopleId", peopleId);

                int afected = cmd.ExecuteNonQuery();
                if (afected > 0)
                {
                    Console.WriteLine($"{peopleId} changed status to {newType}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while update typing: {ex.Message}");
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        public void AddNumReports(int id)
        {
            MySqlCommand cmd = null;

            try
            {
                dal.OpenConnection();

                string query = $"UPDATE people SET num_reports = num_reports + 1 WHERE id = @id;";
                //string query = "INSERT INTO people (id, first_name, last_name, secret_code, type_role) VALUES (@id, @first_name, @last_name, @secret_code, @type_role);";
                cmd = new MySqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@id", id);

                int reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    Console.WriteLine("Table updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating people:{ex.GetType().Name} ----  {ex.Message}");
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        public void AddNumMentions(int id)
        {
            MySqlCommand cmd = null;
            try
            {
                dal.OpenConnection();

                cmd = new MySqlCommand($"UPDATE people SET num_mentions = num_mentions + 1 WHERE id = @id;", _connection);
                cmd.Parameters.AddWithValue("@id", id);
                int reader = cmd.ExecuteNonQuery();
                if (reader > 0)
                {
                    Console.WriteLine("Table updated successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while updating people: {ex.Message}");
            }
            finally
            {
                dal.CloseConnection();
            }
        }

        public int GetAvgLengthTextReport(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                int idPeople = PeopleDal.GetIdByName(fullName);

                dal.OpenConnection();
                cmd = new MySqlCommand($"SELECT AVG(LENGTH(text)) AS avg_length FROM `intel_reports` WHERE reporter_id = @idPeople GROUP BY reporter_id HAVING COUNT(*) >= 10;", _connection);
                cmd.Parameters.AddWithValue("idPeople", idPeople);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int avgLength = reader.GetInt32("avg_length");
                    return avgLength;
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
                dal.CloseConnection();
            }
            return 0;
        }

        public int DangerCheckInLast15Minuts(string fullName)
        {
            MySqlCommand cmd = null;
            MySqlDataReader reader = null;

            try
            {
                int idPeople = PeopleDal.GetIdByName(fullName);
                dal.OpenConnection();
                cmd = new MySqlCommand($"SELECT *, COUNT(`target_id`) AS count_target_id FROM intel_reports GROUP BY `target_id` HAVING (count_target_id > 20 OR COUNT(`timestamp` >= NOW() - INTERVAL 15 MINUTE AND `timestamp` <= NOW()) > 3) AND target_id = @idPeople;", _connection);
                cmd.Parameters.AddWithValue("idPeople", idPeople);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int countTargetId = reader.GetInt32("count_target_id");
                    return countTargetId;
                }
                Console.WriteLine("The count read successful.");
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
                dal.CloseConnection();
            }
            return 0;
        }
    }
}
