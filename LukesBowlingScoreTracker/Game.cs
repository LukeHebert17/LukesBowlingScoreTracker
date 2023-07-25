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
        private bool _hasBallBeenThrownOnceForFrame;


        public Game()
        {
            _frames = new IFrame[10]; // Sets us up for a 10-frame game
            _currentFrame = 1; 
            _hasBallBeenThrownOnceForFrame = false;
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

        public void Strike()
        {
            _frames[_currentFrame - 1] = new Strike(_currentFrame);

            // TODO: PRIORITY Add logic here for final frame strike
        }

        public void FirstAttemptOfFrame(int pinsKnockedDownInFirstThrow)
        {
            if (pinsKnockedDownInFirstThrow > 10 || pinsKnockedDownInFirstThrow < 0)
                // Exceptions could stand to be more specialized, but the generic Exception one works for now
                throw new Exception("Cannot knock down more than 10 pins or less than 0");
            else if (pinsKnockedDownInFirstThrow == 10) // TODO: Logic needed for last 2 frames here
                Strike();
            else
                // Starts as regular frame, may be redefined as spare if need be
                _frames[_currentFrame - 1] = new RegularFrame(_currentFrame);

            // Setting how many pins were knocked down in the first throw
            _frames[_currentFrame - 1].FirstAttemptScore = pinsKnockedDownInFirstThrow;

            _hasBallBeenThrownOnceForFrame = true;
        }

        public void SecondAttemptOfFrame(int pinsKnockedDownInSecondThrow)
        {
            if (pinsKnockedDownInSecondThrow > 10 || pinsKnockedDownInSecondThrow < 0)
                // Exceptions could stand to be more specialized, but the generic Exception one works for now
                throw new Exception("Cannot knock down more than 10 pins or less than 0");
            else if (!_hasBallBeenThrownOnceForFrame)
                throw new Exception("Can't record second attempt of frame yet - ball has yet to be thrown once.");
            else if (_frames[_currentFrame - 1].FirstAttemptScore + pinsKnockedDownInSecondThrow > 10)
                throw new Exception("Can't have more than 10 pins per round knocked down");
            else if (_frames[_currentFrame - 1].GetType() == typeof(Strike))
                throw new Exception("Strike has been thrown for this frame - no more throws can be made"); // TODO: This will need to be edited for final frame

            var pinsKnockedDownInFirstThrow = _frames[_currentFrame - 1].FirstAttemptScore;
            // Setting how many pins were knocked down in the second throw
            _frames[_currentFrame - 1].SecondAttemptScore = pinsKnockedDownInSecondThrow;

            // Checking if this round is a spare and handling it if it is
            if (IsSpare(pinsKnockedDownInFirstThrow, pinsKnockedDownInSecondThrow))
            {
                var spare = new Spare(_currentFrame);
                spare.FirstAttemptScore = pinsKnockedDownInFirstThrow;
                spare.SecondAttemptScore = pinsKnockedDownInSecondThrow;
                _frames[_currentFrame - 1] = spare; // regular frame has now become a spare

                // TODO: PRIORITY Add logic here for spare in final frame
            }
            else
            {
                // This frame's score has been tallied and it's now ready to be displayed and used in spare/strike calc
                _frames[_currentFrame - 1].IsFinalScoreForFrame = true;
            }


        }

        public void MoveToNextFrame()
        {
            if ( _frames[_currentFrame - 1] == null || (_frames[_currentFrame - 1].GetType() != typeof(Strike) 
                && _frames[_currentFrame - 1].SecondAttemptScore == null) ) // TODO: fix this
                throw new Exception("Cannot move to next frame - current frame must be completed.");

            // Assuming all is well, the score needs to be tallied for this frame and any previous spares/strikes
            _currentScoreTotal = TallyScoreSoFar();

            // Resetting throw tracker for next frame
            _hasBallBeenThrownOnceForFrame = false;

            // Incrementing frame counter to indicate moving to the next frame
            _currentFrame++;
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

    }
}
