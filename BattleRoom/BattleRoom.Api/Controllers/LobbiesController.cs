using BattleRoom.Application.Features;
using BattleRoom.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoom.Api.Controllers;

[ApiController]
[Route("api/lobbies")]
public class LobbiesController
{
    private readonly IMediator _mediator;

    public LobbiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IEnumerable<LobbyDto>> GetOpenedLobbies([FromQuery] int offset, [FromQuery] int count)
    {
        return await _mediator.Send(new GetOpenedLobbiesQuery(offset, count));
    }
}