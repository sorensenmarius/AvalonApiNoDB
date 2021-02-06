using System;
using System.Threading.Tasks;
using AvalonApiNoDB.Api.Hubs.Clients;
using AvalonApiNoDB.Api.Hubs.Enums;
using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using Microsoft.AspNetCore.SignalR;

namespace AvalonApiNoDB.Api.Hubs
{
    public class GameHub : Hub<IGameClient>
    {

        public async Task Test()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "abcabc");
        }
        public async Task HostGame(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());
        }

        public async Task JoinGame(Guid gameId, Guid playerId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            Player p = GameStore.GetPlayer(gameId, playerId);

            await Clients.Group(gameId.ToString()).PlayerJoined(p);
        }
    }
}
