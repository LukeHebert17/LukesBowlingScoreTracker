using LukesBowlingScoreTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineScoreTrackerUI
{
    public class CLIBowlingTracker
    {
        public CLIBowlingTracker()
        {

        }

        /// <summary>
        /// Method to begin the game
        /// </summary>
        public void StartGame()
        {
            DisplayIntro();
            var game = new Game();

            RunGameTrackingLoop(game);
            
        }

        private void RunGameTrackingLoop(Game game)
        {
            // Getting into the user interaction portion
            for (int i = 1; i <= 10; i++)
            {
                if (i > 1)
                    DisplayCurrentFramesAndScores(game);
                else
                    Console.WriteLine("Beginning game at frame 1. . .");

                // Retrieving and validating user input for first throw
                Console.Write($"\nFrame {i}:\nEnter pins knocked down during first throw of frame: ");
                var userInput = Console.ReadLine();
                bool isValidInput = IsValidUserInput(userInput);
                while (!isValidInput)
                {
                    Console.Write(" - please enter number of pins again: ");
                    userInput = Console.ReadLine();
                    isValidInput = IsValidUserInput(userInput);
                }

                int firstThrowPins = Int32.Parse(userInput);

                game.FirstAttemptOfFrame(firstThrowPins);

                // An example of the UI still having some need to manage the flow of the game outside the Game class
                if (firstThrowPins == 10 && game.GetCurrentFrameNumber() != 10)
                {
                    Console.WriteLine("\nCongrats on the strike!");
                    game.MoveToNextFrame();
                    continue;
                }
                
                // Retrieving and validating user input for second throw
                Console.Write($"\n\nEnter pins knocked down during second throw of frame: ");
                userInput = Console.ReadLine();
                isValidInput = IsValidUserInput(userInput, firstThrowPins);
                while (!isValidInput)
                {
                    Console.Write(" - please enter number of pins again: ");
                    userInput = Console.ReadLine();
                    isValidInput = IsValidUserInput(userInput);

                }

                int secondThrowPins = Int32.Parse(userInput);

                if (firstThrowPins + secondThrowPins == 10)
                {
                    Console.WriteLine("\nNice spare!");
                }
                
                // Recording the number of pins knocked down by the second throw
                game.SecondAttemptOfFrame(secondThrowPins);

                // Moving to next frame if it's not the final frame
                if (game.GetCurrentFrameNumber() != 10)
                { 
                    game.MoveToNextFrame(); 
                    continue; 
                }

                // Now we get to the final frame handling
                //Console.Write($"\nFrame {i} (final):\nEnter pins knocked down during third throw of frame: ");

                // Checking if a third throw is available
                // Handling a first throw strike in the final frame
                if (game.GetCurrentFrameNumber() == 10 && firstThrowPins == 10)
                {
                    Console.Write($"\nFrame {i} (final):\nEnter pins knocked down during third throw of frame!: ");
                    userInput = Console.ReadLine();
                    isValidInput = IsValidUserInput(userInput);
                    while (!isValidInput)
                    {
                        Console.Write(" - please enter number of pins again: ");
                        userInput = Console.ReadLine();
                        isValidInput = IsValidUserInput(userInput);
                    }
                    int thirdThrowPins = Int32.Parse(userInput);
                    game.ThirdAttemptOfFinalFrame(thirdThrowPins);
                }
                // Handling a first throw spare in the final frame
                else if (game.GetCurrentFrameNumber() == 10 && (firstThrowPins + secondThrowPins) == 10
                    && firstThrowPins != 10)
                {
                    // slightly different (less excited) tone for final throw
                    Console.Write($"\nFrame {i} (final):\nEnter pins knocked down during third throw of frame. . .: ");
                    userInput = Console.ReadLine();
                    isValidInput = IsValidUserInput(userInput, secondThrowPins, true); // making sure the final throw is good
                    while (!isValidInput)
                    {
                        Console.Write(" - please enter number of pins again: ");
                        userInput = Console.ReadLine();
                        isValidInput = IsValidUserInput(userInput);

                    }
                    int thirdThrowPins = Int32.Parse(userInput);
                    game.ThirdAttemptOfFinalFrame(thirdThrowPins);
                }
                else // i.e. (!game.CanMakeThirdThrowThisFrame()) 
                {
                    Console.WriteLine("\nNo more throws left for this frame.\n");
                }

                // By the time this code is reached, the game is over
                game.EndGame();

                // Record and display the final score
                Console.WriteLine();
                DisplayCurrentFramesAndScores(game);

            }
        }

        private void DisplayIntro()
        {
            Console.WriteLine("Welcome to Luke's Bowling Score Tracker for Windows (Console Version)!\n\nBeginning scoring now:\n\n");
        }

        /// <summary>
        /// Displays current known score to the command line during execution
        /// </summary>
        /// <param name="game"></param>
        private void DisplayCurrentFramesAndScores(Game game)
        {
            Console.WriteLine("----------");
            var frameSummaries = game.GetFrameSummaries();
            foreach (var frameSummary in frameSummaries)
            {
                Console.WriteLine(frameSummary.GetPinsKnockedDownForFrame());
            }
            string finalScoreMessage = (game.IsGameFinal) ? "(FINAL)" : "(not final)";
            Console.WriteLine($"Overall score {finalScoreMessage}: {game.GetCurrentScoreTotal()} points");
            Console.WriteLine("----------");
        }

        /// <summary>
        /// For validating the first throw of the frame
        /// </summary>
        /// <param name="userInput">command line input as string</param>
        /// <returns>true is valid, false if not</returns>
        private bool IsValidUserInput(string userInput)
        {
            var userInputAsInt = CanParseUserInputToInt(userInput);
            if (userInputAsInt == -1)
            {
                return false;
            }

            if (userInputAsInt < 0 || userInputAsInt > 10)
            {
                Console.Write("Number of pins knocked down must be an integer between 0 and 10 (inclusive)");
                return false;
            }

            return true;
        }

        /// <summary>
        /// For validating the second throw of the frame (or the third of the final frame in some cases)
        /// </summary>
        /// <param name="userInput">command line input as string</param>
        /// <param name="pinsKnockedDownInPreviousThrow"></param>
        /// <param name="isFinalFrame">used to indicate if this is a follow up throw in the final frame</param>
        /// <returns></returns>
        private bool IsValidUserInput(string userInput, int pinsKnockedDownInPreviousThrow, bool isFinalFrame = false)
        {
            int userInputAsInt = -1;
            try
            {
                userInputAsInt = Int32.Parse(userInput);
            }
            catch (Exception)
            {
                return false;
            }
            if (userInputAsInt < 0 || ((userInputAsInt + pinsKnockedDownInPreviousThrow) > 10 && isFinalFrame))
            {
                Console.Write($"Number of pins knocked down must be an integer between 0 and {10 - pinsKnockedDownInPreviousThrow} "); ;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Making sure the user entered a numeric input - return value of -1 indcates an non-numeric value
        /// </summary>
        /// <param name="userInput">command line input as string</param>
        /// <param name="firstThrowPins"></param>
        /// <returns></returns>
        private int CanParseUserInputToInt(string userInput, int? firstThrowPins = null)
        {
            int userInputAsInt = -1;
            try
            {
                userInputAsInt = Int32.Parse(userInput);
            }
            catch (Exception)
            {
                if (firstThrowPins == null)
                    Console.Write("Value entered for pins must be an integer between 0 and 10");
                else
                    Console.Write($"Value entered for pins must be an integer between 0 and {10 - firstThrowPins}");
                return -1;
            }
            return userInputAsInt;
        }



    }
}
