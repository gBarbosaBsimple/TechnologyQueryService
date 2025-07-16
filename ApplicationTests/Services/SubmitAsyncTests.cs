using Application.Services;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.Tests.TechnologyServiceTests
{
    public class SubmitAsyncTests
    {
        [Fact]
        public async Task SubmitAsync_ShouldCreateAndAddTechnology()
        {
            // Arrange
            var id = Guid.NewGuid();
            var description = "Blazor WebAssembly";

            var tech = new Technology(id, description);

            var factoryDouble = new Mock<ITechnologyFactory>();
            factoryDouble.Setup(f => f.Create(id, description)).Returns(tech);

            var repoDouble = new Mock<ITechnologyRepositoryEF>();
            repoDouble.Setup(r => r.AddAsync(tech)).Returns(Task.FromResult<ITechnology>(tech));

            var mapperDouble = new Mock<IMapper>();

            var service = new TechnologyService(repoDouble.Object, mapperDouble.Object, factoryDouble.Object);

            // Act
            await service.SubmitAsync(id, description);

            // Assert
            factoryDouble.Verify(f => f.Create(id, description), Times.Once);
            repoDouble.Verify(r => r.AddAsync(tech), Times.Once);
        }


    }
}
