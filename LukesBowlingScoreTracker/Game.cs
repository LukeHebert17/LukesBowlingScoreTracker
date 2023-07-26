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
    public partial class Game
    {
        private IFrame[] _frames;
        private int _currentFrame;
        private int _currentScoreTotal;
        private int _throwsPerFrameCount;
        private bool _isFinalFrame;
        private bool _canMakeThirdThrow;


        public Game()
        {
            _frames = new IFrame[10]; // Sets us up for a 10-frame game
            _currentFrame = 1;
            _throwsPerFrameCount = 0;
            _isFinalFrame = false;
        }

        public int GetCurrentFrameNumber()
        {
            return _currentFrame;
        }

        public int GetCurrentScoreTotal()
        {
            // Prevents score from going above 300 - probably bot needed if the calcs ran correctly but just in case
            if (_currentScoreTotal > 300)
                _currentScoreTotal = 300;
            return (_currentScoreTotal);
        }

        private void Strike()
        {
            // For all frames before the final frame
            if (_currentFrame < 10)
            {
                _frames[_currentFrame - 1] = new Strike(_currentFrame);
                return;
            }

            // Unique case for final frame - allows for a third throw to be made if the first throw was a strike
            if (_isFinalFrame)
            {
                _frames[_currentFrame - 1] = new FinalFrame();
                _canMakeThirdThrow = true;
            }
        }

        public void FirstAttemptOfFrame(int pinsKnockedDownInFirstThrow)
        {
            if (pinsKnockedDownInFirstThrow > 10 || pinsKnockedDownInFirstThrow < 0)
                // Exceptions could stand to be more specialized, but the generic Exception one works for now
                throw new Exception("Cannot knock down more than 10 pins or less than 0");
            else if (pinsKnockedDownInFirstThrow == 10) 
                Strike();
            else if (_isFinalFrame)
                _frames[_currentFrame - 1] = new FinalFrame();
            else
                // Starts as regular frame, may be redefined as spare if need be
                _frames[_currentFrame - 1] = new RegularFrame(_currentFrame);

            // Setting how many pins were knocked down in the first throw
            _frames[_currentFrame - 1].FirstAttemptScore = pinsKnockedDownInFirstThrow;

            _throwsPerFrameCount++;

            
        }

        public void SecondAttemptOfFrame(int pinsKnockedDownInSecondThrow)
        {
            // Doing some validation - can't allow score totals greater than 10 or throwing another ball for this frame
            // after already scoring a strike
            if (pinsKnockedDownInSecondThrow > 10 || pinsKnockedDownInSecondThrow < 0)
                throw new Exception("Cannot knock down more than 10 pins or less than 0");
            else if (_throwsPerFrameCount < 1)
                throw new Exception("Can't record second attempt of frame yet - ball has yet to be thrown once.");
            else if (_frames[_currentFrame - 1].FirstAttemptScore + pinsKnockedDownInSecondThrow > 10 && !_canMakeThirdThrow)
                throw new Exception("Can't have more than 10 pins per round knocked down");
            else if (_frames[_currentFrame - 1].GetType() == typeof(Strike))
                throw new Exception("Strike has been thrown for this frame - no more throws can be made");

            var pinsKnockedDownInFirstThrow = _frames[_currentFrame - 1].FirstAttemptScore;
            // Setting how many pins were knocked down in the second throw
            _frames[_currentFrame - 1].SecondAttemptScore = pinsKnockedDownInSecondThrow;

            // Checking if this round is a spare and handling it if it is
            if (IsSpare(pinsKnockedDownInFirstThrow, pinsKnockedDownInSecondThrow))
            {
                if (!_isFinalFrame)
                {
                    var spare = new Spare(_currentFrame);
                    spare.FirstAttemptScore = pinsKnockedDownInFirstThrow;
                    spare.SecondAttemptScore = pinsKnockedDownInSecondThrow;
                    _frames[_currentFrame - 1] = spare; // regular frame has now become a spare
                }
                else
                {
                    _frames[_currentFrame - 1].SecondAttemptScore = pinsKnockedDownInSecondThrow;
                    _canMakeThirdThrow = true;
                }
            }
            else if (_isFinalFrame && !_canMakeThirdThrow)
            {
                // For non-strike final frames
                _frames[_currentFrame - 1].IsFinalScoreForFrame = true;
            }
            else
            {
                // This frame's score has been tallied and it's now ready to be displayed and used in spare/strike calc
                _frames[_currentFrame - 1].IsFinalScoreForFrame = true;
            }

            _throwsPerFrameCount++;


        }

        /// <summary>
        /// Method used to handle the third throw when a strike is the first throw of a final frame. Cannot be done outside of this case.
        /// </summary>
        /// <param name="pinsKnockedDownInThirdThrow">Number of pins knocked down in this throw</param>
        /// <exception cref="Exception"></exception>
        public void ThirdAttemptOfFinalFrame(int pinsKnockedDownInThirdThrow)
        {
            if (!_isFinalFrame)
                throw new Exception("Can't make a third throw outside of the final frame");
            if (!_canMakeThirdThrow) // Final frame's first throw wasn't a strike
                throw new Exception("Can only make third throw in final frame if the first throw was a strike or second throw made it a spare");
            else if (false)
            { }
            // Handle too many pins as arguments
            else if (_throwsPerFrameCount > 2)
                throw new Exception("Cannot make third throw yet - the second has yet to be made");

            // Setting how many pins were knocked down in the third throw and marking the frame as complete
            ((FinalFrame)_frames[_currentFrame - 1]).ThirdAttemptScore = pinsKnockedDownInThirdThrow;
            _frames[_currentFrame - 1].IsFinalScoreForFrame = true;
            _throwsPerFrameCount++;
        }

        public void MoveToNextFrame()
        {
            if ( _frames[_currentFrame - 1] == null || (_frames[_currentFrame - 1].GetType() != typeof(Strike) 
                && _throwsPerFrameCount > 2) ) // only strikes can have less than 2 throws per frame
                throw new Exception("Cannot move to next frame - current frame must be completed.");
            else if (_isFinalFrame)
                throw new Exception("Cannot move to next frame - current the game is in its final frame.");

            // Assuming all is well, the score needs to be tallied for this frame and any previous spares/strikes
            _currentScoreTotal = TallyScoreSoFar();

            // Resetting throw tracker for next frame
            _throwsPerFrameCount = 0;

            // Incrementing frame counter to indicate moving to the next frame
            _currentFrame++;

            // Setting the "final frame" status for the game
            if (_currentFrame == 10)
            {
                _isFinalFrame = true;
            }
        }

        /// <summary>
        /// Signal the end of the game after final frame is completed and return the final score as an integer.
        /// </summary>
        /// <returns>Final score of game as integer</returns>
        public int EndGame()
        {
            if (_isFinalFrame)
            {
                // Making sure final throws have been made
                if (_frames[_currentFrame - 1].FirstAttemptScore == 10 && _throwsPerFrameCount < 3)
                {
                    throw new Exception("Cannot move to end game - the third throw must be completed.");

                }
                if (_frames[_currentFrame - 1].FirstAttemptScore != 10 && _throwsPerFrameCount < 2)
                {
                    throw new Exception("Cannot move to end game - the second throw must be completed.");
                }
                _currentScoreTotal = TallyScoreSoFar();
                return GetCurrentScoreTotal();
            }
            return GetCurrentScoreTotal();
        }



        private bool IsSpare(int pinsKnockedDownInFirstThrow, int pinsKnockedDownInSecondThrow)
        {
            return pinsKnockedDownInFirstThrow + pinsKnockedDownInSecondThrow == 10;
        }

        public string GetCurrentFrameTypeName()
        {
            if (_frames[_currentFrame - 1] == null)
                throw new Exception("Current frame has not been started");

            return _frames[_currentFrame - 1].GetType().Name;
        }

        public bool CanMakeThirdThrowThisFrame()
        {
            return _canMakeThirdThrow;
        }

    }
}
