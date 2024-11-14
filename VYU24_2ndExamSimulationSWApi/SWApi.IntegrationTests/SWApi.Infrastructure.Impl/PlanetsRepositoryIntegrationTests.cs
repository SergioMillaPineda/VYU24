using Microsoft.Extensions.Configuration;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Impl;
using SWApi.Infrastructure.Impl.ExternalConnections;

namespace SWApi.IntegrationTests.SWApi.Infrastructure.Impl
{
    public class PlanetsRepositoryIntegrationTests
    {
        [Fact]
        public async void GetAll_ReturnsOk()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            SWApiPlanetsRepository sut = new SWApiPlanetsRepository(configuration, new SWApiContext(configuration));

            // Act
            List<PlanetStatisticsSWApiEntity> result = await sut.GetAll();

            // Assert
            Assert.NotNull(result);
        }
    }
}
