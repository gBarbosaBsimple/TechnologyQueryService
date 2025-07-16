using Domain.Models;
using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using AutoMapper;
using Xunit;

namespace Infrastructure.Tests.TechnologyRepositoryTests
{
    public class TechnologyRepositoryGetByIdAsyncTests : RepositoryTestBase
    {
        /* [Fact]
        public async Task GetByIdAsync_ShouldReturnTechnology_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var techDM = new TechnologyDataModel { Id = id, Description = "Azure DevOps" };

            Context.Set<TechnologyDataModel>().Add(techDM);
            await Context.SaveChangesAsync();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TechnologyDataModel, Technology>()
                   .ConstructUsing(dm => new Technology(dm.Id, dm.Description));
            }, NullLoggerFactory.Instance);

            var mapper = configuration.CreateMapper();

            var repo = new TechnologyRepositoryEF(Context, mapper);

            // Act
            var result = await repo.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result!.Id);
            Assert.Equal("Azure DevOps", result.Description);
        }
 */


        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull_WhenTechnologyDoesNotExist()
        {
            // Arrange
            var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

            // Act
            var result = await repo.GetByIdAsync(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }
    }
}
