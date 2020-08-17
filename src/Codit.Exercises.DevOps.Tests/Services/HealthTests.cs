using System.Net;
using System.Threading.Tasks;
using Arcus.Testing.Logging;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace Codit.Exercises.DevOps.Tests.Services
{
    [Trait("Category", "Integration")]
    [Trait("Category", "Smoke")]
    public class HealthTests
    {
        protected IConfiguration Configuration { get; }
        protected XunitTestLogger Logger { get; }

        public HealthTests(ITestOutputHelper testOutput)
        {
            Logger = new XunitTestLogger(testOutput);

            // The appsettings.local.json allows users to override (gitignored) settings locally for testing purposes
            Configuration = new ConfigurationBuilder()
                .AddJsonFile(path: "appsettings.json")
                .AddJsonFile(path: "appsettings.local.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        [Fact]
        public async Task Health_Get_ReturnsOk()
        {
            // Arrange
            var warehouseClient = new WarehouseClient(Configuration, Logger);

            // Act
            var response = await warehouseClient.GetHealthAsync();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
