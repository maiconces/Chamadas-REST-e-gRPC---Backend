using ProductMicroservice.Models;
using System.Threading.Tasks;

namespace ProductMicroservice.Repository
{
    public interface IProductRepository
    {
       Task<Product> PostAsync(Product product);
    }
}