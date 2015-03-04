namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;
    using NinjaAssassins.Helper;

    // TODO : refactor
    public static class GameVisualisation
    {
        public static void SetInitialConsoleSize()
        {
            Console.BufferWidth = Console.WindowWidth = 125;
            Console.BufferHeight = Console.WindowHeight = 35;

            Console.CursorVisible = false;
        }

        public static void DisplayInitialMenu()
        {
            try
            {
                Sounds.GameStartMenu(Sounds.PlaySound);
            }
            catch (Exception e)
            {
                ExtensionMethods.PrintOnPosition(10, Console.WindowHeight - 6, "Uh oh!", ConsoleColor.White);
                ExtensionMethods.HandleExceptions(e, 10, Console.WindowHeight - 5, ConsoleColor.White);
            }

            int x = 0;
            int y = 0;

            Console.Clear();
            StreamReader strReader = new StreamReader(Constants.GameMenuIntro);
            using (strReader)
            {
                string line = strReader.ReadLine();
                while (line != null)
                {
                    ExtensionMethods.PrintOnPosition(x, y, line, ConsoleColor.Green);
                    line = strReader.ReadLine();
                    y++;
                }
            }

            x = Constants.InitialMenuX;
            y = Constants.InitialMenuY;

            var text = "Please select: ";
            ExtensionMethods.PrintOnPosition(x, y, text, ConsoleColor.White);

            var options = new string[] 
            {
                "New Game",
                "Options",
                "How to play",
                "High Scores",
                "Quit"
            };

            for (int i = 0; i < options.Length; i++)
            {
                ExtensionMethods.PrintOnPosition(x, y + i + 1, i + 1 + ": ", ConsoleColor.Yellow);
                ExtensionMethods.PrintOnPosition(x + 3, y + i + 1, options[i], ConsoleColor.Green);
            }

            ExtensionMethods.PrintOnPosition(x, y + options.Length + 1, string.Empty);

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);

            Console.Clear();
            switch (pressedKey.KeyChar)
            {
                case '1':
                    var reader = new StreamReader(Constants.GameIntro);
                    DisplayIntro(reader);
                    break;
                case '2':
                    DisplayGameOptions();
                    GoBackToInitialMenu(Constants.GoBackX, Constants.GoBackY, ConsoleColor.White);
                    break;
                case '3':
                    DisplayGameRules();
                    GoBackToInitialMenu(Constants.GoBackX, Constants.GoBackY + 3, ConsoleColor.White);
                    break;
                case '4':
                    reader = new StreamReader(Constants.HighScoreFilePath, Encoding.GetEncoding("Windows-1251"));
                    var highScores = GameLogic.GetHighScores(reader, Constants.HighScoresCount);
                    Console.ForegroundColor = ConsoleColor.Green;
                    DisplayHighScore(highScores);
                    Console.ResetColor();
                    GoBackToInitialMenu(Constants.GoBackX, Constants.GoBackY, ConsoleColor.White);
                    break;
                case '5':
                    ExtensionMethods.PrintMatrix(Constants.ByeByeMessage, Constants.GoBackX - 5, Console.WindowHeight / 2 - 5, ConsoleColor.Green);
                    Thread.Sleep(1000);
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    ExtensionMethods.PrintOnPosition(Constants.GoBackX, Constants.GoBackY, "Please select an option between 1 and 4.");
                    DisplayInitialMenu();
                    break;
            }
        }

        private static void GoBackToInitialMenu(int x, int y, ConsoleColor color)
        {
            ExtensionMethods.PrintOnPosition(x, y, "Press Backspace to go back", color);
            var pressedKey = Console.ReadKey(true);

            while (pressedKey.Key != ConsoleKey.Backspace)
            {
                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.Backspace)
                {
                    break;
                }
            }

            Console.Clear();
            DisplayInitialMenu();
        }

        public static void DisplayGameRules()
        {
            try
            {
                Sounds.GameRulesMenu(Sounds.PlaySound);
            }
            catch (Exception e)
            {
                ExtensionMethods.PrintOnPosition(10, Console.WindowHeight - 6, "Uh oh!", ConsoleColor.White);
                ExtensionMethods.HandleExceptions(e, 10, Console.WindowHeight - 5, ConsoleColor.White);
            }

            StreamReader reader = new StreamReader(Constants.GameRules);
            using (reader)
            {
                string fileContents = reader.ReadToEnd();

                ExtensionMethods.PrintOnPosition(0, 0, fileContents, ConsoleColor.Green);
                Console.ResetColor();
            }
        }

        public static void DisplayGameOptions()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            // (sound on/off)
            // Implement in the cards dealt by the player the sounds
            Console.WriteLine("     By default ALL the sounds are there!");
            Console.WriteLine();
            Console.Write("Do you want to have sound in the game [ YES / NO ] : ");
            string soundState = Console.ReadLine();
            soundState = soundState.ToLower();
            // create isOn / isOff logic in another class for General Sound and For in game Background sound
            if (soundState == "no")
            {
                Sounds.PlaySound = false;
                Console.WriteLine("The sound is OFF !");
            }
            else if (soundState == "yes")
            {
                Sounds.PlaySound = true;
                Console.WriteLine("The sound is ON !");
            }
            else if (!(soundState == "no") && !(soundState == "yes"))
            {
                Console.WriteLine();
                Console.Write("     Yes or No ? --> ");
                soundState = Console.ReadLine();

                if ((!(soundState == "no") && !(soundState == "yes")))
                {
                    Console.WriteLine();
                    Console.WriteLine("In this case I suppose you mean NO.");
                    Sounds.PlaySound = false;
                    Console.WriteLine("The sound is OFF !");
                }
                else if (soundState == "no")
                {
                    Sounds.PlaySound = false;
                    Console.WriteLine("The sound is OFF !");
                }
                else if (soundState == "yes")
                {
                    Sounds.PlaySound = true;
                    Console.WriteLine("The sound is ON !");
                }
            }
            Console.ResetColor();
        }

        public static void DisplayIntro(StreamReader reader)
        {
            try
            {
                Sounds.GameRulesMenu(Sounds.PlaySound);
            }
            catch (Exception e)
            {
                ExtensionMethods.PrintOnPosition(10, Console.WindowHeight - 6, "Uh oh!", ConsoleColor.White);
                ExtensionMethods.HandleExceptions(e, 10, Console.WindowHeight - 5, ConsoleColor.White);
            }

            int x = Constants.ExceptionMessageX;
            int y = Constants.ExceptionMesssageWrongChoiceY;

            ExtensionMethods.PrintOnPosition(x, y, "(Press any key to skip intro)", ConsoleColor.Green);
            using (reader)
            {
                x = Constants.IntroX;
                y = Constants.IntroY;

                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var symbol in line)
                    {
                        ExtensionMethods.PrintOnPosition(x, y, symbol, ConsoleColor.Green);
                        x = x == Constants.IntroX - 10 ? Constants.IntroX : x + 1;

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
                    x = Constants.IntroX;
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
            Console.CursorVisible = true;

            int x = Constants.IntroX;
            int y = Constants.ExceptionMesssageY + 2;

            ExtensionMethods.PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
            string name = Console.ReadLine();

            while (name.Length < 2)
            {
                ExtensionMethods.PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
                name = Console.ReadLine();
            }

            Console.CursorVisible = false;
            return name;
        }

        public static void DisplayGameBoard(Game game)
        {
            var reader = new StreamReader(Constants.GameBoard, Encoding.GetEncoding("Windows-1251"));
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
            ExtensionMethods.PrintOnPosition(20, 3, game.Players[1].Name, ConsoleColor.Green);
            ExtensionMethods.PrintOnPosition(20, Console.WindowHeight - 7, game.Players[3].Name, ConsoleColor.Yellow);

            ExtensionMethods.PrintOnPosition(60, 3, game.Players[1].Score.ToString(), ConsoleColor.Green);
            ExtensionMethods.PrintOnPosition(60, Console.WindowHeight - 7, game.Players[3].Score.ToString(), ConsoleColor.Yellow);

            for (int i = 0; i < game.Players[0].Name.Length; i++)
            {
                ExtensionMethods.PrintOnPosition(7, 7 + i, game.Players[0].Name[i], ConsoleColor.Green);
                ExtensionMethods.PrintOnPosition(Constants.RightBorderX - 8, 7 + i, game.Players[2].Name[i], ConsoleColor.Green);
            }

            for (int i = 0; i < 3; i++)
            {
                ExtensionMethods.PrintOnPosition(7, 23 + i, game.Players[0].Score.ToString().PadRight(3)[i], ConsoleColor.Green);
                ExtensionMethods.PrintOnPosition(Constants.RightBorderX - 8, 23 + i, game.Players[2].Score.ToString().PadRight(3)[i], ConsoleColor.Green);
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
            var options = new string[]
            {
                "Play Card",
                "Save To Hand",
                "Play a card from hand",
            };

            ExtensionMethods.ClearConsolePart(x, y, 40, 5);
            ExtensionMethods.PrintOnPosition(x, y, "Choose an option by pressing a key:", ConsoleColor.White);
            for (int i = 0; i < options.Length; i++)
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

        public static void DisplayDead()
        {
            //            ExtensionMethods.PrintOnPosition(0, Constants.CardY + 5, @"
            //                                          ___   ____  ___   ___ 
            //                                         || \\ ||    // \\ || \\ 
            //                                         ||  | ||==  ||=|| ||  |
            //                                         || // ||___ || || || //
            //            ", ConsoleColor.Red);

            ExtensionMethods.PrintMatrix(Constants.DeadMessage, Constants.CardX, Constants.CardY + 16, ConsoleColor.Red);

            try
            {
                Sounds.GameOverLoseSound(Sounds.PlaySound);
            }
            catch
            {
            }

            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                return;
            }

        }

        public static void DisplayGameEnd(Player currentPlayer, KeyValuePair<string, int> winner, Dictionary<string, int> highScores)
        {
            string diomand = new string((char)4, Console.WindowWidth);
            int position = Constants.EndGameX;

            StringBuilder gameOver = new StringBuilder();

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            gameOver.Append("\n")
            .Append(diomand)
            .Append(@"                                             
                                   ___   ___  ___  ___  ____      ___   __ __  ____ ____ 
                                  // \\ // \\ ||\\//|| ||        // \\  || || ||    || \\
                                 (( ___ ||=|| || \/ || ||==     ((   )) \\ // ||==  ||_//
                                  \\_|| || || ||    || ||___     \\_//   \V/  ||___ || \\
                                                         ")
            .Append("\n\n")
            .Append(diomand + "\n")
            .AppendLine(new string(' ', 53) + "YOUR SCORE: " + currentPlayer.Score)
            .AppendLine(new string(' ', 53) + "WINNER: " + winner.Key + " " + winner.Value)
            .Append("\n\n")
            .Append(diomand);

            Console.Clear();
            Console.Write(gameOver);

            DisplayHighScore(highScores);

            Console.Write("\n" + diomand);
            GetUserEndGameChoice();
        }

        public static void DisplayHighScore(Dictionary<string, int> highScores)
        {
            Console.WriteLine(@"
                               __  __ __   ___  __  __     __    ___   ___   ____   ____  __
                               ||  || ||  // \\ ||  ||    (( \  //    // \\  || \\ ||    (( \
                               ||==|| || (( ___ ||==||     \\  ((    ((   )) ||_// ||==   \\
                               ||  || ||  \\_|| ||  ||    \_))  \\__  \\_//  || \\ ||___ \_))
                                                                        ");
            Console.WriteLine();
            Console.WriteLine();

            foreach (var highScore in highScores)
            {
                var currentHighScore = highScore.Key;
                var playerName = highScore.Value;

                Console.WriteLine("{0}{2,-15}{1,10}", new string(' ', Constants.EndGameX - 5), currentHighScore, playerName);
            }
        }

        public static void GetUserEndGameChoice()
        {
            Console.WriteLine(" Press ENTER for New Game or Press ESC for Exit");

            var pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Play.Main();
            }
            else if (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                ExtensionMethods.PrintMatrix(Constants.ByeByeMessage, Constants.GoBackX - 5, Console.WindowHeight / 2 - 5, ConsoleColor.Green);
                //                ExtensionMethods.PrintOnPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2 - 5, @"
                //                                       ____   ____   ____   ____   ____   ____
                //                                       ||  | \\  // ||      ||  | \\  // ||
                //                                       ||==   \\//  ||==    ||==   \\//  ||==
                //                                       ||___|  ||   ||___   ||___|  ||   ||___
                //", ConsoleColor.Green);
                Thread.Sleep(1000);
                Console.Clear();
                Environment.Exit(0);
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
