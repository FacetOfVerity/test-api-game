using AutoMapper;
using BattleRoom.Domain.Entities;
using BattleRoom.Models;

namespace BattleRoom.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Lobby, LobbyDto>().ForMember(a => a.Host, a => a.MapFrom(lobby => lobby.Host.Player));
        CreateMap<Player, PlayerDto>();
    }
}