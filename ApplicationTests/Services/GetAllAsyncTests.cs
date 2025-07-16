using Application.Services;
using ApplicationTests;
using AutoMapper;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Moq;
using Xunit;

namespace Application.Tests.TechnologyServiceTests
{
    public class GetAllAsyncTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnTechnologyIds_WhenTechnologiesExist()
        {
            // Arrange
            var repoDouble = new Mock<ITechnologyRepositoryEF>();
            var mapperDouble = new Mock<IMapper>();
            var factoryDouble = new Mock<ITechnologyFactory>();

            var techList = new List<ITechnology>
            {
                Mock.Of<ITechnology>(t => t.Id == Guid.NewGuid()),
                Mock.Of<ITechnology>(t => t.Id == Guid.NewGuid()),
                Mock.Of<ITechnology>(t => t.Id == Guid.NewGuid())
            };

            repoDouble.Setup(r => r.GetAllAsync()).ReturnsAsync(techList);

            var service = new TechnologyService(repoDouble.Object, mapperDouble.Object, factoryDouble.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(3, result.Value.Count());
            repoDouble.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnFailure_WhenExceptionIsThrown()
        {
            // Arrange
            var repoDouble = new Mock<ITechnologyRepositoryEF>();
            var mapperDouble = new Mock<IMapper>();
            var factoryDouble = new Mock<ITechnologyFactory>();

            var exceptionMessage = "Database unavailable";
            repoDouble.Setup(r => r.GetAllAsync()).ThrowsAsync(new Exception(exceptionMessage));

            var service = new TechnologyService(repoDouble.Object, mapperDouble.Object, factoryDouble.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal(exceptionMessage, result.Error!.Message);
            Assert.Equal(500, result.Error.StatusCode);
            repoDouble.Verify(r => r.GetAllAsync(), Times.Once);
        }
    }
}
