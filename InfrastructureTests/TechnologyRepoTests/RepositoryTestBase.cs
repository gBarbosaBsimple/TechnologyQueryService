using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;

namespace Infrastructure.Tests
{
    public abstract class RepositoryTestBase : IDisposable
    {
        protected readonly AbsanteeContext Context;
        protected readonly Mock<IMapper> Mapper;

        protected RepositoryTestBase()
        {
            var options = new DbContextOptionsBuilder<AbsanteeContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Isolamento entre testes
                .Options;

            Context = new AbsanteeContext(options);
            Mapper = new Mock<IMapper>();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
