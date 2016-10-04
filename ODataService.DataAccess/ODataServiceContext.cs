using System.Data.Entity;
using ODataService.Models;

namespace ODataService.DataAccess
{
    public class ODataServiceContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Person> People { get; set; }

        public ODataServiceContext()
            : base("ODataServiceDb")
        {
            Database.SetInitializer(new ODataServiceDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasMany(p => p.Family).WithMany();
            base.OnModelCreating(modelBuilder);
        }
    }
}
