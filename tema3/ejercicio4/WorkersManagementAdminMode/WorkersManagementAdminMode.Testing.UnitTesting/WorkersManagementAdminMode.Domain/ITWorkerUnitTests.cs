using WorkersManagementAdminMode.Domain.DomainModels;

namespace WorkersManagementAdminMode.Testing.UnitTesting.WorkersManagementAdminMode.Domain
{
    public class ITWorkerUnitTests
    {
        private ITWorker CreateITWorkerByLevel(string levelToTest)
        {
            return new("", "", DateTime.Now, 0, new List<string>(), levelToTest);
        }
        private ITWorker CreateITWorkerByYearsOfExperience(int yearsOfExperienceToTest)
        {
            return new("", "", DateTime.Now, yearsOfExperienceToTest, new List<string>(), "");
        }
        private ITWorker CreateITWorkerByBirthDate(DateTime birthDate)
        {
            return new("", "", birthDate, 0, new List<string>(), "");
        }

        #region IsValidLevel
        [Fact]
        public void IsValidLevel_WhenInvalidInput_ReturnFalse()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByLevel("");

            // Act
            bool result = sut.IsValidLevel;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValidLevel_WhenValidInput_ReturnTrue()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByLevel("Senior");

            // Act
            bool result = sut.IsValidLevel;

            // Assert
            Assert.True(result);
        }
        #endregion

        #region CanBeAssignedAsTeamManager
        [Fact]
        public void CanBeAssignedAsTeamManager_WhenInvalidInput_ReturnFalse()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByLevel("");

            // Act
            bool result = sut.CanBeAssignedAsTeamManager;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeAssignedAsTeamManager_WhenValidInput_ReturnTrue()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByLevel("Senior");

            // Act
            bool result = sut.CanBeAssignedAsTeamManager;

            // Assert
            Assert.True(result);
        }
        #endregion

        #region CanBeConsideredSenior
        [Fact]
        public void CanBeConsideredSenior_WhenInvalidInput_ReturnFalse()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByYearsOfExperience(0);

            // Act
            bool result = sut.CanBeConsideredSenior;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanBeConsideredSenior_WhenValidInput_ReturnTrue()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByYearsOfExperience(5);

            // Act
            bool result = sut.CanBeConsideredSenior;

            // Assert
            Assert.True(result);
        }
        #endregion

        #region CanWork
        [Fact]
        public void CanWork_WhenInvalidInput_ReturnFalse()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByBirthDate(DateTime.Now.AddYears(-10));

            // Act
            bool result = sut.CanWork;

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CanWork_WhenValidInput_ReturnTrue()
        {
            // Arrange
            ITWorker sut = CreateITWorkerByBirthDate(DateTime.Now.AddYears(-20));

            // Act
            bool result = sut.CanWork;

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}
