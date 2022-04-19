using BattleRoom.Application.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BattleRoom.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class LobbiesController
{
    private readonly IMediator _mediator;

    public LobbiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("{lobbyId}/{hostId}")]
    public async Task Create(Guid lobbyId, Guid hostId)
    {
        await _mediator.Send(new CreateLobbyCommand(lobbyId, hostId));
    }
    
    [HttpPost]
    [Route("{lobbyId}/join/{secondPlayerId}")]
    public async Task Join(Guid lobbyId, Guid secondPlayerId)
    {
        await _mediator.Send(new JoinTheLobbyCommand(lobbyId, secondPlayerId));
    }
}