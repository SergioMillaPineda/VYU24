using CountriesManagement.Domain;
using System.ComponentModel.DataAnnotations;

namespace CountriesManagement.UnitTests.Domain
{
    public class QueryDataUnitTests
    {
        const char VALIDCOUNTRYINITIAL = CommonTestData.VALIDCOUNTRYINITIAL;
        const char INVALIDCOUNTRYINITIAL = CommonTestData.INVALIDCOUNTRYINITIAL;

        const int VALIDYEAR = CommonTestData.VALIDYEAR;
        const int INVALIDYEAR = CommonTestData.INVALIDYEAR;

        [Fact]
        public void Validate_WhenNoErrors_ReturnsNoErrors()
        {
            // Arrange
            QueryData sut = new(VALIDCOUNTRYINITIAL, VALIDYEAR);

            // Act
            List<ValidationResult>? result = sut.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void Validate_WhenInvalidCountryInitial_ReturnsInvalidCountryInitialError()
        {
            // Arrange
            QueryData sut = new(INVALIDCOUNTRYINITIAL, VALIDYEAR);

            // Act
            List<ValidationResult>? result = sut.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("The field CountryInitial must be in [a-zA-Z]", result[0].ErrorMessage);
        }

        [Fact]
        public void Validate_WhenInvalidYear_ReturnsInvalidYearError()
        {
            // Arrange
            QueryData sut = new(VALIDCOUNTRYINITIAL, INVALIDYEAR);

            // Act
            List<ValidationResult>? result = sut.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("The field Year must be between 1961 and 2018.", result[0].ErrorMessage);
        }

        [Fact]
        public void Validate_WhenInvalidCountryInitialAndYear_ReturnsInvalidCountryInitialAndYearErrors()
        {
            // Arrange
            QueryData sut = new(INVALIDCOUNTRYINITIAL, INVALIDYEAR);

            // Act
            List<ValidationResult>? result = sut.Validate();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }
    }
}
