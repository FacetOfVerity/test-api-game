using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using BattleRoom.Application.Abstractions;
using BattleRoom.Application.Specifications;
using BattleRoom.Domain.Entities;
using BattleRoom.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BattleRoom.Application.Features;

public class GetOpenedLobbiesQuery : IRequest<IEnumerable<LobbyDto>>
{
   #region Data

   private readonly int _skip;
   private readonly int _take;

   public GetOpenedLobbiesQuery(int skip, int take)
   {
      _skip = skip;
      _take = take;
   }

   #endregion
   
   
   #region Handler

   public class Handler : IRequestHandler<GetOpenedLobbiesQuery, IEnumerable<LobbyDto>>
   {
      private readonly ILobbiesContext _lobbiesContext;
      private readonly IMapper _mapper;

      public Handler(ILobbiesContext lobbiesContext, IMapper mapper)
      {
         _lobbiesContext = lobbiesContext;
         _mapper = mapper;
      }

      public async Task<IEnumerable<LobbyDto>> Handle(GetOpenedLobbiesQuery request,
         CancellationToken cancellationToken)
      {
         var query = _lobbiesContext.Lobbies
            .WithSpecification(new LobbyWithPlayersSpec())
            .WithSpecification(new PagingSpec<Lobby>(request._skip, request._take, a => a.CreatedAt))
            .Where(a => !a.StartedAt.HasValue)
            .AsSplitQuery();

         return await _mapper.ProjectTo<LobbyDto>(query).ToListAsync(cancellationToken);
      }
   }

   #endregion
}