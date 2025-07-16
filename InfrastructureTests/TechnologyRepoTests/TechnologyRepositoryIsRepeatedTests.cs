using Infrastructure.DataModel;
using Infrastructure.Repositories;
using Xunit;

namespace Infrastructure.Tests.TechnologyRepositoryTests
{
    public class TechnologyRepositoryIsRepeatedTests : RepositoryTestBase
    {
        [Theory]
        [InlineData("Docker", true)]
        [InlineData("Terraform", false)]
        public async Task IsRepeated_ShouldReturnExpectedResult(string description, bool expected)
        {
            // Arrange
            var existing = new TechnologyDataModel
            {
                Id = Guid.NewGuid(),
                Description = "Docker"
            };

            Context.Set<TechnologyDataModel>().Add(existing);
            await Context.SaveChangesAsync();

            var repo = new TechnologyRepositoryEF(Context, Mapper.Object);

            // Act
            var result = await repo.IsRepeated(description);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
