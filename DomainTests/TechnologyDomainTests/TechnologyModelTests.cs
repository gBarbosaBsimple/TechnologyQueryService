using Domain.Models;
using Xunit;

namespace Domain.Tests.TechnologyDomainTests
{
    public class TechnologyModelTests
    {
        [Fact]
        public void WhenCreatingTechnologyWithValidIdAndDescription_ThenTechnologyIsCreatedCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var description = "ASP.NET Core";

            // Act
            var technology = new Technology(id, description);

            // Assert
            Assert.NotNull(technology);
            Assert.Equal(id, technology.Id);
            Assert.Equal(description, technology.Description);
        }

        [Fact]
        public void WhenCreatingTechnologyWithEmptyConstructor_ThenPropertiesAreDefault()
        {
            // Act
            var technology = new Technology();

            // Assert
            Assert.Equal(Guid.Empty, technology.Id);
            Assert.Null(technology.Description);
        }
    }
}