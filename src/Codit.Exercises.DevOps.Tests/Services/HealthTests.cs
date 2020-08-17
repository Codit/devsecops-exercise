using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Codit.Exercises.DevOps.Tests.Services
{
    [Trait("Category", "Integration")]
    [Trait("Category", "Smoke")]
    public class HealthTests : IntegrationTest
    {
        public HealthTests(ITestOutputHelper testOutput)
            : base(testOutput)
        {
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
