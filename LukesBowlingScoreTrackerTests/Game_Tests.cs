using LukesBowlingScoreTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LukesBowlingScoreTrackerTests
{
    [TestClass]
    public class Game_Tests
    {
        // Arrange
        // Act
        // Assert

        #region Testing frame type creation
        [TestMethod]
        public void Strike_Test()
        {
            // Arrange
            var game = new Game();

            // Act
            game.Strike();

            // Assert
            Assert.IsTrue(game.GetCurrentFrameTypeName() == "Strike");
        }
        [TestMethod]
        public void Spare_Test()
        {
            // Arrange
            var game = new Game();

            // Act
            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(2);

            // Assert
            Assert.IsTrue(game.GetCurrentFrameTypeName() == "Spare");
        }
        [TestMethod]
        public void RegularFrame_Test()
        {
            // Arrange
            var game = new Game();

            // Act
            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(2);

            // Assert
            Assert.IsTrue(game.GetCurrentFrameTypeName() == "RegularFrame");
        }
        #endregion

        #region Basic Game logic/actions tests
        [TestMethod]
        public void MoveToNextFrameAfterStrike_Test()
        {
            
            // Arrange
            var game = new Game();
            game.Strike();

            // Act
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(2, game.GetCurrentFrameNumber());
            
        }
        #endregion

        #region Error handling tests

        #endregion

        #region Partial game frame/score calcs
        [TestMethod]
        public void StrikeAndTwoOpenFrames_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.Strike();
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(1);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(19, game.GetCurrentScoreTotal());
        }

        [TestMethod]
        public void TwoStrikesAndOpenFrame_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.Strike();
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(9);
            game.SecondAttemptOfFrame(0);
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(57, game.GetCurrentScoreTotal());
        }
        [TestMethod]
        public void TwoStrikesAndSpare_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.Strike();
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(9);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(49, game.GetCurrentScoreTotal());
        }
        [TestMethod]
        public void OneSpareAndAStrike_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.FirstAttemptOfFrame(9);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(20, game.GetCurrentScoreTotal());
        }

        // Long name, but very specific test
        [TestMethod]
        public void SpareStrikeStrikeStrikeOpenOpen_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.FirstAttemptOfFrame(8);
            game.SecondAttemptOfFrame(2);
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();
            game.Strike();
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(5);
            game.SecondAttemptOfFrame(1);
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(103, game.GetCurrentScoreTotal());
        }

        #endregion
    }
}
