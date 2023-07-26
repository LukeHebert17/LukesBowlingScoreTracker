using LukesBowlingScoreTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LukesBowlingScoreTrackerTests
{
    [TestClass]
    public class GameLogic_Tests
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
            game.FirstAttemptOfFrame(10);

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
            game.FirstAttemptOfFrame(10);

            // Act
            game.MoveToNextFrame();

            // Assert
            Assert.AreEqual(2, game.GetCurrentFrameNumber());
            
        }
        #endregion

        #region Error handling tests
        [TestMethod]
        public void MovingToNextFrameTooEarly_Test()
        {
            // Arrange
            var game = new Game();
            string errorMessage = "";

            // Act
            try
            {
                game.MoveToNextFrame();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }            
            // Assert
            Assert.AreEqual("Cannot move to next frame - current frame must be completed.", errorMessage);
        }

        [TestMethod]
        public void SecondFrameBeforeFirst_Test()
        {
            // Arrange
            var game = new Game();
            string errorMessage = "";

            // Act
            try
            {
                game.SecondAttemptOfFrame(10);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            // Assert
            Assert.AreEqual("Can't record second attempt of frame yet - ball has yet to be thrown once.", errorMessage);
        }

        [TestMethod]
        public void MovingToNextFrameInFinalFrame_Test()
        {
            // Arrange
            var game = new Game();
            string errorMessage = "";

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

            // Act
            try
            {
                game.MoveToNextFrame();
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            // Assert
            Assert.AreEqual("Cannot move to next frame - current the game is in its final frame.", errorMessage);
        }

        [TestMethod]
        public void AttemptingThirdThrowOfFrameThatIsntFinalFrame_Test()
        {
            // Arrange
            var game = new Game();
            string errorMessage = "";

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

            // Act
            try
            {
                game.ThirdAttemptOfFinalFrame(1);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }

            // Assert
            Assert.AreEqual("Can't make a third throw outside of the final frame", errorMessage);
        }
        #endregion

        #region Partial game frame/score calcs
        [TestMethod]
        public void StrikeAndTwoOpenFrames_Test()
        {
            // Arrange
            var game = new Game();
            // Act
            game.FirstAttemptOfFrame(10);
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
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(10);
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
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(10);
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
            game.FirstAttemptOfFrame(10);
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
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(10);
            game.MoveToNextFrame();
            game.FirstAttemptOfFrame(10);
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
