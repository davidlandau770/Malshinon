using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class People
    {
        public int Id;
        public string First_name;
        public string Last_name;
        public string Full_name;
        public string Secret_code;
        public string Type_role = "reporter";
        public int Num_reports;
        public int Num_mentions;
        
        public People(string full_name)
        {
            string[] names = full_name.Split();
            if (names.Length < 2)
            {
                Console.WriteLine("Enter name invalid!");
                return;
            }
            Last_name = string.Join(" ", names.Skip(1).ToArray());
        }

        public People(int id, string first_name, string last_name, string secret_code, string type_role, int num_reports, int num_mentions)
        {
            Id = id;
            First_name = first_name;
            Last_name = last_name;
            Full_name = first_name + " " + last_name;
            Secret_code = secret_code;
            Type_role = type_role;
            Num_reports = num_reports;
            Num_mentions = num_mentions;
        }

        public override string ToString()
        {
            return $"Id: {Id}, First_name: {First_name}, Last_name: {Last_name}, Secret_code: {Secret_code}, Type_role: {Type_role}, Num_reports: {Num_reports}, Num_mentions: {Num_mentions}";
        }
    }
}
