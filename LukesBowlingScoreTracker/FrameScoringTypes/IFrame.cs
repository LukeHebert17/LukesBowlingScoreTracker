using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker.FrameScoringTypes
{
    internal interface IFrame
    {
        int FirstAttemptScore { get; set; }
        int SecondAttemptScore { get; set; }

        bool IsFinalScoreForFrame { get; set; }
        int FrameNumber { get; set;  }
        int GetTotalScoreForFrame();

    }
}
