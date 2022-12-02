using System;
using App.Metrics;
using System.Transactions;
using System.Threading.Tasks;
using ProductMicroservice.Models;
using ProductMicroservice.DBContexts;

namespace ProductMicroservice.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _dbContext;
        private readonly IMetrics _metrics;

        public ProductRepository(ProductContext dbContext, IMetrics metrics)
        {
            _dbContext = dbContext;
            _metrics = metrics;
        }
        public Task<Product> PostAsync(Product product)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _metrics.Measure.Counter.Increment(MetricsRegistry.CreateProductsCounter);
                    _dbContext.AddAsync(product);
                    _metrics.Measure.Counter.Increment(MetricsRegistry.InsertProductDbConnectionCounter);
                    _dbContext.SaveChanges();
                    scope.Complete();
                    
                    return  Task.FromResult(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao salvar produto", ex);
            }
        }
    }
}
