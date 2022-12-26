using Application.Commands.Owners;
using Application.DTOs.Advert;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Produces("application/json")]
[Route("api/owners")]
public class OwnerController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public OwnerController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOwners()
    {
        var command = new ListOwnersCommand();

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetOwner([FromRoute] int id)
    //{
    //    var command = new GetOwnerCommand(id);

    //    var result = await _mediator.Send(command);

    //    return new OkObjectResult(result);
    //}

    //[HttpPost("owner")]
    //public async Task<IActionResult> CreateNewOwner([FromBody] OwnerDTO ownerDto)
    //{
    //    var command = new CreateOwnerCommand(newAdvertDto);

    //    var result = await _mediator.Send(command);

    //    return new OkObjectResult(result);
    //}

    //[HttpPut("owner")]
    //public async Task<IActionResult> UpdateOwner([FromBody] OwnerDTO ownerDto)
    //{
    //    var command = new UpdateAdvertCommand(advertDto);

    //    var result = await _mediator.Send(command);

    //    return new OkObjectResult(result);
    //}
}
