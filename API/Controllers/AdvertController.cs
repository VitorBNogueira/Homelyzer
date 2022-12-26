using Application.Commands.Adverts;
using Application.DTOs.Advert;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Produces("application/json")]
[Route("api/adverts")]
public class AdvertController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AdvertController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdverts()
    {
        var command = new ListAdvertsCommand();

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvert([FromRoute] int id)
    {
        var command = new GetAdvertCommand(id);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpGet("reset")]
    public async Task<IActionResult> ResetAdverts()
    {
        var command = new ResetAdvertsCommand();

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewAdvert([FromBody] AdvertDTO newAdvertDto)
    {
        var command = new CreateAdvertCommand(newAdvertDto);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAdvert([FromBody] AdvertDTO advertDto, [FromRoute] int Id)
    {
        var command = new UpdateAdvertCommand(advertDto, Id);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAdvert([FromRoute] int Id)
    {
        var command = new DeleteAdvertCommand(Id);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpPost("toggle/{Id}")]
    public async Task<IActionResult> ToggleAdvert([FromBody] bool IsActive, [FromRoute] int Id)
    {
        var command = new ToggleAdvertCommand(Id, IsActive);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }
}
