using BattleRoom.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoom.Api.Controllers;

[ApiController]
[Route("[controller]")]
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
}