using AvalonApiNoDB.Core.Domain.Games;
using AvalonApiNoDB.Core.Domain.Players;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AvalonApiNoDB.Tests.Core.Games
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void IdIsZero()
        {
            Game g = new Game();

            Assert.AreNotEqual(g.Id, new Guid());
        }
        
        [TestMethod]
        public void NewGameStatusIsWaitingForPlayers()
        {
            Game g = new Game();

            Assert.AreEqual(g.Status, GameStatus.WaitingForPlayers);
        }

        [TestMethod]
        public void AddPlayer()
        {
            Game g = new Game();

            Player p = new Player();

            g.Players.Add(p);

            Assert.AreEqual(p, g.GetPlayer(p.Id));
        }
    }
}
