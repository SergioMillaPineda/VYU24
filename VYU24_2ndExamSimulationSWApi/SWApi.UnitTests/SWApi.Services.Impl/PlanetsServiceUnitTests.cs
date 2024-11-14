using Moq;
using SWApi.Enums;
using SWApi.Infrastructure.Contracts;
using SWApi.Infrastructure.Contracts.Entities.SWApi;
using SWApi.Infrastructure.Contracts.Entities.SWDB;
using SWApi.Services.Contracts.Dtos;
using SWApi.Services.Impl;

namespace SWApi.UnitTests.SWApi.Services.Impl
{
    public class PlanetsServiceUnitTests
    {
        [Fact]
        public async void RefreshPlanets_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            Mock<ISWApiPlanetsRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetStatisticsSWApiEntity>());

            Mock<ISWDBPlanetsRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshPlanetsRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
        }

        [Fact]
        public async void RefreshPlanets_WhenSWApiError_ReturnSWApiError()
        {
            // Arrange
            Mock<ISWApiPlanetsRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ThrowsAsync(new Exception());

            Mock<ISWDBPlanetsRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshPlanetsRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshPlanetsErrorEnum.SWApiErrorConnection, result.errors[0]);
        }

        [Fact]
        public async void RefreshPlanets_WhenMappingError_ReturnMappingError()
        {
            // Arrange
            Mock<ISWApiPlanetsRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetStatisticsSWApiEntity>
                {
                    new()
                    {
                        PlanetName = "",
                        RotationPeriod = 0,
                        OrbitalPeriod = 0,
                        Climate = "",
                        Population = "",
                        DetailedInfoUrl = ""
                    }
                });

            Mock<ISWDBPlanetsRepository> mockedSWDBPlanetsRepository = new();

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshPlanetsRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshPlanetsErrorEnum.EntityMappingConnection, result.errors[0]);
        }

        [Fact]
        public async void RefreshPlanets_WhenSWDBError_ReturnSWDBError()
        {
            // Arrange
            Mock<ISWApiPlanetsRepository> mockedSWApiPlanetsRepository = new();
            mockedSWApiPlanetsRepository
                .Setup(x => x.GetAll())
                .ReturnsAsync(new List<PlanetStatisticsSWApiEntity>());

            Mock<ISWDBPlanetsRepository> mockedSWDBPlanetsRepository = new();
            mockedSWDBPlanetsRepository
                .Setup(x => x.InsertOrUpdate(It.IsAny<List<PlanetsTableRowEntity>>()))
                .Throws(new Exception());

            PlanetsService sut = new(
                mockedSWApiPlanetsRepository.Object,
                mockedSWDBPlanetsRepository.Object);

            // Act
            RefreshPlanetsRsDto result = await sut.RefreshPlanets();

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(RefreshPlanetsErrorEnum.SWDbErrorConnection, result.errors[0]);
        }
    }
}
