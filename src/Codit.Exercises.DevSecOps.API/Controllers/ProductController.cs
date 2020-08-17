using System.Net;
using System.Threading.Tasks;
using Codit.Exercises.DevSecOps.Core.Model;
using Codit.Exercises.DevSecOps.Core.Repositories;
using Codit.Exercises.DevSecOps.Data.InMemory.Exceptions;
using GuardNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Filters;

namespace Codit.Exercises.DevSecOps.API.Controllers
{
    /// <summary>
    /// API endpoint to explore and manage products in the warehouse.
    /// </summary>
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productRepository">Repository providing information about products in our warehouse.</param>
        public ProductController(IProductRepository productRepository)
        {
            Guard.NotNull(productRepository, nameof(productRepository));

            _productRepository = productRepository;
        }

        /// <summary>
        ///     Get Products
        /// </summary>
        /// <remarks>Provides an overview of the products in our warehouse.</remarks>
        /// <response code="200">Product information is provided</response>
        /// <response code="500">We're expecting failures in our backend</response>
        [HttpGet(Name = "Product_Get")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [SwaggerResponseHeader(new[] { 200, 500 }, "RequestId", "string","The header that has a request ID that uniquely identifies this operation call")]
        [SwaggerResponseHeader(new[] { 200, 500 }, "X-Transaction-Id", "string","The header that has the transaction ID is used to correlate multiple operation calls.")]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAsync();

            return Ok(products);
        }

        /// <summary>
        ///     New Product
        /// </summary>
        /// <remarks>Introduces a new product in our warehouse.</remarks>
        /// <response code="201">New product is persisted</response>
        /// <response code="409">Product with same name already exists</response>
        /// <response code="500">We're expecting failures in our backend</response>
        [HttpPost(Name = "Product_New")]
        [SwaggerResponseHeader(new[] { 201, 409, 500 }, "RequestId", "string", "The header that has a request ID that uniquely identifies this operation call")]
        [SwaggerResponseHeader(new[] { 201, 409, 500 }, "X-Transaction-Id", "string", "The header that has the transaction ID is used to correlate multiple operation calls.")]
        public async Task<IActionResult> New([FromBody] Product product)
        {
            try
            {
                await _productRepository.AddAsync(product);

                return StatusCode((int) HttpStatusCode.Created);
            }
            catch (ConflictException)
            {
                return Conflict();
            }
        }
    }
}