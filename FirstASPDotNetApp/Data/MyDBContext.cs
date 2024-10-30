using FirstASPDotNetApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstASPDotNetApp.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        
        }
        public DbSet<Product> Products { get; set; }
    }
}
