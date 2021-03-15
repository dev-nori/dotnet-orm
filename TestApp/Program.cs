using System;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new MyDbContext())
            {
                var haha = from p in context.People
                           join t in context.Teams on p.Team equals t
                           where p.Name != string.Empty
                           orderby p.Name descending
                           select new
                           {
                               Hello = p.Name,
                               World = t.Name,
                               Test = "hello"
                           };

                foreach (var p in haha)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
