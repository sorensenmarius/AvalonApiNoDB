using AvalonApiNoDB.Core.Domain.Players;
using AvalonApiNoDB.Core.Domain.Rounds;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public class Game
    {
        public Guid Id { get; set; }
        public int JoinCode { get; set; }
        public int PointsInnocent { get; set; }
        public int PointsEvil { get; set; }
        public List<Player> Players { get; set; }
        public DateTime CreationTime { get; set; }
        public GameStatus Status { get; set; }
        public List<Round> Rounds { get; set; }
        private int _playerIndex { get; set; }
        public int PlayerIndex // Might not need this as I have added order to the Player class. Why not just sort the Players array though :thinking:
        {
            get
            {
                return _playerIndex;
            }
            set
            {
                _playerIndex = value % Players.Count();
            }
        }
        
        public Round CurrentRound
        {
            get
            {
                if (Rounds.Count == 0)
                    return null;
                return Rounds[^1];
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                if (Players.Count == 0)
                    return null;
                return Players[PlayerIndex];
            }
        }

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

        public void Start()
        {
            Status = GameStatus.Playing;
            Rounds.Add(new Round(0, Players.Count()));
            PlayerIndex = new Random().Next(0, Players.Count());
        }

        public void Assassinate(Guid playerId)
        {
            Player p = Players.Find(p => p.Id == playerId);

            if (p.RoleId == Role.Merlin)
            {
                PointsInnocent = -1;
            }

            Status = GameStatus.Ended;
        }

        public void SetCurrentTeam(IEnumerable<Guid> playerIds)
        {
            IEnumerable<Player> players = playerIds.Select(pId => Players.Find(p => p.Id == pId));

            CurrentRound.CurrentTeam = players.ToList();
        }
    }
}