using System;
using System.Linq;

namespace TestLib
{
    public class DbContext : IQueryContext, IDisposable
    {
        public IQueryProvider Provider { get; }

        public DbContext()
        {
            Console.WriteLine("#################");
            Provider = new QueryProvider();
        }

        public void Dispose()
        {
            
        }
    }
}
