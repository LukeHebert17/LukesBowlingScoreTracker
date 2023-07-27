using LukesBowlingScoreTracker.FrameScoringTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LukesBowlingScoreTracker
{
    /// <summary>
    ///  A universal representation of all frames currently in the game - for displaying status
    ///  to classes outside of Game and its internal classes.
    /// </summary>
    public class FrameSummary
    {
        public string FrameType { get; set; }
        
        public string PinsKnockedDownInFirstThrow { get; set; }
        public bool IsFinalScoreForGame { get; set; }

        private int _framePosition;
        private string _type;
        // TODO: Made nullable now - may do the same for original IFrame implementations properties
        private int? _firstThrowPins;
        private int? _secondThrowPins;
        private int? _thirdThrowPins;



        public FrameSummary(int framePosition, bool isFinalScoreForFrame, string type, int? firstThrowPins, int? secondThrowPins, int? thirdThrowPins = null)
        {
            _framePosition = framePosition;
            IsFinalScoreForGame = isFinalScoreForFrame;
            _type = type;
            _firstThrowPins = firstThrowPins;
            _secondThrowPins = secondThrowPins;
            _thirdThrowPins = thirdThrowPins;
        }

        public string GetPinsKnockedDownForFrame() 
        {
            var outputString = $"Frame position: {_framePosition}\n";

            switch (_type)
            {
                case "Strike":
                    outputString += "Roll 1: X\n";
                    break;
                case "Spare":
                    outputString += $"Roll 1: {_firstThrowPins}\tRoll 2: \\ \n";
                    break;
                case "FinalFrame":
                    outputString += GetFinalFrameString();
                    break;
                default:
                    outputString += GetRegularFrameString();
                    break;
            }
            return outputString;
        }


        private string GetFinalFrameString()
        {
            string outputString = "Roll 1: ";
            outputString += (_firstThrowPins == null) ? "" : _firstThrowPins.ToString();
            outputString += "\tRoll 2: ";
            outputString += (_secondThrowPins == null) ? "" : _secondThrowPins.ToString();
            outputString += "\tRoll 3: ";
            outputString += (_thirdThrowPins == null) ? "" : _thirdThrowPins.ToString();
            outputString += '\n';
            return outputString;
        }

        private string GetRegularFrameString()
        {
            string outputString = "Roll 1: ";
            outputString += (_firstThrowPins == null) ? "" : _firstThrowPins.ToString();
            outputString += "\tRoll 2: ";
            outputString += (_secondThrowPins == null) ? "" : _secondThrowPins.ToString();
            outputString += '\n';
            return outputString;
        }
    }
}
