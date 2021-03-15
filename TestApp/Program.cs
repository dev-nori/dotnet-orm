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
                           where p.Name != string.Empty
                           join t in context.Teams on p.Team equals t
                           orderby p.Name descending
                           select new
                           {
                               Person = p.Name,
                               Team = t.Name
                           };

                foreach (var p in haha)
                {
                    Console.WriteLine(p);
                }
            }
        }
    }
}
