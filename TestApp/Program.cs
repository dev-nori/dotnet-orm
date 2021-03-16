using System;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new MyDbContext();
            var haha = from p in context.People
                       where p.Name != string.Empty && p.Id == 0 || p.Id > 2
                       join t in context.Teams on p.Team equals t
                       orderby p.Name descending
                       select new
                       {
                           Hello = p.Name,
                           World = t.Name,
                           Test = "hello",
                           Test2 = 1 + 1
                       };

            foreach (var p in haha)
            {
                Console.WriteLine(p);
            }
        }
    }
}
