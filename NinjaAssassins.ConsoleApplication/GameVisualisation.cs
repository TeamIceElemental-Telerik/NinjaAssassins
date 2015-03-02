namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;
    using NinjaAssassins.Helper;

    // TODO
    public static class GameVisualisation
    {
        public static void DisplayLogo()
        {
            //int[,]?
        }

        public static void DisplayInitialMenu()
        {
            // TODO: beautify (select with arrow keys, highlight on select, change color)
            // for test purposes:



            Console.WriteLine("Please select: ");
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. Options");
            Console.WriteLine("3. High score");
            Console.WriteLine("4. How to play");
            Console.WriteLine("5. Quit");
            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            switch (choice)
            {
                case 1:
                    var reader = new StreamReader(Constants.GameIntro);
                    DisplayIntro(reader);
                    break;
                case 2:
                    DisplayGameOptions();
                    break;
                case 3:
                    DisplayHighScore();
                    break;
                case 4:
                    DisplayGameRules();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option between 1 and 4.");
                    break;
            }

        }

        public static void DisplayGameRules()
        {
            // StringBuilder
            // color
        }

        public static void DisplayGameOptions()
        {
            //(sound on/off)
        }

        public static void DisplayIntro(StreamReader reader)
        {
            int x = Console.WindowWidth - 40;
            int y = Console.WindowHeight - 9;

            ExtensionMethods.PrintOnPosition(x, y, "(Press any key to skip intro)", ConsoleColor.Green);
            using (reader)
            {
                x = Console.WindowWidth / 2 - 40;
                y = Console.WindowHeight / 2 - 10;

                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var symbol in line)
                    {
                        ExtensionMethods.PrintOnPosition(x, y, symbol, ConsoleColor.Green);
                        x = x == Console.WindowWidth - 40 ? Console.WindowWidth / 2 - 40 : x + 1;

                        if (Console.KeyAvailable)
                        {
                            Thread.Sleep(0);
                        }
                        else
                        {
                            Thread.Sleep(10);
                        }
                    }

                    y++;
                    x = Console.WindowWidth / 2 - 40;
                    line = reader.ReadLine();
                }
            }

            // PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2, gameIntro, ConsoleColor.Green);
            // TODO : beautify
            // http://en.wikipedia.org/wiki/League_of_Assassins
            // http://en.wikipedia.org/wiki/Ra%27s_al_Ghul
            // color
        }

        public static string AskForUsername()
        {
            int x = Console.WindowWidth / 2 - 40;
            int y = Console.WindowHeight / 2 + 10;

            ExtensionMethods.PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
            string name = Console.ReadLine();

            while (name.Length < 2)
            {
                ExtensionMethods.PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
                name = Console.ReadLine();
            }

            return name;
        }

        public static void DisplayGameBoard(Game game)
        {
            var reader = new StreamReader(Constants.GameBoard);
            using (reader)
            {
                int y = 0;
                string line = reader.ReadLine();
                while (line != null)
                {
                    ExtensionMethods.PrintOnPosition(0, y, line, ConsoleColor.Green);
                    line = reader.ReadLine();
                    y++;
                }
            }
            ExtensionMethods.PrintOnPosition(Console.WindowWidth - 30, Console.WindowHeight - 3, "NINJA ASSASSINS", ConsoleColor.Green);
            ExtensionMethods.PrintOnPosition(20, Console.WindowHeight - 2, "A console card game - a team project for Telerik Software Academy, course C# Part 2", ConsoleColor.Green);

            // display names and scores
            string score = "Score";
            ExtensionMethods.PrintOnPosition(20, 3, game.Players[1].Name, ConsoleColor.Green);
            ExtensionMethods.PrintOnPosition(20, Console.WindowHeight - 7, game.Players[3].Name, ConsoleColor.Yellow);

            ExtensionMethods.PrintOnPosition(53, 3, score + ": " + game.Players[1].Score, ConsoleColor.Green);
            ExtensionMethods.PrintOnPosition(53, Console.WindowHeight - 7, score + ": " + game.Players[3].Score, ConsoleColor.Yellow);

            for (int i = 0; i < game.Players[0].Name.Length; i++)
            {
                ExtensionMethods.PrintOnPosition(7, 7 + i, game.Players[0].Name[i], ConsoleColor.Green);
                ExtensionMethods.PrintOnPosition(Constants.xRightBorder - 8, 7 + i, game.Players[2].Name[i], ConsoleColor.Green);
            }

            for (int i = 0; i < score.Length; i++)
            {
                ExtensionMethods.PrintOnPosition(7, 17 + i, score[i], ConsoleColor.Green);
                ExtensionMethods.PrintOnPosition(Constants.xRightBorder - 8, 17 + i, score[i], ConsoleColor.Green);
            }

            for (int i = 0; i < 3; i++)
            {
                ExtensionMethods.PrintOnPosition(7, 23 + i, game.Players[0].Score.ToString().PadRight(3)[i], ConsoleColor.Green);
                ExtensionMethods.PrintOnPosition(Constants.xRightBorder - 8, 23 + i, game.Players[2].Score.ToString().PadRight(3)[i], ConsoleColor.Green);
            }

            // display player's cards
            for (int i = 0; i < game.Players[3].Hand.Count && i < 3; i++)
            {
                 ExtensionMethods.PrintOnPosition(19 + i * 17, Console.WindowHeight - 5, game.Players[3].Hand[i].ToString(), ConsoleColor.Yellow);
            }
        }

        public static void DisplayCard(StreamReader reader, Card card, int x, int y)
        {
            ExtensionMethods.ClearConsolePart(x, y, 20, 20);

            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    ExtensionMethods.PrintOnPosition(x, y, line, ConsoleColor.Green);
                    line = reader.ReadLine();
                    y++;
                }
            }
        }

        public static void DisplayPlayersChoiceOptions(int x, int y, ConsoleColor color = ConsoleColor.Green)
        {
            var options = new List<string>
            {
                "Play Card",
                "Save To Hand",
                "Play a card from hand",
            };

            ExtensionMethods.ClearConsolePart(x, y, 40, 5);
            ExtensionMethods.PrintOnPosition(x, y, "Choose an option by pressing a key:", ConsoleColor.White);
            for (int i = 0; i < options.Count; i++)
            {
                string key = i == 0 ? "A" : i == 1 ? "S" : "D";

                ExtensionMethods.PrintOnPosition(x, y + i + 1, key + ": ", ConsoleColor.Yellow);
                ExtensionMethods.PrintOnPosition(x + 3, y + i + 1, options[i], ConsoleColor.White);
            }
        }

        public static PlayersChoice GetPlayersChoice()
        {
            PlayersChoice choice = PlayersChoice.NotSelected;

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            switch (pressedKey.Key)
            {
                case ConsoleKey.A:
                    choice = PlayersChoice.PlayCard;
                    break;
                case ConsoleKey.S:
                    choice = PlayersChoice.SaveToHand;
                    break;
                case ConsoleKey.D:
                    choice = PlayersChoice.PlayDifferentCard;
                    break;
                default:
                    throw new InvalidOperationException("You did not choose an option.");
            }

            return choice;
        }

        public static void DisplayPlayerMoves(List<string> moves, Game game, int x, int y)
        {
            ExtensionMethods.ClearConsolePart(x, y, 40, 10);
            ExtensionMethods.PrintOnPosition(x, y, "Players' moves:", ConsoleColor.White);
            foreach (var move in moves)
            {
                int separatorIndex = move.IndexOf('|');

                string playerName = move.Substring(0, separatorIndex);
                string message = move.Substring(separatorIndex + 1, move.Length - (separatorIndex + 1));

                Player player = game.Players.FirstOrDefault(p => p.Name == playerName);
                int playerID = Array.IndexOf(game.Players, player);

                ConsoleColor color = message.IndexOf("killed") > -1 ? ConsoleColor.Red : SetPlayerColor(playerID);

                string printOnPosition = string.Format("{0}{1}", playerName, message);
                ExtensionMethods.PrintOnPosition(x, y + 1, printOnPosition, color);
                y++;
            }
        }

        public static void DisplayCurrentGameState(Game game, int x, int y, ConsoleColor color = ConsoleColor.Green)
        {
            ExtensionMethods.PrintOnPosition(x, y, "Ninja Assassins ", ConsoleColor.White);

            var stateLog = new Dictionary<string, string>();
            stateLog.Add("Remaining cards in deck: ", (game.Deck.Count - 1).ToString());
            stateLog.Add("Player in turn: ", game.PlayerInTurn.Name);
            stateLog.Add("Next player: ", game.NextPlayer.Name);
            stateLog.Add("Game state: ", game.GameState.ToString());

            foreach (var log in stateLog)
            {
                ExtensionMethods.PrintOnPosition(x, y + 1, log.Key, ConsoleColor.Gray);
                ExtensionMethods.PrintOnPosition(x + 25, y + 1, log.Value, ConsoleColor.Yellow);
                y++;
            }
        }

        public static void DisplayEndGame(Player currentPlayer)
        {
            string diomand = new string((char)4, Console.WindowWidth);
            // char symbol4 = (char)4;
            int position = Console.WindowWidth / 2 - 10;

            using (StreamWriter highScoreWrite = new StreamWriter(Constants.HighScoreFilePath, true))
            {
                highScoreWrite.WriteLine("{0}|{1}", currentPlayer.Score, currentPlayer.Name);
                highScoreWrite.Close();
            }

            StringBuilder frame = new StringBuilder();
            //frame.Append(symbol4);
            //frame.Append(' ', 78);
            //frame.Append(symbol4);
            StringBuilder gameOver = new StringBuilder();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            // gameOver.Append(symbol4);
            //gameOver.Append(' ', 34);
            gameOver.Append(@"                                             
                                   ___   ___  ___  ___  ____      ___   __ __  ____ ____ 
                                  // \\ // \\ ||\\//|| ||        // \\  || || ||    || \\
                                 (( ___ ||=|| || \/ || ||==     ((   )) \\ // ||==  ||_//
                                  \\_|| || || ||    || ||___     \\_//   \V/  ||___ || \\
                                                         ");
            gameOver.Append("\n");
            gameOver.Append("\n");

            // gameOver.Append(' ', 35);
            //   gameOver.Append(symbol4);

            Console.Clear();
            Console.WriteLine();
            Console.Write(diomand);
            // Console.Write(frame);
            Console.Write(gameOver);
            //  Console.Write(frame);
            Console.WriteLine(diomand);

            Console.Write("\n\t\t\t\t\t\t\tYOUR SCORE: " + currentPlayer.Score);
            Console.Write("\n\n" + diomand);

            DisplayHighScore();

            Console.WriteLine();
            Console.Write(diomand);
            Console.WriteLine(" Press ENTER for New Game or Press ESC for Exit");

            var pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.Enter)
            {
                Console.Clear();
            }
            else if (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.WriteLine("Bye Bye");
                Environment.Exit(0);
            }
        }

        public static void DisplayHighScore()
        {
            var highScores = new SortedDictionary<int, string>();
            using (StreamReader highScoreRead = new StreamReader(Constants.HighScoreFilePath))
            {
                var line = highScoreRead.ReadLine();

                while (line != null)
                {

                    var currentHighScore = line.Split('|');

                    try
                    {
                        if ((highScores.ContainsValue(currentHighScore[1]) && highScores.ContainsKey(int.Parse(currentHighScore[0]))))
                        {
                            line = highScoreRead.ReadLine();
                        }
                        else
                        {
                            highScores.Add(int.Parse(currentHighScore[0]), currentHighScore[1]);
                            line = highScoreRead.ReadLine();
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        ExtensionMethods.HandleExceptions(e, Constants.xRightBorder + 3, Console.WindowHeight - 9, ConsoleColor.White);
                        break;
                    }
                }
                highScoreRead.Close();
            }
            int scoreCount = highScores.Count >= 10 ? 10 : highScores.Count;

            Console.WriteLine(@"
                                 __  __ __   ___  __  __     __    ___   ___   ____   ____
                                 ||  || ||  // \\ ||  ||    (( \  //    // \\  || \\ ||   
                                 ||==|| || (( ___ ||==||     \\  ((    ((   )) ||_// ||== 
                                 ||  || ||  \\_|| ||  ||    \_))  \\__  \\_//  || \\ ||___
                                                                        ");

            Console.WriteLine();
            Console.WriteLine();
            foreach (var score in highScores.Skip(highScores.Count - scoreCount).Take(scoreCount).Reverse())
            {
                var currentHighScore = score;
                int x = Console.WindowWidth / 2 - 10;

                Console.WriteLine("{0}{2,-10} {1,5}", new string(' ', x), currentHighScore.Key, currentHighScore.Value);
            }

        }

        private static ConsoleColor SetPlayerColor(int playerID)
        {
            ConsoleColor color;
            switch (playerID)
            {
                case 0:
                    color = ConsoleColor.Gray;
                    break;
                case 1:
                    color = ConsoleColor.Cyan;
                    break;
                case 2:
                    color = ConsoleColor.Magenta;
                    break;
                case 3:
                    color = ConsoleColor.Yellow;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }

            return color;
        }
    }
}
