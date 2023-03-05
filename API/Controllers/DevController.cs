using Application.Commands.Adverts;
using Application.Commands.Dev.ResetAdverts;
using Application.Contracts.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]/advert")]
[ApiController]
public class DevController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public DevController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Get ALL adverts, including the inactive ones
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAdverts([FromQuery] string sort = "score", [FromQuery] int direction = 1)
    {
        var command = new ListAdvertsCommand(new SortDTO { OrderBy = sort, Direction = (EDirection)direction });

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }

    [HttpGet("reset")]
    public async Task<IActionResult> ResetAdverts()
    {
        var command = new ResetAdvertsCommand();

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }
}
