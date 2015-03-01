namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Drawing;
    using System.Drawing.Imaging;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;
    using System.Threading;

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

            PrintOnPosition(x, y, "(Press any key to skip intro)", ConsoleColor.Green);
            using (reader)
            {
                x = Console.WindowWidth / 2 - 40;
                y = Console.WindowHeight / 2 - 10;

                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var symbol in line)
                    {
                        PrintOnPosition(x, y, symbol, ConsoleColor.Green);
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

            PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
            string name = Console.ReadLine();

            while (name.Length < 2)
            {
                PrintOnPosition(x, y, "Enter username (at least 2 characters long): ", ConsoleColor.Green);
                name = Console.ReadLine();
            }

            return name;
        }

        public static void DisplayGameBoard(StreamReader reader)
        {
            using (reader)
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
            }
        }

        public static void DisplayCard(StreamReader reader, Card card)
        {
            int x = Console.WindowWidth / 2 - 40;
            int y = Console.WindowHeight / 2 - 10;

            using (reader)
            {
                string fileContents = reader.ReadToEnd();
                //PrintOnPosition(x, y, fileContents, ConsoleColor.Green);
                Console.WriteLine(fileContents);
            }
        }

        public static PlayersChoice GetPlayersChoice()
        {
            Console.WriteLine("Press: \nA: Play Card \nS: Save To Hand \nD: Play a card from hand");

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

        public static void PlayerDisplayMoves(List<string> moves, Player[] allPlayers, int x, int y)
        {
            foreach (var move in moves)
            {
                int separatorIndex = move.IndexOf('|');

                string playerName = move.Substring(0, separatorIndex - 1);

                string card = move.Substring(separatorIndex + 1, move.Length - (separatorIndex + 1));

                Player player = allPlayers.FirstOrDefault(p => p.Name == playerName);
                int playerID = Array.IndexOf(allPlayers, player);

                ConsoleColor color = SetPlayerColor(playerID);
                string printOnPosition = string.Format("{0} played {1}", playerName, card);
                PrintOnPosition(x, y, printOnPosition, color);
                y++;
            }
        }

        public static void DisplayEndGame(Player currentPlayer)
        {
            string diomand = new string((char)4, 80);
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

            Console.Write("\n\t\t\t\tYOUR SCORE: " + currentPlayer.Score);
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
                    color = ConsoleColor.Blue;
                    break;
                case 1:
                    color = ConsoleColor.Cyan;
                    break;
                case 2:
                    color = ConsoleColor.Green;
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

        private static void PrintOnPosition(int x, int y, char symbol, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(symbol);
        }

        private static void PrintOnPosition(int x, int y, string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        public static void DisplayImageOnTheConsole(string path)
        {
            Image Picture = Image.FromFile(path);
            Console.SetBufferSize((Picture.Width * 0x2), (Picture.Height * 0x2));
            FrameDimension Dimension = new FrameDimension(Picture.FrameDimensionsList[0x0]);
            int FrameCount = Picture.GetFrameCount(Dimension);
            int Left = Console.WindowLeft, Top = Console.WindowTop;
            char[] Chars = { '#', '#', '@', '%', '=', '+', '*', ':', '-', '.', ' ' };
            Picture.SelectActiveFrame(Dimension, 0x0);

            for (int i = 0x0; i < Picture.Height; i++)
            {
                for (int x = 0x0; x < Picture.Width; x++)
                {
                    Color Color = ((Bitmap)Picture).GetPixel(x, i);
                    int Gray = (Color.R + Color.G + Color.B) / 0x3;
                    int Index = (Gray * (Chars.Length - 0x1)) / 0xFF;
                    Console.Write(Chars[Index]);
                }

                Console.Write('\n');
            }

            Console.SetCursorPosition(Left, Top);
            Console.Read();
        }
    }
}
