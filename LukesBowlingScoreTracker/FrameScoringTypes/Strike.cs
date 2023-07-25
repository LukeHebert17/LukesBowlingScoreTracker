using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker.FrameScoringTypes
{
    internal class Strike : IFrame
    {
        public int FirstAttemptScore { get; set; }
        public int SecondAttemptScore { get; set; }
        public bool IsFinalScoreForFrame { get; set; }
        public int FrameNumber { get; set; }

        // Properties unique to strikes
        public int FirstFrameAfterStrikeTotalScore { get; set; }
        public int SecondFrameAfterStrikeTotalScore { get; set; }

        public int GetTotalScoreForFrame()
        {
            return 10 + FirstFrameAfterStrikeTotalScore + SecondFrameAfterStrikeTotalScore;
        }

        public Strike(int frameNumber)
        {
            FrameNumber = frameNumber;
        }
    }
}
