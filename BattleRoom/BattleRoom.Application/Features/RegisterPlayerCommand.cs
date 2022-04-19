using BattleRoom.Application.Abstractions;
using BattleRoom.Domain.Entities;
using MediatR;

namespace BattleRoom.Application.Features;

public class RegisterPlayerCommand : IRequest<Guid>
{
    #region Data

    private readonly string _nickName;

    public RegisterPlayerCommand(string nickName)
    {
        _nickName = nickName;
    }
    
    #endregion

    #region Handler

    public class Handler : IRequestHandler<RegisterPlayerCommand, Guid>
    {
        private readonly IPlayersContext _playersContext;

        public Handler(IPlayersContext playersContext)
        {
            _playersContext = playersContext;
        }

        public async Task<Guid> Handle(RegisterPlayerCommand request, CancellationToken cancellationToken)
        {
            var player = new Player(request._nickName);
            _playersContext.Players.Add(player);
            await _playersContext.SaveChangesAsync(cancellationToken);

            return player.Id;
        }
    }

    #endregion
}