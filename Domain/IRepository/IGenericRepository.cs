namespace Domain.IRepository;

using System.Linq.Expressions;

public interface IGenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
{
    Task<TInterface?> GetByIdAsync(Guid id);
    Task<IEnumerable<TInterface>> GetAllAsync();
    Task<TInterface> AddAsync(TInterface entity);
    Task<int> SaveChangesAsync();
}