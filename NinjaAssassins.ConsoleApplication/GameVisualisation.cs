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

            switch ( Console.ReadLine())
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
            // TODO : Some ninja assassin story
            // http://en.wikipedia.org/wiki/League_of_Assassins
            // http://en.wikipedia.org/wiki/Ra%27s_al_Ghul
            // color
        }

        public static string AskForUsername()
        {
            // TODO : beautify
            Console.Write("Please enter your name: ");

            return Console.ReadLine();
        }

        public static void DisplayGameBoard()
        {
            // TODO - draw board
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
    }
}
