using System.Collections.Generic;
using System.Threading.Tasks;
using Codit.Exercises.DevSecOps.Core.Model;

namespace Codit.Exercises.DevSecOps.Core.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task AddAsync(Product product);
    }
}