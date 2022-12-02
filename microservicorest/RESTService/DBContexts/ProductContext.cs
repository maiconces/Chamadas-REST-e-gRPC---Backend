using System.Collections.Generic;
using ProductMicroservice.DBContexts;
using ProductMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductMicroservice.DBContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) 
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
    }
}