using Moq;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Contracts;
using SWApi.Services.Contracts.Dtos;
using SWApi.Services.Impl;
using SWApi.Infrastructure.Contracts.Entities.SWDB;

namespace SWApi.UnitTests.SWApi.Services.Impl
{
    public class ResidentsServiceUnitTests
    {
        [Fact]
        public async void GetResidentsByPlanetName_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            Mock<ISWDBPlanetsRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.TryGet(It.IsAny<string>()))
                .Returns(new PlanetsTableRowEntity());

            Mock<ISWApiPlanetsRepository> mockedSWDBPlanetsRepository = new();
            mockedSWDBPlanetsRepository
                .Setup(x => x.TryGetPlanetResidentListByPlanetUrl(It.IsAny<string>()))
                .ReturnsAsync(new PlanetResidentListSWApiEntity());

            Mock<ISWApiPeopleRepository> mockedSWApiPeopleRepository = new();
            mockedSWApiPeopleRepository
                .Setup(x => x.TryGetByUrl(It.IsAny<string>()))
                .ReturnsAsync(new PeopleSWApiEntity());

            ResidentsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object,
                mockedSWApiPeopleRepository.Object);

            const string VALIDPLANET = "Tatooine";

            // Act
            GetResidentsByPlanetNameRsDto result = await sut.GetResidentsByPlanetName(VALIDPLANET);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
        }
    }
}
