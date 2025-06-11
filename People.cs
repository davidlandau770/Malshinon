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
        public string First_name = "";
        public string Last_name = "";
        public string Full_name = "";
        public string Secret_code = "";
        public string Type_role = "";
        public int Num_reports;
        public int Num_mentions;
        
        public People(string full_name, string typeRole = "reporter")
        {
            string[] names = full_name.Split();
            if (names.Length < 2)
            {
                Console.WriteLine("Enter invalid name!");
                return;
            }

            First_name = FirstNameAndLast(full_name)[0];
            Last_name = FirstNameAndLast(full_name)[1];
            Full_name = First_name + " " + Last_name;
            Secret_code = CreateSecretCode().ToString();
            Type_role = typeRole;
        }

        public People(int id, string first_name, string last_name)
        {
            Id = id;
            First_name = first_name;
            Last_name = last_name;
            Full_name = first_name + " " + last_name;
            Secret_code = CreateSecretCode().ToString();
        }

        DAL dAL = new DAL();

        public int CreateSecretCode()
        {
            Random random = new Random();
            int numRandom = 0;
            while (numRandom == 0)
            {
                int rand = random.Next(10000000, 100000000);
                bool isFound = dAL.GetSecretCodeIfFound($"{rand}");
                if (!isFound)
                {
                    numRandom = rand;
                }
            }
            return numRandom;
        }

        static public string[] FirstNameAndLast(string fullName)
        {
            string[] names = { };
            names = fullName.Split();
            //string first_name = "";
            //first_name = string.Join(" ", names.Take(names.Length - 1));
            //string last_name = "";
            //last_name = names[names.Length - 1];
            //string[] splitNames = { first_name, last_name };
            //Console.WriteLine($"fullName: {fullName}, people.First_name: {first_name}, people.Last_name: {last_name}, splitNames[0]: {splitNames[0]}, splitNams[1]: {splitNames[1]}, names[0]: {names[0]}, names[1]: {names[1]}");
            return names;
        }

        public override string ToString()
        {
            return $"Id: {Id}, First_name: {First_name}, Last_name: {Last_name}, Secret_code: {Secret_code}, Type_role: {Type_role}, Num_reports: {Num_reports}, Num_mentions: {Num_mentions}";
        }
    }
}
