using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class MenuToUser
    {
        public async Task StartProgram()
        {
            bool stop = true;
            Report report = new Report();
            DAL dal = new DAL();
            while (stop)
            {

                Console.Clear();
                Console.WriteLine("Enter your full name");
                string fullName = Console.ReadLine();
                dal.PersonIdentificationFlow(fullName);

                Console.Clear();
                Console.WriteLine("Choose by number!\nmenu:\n1. Report\n2. get secret code");
                int chooseNumber = int.Parse(Console.ReadLine());
                switch (chooseNumber)
                {
                    case 1:
                        report.ReportPerson(fullName);
                        break;
                    case 2:
                        Console.WriteLine(dal.GetSecretCode(fullName));
                        await Task.Delay(5000);
                        break;
                }
            }
        }
    }
}
