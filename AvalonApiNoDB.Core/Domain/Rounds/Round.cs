using AvalonApiNoDB.Core.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvalonApiNoDB.Core.Domain.Rounds
{
    public class Round
    {
        public Guid Id { get; set; }
        public int FailedTeams { get; set; }
        public List<Player> CurrentTeam { get; set; }
        public string TeamString { get; set; }
        public DateTime CreationTime { get; set; }
        public RoundStatus Status { get; set; }
        public int VotesForTeam { get; set; }
        public int VotesAgainstTeam { get; set; }
        public int MissionVoteGood { get; set; }
        public int MissionVoteBad { get; set; }
        public int RequiredPlayers { get; set; }
        public Round()
        {
            Id = Guid.NewGuid();
            CurrentTeam = new List<Player>();
            Status = RoundStatus.SelectingTeam;
            CreationTime = DateTime.Now;
        }
        public Round(int roundNumber, int totalPlayerCount)
        {
            Id = Guid.NewGuid();
            CurrentTeam = new List<Player>();
            Status = RoundStatus.SelectingTeam;
            CreationTime = DateTime.Now;
            RequiredPlayers = HowManyPlayers(roundNumber, totalPlayerCount);
        }

        private int HowManyPlayers(int roundNumber, int totalPlayers)
        {
            int[,] playersPerRoundMatrix = new int[5, 6] { { 2, 2, 2, 3, 3, 3 }, 
                                                           { 3, 3, 3, 4, 4, 4 }, 
                                                           { 2, 4, 3, 4, 4, 4 }, 
                                                           { 3, 3, 4, 5, 5, 5 }, 
                                                           { 3, 4, 4, 5, 5, 5 } };
            int numberOfPlayers = playersPerRoundMatrix[roundNumber - 1, totalPlayers - 5];
            return numberOfPlayers;
        }

        public int TotalTeamVotes
        {
            get
            {
                return VotesForTeam + VotesAgainstTeam;
            }
        }

        public int TotalMissionVotes
        {
            get
            {
                return MissionVoteGood + MissionVoteBad;
            }
        }

        public void TeamVote(bool acceptTeam)
        {
            if (acceptTeam)
                VotesForTeam++;
            else
                VotesAgainstTeam++;
        }

        public void ExpeditionVote(bool goodVote)
        {
            if (goodVote)
                MissionVoteGood++;
            else
                MissionVoteBad++;
        }
    }
}
