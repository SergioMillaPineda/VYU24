using Moq;
using WorkersManagementAdminMode.Infrastructure.Contracts;
using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;
using WorkersManagementAdminMode.Library.Contracts.DTOs;
using WorkersManagementAdminMode.Library.Impl;
using WorkersManagementAdminMode.XCutting.Enums;

namespace WorkersManagementAdminMode.Testing.UnitTesting.WorkersManagementAdminMode.Library.Impl
{
    public class ITWorkerServiceUnitTests
    {
        [Fact]
        public void Register_WhenValidInputAndNoDbError_ReturnNoErrors()
        {
            //Arrange
            Mock<IITWorkersRepository> _mockITWorkersRepository = new();

            ITWorkerService sut = new(_mockITWorkersRepository.Object);

            string validLevel = "Junior";
            DateTime validBirthDate = DateTime.Now.AddYears(-20);
            RegisterITWorkerRqDTO input =
                new("", "", validBirthDate, 0, new List<string>(), validLevel);

            // Act
            RegisterITWorkerRsDTO result = sut.Register(input);

            //Assert
            Assert.NotNull(result.errors);
            Assert.False(result.errors.HasErrors);
        }

        [Fact]
        public void Register_WhenValidInputAndRepositoryCallThrowsException_ReturnDBError()
        {
            //Arrange
            Mock<IITWorkersRepository> _mockITWorkersRepository = new();
            _mockITWorkersRepository
                .Setup(x => x.Register(It.IsAny<ITWorkerEntity>()))
                .Throws(new Exception());

            ITWorkerService sut = new(_mockITWorkersRepository.Object);

            string validLevel = "Junior";
            DateTime validBirthDate = DateTime.Now.AddYears(-20);
            RegisterITWorkerRqDTO input =
                new("", "", validBirthDate, 0, new List<string>(), validLevel);

            // Act
            RegisterITWorkerRsDTO result = sut.Register(input);

            //Assert
            Assert.NotNull(result.errors);
            Assert.True(result.errors.HasErrors);
            Assert.Equal((int)RegisterITWorkerRsErrorsEnum.DBError, result.errors.ErrorCodes.First());
        }

        [Fact]
        public void Register_WhenInvalidLevel_ReturnInvalidLevelError()
        {
            //Arrange
            Mock<IITWorkersRepository> _mockITWorkersRepository = new();

            ITWorkerService sut = new(_mockITWorkersRepository.Object);

            string invalidLevel = "";
            DateTime validBirthDate = DateTime.Now.AddYears(-20);
            RegisterITWorkerRqDTO input =
                new("", "", validBirthDate, 0, new List<string>(), invalidLevel);

            // Act
            RegisterITWorkerRsDTO result = sut.Register(input);

            //Assert
            Assert.NotNull(result.errors);
            Assert.True(result.errors.HasErrors);
            Assert.Equal((int)RegisterITWorkerRsErrorsEnum.InvalidLevel, result.errors.ErrorCodes.First());
        }

        [Fact]
        public void Register_WhenCannotWork_ReturnCannotWorkError()
        {
            //Arrange
            Mock<IITWorkersRepository> _mockITWorkersRepository = new();

            ITWorkerService sut = new(_mockITWorkersRepository.Object);

            string validLevel = "Junior";
            DateTime invalidBirthDate = DateTime.Now;
            RegisterITWorkerRqDTO input =
                new("", "", invalidBirthDate, 0, new List<string>(), validLevel);

            // Act
            RegisterITWorkerRsDTO result = sut.Register(input);

            //Assert
            Assert.NotNull(result.errors);
            Assert.True(result.errors.HasErrors);
            Assert.Equal((int)RegisterITWorkerRsErrorsEnum.CannotWork, result.errors.ErrorCodes.First());
        }
    }
}
