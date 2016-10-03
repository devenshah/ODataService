using System.Data.Entity;
using ODataService.Models;

namespace ODataService.DataAccess
{
    public class ODataServiceContext : DbContext
    {
        public ODataServiceContext()
            : base("ODataServiceDb")
        {
            Database.SetInitializer(new ODataServiceDbInitializer());
        }

        public DbSet<Country> Countries { get; set; }
    }
}
