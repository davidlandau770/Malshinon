using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class Report
    {
        DAL dal = new DAL();
        public void ReportPerson(string fullName)
        {
            Console.WriteLine("Enter text to report:");
            string textReport = Console.ReadLine();
            List<string> namesFromReports = new List<string>();
            string full_name = "";

            bool isName = false;
            string[] textArr = textReport.Split();
            for (int i = 0; i < textArr.Length; i++)
            {
                if (Char.IsUpper(textArr[i][0])) {
                    isName = true;
                    full_name += textArr[i];
                }
                else
                {
                    if (isName)
                    {
                        namesFromReports.Add(full_name);
                    }
                    isName = false;
                }
            }
        }
    }
}
