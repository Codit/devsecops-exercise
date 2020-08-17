using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codit.Exercises.DevSecOps.Core.Model;
using Codit.Exercises.DevSecOps.Core.Repositories;
using Codit.Exercises.DevSecOps.Data.InMemory.Exceptions;

namespace Codit.Exercises.DevSecOps.Data.InMemory
{
    public class InMemoryProductRepository: IProductRepository
    {
        private readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Name = "Xbox One X",
                Description = "Experience the new generation of games and entertainment with Xbox",
                Price = 399.00
            },
            new Product
            {
                Name = "Xbox One S",
                Description = "Experience the new generation of games and entertainment with Xbox",
                Price = 229.99
            }
    };

        public Task<List<Product>> GetAsync()
        {
            return Task.FromResult(_products);
        }

        public Task AddAsync(Product product)
        {
            if (_products.Any(p => p.Name.Equals(product.Name, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ConflictException();
            }

            _products.Add(product);

            return Task.CompletedTask;
        }
    }
}
