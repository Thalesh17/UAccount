using Microsoft.EntityFrameworkCore;

namespace UAccount.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Conta> Contas { get; set;}
    }
}