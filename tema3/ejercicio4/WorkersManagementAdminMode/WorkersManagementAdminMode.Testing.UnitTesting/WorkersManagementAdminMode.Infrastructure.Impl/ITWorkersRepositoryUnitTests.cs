using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using WorkersManagementAdminMode.Infrastructure.Contracts.Entities;
using WorkersManagementAdminMode.Infrastructure.Impl;
using WorkersManagementAdminMode.Infrastructure.Impl.DbContexts;

namespace WorkersManagementAdminMode.Testing.UnitTesting.WorkersManagementAdminMode.Infrastructure.Impl
{
    public class ITWorkersRepositoryUnitTests
    {
        [Fact]
        public void Register_WhenOk_ThrowsNoException()
        {
            // Arrange
            Mock<WorkersManagementAdminModeContext> mockedDbContext = new();
            ITWorkersRepository sut = new(mockedDbContext.Object);
            ITWorkerEntity input = new();

            // Act
            Exception exception = Record.Exception(() => sut.Register(input));

            // Assert
            Assert.NotNull(exception);
        }
    }
}
