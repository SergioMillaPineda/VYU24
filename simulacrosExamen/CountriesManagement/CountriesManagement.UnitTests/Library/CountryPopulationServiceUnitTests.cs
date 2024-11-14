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
            QueryDataDto queryDataDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
            };
            const int POPULATIONFORVALIDCOUNTRY = 1;
            string NAMEFORVALIDCOUNTRY = $"{VALIDCOUNTRYINITIAL}";
            Mock<ICountryPopulationRepository> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByCountryInitialAndYear(VALIDCOUNTRYINITIAL, VALIDYEAR))
                .Returns(Task.FromResult<AllCountriesPopulationEntity?>(new AllCountriesPopulationEntity
                {
                    Error = false,
                    Data = new List<CountryPopulationEntity>()
                    {
                        new() {
                            CountryName = NAMEFORVALIDCOUNTRY,
                            PopulationListByYear = new List<PopulationByYearEntity>
                            {
                                new() {
                                    Year = VALIDYEAR,
                                    Population = POPULATIONFORVALIDCOUNTRY
                                }
                            }
                        }
                    }
                }));
            CountryPopulationService sut = new(mockedRepository.Object);

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.errors);
            Assert.NotNull(result.data);
            Assert.Single(result.data);
            Assert.Equal(NAMEFORVALIDCOUNTRY, result.data[0].country);
            Assert.Equal(VALIDYEAR, result.data[0].year);
            Assert.Equal(POPULATIONFORVALIDCOUNTRY, result.data[0].population);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenInvalidCountryInitial_ReturnCountryInitialError()
        {
            // Arrange
            Mock<ICountryPopulationRepository> mockedRepository = new();
            CountryPopulationService sut = new(mockedRepository.Object);
            QueryDataDto queryDataDto = new()
            {
                CountryInitial = INVALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
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
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = INVALIDYEAR
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
                CountryInitial = INVALIDCOUNTRYINITIAL,
                Year = INVALIDYEAR
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
        public async Task GetPopulationByInitialAndYear_WhenJsonReturnedErrorTrue_ReturnJsonReturnedErrorTrueError()
        {
            // Arrange
            QueryDataDto queryDataDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
            };
            string NAMEFORVALIDCOUNTRY = $"{VALIDCOUNTRYINITIAL}";
            Mock<ICountryPopulationRepository> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByCountryInitialAndYear(VALIDCOUNTRYINITIAL, VALIDYEAR))
                .Returns(Task.FromResult<AllCountriesPopulationEntity?>(new AllCountriesPopulationEntity
                {
                    Error = true,
                    Data = null
                }));
            CountryPopulationService sut = new(mockedRepository.Object);

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.WebApiReturnedErrorTrue, result.errors[0]);
        }

        [Fact]
        public async Task GetPopulationByInitialAndYear_WhenDbError_ReturnDbError()
        {
            // Arrange
            QueryDataDto queryDataDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
            };
            Mock<ICountryPopulationRepository> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByCountryInitialAndYear(VALIDCOUNTRYINITIAL, VALIDYEAR))
                .Throws<Exception>();
            CountryPopulationService sut = new(mockedRepository.Object);

            // Act
            GetPopByInitialAndYearRsDto? result = await sut.GetPopulationByInitialAndYear(queryDataDto);
            //Exception ex = await Record.ExceptionAsync(async () => await sut.GetPopulationByInitialAndYear(queryDataDto));

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.data);
            Assert.NotNull(result.errors);
            Assert.Single(result.errors);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.DBError, result.errors[0]);
        }
    }
}
