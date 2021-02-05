using AvalonApiNoDB.Core.Domain.Players;
using AvalonApiNoDB.Core.Domain.Rounds;
using System;
using System.Collections.Generic;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public class Game
    {
        public Guid Id { get; set; }
        public int JoinCode { get; set; }
        public int Counter { get; set; }
        public int PointsInnocent { get; set; }
        public int PointsEvil { get; set; }
        public Player CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }
        public DateTime CreationTime { get; set; }
        public GameStatus Status { get; set; }
        public List<Round> Rounds { get; set; }
        public Game()
        {
            Random generator = new Random();
            JoinCode = generator.Next(99999, 999999);
            Players = new List<Player>();
            CreationTime = DateTime.Now;
            Status = GameStatus.WaitingForPlayers;
            Id = Guid.NewGuid();
            Rounds = new List<Round>();
        }

        public Player GetPlayer(Guid playerId)
        {
            return Players.Find(p => p.Id == playerId);
        }

        public Round CurrentRound
        {
            get
            {
                if (Rounds.Count == 0)
                    return null;
                return Rounds[Rounds.Count - 1];
            }
        }
    }
}