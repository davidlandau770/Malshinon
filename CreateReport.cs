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
        public async Task ReportPerson(string fullName)
        {
            Console.WriteLine("Enter text to report:");
            string textReport = Console.ReadLine();
            List<string> names = GetPersonListFromReport(textReport);

            if (names.Count == 0 || names[0].Length == 0)
            {
                Console.WriteLine("not found name valid in the text.");
                return;
            }

            int reporterId = dal.GetIdByName(fullName);
            foreach (string name in names)
            {
                dal.PersonIdentificationFlow(name);
                int targetId = dal.GetIdByName(name);
                dal.ReportIdentificationFlow(reporterId, targetId, textReport);
                dal.AddNumReports(reporterId);
                dal.AddNumMentions(targetId);
            }

            Console.WriteLine("The report was successfully received.");
        }

        public List<string> GetPersonListFromReport(string textReport)
        {
            List<string> namesFromReports = new List<string>();
            string full_name = "";

            bool isName = false;
            string[] textArr = textReport.Split();
            for (int i = 0; i < textArr.Length; i++)
            {
                if (Char.IsUpper(textArr[i][0]))
                {
                    isName = true;
                    full_name += textArr[i] + " ";
                }
                else
                {
                    if (isName)
                    {
                        string[] countWords = full_name.Split(' ');
                        if (countWords.Length > 1)
                        {
                            full_name.Trim();
                            namesFromReports.Add(full_name);
                        }
                        full_name = "";
                    }
                    isName = false;
                }
                if (i == textArr.Length - 1 && full_name.Split().Length > 1)
                {
                    namesFromReports.Add(full_name);
                }
            }

            return namesFromReports;
        }
    }
}
