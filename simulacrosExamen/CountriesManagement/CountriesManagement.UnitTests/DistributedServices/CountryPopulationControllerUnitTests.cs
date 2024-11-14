using CountriesManagement.DistributedServices.WebApi.Controllers;
using CountriesManagement.Domain;
using CountriesManagement.Enums;
using CountriesManagement.Library.Contracts;
using CountriesManagement.Library.Contracts.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CountriesManagement.UnitTests.DistributedServices
{
    public class CountryPopulationControllerUnitTests
    {
        const char VALIDCOUNTRYINITIAL = CommonTestData.VALIDCOUNTRYINITIAL;
        const char INVALIDCOUNTRYINITIAL = CommonTestData.INVALIDCOUNTRYINITIAL;

        const int VALIDYEAR = CommonTestData.VALIDYEAR;
        const int INVALIDYEAR = CommonTestData.INVALIDYEAR;

        [Fact]
        public async Task GetByInitialAndYear_WhenNoErrors_ReturnOk()
        {
            // Arrange
            string NAMEFORVALIDCOUNTRY = $"{VALIDCOUNTRYINITIAL}";
            const int POPULATIONFORVALIDCOUNTRY = 1;
            Mock<ICountryPopulationService> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByInitialAndYear(It.IsAny<QueryDataDto>()))
                .ReturnsAsync(new GetPopByInitialAndYearRsDto
                {
                    data = new List<CountryYearPopulationDto>
                    {
                        new(NAMEFORVALIDCOUNTRY, VALIDYEAR, POPULATIONFORVALIDCOUNTRY)
                    }
                });
            CountryPopulationController sut = new(mockedRepository.Object);
            QueryDataDto rqDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
            };

            // Act
            IActionResult result = await sut.GetByInitialAndYear(rqDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetByInitialAndYear_WhenInvalidCountryInitial_ReturnBadRequest()
        {
            // Arrange
            Mock<ICountryPopulationService> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByInitialAndYear(It.IsAny<QueryDataDto>()))
                .Returns(Task.FromResult(new GetPopByInitialAndYearRsDto
                {
                    errors = new List<GetPopByInitialAndYearErrorEnum>
                    {
                        GetPopByInitialAndYearErrorEnum.InvalidInitial
                    }
                }));
            CountryPopulationController sut = new(mockedRepository.Object);
            QueryDataDto rqDto = new()
            {
                CountryInitial = INVALIDCOUNTRYINITIAL,
                Year = VALIDYEAR
            };

            // Act
            IActionResult result = await sut.GetByInitialAndYear(rqDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<GetPopByInitialAndYearErrorEnum>>(((BadRequestObjectResult)result).Value);
            Assert.Single((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.InvalidInitial,
                ((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!)[0]);
        }

        [Fact]
        public async Task GetByInitialAndYear_WhenInvalidYear_ReturnBadRequest()
        {
            // Arrange
            Mock<ICountryPopulationService> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByInitialAndYear(It.IsAny<QueryDataDto>()))
                .Returns(Task.FromResult(new GetPopByInitialAndYearRsDto
                {
                    errors = new List<GetPopByInitialAndYearErrorEnum>
                    {
                        GetPopByInitialAndYearErrorEnum.InvalidYear
                    }
                }));
            CountryPopulationController sut = new(mockedRepository.Object);
            QueryDataDto rqDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = INVALIDYEAR
            };

            // Act
            IActionResult result = await sut.GetByInitialAndYear(rqDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<GetPopByInitialAndYearErrorEnum>>(((BadRequestObjectResult)result).Value);
            Assert.Single((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.InvalidYear,
                ((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!)[0]);
        }

        [Fact]
        public async Task GetByInitialAndYear_WhenDbError_ReturnBadRequest()
        {
            // Arrange
            Mock<ICountryPopulationService> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByInitialAndYear(It.IsAny<QueryDataDto>()))
                .Returns(Task.FromResult(new GetPopByInitialAndYearRsDto
                {
                    errors = new List<GetPopByInitialAndYearErrorEnum>
                    {
                        GetPopByInitialAndYearErrorEnum.DBError
                    }
                }));
            CountryPopulationController sut = new(mockedRepository.Object);
            QueryDataDto rqDto = new()
            {
                CountryInitial = VALIDCOUNTRYINITIAL,
                Year = INVALIDYEAR
            };

            // Act
            IActionResult result = await sut.GetByInitialAndYear(rqDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<GetPopByInitialAndYearErrorEnum>>(((BadRequestObjectResult)result).Value);
            Assert.Single((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!);
            Assert.Equal(GetPopByInitialAndYearErrorEnum.DBError,
                ((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!)[0]);
        }

        [Fact]
        public async Task GetByInitialAndYear_WhenInvalidCountryInitialAndYear_ReturnBadRequest()
        {
            // Arrange
            Mock<ICountryPopulationService> mockedRepository = new();
            mockedRepository
                .Setup(x => x.GetPopulationByInitialAndYear(It.IsAny<QueryDataDto>()))
                .Returns(Task.FromResult(new GetPopByInitialAndYearRsDto
                {
                    errors = new List<GetPopByInitialAndYearErrorEnum>
                    {
                        GetPopByInitialAndYearErrorEnum.InvalidInitial,
                        GetPopByInitialAndYearErrorEnum.InvalidYear,
                    }
                }));
            CountryPopulationController sut = new(mockedRepository.Object);
            QueryDataDto rqDto = new()
            {
                CountryInitial = INVALIDCOUNTRYINITIAL,
                Year = INVALIDYEAR
            };

            // Act
            IActionResult result = await sut.GetByInitialAndYear(rqDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<List<GetPopByInitialAndYearErrorEnum>>(((BadRequestObjectResult)result).Value);
            Assert.Equal(2, ((List<GetPopByInitialAndYearErrorEnum>)((BadRequestObjectResult)result).Value!).Count);
        }
    }
}
