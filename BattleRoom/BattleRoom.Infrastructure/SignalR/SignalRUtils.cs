using Microsoft.AspNetCore.SignalR;

namespace BattleRoom.Infrastructure.SignalR;

public static class SignalRUtils
{
    /// <summary/>
    public static string GetGroupName<THub>(object groupParameter) where THub : Hub
    {
        var hubName = typeof(THub).Name;
        var trimmedName = hubName.Contains("Hub")
            ? hubName.Substring(0, hubName.IndexOf("Hub", StringComparison.Ordinal))
            : hubName;


        return $"{trimmedName}_{groupParameter}";
    }
}