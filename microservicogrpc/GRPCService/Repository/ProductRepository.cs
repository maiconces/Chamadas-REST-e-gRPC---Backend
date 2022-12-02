using System;
using Grpc.Core;
using App.Metrics;
using ProductService;
using System.Transactions;
using System.Threading.Tasks;
using static ProductService.gRPC;
using ProductMicroservice.DBContexts;

namespace ProductMicroservice.Repository
{
    public class ProductRepository : gRPCBase
    {
        private readonly ProductContext _dbContext;
        private readonly IMetrics _metrics;

        public ProductRepository(ProductContext dbContext, IMetrics metrics)
        {
            _dbContext = dbContext;
            _metrics = metrics;
        }
        public override Task<Product> Post(Product product, ServerCallContext context)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    _metrics.Measure.Timer.Time(MetricsRegistry.TimeProductCounter);
                    _dbContext.AddAsync(product);
                    _metrics.Measure.Counter.Increment(MetricsRegistry.CreateProductsCounter);
                    _dbContext.SaveChanges();
                    scope.Complete();

                    return Task.FromResult(product);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Falha ao salvar produto", ex);
            }
        }
    }
}