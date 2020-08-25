using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLogic.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var context = new blocklogicEntities();
                var bac = new Backer
                {
                    ID = 10,
                    Funding = "hi",
                    Name = "ButternutBob"
                };

                context.Backers.Add(bac);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
