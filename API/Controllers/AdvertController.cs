﻿using Application.Commands.Adverts;
using Application.DTOs;
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
    public async Task<IActionResult> GetAllAdverts([FromQuery] string sort = "score", [FromQuery] int direction = 1)
    {
        var command = new ListAdvertsCommand(new SortDTO { OrderBy = sort, Direction = (EDirection)direction }) ;

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAdvert([FromRoute] int id)
    {
        var command = new GetAdvertCommand(id);

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }



    [HttpPost]
    public async Task<IActionResult> CreateNewAdvert([FromBody] AdvertDTO newAdvertDto)
    {
        var command = new CreateAdvertCommand(newAdvertDto);

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateAdvert([FromBody] AdvertDTO advertDto, [FromRoute] int Id)
    {
        var command = new UpdateAdvertCommand(advertDto, Id);

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteAdvert([FromRoute] int Id)
    {
        var command = new DeleteAdvertCommand(Id);

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }

    [HttpPost("toggle/{Id}")]
    public async Task<IActionResult> ToggleAdvert([FromBody] bool IsActive, [FromRoute] int Id)
    {
        var command = new ToggleAdvertCommand(Id, IsActive);

        var result = await _mediator.Send(command);

        return ApiResponse.Response(result);
    }
}
