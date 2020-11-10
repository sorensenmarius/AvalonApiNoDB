using AvalonApiNoDB.Core.Domain.Rounds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AvalonApiNoDB.Tests.Core.Rounds
{
    [TestClass]
    public class RoundTests
    {
        [TestMethod]
        public void IdIsNotZero()
        {
            Round r = new Round();

            Assert.AreNotEqual(new Guid(), r.Id);
        }

        [TestMethod]
        public void NewRoundStatusIsSelectingTeam()
        {
            Round r = new Round();

            Assert.AreEqual(RoundStatus.SelectingTeam, r.Status);
        }

        [TestMethod]
        public void TeamVotes()
        {
            Round r = new Round();

            r.TeamVote(true);

            Assert.AreEqual(1, r.VotesForTeam);

            r.TeamVote(false);

            Assert.AreEqual(1, r.VotesAgainstTeam);
        }

        [TestMethod]
        public void TotalTeamVotes()
        {
            Round r = new Round();

            r.TeamVote(true);
            r.TeamVote(false);

            Assert.AreEqual(2, r.TotalTeamVotes);
        }

        [TestMethod]
        public void MissionVotes()
        {
            Round r = new Round();

            r.ExpeditionVote(true);

            Assert.AreEqual(1, r.VotesForExpedition);

            r.ExpeditionVote(false);

            Assert.AreEqual(1, r.VotesAgainstExpedition);
        }

        [TestMethod]
        public void TotalExpeditionVotes()
        {
            Round r = new Round();

            r.ExpeditionVote(true);
            r.ExpeditionVote(false);

            Assert.AreEqual(2, r.TotalExpeditionVotes);
        }
    }
}
