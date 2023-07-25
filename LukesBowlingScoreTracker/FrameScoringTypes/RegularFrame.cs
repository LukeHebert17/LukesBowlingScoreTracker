using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker.FrameScoringTypes
{
    public class RegularFrame : IFrame
    {
        public int FirstAttemptScore { get; set; }
        public int SecondAttemptScore { get; set; }
        public bool IsFinalScoreForFrame { get; set; }
        public int FrameNumber { get; set; }


        public int GetTotalScoreForFrame()
        {
            return FirstAttemptScore + SecondAttemptScore;
        }

        public RegularFrame(int frameNumber)
        {
            FrameNumber = frameNumber;
        }
    }
}
