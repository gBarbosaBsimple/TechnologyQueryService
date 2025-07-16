using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Domain.Factory;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using AutoMapper;


namespace Application.Services
{
    public class TechnologyService : ITechnologyService
    {
        private ITechnologyRepositoryEF _technologyRepository;
        private ITechnologyFactory _technologyFactory;
        private readonly IMapper _mapper;

        public TechnologyService(ITechnologyRepositoryEF technologyRepository, IMapper mapper, ITechnologyFactory technologyFactory)
        {
            _technologyRepository = technologyRepository;
            _mapper = mapper;
            _technologyFactory = technologyFactory;
        }

        public async Task<Result<IEnumerable<Guid>>> GetAllAsync()
        {
            try
            {
                var tech = await _technologyRepository.GetAllAsync();
                var techIds = tech.Select(U => U.Id);

                return Result<IEnumerable<Guid>>.Success(techIds);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<Guid>>.Failure(Error.InternalServerError(e.Message));
            }
        }

        public async Task<Result<TechnologyDTO>> GetByIdAsync(Guid id)
        {
            var tech = await _technologyRepository.GetByIdAsync(id);

            if (tech is null)
                return Result<TechnologyDTO>.Failure(Error.NotFound("Technology not found."));

            var techDTO = _mapper.Map<TechnologyDTO>(tech);

            return Result<TechnologyDTO>.Success(techDTO);
        }

        public async Task SubmitAsync(Guid id, string description)
        {
            var tech = _technologyFactory.Create(
            id,
            description
        );

            await _technologyRepository.AddAsync(tech);
        }
    }
}