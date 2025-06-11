using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZstdSharp.Unsafe;

namespace Malshinon
{
    internal class MenuToUser
    {
        public async Task StartProgram()
        {
            bool stop = true;
            CreateReport report = new CreateReport();
            DAL dal = new DAL();
            while (stop)
            {

                //Console.Clear();
                Console.WriteLine("Enter your full name");
                string fullName = Console.ReadLine();
                dal.PersonIdentificationFlow(fullName);

                //Console.Clear();
                Console.WriteLine("Choose by number!\nmenu:\n1. Report\n2. get secret code");
                bool isNumber = int.TryParse(Console.ReadLine(), out int chooseNumber);
                if (!isNumber)
                {
                    Console.WriteLine("Enter invalid number");
                    await Task.Delay(2000);
                    continue;
                }
                switch (chooseNumber)
                {
                    case 1:
                        await report.ReportPerson(fullName);
                        break;
                    case 2:
                        Console.WriteLine($"****\nSecret Code is: {dal.GetSecretCode(fullName)}\n****\n5 seconds to view");
                        await Task.Delay(1000);
                        Console.WriteLine("4 seconds to view");
                        await Task.Delay(1000);
                        Console.WriteLine("3 seconds to view");
                        await Task.Delay(1000);
                        Console.WriteLine("2 seconds to view");
                        await Task.Delay(1000);
                        Console.WriteLine("1 seconds to view");
                        await Task.Delay(1000);
                        break;
                }
            }
        }
    }
}
