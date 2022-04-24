using BattleRoom.Application.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace BattleRoom.Infrastructure.SignalR;

public class SignalRNotifier<THub, TClient> : ISubscribersNotifier<TClient> where TClient : class where THub : Hub<TClient>
{
    private readonly IHubContext<THub, TClient> _hub;

    public SignalRNotifier(IHubContext<THub, TClient> hub)
    {
        _hub = hub;
    }

    public TClient Subscribers(string groupParameter)
    {
        var group = SignalRUtils.GetGroupName<THub>(groupParameter);

        return _hub.Clients.Group(group);
    }

    public TClient All()
    {
        return _hub.Clients.All;
    }
}
