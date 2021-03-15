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
                           orderby p.Name descending
                           where p.Name != string.Empty
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
