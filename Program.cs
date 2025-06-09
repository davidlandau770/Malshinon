using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malshinon
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DAL dAL = new DAL();
            dAL.PrintPeople();

            People people = new People("david landau");
            People people2 = new People("avishay landau");
            People people3 = new People("landau");

            dAL.PersonIdentificationFlow("david landau");
        }
    }
}
