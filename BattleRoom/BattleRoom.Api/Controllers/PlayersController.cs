using BattleRoom.Application.Features;
using BattleRoom.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoom.Api.Controllers;

[ApiController]
[Route("api/players")]
public class PlayersController
{
    private readonly IMediator _mediator;

    public PlayersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<Guid> Register([FromQuery] string nickName)
    {
        return await _mediator.Send(new RegisterPlayerCommand(nickName));
    }
    
    [HttpGet]
    public async Task<IEnumerable<PlayerDto>> GetPlayers([FromQuery] int offset, [FromQuery] int count)
    {
        return await _mediator.Send(new GetPlayersQuery(offset, count));
    }
}