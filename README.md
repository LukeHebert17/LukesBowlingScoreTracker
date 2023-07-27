# Luke Hebert's Bowling Score Tracker

## About

This program allows a user to track their score in a round of bowling via the command line. The initial objective for me was to create a strong core class (the Game class) that a variety of UI types could use. I had originally planned on using ASP.NET MVC (and even had it drawn out) but due to time, I ended up going with a basic command line app. The Game class does the bulk of the heavy lifting for the score tracking/calculation and that was intentional - I built this class to be used in a UI but also had back-end/scheduled-job purposes in mind such as using it to validate and/or calculate a CSV file with thousands of bowling scores. It was built using C# and .NET Framework 4.8.

## To use: the RunApp project

1. Make sure the startup object is set to the "RunApp" project's Program.cs before compilation.
2. Run the program and follow the prompts - scores are entered once per throw/roll.

Feel free to make use of the provided tests that serve as simulated games to test the program's logic. The original scorecards these tests are based on can be found in the .\LukesBowlingScareTracker.Tests\ScorecardImages\ directory.

### Game class

This class is made up of 2 files: Game.cs and GameCalcs.cs. The former handles rulekeeping and tracking the frames. The latter is used to calculate the current score of the game. They were split up to prevent clutter, but I think it could be refactored and refined further. One example being that generic Exceptions are used - these could be defined into custom exceptions in the future. The Game class is one of only 2 public classes in its project. While this class enforces some rules, the trade-off for making it versatile was relying on the UI or app that uses it to control some of the input logic.

### IFrame and its Implementations

This interface is used to represent the unique frames of a bowling scorecard. There are 4 implementations: regular (open) frames, spares, strikes, and final frames (represents the final frame of the game). The are internal to the Game class/project and are subject to the rules that class enforces.

### FrameSummary

This class is meant to give whatever kind of application that uses the Game class a way to display the current state/score of the game without directly accessing internal members. It also gives a more one-size-fits-all output, making formatting to a webpage or output file a bit easier.
