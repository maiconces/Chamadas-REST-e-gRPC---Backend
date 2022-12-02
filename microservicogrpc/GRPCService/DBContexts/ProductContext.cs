using Microsoft.EntityFrameworkCore;
using ProductService;

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