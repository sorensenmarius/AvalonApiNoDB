using AvalonApiNoDB.Core.Domain.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AvalonApiNoDB.Tests.Core.Games
{
    [TestClass]
    public class CreateGameTests
    {
        [TestMethod]
        public void IdIsNotNull()
        {
            Game g = new Game();

            Assert.AreNotEqual(g.Id, new Guid());
        }
        
        [TestMethod]
        public void GameStatusIsWaitingForPlayers()
        {
            Game g = new Game();

            Assert.AreEqual(g.Status, GameStatus.WaitingForPlayers);
        }
    }
}
