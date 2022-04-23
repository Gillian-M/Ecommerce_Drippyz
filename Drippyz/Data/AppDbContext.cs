using Drippyz.Models;
using Microsoft.EntityFrameworkCore;

namespace Drippyz.Data
{
    public class AppDbContext : DbContext
    {
        //configure db context 
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Passing the model builder to the base class
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            //Automatically define identifiers when generating default authentication tables
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }




    }
}
