﻿namespace NinjaAssassins.ConsoleApplication
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
            Console.WriteLine("3. How to play");
            Console.WriteLine("4. Quit");

            switch (Console.ReadLine())
            {
                case "1":
                    DisplayIntro();
                    break;
                case "2":
                    DisplayGameOptions();
                    break;
                case "3":
                    DisplayGameRules();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select an option between 1 and 4.");
                    break;

            }
            Console.Clear();
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

        public static void DisplayIntro()
        {
            var reader = new StreamReader(Constants.GameIntro);
            using (reader)
            {
                int x = Console.WindowWidth / 2 - 40;
                int y = Console.WindowHeight / 2 - 10;

                string line = reader.ReadLine();
                while (line != null)
                {
                    foreach (var symbol in line)
                    {
                        PrintOnPosition(x, y, symbol, ConsoleColor.Green);
                        x = x == Console.WindowWidth - 40 ? Console.WindowWidth / 2 - 40 : x + 1;
                        Thread.Sleep(10);
                    }
                    y++;
                    x = Console.WindowWidth / 2 - 40;
                    line = reader.ReadLine();
                }
            }
            Thread.Sleep(10000);
            Console.Clear();

            // PrintOnPosition(Console.WindowWidth / 2, Console.WindowHeight / 2, gameIntro, ConsoleColor.Green);
            // TODO : beautify
            // http://en.wikipedia.org/wiki/League_of_Assassins
            // http://en.wikipedia.org/wiki/Ra%27s_al_Ghul
            // color
        }

        public static string AskForUsername()
        {
            // TODO : beautify
            int x = Console.WindowWidth / 2 - 40;
            int y = Console.WindowHeight / 2 - 20;
            PrintOnPosition(x, y, "Please enter your name: ", ConsoleColor.Green);

            string name = Console.ReadLine();

            if (name.Length < 2)
            {
                while (name.Length < 2)
                {
                    y = Console.WindowHeight / 2 - 20;
                    PrintOnPosition(x, y, "Your name must be at least 2 characters long.", ConsoleColor.Green);
                    PrintOnPosition(x, y+1, "Please enter your name: ", ConsoleColor.Green);
                    name = Console.ReadLine();
                }
            }

            return name;
        }

        public static void DisplayGameBoard()
        {
            char[,] displayBord = new char[100, 100];

        }

        public static void DisplayCard(Card card)
        {
            try
            {
                using (StreamReader sr = new StreamReader(card.FilePath))
                {
                    string fileContents = sr.ReadToEnd();

                    // TODO : change color
                    Console.WriteLine(fileContents);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(card.ToString());
                //Console.WriteLine(e.Message);
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
            char symbol4 = (char)4;

            StringBuilder frame = new StringBuilder();
            frame.Append(symbol4);
            frame.Append(' ', 78);
            frame.Append(symbol4);
            StringBuilder gameOver = new StringBuilder();

            gameOver.Append(symbol4);
            gameOver.Append(' ', 34);
            gameOver.Append("GAME OVER");
            gameOver.Append(' ', 35);
            gameOver.Append(symbol4);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.Write("\n" + diomand + frame + gameOver + frame);
            Console.WriteLine(diomand);

            Console.Write("\n\t\t\t\tYOUR SCORE: " + currentPlayer.Score);
            Console.Write("\n\n" + diomand);
            Console.WriteLine("\n \t\t\t\t  HIGH SCORE");

            var highScores = new List<string>();
            using (StreamReader highScoreRead = new StreamReader(Constants.HighScoreFilePath))
            {
                var line = highScoreRead.ReadLine();
                while (line != null)
                {
                    highScores.Add(line);
                    line = highScoreRead.ReadLine();
                }
            }

            highScores.Sort();
            int scoreCount = highScores.Count >= 10 ? 10 : highScores.Count;

            foreach (var score in highScores.Skip(highScores.Count - scoreCount).Take(scoreCount))
            {
                var currentHighScore = score.Split('|');
                int x = Console.WindowWidth / 2 - 10;

                Console.WriteLine(currentHighScore[1] + " " + int.Parse(currentHighScore[0]));
            }

            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" + diomand);

            Console.WriteLine("Press ENTER for New Game or ESC for Exit");
            var pressedKey = Console.ReadKey(true);

            if (pressedKey.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("ENJOY!");
            }
            else if (pressedKey.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("ARE YOU SURE?");
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
