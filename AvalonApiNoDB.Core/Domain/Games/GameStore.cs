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
            instance.Games[g.Id] = g;
        }

        public static Game GetGame(Guid id)
        {
            if(instance.Games.TryGetValue(id, out Game g))
                return g;
            return null;
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
    }
}
