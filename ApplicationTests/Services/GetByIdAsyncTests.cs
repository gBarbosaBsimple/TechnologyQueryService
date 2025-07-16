using Application.DTO;
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
    public class GetByIdAsyncTests
    {
        [Fact]
        public async Task GetByIdAsync_ShouldReturnTechnologyDTO_WhenTechnologyExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var techMock = new Mock<ITechnology>();
            techMock.SetupGet(t => t.Id).Returns(id);
            techMock.SetupGet(t => t.Description).Returns("ASP.NET Core");

            var techDTO = new TechnologyDTO(id, "ASP.NET Core");

            var repoDouble = new Mock<ITechnologyRepositoryEF>();
            repoDouble.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(techMock.Object);

            var mapperDouble = new Mock<IMapper>();
            mapperDouble.Setup(m => m.Map<TechnologyDTO>(techMock.Object)).Returns(techDTO);

            var factoryDouble = new Mock<ITechnologyFactory>();

            var service = new TechnologyService(repoDouble.Object, mapperDouble.Object, factoryDouble.Object);

            // Act
            var result = await service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(id, result.Value.Id);
            Assert.Equal("ASP.NET Core", result.Value.Description);

            repoDouble.Verify(r => r.GetByIdAsync(id), Times.Once);
            mapperDouble.Verify(m => m.Map<TechnologyDTO>(techMock.Object), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNotFoundError_WhenTechnologyDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            var repoDouble = new Mock<ITechnologyRepositoryEF>();
            repoDouble.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((ITechnology?)null);

            var mapperDouble = new Mock<IMapper>();
            var factoryDouble = new Mock<ITechnologyFactory>();

            var service = new TechnologyService(repoDouble.Object, mapperDouble.Object, factoryDouble.Object);

            // Act
            var result = await service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsFailure);
            Assert.Equal("Technology not found.", result.Error!.Message);
            Assert.Equal(404, result.Error.StatusCode);

            repoDouble.Verify(r => r.GetByIdAsync(id), Times.Once);
            mapperDouble.Verify(m => m.Map<TechnologyDTO>(It.IsAny<ITechnology>()), Times.Never);
        }
    }
}
