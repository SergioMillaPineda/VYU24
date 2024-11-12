using CountriesManagement.Enums;
using CountriesManagement.Infrastructure.Contracts;
using CountriesManagement.Infrastructure.Contracts.Entities;
using CountriesManagement.Library.Contracts.DTOs;
using CountriesManagement.Library.Impl;
using Moq;

namespace CountriesManagement.UnitTests.Library
{
    public class CountryPopulationServiceUnitTests
    {
        const char VALIDCOUNTRYINITIAL = CommonTestData.VALIDCOUNTRYINITIAL;
        const char INVALIDCOUNTRYINITIAL = CommonTestData.INVALIDCOUNTRYINITIAL;

        const int VALIDYEAR = CommonTestData.VALIDYEAR;
        const int INVALIDYEAR = CommonTestData.INVALIDYEAR;

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenNoErrors_ReturnNoErrors()
        {
            // Arrange
            const int POPULATIONFORVALIDCOUNTRY = 1;
            string NAMEFORVALIDCOUNTRY = $"{VALIDCOUNTRYINITIAL}";
            Mock<ICountryPopulationRepository> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationForAllCountries())
                .Returns(Task.FromResult<AllCountriesPopulationEntity?>(new AllCountriesPopulationEntity
                {
                    Title = "Title",
                    Error = false,
                    Data = new List<CountryPopulationEntity>()
                    {
                        new() {
                            Code = "",
                            Country = NAMEFORVALIDCOUNTRY,
                            Iso3 = "",
                            PopulationListByYear = new List<PopulationByYearEntity>
                            {
                                new() {
                                    Year = VALIDYEAR,
                                    Value = POPULATIONFORVALIDCOUNTRY
                                }
                            }
                        }
                    }
                }));
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                countryInitial = VALIDCOUNTRYINITIAL,
                year = VALIDYEAR
            };

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
            Assert.Single(result.data);
            Assert.Equal(NAMEFORVALIDCOUNTRY, result.data[0].Country);
            Assert.Equal(VALIDYEAR, result.data[0].Year);
            Assert.Equal(POPULATIONFORVALIDCOUNTRY, result.data[0].Population);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenInvalidCountryInitial_ReturnCountryInitialError()
        {
            // Arrange
            Mock<ICountryPopulationRepository> mockedRepository = new();
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                countryInitial = INVALIDCOUNTRYINITIAL,
                year = VALIDYEAR
            };

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.InvalidInitial, result.errors[0]);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenInvalidYear_ReturnYearError()
        {
            // Arrange
            Mock<ICountryPopulationRepository> mockedRepository = new();
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                countryInitial = VALIDCOUNTRYINITIAL,
                year = INVALIDYEAR
            };

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.InvalidYear, result.errors[0]);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenInvalidCountryInitialAndYear_ReturnCountryInitialAndYearError()
        {
            // Arrange
            Mock<ICountryPopulationRepository> mockedRepository = new();
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                countryInitial = INVALIDCOUNTRYINITIAL,
                year = INVALIDYEAR
            };

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Equal(2, result.errors.Count);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenDbError_ReturnDbError()
        {
            // Arrange
            Mock<ICountryPopulationRepository> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationForAllCountries())
                .Throws<Exception>();
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                countryInitial = VALIDCOUNTRYINITIAL,
                year = VALIDYEAR
            };

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.DBError, result.errors[0]);
        }
    }
}
