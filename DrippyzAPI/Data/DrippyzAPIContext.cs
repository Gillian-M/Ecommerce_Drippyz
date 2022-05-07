#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Drippyz.Models;

    public class DrippyzAPIContext : DbContext
    {
        public DrippyzAPIContext (DbContextOptions<DrippyzAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        


}
