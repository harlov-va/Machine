using System.Data.Entity;
using Machine.Models;
 
namespace Machine.Concrete
{
    public class EFDbContext : DbContext
    {
        public DbSet<Drinks> Drinks { get; set; }
        public DbSet<Coins> Coins { get; set; }
    }
}