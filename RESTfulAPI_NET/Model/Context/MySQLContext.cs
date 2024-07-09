using Microsoft.EntityFrameworkCore;

namespace RESTfulAPI_NET.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() {}

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
    }
}
