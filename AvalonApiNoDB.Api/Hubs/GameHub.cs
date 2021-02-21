using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvalonApiNoDB.Api.Hubs.Clients;
using AvalonApiNoDB.Core.Domain.Games;
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

        public async Task StartGame(Guid gameId)
        {
            Game g = GameStore.GetGame(gameId);

            g.Start();

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task JoinGame(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId.ToString());

            Game g = GameStore.GetGame(gameId);

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task Assassinate(Guid gameId, Guid assassinationTargetId)
        {
            Game g = GameStore.GetGame(gameId);

            g.Assassinate(assassinationTargetId);

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task ChangeSelectedTeam(Guid gameId, IEnumerable<Guid> playerIds)
        {
            Game g = GameStore.GetGame(gameId);

            g.SetCurrentTeam(playerIds);

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }
    }
}
