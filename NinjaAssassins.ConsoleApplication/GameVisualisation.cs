namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
            PrintOnPosition(x, y,"Please enter your name: ", ConsoleColor.Green);

            return Console.ReadLine();
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
                Console.WriteLine(e.Message);
            }
        }

        public static PlayersChoice GetPlayersChoice()
        {
            PlayersChoice choice = PlayersChoice.NotSelected;

            // TODO:
            // switch case for pressed key
            // set choice (using PlayersChoice enum) depending on key

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
    }
}
