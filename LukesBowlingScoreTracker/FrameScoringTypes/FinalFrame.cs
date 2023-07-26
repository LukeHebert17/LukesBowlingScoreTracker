using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker.FrameScoringTypes
{
    internal class FinalFrame : IFrame
    {
        public int FirstAttemptScore { get; set; }
        public int SecondAttemptScore { get; set; }
        public int ThirdAttemptScore { get; set; }
        public bool IsFinalScoreForFrame { get; set; }
        public int FrameNumber { get; set; }

        public int GetTotalScoreForFrame()
        {
            return FirstAttemptScore + SecondAttemptScore + ThirdAttemptScore;
        }

        public FinalFrame()
        {
            FirstAttemptScore = 0;
            SecondAttemptScore = 0;
            ThirdAttemptScore = 0;
            IsFinalScoreForFrame = false;
            FrameNumber = 10; // assuming the game ends at 10 frames - could be modified for non-traditional bowling!
        }

    }
}
