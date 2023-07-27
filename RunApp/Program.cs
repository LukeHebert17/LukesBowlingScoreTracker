using CommandLineScoreTrackerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bowlingTracker = new CLIBowlingTracker();
            bowlingTracker.StartGame();
        }
    }
}
