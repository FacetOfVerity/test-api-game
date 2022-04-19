using BattleRoom.Application.Abstractions;
using BattleRoom.Domain.Entities;
using MediatR;

namespace BattleRoom.Application.Features;

public class CreateLobbyCommand : IRequest
{
    #region Data
    
    private readonly Guid _lobbyId;

    private readonly Guid _hostId;

    public CreateLobbyCommand(Guid lobbyId, Guid hostId)
    {
        _lobbyId = lobbyId;
        _hostId = hostId;
    }

    #endregion

    #region Handler
    
    public class Handler : IRequestHandler<CreateLobbyCommand>
    {
        private readonly ILobbiesContext _lobbiesContext;

        public Handler(ILobbiesContext lobbiesContext)
        {
            _lobbiesContext = lobbiesContext;
        }

        public async Task<Unit> Handle(CreateLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = Lobby.CreateWithHost(request._lobbyId, request._hostId);
            _lobbiesContext.Lobbies.Add(lobby);

            await _lobbiesContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }

    #endregion
}