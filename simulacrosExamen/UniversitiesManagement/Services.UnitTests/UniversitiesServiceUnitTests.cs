using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text.Json;
using UniversitiesManagement.Enums;
using UniversitiesManagement.Infrastructure.Contracts;
using UniversitiesManagement.Infrastructure.Contracts.DbEntities;
using UniversitiesManagement.Infrastructure.Contracts.WebApiEntities;
using UniversitiesManagement.Services.Contracts.Dtos;
using UniversitiesManagement.Services.Impl;

namespace Services.UnitTests
{
    public class UniversitiesServiceUnitTests
    {
        #region MigrateAllAsync
        [Fact]
        public async void MigrateAllAsync_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            List<UniversityWebApiEntity> simulatedWebApiResponse = new();
            Mock<IUniversitiesWebApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(simulatedWebApiResponse);
            Mock<IUniversitiesDbRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.Null(result.errors);
        }

        [Fact]
        public async void MigrateAllAsync_WhenApiConnectionError_ReturnApiConnectionError()
        {
            // Arrange
            Mock<IUniversitiesWebApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .Throws<HttpRequestException>();
            Mock<IUniversitiesDbRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiConnectionError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDeserializationThrowsException_ReturnDeserializationExceptionError()
        {
            // Arrange
            Mock<IUniversitiesWebApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .Throws<JsonException>();
            Mock<IUniversitiesDbRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiDataDeserializationExceptionError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDeserializationReturnsNull_ReturnDeserializationNullError()
        {
            // Arrange
            Mock<IUniversitiesWebApiRepository> mockedWebApiRepository = new();
            Mock<IUniversitiesDbRepository> mockedDbRepository = new();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.WebApiDataDeserializationReturnsNullError, result.errors[0]);
        }

        [Fact]
        public async void MigrateAllAsync_WhenDbConnectionError_ReturnDbConnectionError()
        {
            // Arrange
            List<UniversityWebApiEntity> simulatedWebApiResponse = new();
            Mock<IUniversitiesWebApiRepository> mockedWebApiRepository = new();
            mockedWebApiRepository
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(simulatedWebApiResponse);

            Mock<IUniversitiesDbRepository> mockedDbRepository = new();
            mockedDbRepository
                .Setup(x => x.SaveAll(It.IsAny<List<UniversitiesDbTableRow>>()))
                .Throws<DbUpdateException>();

            UniversitiesService sut = new(
                mockedWebApiRepository.Object,
                mockedDbRepository.Object);

            // Act
            MigrateAllRsDto result = await sut.MigrateAllAsync();

            // Assert
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(ErrorsEnum.DbSaveError, result.errors[0]);
        }
        #endregion
    }
}
