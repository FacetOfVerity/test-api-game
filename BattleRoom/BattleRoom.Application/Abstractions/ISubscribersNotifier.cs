namespace BattleRoom.Application.Abstractions;

public interface ISubscribersNotifier<out TClient>
{
    TClient Subscribers(string groupParameter);
    
    TClient All();
}