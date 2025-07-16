using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;


namespace Domain.Models
{
    public class Technology : ITechnology
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Technology()
        {
        }
        public Technology(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}