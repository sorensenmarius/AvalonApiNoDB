using AvalonApiNoDB.Core.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public sealed class GameStore
    {
        private static readonly GameStore instance = new GameStore();
        public Dictionary<Guid, Game> Games;
        static GameStore()
        {
        }

        private GameStore()
        {
            Games = new Dictionary<Guid, Game>();
        }

        public static GameStore Instance
        {
            get
            {
                return instance;
            }
        }

        public static void AddGame(Game g)
        {
            CleanUpOldGames();
            instance.Games[g.Id] = g;
        }

        public static Game GetGame(Guid gameId)
        {
            Game g = instance.Games.GetValueOrDefault(gameId);

            if (g == default(Game))
                throw new KeyNotFoundException($"No game with ID {gameId} was found in GameStore");

            return g;
        }

        public static void DeleteGame(Guid id)
        {
            instance.Games.Remove(id);
        }

        public static Game GetGameByJoinCode(int joinCode)
        {
            Game g = instance.Games.Where(keyValuePair => keyValuePair.Value.JoinCode == joinCode).FirstOrDefault().Value;
            if (g == default(Game))
                throw new KeyNotFoundException($"Game with Join Code {joinCode} does not exist");
            return g;
        }

        public static void Reset()
        {
            instance.Games = new Dictionary<Guid, Game>();
        }

        public static Player GetPlayer(Guid gameId, Guid playerId)
        {
            Game g = GetGame(gameId);

            Player p = g.Players.Where(p => p.Id == playerId).FirstOrDefault();

            if (p == default(Player))
                throw new KeyNotFoundException($"No player with ID {playerId} was found");

            return p;
        }

        private static void CleanUpOldGames()
        {
            instance.Games = (Dictionary<Guid, Game>)instance.Games.Where(kvp => kvp.Value.CreationTime.AddHours(3) > DateTime.Now);
        }
    }
}
