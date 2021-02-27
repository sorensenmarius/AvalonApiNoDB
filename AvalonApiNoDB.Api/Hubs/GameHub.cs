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

        public async Task StartGame(Guid gameId, List<int> roles)
        {
            Game g = GameStore.GetGame(gameId);

            g.Start(roles);

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

        public async Task SubmitSelectedTeam(Guid gameId)
        {
            Game g = GameStore.GetGame(gameId);

            g.SubmitCurrentTeam();

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task SubmitTeamVote(Guid gameId, bool votedSuccess)
        {
            Game g = GameStore.GetGame(gameId);

            g.AddTeamVote(votedSuccess);

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task SkipRevealTeamVotes(Guid gameId)
        {
            Game g = GameStore.GetGame(gameId);

            g.SkipRevealTeamVote();

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task SubmitExpeditionVote(Guid gameId, bool votedSuccess)
        {
            Game g = GameStore.GetGame(gameId);

            g.AddExpeditionVote(votedSuccess);

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }

        public async Task SkipExpeditionVotes(Guid gameId)
        {
            Game g = GameStore.GetGame(gameId);

            g.NextRound();

            await Clients.Group(gameId.ToString()).GameUpdated(g);
        }
    }
}
