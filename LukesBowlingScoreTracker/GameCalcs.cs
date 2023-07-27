using LukesBowlingScoreTracker.FrameScoringTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("LukesBowlingScoreTrackerTests")]
namespace LukesBowlingScoreTracker
{
    /// <summary>
    /// The purpose of this class is to store the calculations used by the Game-class objects since those can make things 
    /// in the main Game.cs file a bit cluttered.
    /// </summary>
    public partial class Game
    {
        private int TallyScoreSoFar()
        {
            _currentScoreTotal = 0;
            var scoreTotal = 0;
            var currentPositionInFrameArray = 0;
            // Checking each frame to see if its score can or needs to be calculated
            foreach (var frame in _frames)
            {
                // Skipping frames that haven't been completed yet
                if (frame == null)
                {
                    currentPositionInFrameArray++;
                    continue;
                }

                // This logic will add completed spares, strikes, final frames, and regular frames
                // (and will check if calcs on strikesand spares can be completed)
                if (frame.IsFinalScoreForFrame)
                {
                    scoreTotal += frame.GetTotalScoreForFrame();
                }
                else if (frame.GetType() == typeof(Strike))
                {
                    scoreTotal += CalculateStrikeScoreTotal(currentPositionInFrameArray);
                }
                else if (frame.GetType() == typeof(Spare))
                {
                    scoreTotal += CalculateSpareScoreTotal(currentPositionInFrameArray);
                }
                else
                {
                    // This is a regular frame - continue
                    scoreTotal += frame.GetTotalScoreForFrame();
                }
                currentPositionInFrameArray++;
            }
            return scoreTotal;
        }

        /// <summary>
        /// Calculating scores for strikes
        /// </summary>
        /// <param name="positionInFrameArray">frame's index in Game's array used to track place in game</param>
        /// <returns>returns score if it can be calculated, zero if not</returns>
        private int CalculateStrikeScoreTotal(int positionInFrameArray)
        {
            int scoreForFirstFrameAfterStrike = 0;
            int scoreForSecondFrameAfterStrike = 0;
            int strikeFrameIndex = positionInFrameArray;

            if (_frames[strikeFrameIndex + 1] == null)
                return 0;

            // Checking the next frame's scores (if possible)
            var nextFrame = _frames[strikeFrameIndex + 1];
            if (nextFrame.GetType() == typeof(Spare))
            {
                // Casting isn't super elegant but it works for now - could be refactored
                ((Strike)_frames[strikeFrameIndex]).FirstFrameAfterStrikeTotalScore = 10;
                ((Strike)_frames[strikeFrameIndex]).SecondFrameAfterStrikeTotalScore = 0;
                _frames[strikeFrameIndex].IsFinalScoreForFrame = true;
                return _frames[strikeFrameIndex].GetTotalScoreForFrame();
            }
            else if (nextFrame.GetType() == typeof(Strike))
            {
                ((Strike)_frames[strikeFrameIndex]).FirstFrameAfterStrikeTotalScore = 10;
            }
            else if (nextFrame.GetType() == typeof(FinalFrame))
            {
                ((Strike)_frames[strikeFrameIndex]).FirstFrameAfterStrikeTotalScore = nextFrame.FirstAttemptScore;
                ((Strike)_frames[strikeFrameIndex]).SecondFrameAfterStrikeTotalScore = nextFrame.SecondAttemptScore;
                _frames[strikeFrameIndex].IsFinalScoreForFrame = true;
                return _frames[strikeFrameIndex].GetTotalScoreForFrame();
            }
            else
            {
                ((Strike)_frames[strikeFrameIndex]).FirstFrameAfterStrikeTotalScore = nextFrame.FirstAttemptScore;
                ((Strike)_frames[strikeFrameIndex]).SecondFrameAfterStrikeTotalScore = nextFrame.SecondAttemptScore;
                _frames[strikeFrameIndex].IsFinalScoreForFrame = true;
                return _frames[strikeFrameIndex].GetTotalScoreForFrame();
            }

            // This logic only executes if the first throw following a strike is another strike
            if (_frames[strikeFrameIndex + 2] == null)
                return 0;

            nextFrame = _frames[strikeFrameIndex + 2];
            if (_frames[strikeFrameIndex + 2].GetType() == typeof(Strike))
            {
                ((Strike)_frames[strikeFrameIndex]).SecondFrameAfterStrikeTotalScore = 10;
            }
            else //if (_frames[strikeFrameIndex + 2].GetType() == typeof(Spare))
            {
                ((Strike)_frames[strikeFrameIndex]).SecondFrameAfterStrikeTotalScore = _frames[strikeFrameIndex + 2].FirstAttemptScore;
            }


            _frames[strikeFrameIndex].IsFinalScoreForFrame = true;
            return _frames[strikeFrameIndex].GetTotalScoreForFrame();

        }

        /// <summary>
        /// Calculating scores for spares
        /// </summary>
        /// <param name="positionInFrameArray">frame's index in Game's array used to track place in game</param>
        /// <returns>returns score if it can be calculated, zero if not</returns>
        private int CalculateSpareScoreTotal(int positionInFrameArray)
        {
            int spareFrameIndex = positionInFrameArray;

            if (_frames[spareFrameIndex + 1] == null)
                return 0;

            // Checking the next frame's scores (if possible)
            var nextFrame = _frames[spareFrameIndex + 1];
            if (nextFrame.GetType() == typeof(Spare))
            {
                // Casting isn't super elegant but it works for now - could be refactored
                ((Spare)_frames[spareFrameIndex]).FirstFrameAfterSpareTotalScore = nextFrame.FirstAttemptScore; ;

            }
            else if (nextFrame.GetType() == typeof(Strike))
            {
                ((Spare)_frames[spareFrameIndex]).FirstFrameAfterSpareTotalScore = 10;
            }
            else
            {
                ((Spare)_frames[spareFrameIndex]).FirstFrameAfterSpareTotalScore = nextFrame.FirstAttemptScore;
            }
            _frames[spareFrameIndex].IsFinalScoreForFrame = true;
            return _frames[spareFrameIndex].GetTotalScoreForFrame();
        }
    }
}
