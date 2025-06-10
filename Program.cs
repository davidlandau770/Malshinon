using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            DAL dAL = new DAL();
            //dAL.PrintPeople();

            //People people = new People(101, "david", "landau");
            //People people2 = new People(100, "david114", "landau221");
            //dAL.InsertPeople(people2);

            //dAL.PersonIdentificationFlow("david landau");
            //dAL.PersonIdentificationFlow("david landau3");
            //dAL.PersonIdentificationFlow("david landau4");
            //dAL.PersonIdentificationFlow("david avishay landau");
            //dAL.PersonIdentificationFlow("david landau");
            //dAL.PersonIdentificationFlow("david a landau");
            //dAL.PersonIdentificationFlow("david av landau");
            //dAL.PersonIdentificationFlow("david avi landau");
            //dAL.PersonIdentificationFlow("david avis landau");
            MenuToUser menuToUser = new MenuToUser();
            await menuToUser.StartProgram();
        }
    }
}
