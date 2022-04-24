using BattleRoom.Application.Features;
using BattleRoom.Client.Abstractions;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace BattleRoom.Infrastructure.SignalR.Hubs;

public class LobbiesEventsHub : Hub<ILobbyActionsHandler>
{
    private readonly ILogger<LobbiesEventsHub> _logger;
    private readonly IMediator _mediator;
    
    public LobbiesEventsHub(ILogger<LobbiesEventsHub> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task OpenTheLobby(Guid lobbyId)
    {
        var group = SignalRUtils.GetGroupName<LobbiesEventsHub>(lobbyId);
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
        _logger.LogInformation($"Client {Context.ConnectionId} connected to the group {group}");

        var userId = Guid.Parse(Context.UserIdentifier);

        await _mediator.Send(new OpenTheLobbyCommand(lobbyId, userId));
    }
    
    public async Task JoinTheLobby(Guid lobbyId)
    {
        var group = SignalRUtils.GetGroupName<LobbiesEventsHub>(lobbyId);
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
        _logger.LogInformation($"Client {Context.ConnectionId} connected to the group {group}");

        var userId = Guid.Parse(Context.UserIdentifier);

        await _mediator.Send(new JoinTheLobbyCommand(lobbyId, userId));
    }
    
    public async Task LeaveTheLobby(Guid lobbyId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, SignalRUtils.GetGroupName<LobbiesEventsHub>(lobbyId));
        _logger.LogInformation($"Client {Context.ConnectionId} disconnected");
    }
}