using AvalonApiNoDB.Core.Domain.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AvalonApiNoDB.Tests.Core.Games
{
    [TestClass]
    public class GameStoreTests
    {
        private Game SetupGameStoreWithSingleGame()
        {
            Game g = new Game();
            GameStore.Instance.Games[g.Id] = g;

            return g;
        }

        [TestInitialize]
        public void TestInitialize()
        {
            GameStore.Reset();
        }

        [TestMethod]
        public void AddGame()
        {
            Game g = new Game();
            GameStore.AddGame(g);

            Assert.AreEqual(g, GameStore.Instance.Games[g.Id]);
        }

        [TestMethod]
        public void GetGame()
        {
            Game g = SetupGameStoreWithSingleGame();

            Assert.AreEqual(g, GameStore.GetGame(g.Id));
        }

        [TestMethod]
        public void DeleteGame()
        {
            Game g = SetupGameStoreWithSingleGame();

            GameStore.DeleteGame(g.Id);

            Assert.AreEqual(0, GameStore.Instance.Games.Keys.Count);
        }

        [TestMethod]
        public void DeleteGameWithMultipleOtherGames()
        {
            Game g = SetupGameStoreWithSingleGame();
            Game g2 = SetupGameStoreWithSingleGame();
            Game g3 = SetupGameStoreWithSingleGame();

            GameStore.DeleteGame(g.Id);

            Assert.IsFalse(GameStore.Instance.Games.ContainsValue(g));
            Assert.IsTrue(GameStore.Instance.Games.ContainsValue(g2));
            Assert.IsTrue(GameStore.Instance.Games.ContainsValue(g3));
        }

        [TestMethod]
        public void GetGameByJoinCode()
        {
            Game g = SetupGameStoreWithSingleGame();

            Assert.AreEqual(g, GameStore.GetGameByJoinCode(g.JoinCode));
        }
    }
}
