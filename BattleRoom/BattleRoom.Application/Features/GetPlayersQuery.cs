using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Specifications;
using BattleRoom.Domain.Entities;
using BattleRoom.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Features;

public class GetPlayersQuery : IRequest<IEnumerable<PlayerDto>>
{
    #region Data

    private readonly int _offset;
    private readonly int _count;

    public GetPlayersQuery(int offset, int count)
    {
        _offset = offset;
        _count = count;
    }

    #endregion

    #region Handler

    public class Handler : IRequestHandler<GetPlayersQuery, IEnumerable<PlayerDto>>
    {
        private readonly IPlayersContext _playersContext;
        private readonly IMapper _mapper;

        public Handler(IPlayersContext playersContext, IMapper mapper)
        {
            _playersContext = playersContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDto>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var query = _playersContext.Players
                .WithSpecification(new PagingSpec<Player>(request._offset, request._count, a => a.NickName));
            
            return await _mapper.ProjectTo<PlayerDto>(query).ToListAsync(cancellationToken);
        }
    }

    #endregion
}