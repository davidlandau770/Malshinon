using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Org.BouncyCastle.Asn1.X509;

namespace Malshinon
{
    internal class CreateReport
    {
        DAL dal = new DAL();
        PeopleDal peopleDal = new PeopleDal();
        ReportDal reportDal = new ReportDal();
        AlertDal alertDal = new AlertDal();

        public void CreateReportPerson(string fullName)
        {
            Console.WriteLine("Enter text to report:\nOnly the first and last name should begin with a capital letter. For example: \"I saw Muhammad Sinwar and Hassan Nasrallah planning an attack\"");
            string textReport = Console.ReadLine();
            List<string> names = GetPersonListFromReport(textReport);

            if (names.Count == 0 || names[0].Length == 0)
            {
                Console.WriteLine("not found name valid in the text.");
                return;
            }

            int reporterId = peopleDal.GetIdByName(fullName);
            foreach (string name in names)
            {
                Console.WriteLine($"!!!!!\nname: {name}\n!!!!!");
                peopleDal.PersonIdentificationFlow(name);
                int targetId = peopleDal.GetIdByName(name);
                reportDal.ReportIdentificationFlow(reporterId, targetId, textReport);
                reportDal.AddNumReports(reporterId);
                reportDal.AddNumMentions(targetId);
                string typeRoleReporter = CheckStatusTypeRole(fullName);
                reportDal.ChangeTypeRole(reporterId, typeRoleReporter);
                string typeRoleMention = CheckStatusTypeRole(name);
                reportDal.ChangeTypeRole(targetId, typeRoleMention);
                Alert alert = new Alert(targetId, textReport);
                alertDal.InsertAlert(alert);
            }

            Console.WriteLine("The report was successfully received.");
        }

        public List<string> GetPersonListFromReport(string textReport)
        {
            List<string> namesFromReports = new List<string>();
            string full_name = "";

            bool isName = false;
            string[] textArr = textReport.Split(' ');
            for (int i = 0; i < textArr.Length; i++)
            {
                if (Char.IsUpper(textArr[i][0]))
                {
                    isName = true;
                    full_name += $"{textArr[i]} ";
                    //full_name.TrimStart();
                }
                else
                {
                    if (isName)
                    {
                        full_name.Trim();
                        string[] countWords = full_name.Split(' ');
                        if (countWords.Length > 2)
                        {
                            namesFromReports.Add(full_name);
                        }
                        full_name = "";
                    }
                    isName = false;
                }
                string[] countWords2 = full_name.Split(' ');
                if (i == textArr.Length - 1 && countWords2.Length > 2)
                {
                    namesFromReports.Add(full_name);
                }
            }
            namesFromReports = namesFromReports.Distinct().ToList();
            return namesFromReports;
        }

        public string CheckStatusTypeRole(string fullName)
        {
            int numReport = reportDal.GetNumReportByName(fullName);
            int numMention = reportDal.GetNumMentionByName(fullName);
            int avgLengthTextReport = reportDal.GetAvgLengthTextReport(fullName);
            int countAlert = reportDal.DangerCheckInLast15Minuts(fullName);
            if (numReport >= 20 && numReport % 10 == 0)
            {
                Console.WriteLine($"****\nALERT: {fullName} is potential threat alert\n****");
            }
            if (countAlert >= 3)
            {
                Console.WriteLine($"****\nALERT: {fullName} is a high-risk target.\n****");
            }
            if (numReport > 0 && numMention > 0)
            {
                return "both";
            }
            else if (numReport >= 10 && avgLengthTextReport >= 100)
            {
                return "potential_agent";
            }
            else if (numMention > 0)
            {
                return "target";
            }
            else if (numReport > 0)
            {
                return "reporter";
            }
            else
            {
                return "null";
            }
        }
    }
}
