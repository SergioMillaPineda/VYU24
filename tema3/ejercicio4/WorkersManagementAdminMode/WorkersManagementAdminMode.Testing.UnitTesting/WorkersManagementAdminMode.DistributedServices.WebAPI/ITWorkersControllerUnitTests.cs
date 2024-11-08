using Microsoft.AspNetCore.Mvc;
using Moq;
using WorkersManagementAdminMode.DistributedServices.WebAPI.Controllers;
using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;
using WorkersManagementAdminMode.Infrastructure.Contracts;
using WorkersManagementAdminMode.Library.Contracts;
using WorkersManagementAdminMode.Library.Contracts.DTOs;
using WorkersManagementAdminMode.Library.Impl;
using WorkersManagementAdminMode.XCutting.Enums;
using Microsoft.AspNetCore.Http;

namespace WorkersManagementAdminMode.Testing.UnitTesting.WorkersManagementAdminMode.DistributedServices.WebAPI
{
    public class ITWorkersControllerUnitTests
    {
        [Fact]
        public void Register_WhenValidInputAndNoDbError_ReturnNoErrors()
        {
            //Arrange
            RegisterITWorkerRsDTO serviceResponse = new()
            {
                itWorker = new(1, "", "", DateTime.Now, 0, new List<string>(), "")
            };
            Mock<IITWorkerService> _mockITWorkersService = new();
            _mockITWorkersService
                .Setup(x => x.Register(It.IsAny<RegisterITWorkerRqDTO>()))
                .Returns(serviceResponse);

            ITWorkersController sut = new(_mockITWorkersService.Object);

            string validLevel = "Junior";
            DateTime validBirthDate = DateTime.Now.AddYears(-20);
            RegisterITWorkerRqDTO input =
                new("", "", validBirthDate, 0, new List<string>(), validLevel);

            // Act
            IActionResult result = sut.Register(input);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Register_WhenValidInputAndServiceReturnsDBError_ReturnDBError()
        {
            //Arrange
            RegisterITWorkerRsDTO serviceResponse = new()
            {
                errors = new()
                {
                    ErrorCodes = new List<int> { (int)RegisterITWorkerRsErrorsEnum.DBError }
                }
            };
            Mock<IITWorkerService> _mockITWorkersService = new();
            _mockITWorkersService
                .Setup(x => x.Register(It.IsAny<RegisterITWorkerRqDTO>()))
                .Returns(serviceResponse);

            ITWorkersController sut = new(_mockITWorkersService.Object);

            RegisterITWorkerRqDTO input =
                new("", "", DateTime.Now, 0, new List<string>(), "");

            // Act
            IActionResult result = sut.Register(input);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, ((ObjectResult)result).StatusCode);
            Assert.Equal("Error accessing Database", ((ObjectResult)result).Value);
        }

        [Fact]
        public void Register_WhenValidInputAndServiceReturnsInvalidLevel_ReturnInvalidLevel()
        {
            //Arrange
            RegisterITWorkerRsDTO serviceResponse = new()
            {
                errors = new()
                {
                    ErrorCodes = new List<int> { (int)RegisterITWorkerRsErrorsEnum.InvalidLevel }
                }
            };
            Mock<IITWorkerService> _mockITWorkersService = new();
            _mockITWorkersService
                .Setup(x => x.Register(It.IsAny<RegisterITWorkerRqDTO>()))
                .Returns(serviceResponse);

            ITWorkersController sut = new(_mockITWorkersService.Object);

            RegisterITWorkerRqDTO input =
                new("", "", DateTime.Now, 0, new List<string>(), "");

            // Act
            IActionResult result = sut.Register(input);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status406NotAcceptable, ((ObjectResult)result).StatusCode);
            Assert.Equal("Level field has invalid value. Valid values: [Junior, Middle, Senior]", ((ObjectResult)result).Value);
        }

        [Fact]
        public void Register_WhenValidInputAndServiceReturnsCannotWork_ReturnCannotWork()
        {
            //Arrange
            RegisterITWorkerRsDTO serviceResponse = new()
            {
                errors = new()
                {
                    ErrorCodes = new List<int> { (int)RegisterITWorkerRsErrorsEnum.CannotWork }
                }
            };
            Mock<IITWorkerService> _mockITWorkersService = new();
            _mockITWorkersService
                .Setup(x => x.Register(It.IsAny<RegisterITWorkerRqDTO>()))
                .Returns(serviceResponse);

            ITWorkersController sut = new(_mockITWorkersService.Object);

            RegisterITWorkerRqDTO input =
                new("", "", DateTime.Now, 0, new List<string>(), "");

            // Act
            IActionResult result = sut.Register(input);

            //Assert
            Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status406NotAcceptable, ((ObjectResult)result).StatusCode);
            Assert.Equal("ITWorker has to be at least 18 years old to be allowed to work", ((ObjectResult)result).Value);
        }
    }
}
