using AvalonApiNoDB.Core.Domain.Players;
using AvalonApiNoDB.Core.Domain.Rounds;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AvalonApiNoDB.Core.Domain.Games
{
    public partial class Game
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
        public int PlayerIndex
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

        public Round CurrentRound => Rounds.Count == 0 ? null : Rounds[^1];

        public Player CurrentPlayer => Players.Count == 0 ? null : Players[PlayerIndex];

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

        public void ValidateJoin(string name)
        {
            if (Status != GameStatus.WaitingForPlayers)
                throw new Exception("Game has already started");

            if (Players.Count >= 10)
                throw new Exception("Game is full");

            if (name.Length > 15)
                throw new Exception("Name can be no longer than 15 characters");

            if (Players.Select(p => p.Name).Contains(name))
                throw new Exception("Name already taken");
        }

        public void ValidateStart(List<int> roles)
        {
            if (Players.Count < 5)
                throw new Exception("You need to be atleast 5 players");

            var howManyEvils = new List<int>() { 2, 2, 3, 3, 3, 4 };
            var numEvils = howManyEvils[Players.Count - 5];
            var numEvilRolesChosen = roles.Where(r => r > 3).Count();

            if (numEvilRolesChosen > numEvils)
                throw new Exception($"You can only choose {numEvils} evil roles with {Players.Count} players.");
        }

        private void ValidationProblem(string v)
        {
            throw new NotImplementedException();
        }

        public void Start(List<int> roles)
        {
            Status = GameStatus.Playing;
            Rounds.Add(new Round(0, Players.Count()));
            PlayerIndex = new Random().Next(0, Players.Count());
            DistributeRoles(roles);
        }

        public void DistributeRoles(List<int> roles)
        {
            var numberOfGood = roles.FindAll(r => r <= 3).Count;
            var numberOfEvil = roles.Count - numberOfGood;

            var roleList = roles.Select(r => (Role)r).ToList();

            var numberOfMinions = GetNumberOfEvils(Players.Count) - numberOfEvil;
            roleList.AddRange(Enumerable.Repeat(Role.Minion, numberOfMinions).ToList());
            
            var numberOfServants = Players.Count - roleList.Count;
            roleList.AddRange(Enumerable.Repeat(Role.Servant, numberOfServants).ToList());

            roleList = roleList.OrderBy(x => Guid.NewGuid()).ToList();

            Players = Players.Select((p, i) =>
            {
                p.RoleId = roleList[i];
                return p;
            }).ToList();

            Players = Players.Select((p) =>
            {
                p.RoleInfo = GetRoleString(p.RoleId);
                return p;
            }).ToList();
        }

        public void NextRound()
        {
            if (CurrentRound.TotalExpeditionVotes > 0)
            {
                // TODO: Add so that you might need two evil votes on some rounds
                if (CurrentRound.VotesAgainstExpedition > 0)
                {
                    PointsEvil++;
                } else
                {
                    PointsInnocent++;
                }
            }

            if (PointsEvil >= 3 || PointsInnocent >= 3)
            {

                Status = PointsEvil < 3 && Players.Exists(p => p.RoleId == Role.Assassin) ? GameStatus.AssassinTurn : GameStatus.Ended;
            }
            else
            {
                PlayerIndex++;
                Rounds.Add(new Round(Rounds.Count, Players.Count));
            }
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

        public void SubmitCurrentTeam()
        {
            CurrentRound.Status = RoundStatus.VotingForTeam;

            if (CurrentRound.FailedTeams >= 4)
                CurrentRound.Status = RoundStatus.VotingExpedition;
        }

        public void AddTeamVote(bool votedSuccess)
        {
            CurrentRound.TeamVote(votedSuccess);

            if (CurrentRound.TotalTeamVotes >= Players.Count)
            {
                CurrentRound.Status = RoundStatus.RevealTeamVote;
            }
        }

        public void AddExpeditionVote(bool votedSuccess)
        {
            CurrentRound.ExpeditionVote(votedSuccess);

            if (CurrentRound.TotalExpeditionVotes >= CurrentRound.CurrentTeam.Count)
            {
                CurrentRound.Status = RoundStatus.RevealExpeditionVote;
            }
        }

        public void SkipRevealTeamVote()
        {
            if (CurrentRound.VotesForTeam > CurrentRound.VotesAgainstTeam)
            {
                CurrentRound.Status = RoundStatus.VotingExpedition;
            } else
            {
                CurrentRound.RejectedTeam();
                PlayerIndex++;
            }
        }
    }
}