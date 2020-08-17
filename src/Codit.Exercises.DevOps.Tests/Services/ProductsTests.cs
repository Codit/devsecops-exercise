using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bogus;
using Codit.Exercises.DevOps.Tests.Extensions;
using Codit.Exercises.DevSecOps.Core.Model;
using Xunit;
using Xunit.Abstractions;

namespace Codit.Exercises.DevOps.Tests.Services
{
    public class ProductsTests : IntegrationTest
    {
        public ProductsTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public async Task Products_GetAll_ReturnsOk()
        {
            // Arrange
            var warehouseClient = new WarehouseClient(Configuration, Logger);

            // Act
            var response = await warehouseClient.GetAllProductsAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var products = response.Content.ReadAsAsync<List<Product>>();
            Assert.NotNull(products);
        }

        [Fact]
        public async Task Products_AddNewProduct_ReturnsCreated()
        {
            // Arrange
            var warehouseClient = new WarehouseClient(Configuration, Logger);
            var initialProducts = await GetAllProductsAsync(warehouseClient);
            var newProduct = GenerateNewProduct();

            // Act
            var response = await warehouseClient.NewProductAsync(newProduct);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var newProducts = await GetAllProductsAsync(warehouseClient);
            Assert.Contains(newProducts, p => p.Name.Equals(newProduct.Name, StringComparison.InvariantCultureIgnoreCase));
            Assert.True(newProducts.Count == initialProducts.Count + 1);
        }

        [Fact]
        public async Task Products_AddNewProductWithConflictingName_ReturnsConflict()
        {
            // Arrange
            var warehouseClient = new WarehouseClient(Configuration, Logger);
            var initialProducts = await GetAllProductsAsync(warehouseClient);
            Assert.True(initialProducts.Any());

            // Act
            var response = await warehouseClient.NewProductAsync(initialProducts.First());

            // Assert
            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        }

        private Product GenerateNewProduct()
        {
            var product = new Faker<Product>()
                .RuleFor(p => p.Name, faker => faker.Name.FullName())
                .RuleFor(p => p.Description, faker => faker.Lorem.Paragraph(1))
                .RuleFor(p => p.Price, faker => double.Parse(faker.Commerce.Price()))
                .Generate();

            return product;
        }

        private static async Task<List<Product>> GetAllProductsAsync(WarehouseClient warehouseClient)
        {
            var initialProductResponse = await warehouseClient.GetAllProductsAsync();
            initialProductResponse.EnsureSuccessStatusCode();
            var initialProducts = await initialProductResponse.Content.ReadAsAsync<List<Product>>();
            return initialProducts;
        }
    }
}