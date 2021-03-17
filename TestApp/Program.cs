using System;
using System.Linq;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new MyDbContext();
            //var haha = (from p in context.People
            //           select new
            //           {
            //               Test = p
            //           }).FirstOrDefault();

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

            //var haha2 = context.People
            //    .Where(p => p.Name != string.Empty && p.Id == 0 || p.Id > 2)
            //    .Join(context.Teams, p => p.Team, t => t, (p, t) => new
            //    {
            //        Person = p,
            //        Team = t
            //    })
            //    .OrderByDescending(p => p.Person.Name)
            //    .Select(n => new
            //    {
            //        Hello = n.Person.Name,
            //        World = n.Team.Name,
            //        Test = "hello",
            //        Test2 = 1 + 1
            //    }).FirstOrDefault();


            foreach (var p in haha)
            {
                
                Console.WriteLine(p);
            }
        }
    }
}
