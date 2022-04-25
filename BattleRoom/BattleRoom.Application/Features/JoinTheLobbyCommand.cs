using Ardalis.Specification.EntityFrameworkCore;
using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Features;

public class JoinTheLobbyCommand : IRequest
{
    #region Data
    
    private readonly Guid _lobbyId;

    private readonly Guid _secondPlayerId;

    public JoinTheLobbyCommand(Guid lobbyId, Guid secondPlayerId)
    {
        _lobbyId = lobbyId;
        _secondPlayerId = secondPlayerId;
    }

    #endregion

    #region Handler

    public class Handler : IRequestHandler<JoinTheLobbyCommand>
    {
        private readonly ILobbiesContext _lobbiesContext;
        private readonly IMediator _mediator;

        public Handler(ILobbiesContext lobbiesContext, IMediator mediator)
        {
            _lobbiesContext = lobbiesContext;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(JoinTheLobbyCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbiesContext
                .Lobbies
                .WithSpecification(new LobbyWithPlayersSpec(request._lobbyId))
                .SingleAsync(cancellationToken);

            lobby.Start(request._secondPlayerId);
            await _lobbiesContext.SaveChangesAsync(cancellationToken);

            //Run game process in separated scope
            await _mediator.Send(new ExecuteAsyncCommand(new RunGameCommand(lobby.Id)), cancellationToken);

            return Unit.Value;
        }
    }

    #endregion
}