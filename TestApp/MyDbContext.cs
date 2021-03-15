using TestLib;

namespace TestApp
{
    public class MyDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public DbSet<Team> Teams { get; set; }

        public MyDbContext()
        {
            People = new DbSet<Person>(this);
            Teams = new DbSet<Team>(this);
        }
    }
}
