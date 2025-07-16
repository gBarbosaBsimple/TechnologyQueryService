using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Domain.Visitor;
using Domain.Factory;
namespace Infrastructure.Repositories;

public class TechnologyRepositoryEF : GenericRepositoryEF<ITechnology, Technology, TechnologyDataModel>, ITechnologyRepositoryEF
{
    private readonly IMapper _mapper;
    private readonly ITechnologyFactory _technologyFactory;
    public TechnologyRepositoryEF(AbsanteeContext context, IMapper mapper, ITechnologyFactory technologyFactory) : base(context, mapper)
    {
        _mapper = mapper;
        _technologyFactory = technologyFactory;
    }
    public TechnologyRepositoryEF(DbContext context, IMapper mapper) : base(context, mapper)
    {
    }
    public async Task<bool> IsRepeated(string description)
    {
        return await this._context.Set<TechnologyDataModel>()
                .AnyAsync(c => c.Description == description);
    }
    public override async Task<ITechnology?> GetByIdAsync(Guid id)
    {
        var techDM = await _context.Set<TechnologyDataModel>()
                                    .FirstOrDefaultAsync(t => t.Id == id);

        if (techDM == null)
            return null;

        return _mapper.Map<TechnologyDataModel, Technology>(techDM);
    }  
}