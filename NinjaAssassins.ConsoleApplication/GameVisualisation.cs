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
        public static void DisplayInitialMenu()
        {
            // TODO
            // Logo int[,]
            // Start game button
            // Options (sound on/off)
            // How to play / Game rules
            throw new NotImplementedException();
        }

        public static void DisplayIntro()
        {
            // TODO : Some ninja assassin story
            // http://en.wikipedia.org/wiki/League_of_Assassins
            // http://en.wikipedia.org/wiki/Ra%27s_al_Ghul
            throw new NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        private static void DisplayCard(Card card)
        {
            try
            {
                using (StreamReader sr = new StreamReader(card.FilePath))
                {
                    // TODO : change color
                    string fileContents = sr.ReadToEnd();
                    Console.WriteLine(fileContents);
                }
            }
            catch (Exception e)
            {
                // TODO: Display card name instead
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
            }
        }
    }
}
