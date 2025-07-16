using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class TechnologyFactory : ITechnologyFactory
    {
        public TechnologyFactory()
        {
        }

        public Technology Create(Guid Id, string description)
        {

            return new Technology(Id, description);
        }

        public Technology Create(ITechnologyVisitor visitor)
        {
            return new Technology(visitor.Id, visitor.Description);
        }
    }
}