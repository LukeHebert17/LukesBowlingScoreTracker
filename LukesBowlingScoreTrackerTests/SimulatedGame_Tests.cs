using LukesBowlingScoreTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LukesBowlingScoreTrackerTests
{
    /// <summary>
    /// See README.md on the github page to see the scorecard/games that these tests simulate
    /// </summary>
    [TestClass]
    public class SimulatedGame_Tests
    {
        [TestMethod]
        public void Game1()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(2);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(4);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(7);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(3);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(0);
            

            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(58, finalScore);
        }
        [TestMethod]
        public void Game2()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(2);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(7);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(3);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(2);
            game.ThirdAttemptOfFinalFrame(1);


            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(80, finalScore);
        }

        // Scoring a perfect game
        [TestMethod]
        public void Game3()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame(); 
            game.FirstAttemptOfFrame(10);
            game.SecondAttemptOfFrame(10);
            game.ThirdAttemptOfFinalFrame(10);

            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(300, finalScore);
        }

        [TestMethod]
        public void Game4()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(2);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(7);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(3);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.SecondAttemptOfFrame(2);
            game.ThirdAttemptOfFinalFrame(1);


            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(100, finalScore);
        }

        [TestMethod]
        public void Game5()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(2);
            game.SecondAttemptOfFrame(3);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(7);
            game.SecondAttemptOfFrame(3);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(9);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(9);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(2);


            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(111, finalScore);
        }

        [TestMethod]
        public void Game6()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.SecondAttemptOfFrame(10);
            game.ThirdAttemptOfFinalFrame(10);


            // Act
            var finalScore = game.EndGame();

            //Assert
            Assert.AreEqual(30, finalScore);
        }

        [TestMethod]
        public void Game7()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(05);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(05);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(05);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.ThirdAttemptOfFinalFrame(5);
            game.EndGame();


            // Act - slightly different from the other tests
            var finalScore = game.GetCurrentScoreTotal();

            //Assert
            Assert.AreEqual(150, finalScore);
        }

        [TestMethod]
        public void Game8()
        {
            // Arrange
            var game = new Game();
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(0);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(5);
            game.MoveToNextFrame();

            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(4);
            game.EndGame();


            // Act - slightly different from the other tests
            var finalScore = game.GetCurrentScoreTotal();

            //Assert
            Assert.AreEqual(124, finalScore);
        }
    }
}
