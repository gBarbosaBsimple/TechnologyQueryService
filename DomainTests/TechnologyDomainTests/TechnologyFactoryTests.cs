using Domain.Factory;
using Domain.Visitor;
using Moq;
using Xunit;

namespace Domain.Tests.TechnologyDomainTests
{
    public class TechnologyFactoryTests
    {
        [Fact]
        public void WhenCreatingTechnologyWithValidFieldsAndId_ThenTechnologyIsCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var description = "Entity Framework Core";
            var factory = new TechnologyFactory();

            // Act
            var tech = factory.Create(id, description);

            // Assert
            Assert.NotNull(tech);
            Assert.Equal(id, tech.Id);
            Assert.Equal(description, tech.Description);
        }

        [Fact]
        public void WhenCreatingTechnologyFromVisitor_ThenTechnologyIsCreated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var description = "SignalR";
            var factory = new TechnologyFactory();

            var visitorMock = new Mock<ITechnologyVisitor>();
            visitorMock.Setup(v => v.Id).Returns(id);
            visitorMock.Setup(v => v.Description).Returns(description);

            // Act
            var tech = factory.Create(visitorMock.Object);

            // Assert
            Assert.NotNull(tech);
            Assert.Equal(id, tech.Id);
            Assert.Equal(description, tech.Description);
        }
    }
}
