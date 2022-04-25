using Ardalis.Specification.EntityFrameworkCore;
using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Specifications;
using BattleRoom.Domain.Abstractions;
using BattleRoom.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Features;

public class RunGameCommand : IRequest
{
    #region Data

    private readonly Guid _lobbyId;

    public RunGameCommand(Guid lobbyId)
    {
        _lobbyId = lobbyId;
    }

    #endregion

    #region Handler

    public class Handler : IRequestHandler<RunGameCommand>
    {
        private readonly ILobbiesContext _lobbiesContext;
        private readonly IGameActionsPublisher _gameActionsPublisher;

        public Handler(ILobbiesContext lobbiesContext, IGameActionsPublisher gameActionsPublisher)
        {
            _lobbiesContext = lobbiesContext;
            _gameActionsPublisher = gameActionsPublisher;
        }

        public async Task<Unit> Handle(RunGameCommand request, CancellationToken cancellationToken)
        {
            var lobby = await _lobbiesContext
                .Lobbies
                .WithSpecification(new LobbyWithPlayersSpec(request._lobbyId))
                .SingleAsync(cancellationToken);

            await Game.Create(lobby, _gameActionsPublisher).Run();

            await _lobbiesContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }

    #endregion
}