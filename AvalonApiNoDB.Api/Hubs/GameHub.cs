using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AvalonApiNoDB.Api.Hubs.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AvalonApiNoDB.Api.Hubs
{
    public class GameHub : Hub
    {

        public async Task Test()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "abcabc");
        }
        public async Task HostGame(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }

        public async Task JoinGame(string gameId, string playerName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);

            await Clients.Group(gameId).SendAsync(GameHubMethods.PlayerJoined, playerName);
        }
    }
}
