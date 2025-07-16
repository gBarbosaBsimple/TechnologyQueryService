using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.IServices
{
    public interface ITechnologyService
    {
        Task SubmitAsync(Guid id, string description);
        Task<Result<TechnologyDTO>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<Guid>>> GetAllAsync();
    }
}