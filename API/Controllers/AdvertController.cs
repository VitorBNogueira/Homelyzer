﻿using Application.Commands.ListAdverts;
using Application.DTOs.Advert;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ApiController]
[Produces("application/json")]
[Route("adverts")]
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

    [HttpGet("reset")]
    public async Task<IActionResult> ResetAdverts()
    {
        var command = new ResetAdvertsCommand();

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }

    [HttpPost("advert")]
    public async Task<IActionResult> CreateNewAdvert([FromBody] NewAdvertDTORequest newAdvertDto)
    {
        var newOwner = _mapper.Map<OwnerDTO>(newAdvertDto);
        var commandOwner = new GetAndCreateOwnerIfNewCommand(newOwner);
        var resultOwner = await _mediator.Send(commandOwner);

        newAdvertDto.OwnerId = resultOwner.OwnerId;
        var command = new CreateAdvertCommand(newAdvertDto);

        var result = await _mediator.Send(command);

        return new OkObjectResult(result);
    }
}
