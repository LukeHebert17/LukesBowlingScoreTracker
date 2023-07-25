using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker.FrameScoringTypes
{
    public class Spare : IFrame
    {
        public int FirstAttemptScore { get; set; }
        public int SecondAttemptScore { get; set; }
        public bool IsFinalScoreForFrame { get; set; }

        // Properties unique to spares
        public int FirstFrameAfterSpareTotalScore { get; set; }
        public int FrameNumber { get; set; }

        public int GetTotalScoreForFrame()
        {
            return 10 + FirstFrameAfterSpareTotalScore;
        }

        public Spare(int frameNumber)
        {
            FrameNumber = frameNumber;
        }

        public Spare(RegularFrame regularFrameToBecomeSpare)
        {
            FirstAttemptScore = regularFrameToBecomeSpare.FirstAttemptScore;
        }
    }
}
