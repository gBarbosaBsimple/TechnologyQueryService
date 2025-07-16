using Application.DTO;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/technologies")]
[ApiController]
public class TechnologyController : ControllerBase
{
    private readonly ITechnologyService _technologyService;

    public TechnologyController(ITechnologyService technologyService)
    {
        _technologyService = technologyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Guid>>> GetAll()
    {
        var tech = await _technologyService.GetAllAsync();

        return tech.ToActionResult();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TechnologyDTO>> GetById(Guid id)
    {
        var tech = await _technologyService.GetByIdAsync(id);

        return tech.ToActionResult();
    }
}
