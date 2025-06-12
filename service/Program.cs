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
            MenuToUser menuToUser = new MenuToUser();
            await menuToUser.StartProgram();
        }
    }
}
