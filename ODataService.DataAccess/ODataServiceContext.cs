using System.Data.Entity;
using ODataService.Models;

namespace ODataService.DataAccess
{
    public class ODataServiceContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
    }
}
