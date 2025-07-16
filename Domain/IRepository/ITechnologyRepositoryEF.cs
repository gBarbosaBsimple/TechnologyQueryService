using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Visitor;
using Domain.Models;
using Domain.Interfaces;
using Domain.IRepository;

namespace Domain.IRepository
{
    public interface ITechnologyRepositoryEF : IGenericRepositoryEF<ITechnology, Technology, ITechnologyVisitor>
    {
        Task<bool> IsRepeated(string ddescription);
    }
}