namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NinjaAssassins.Models;

    // TODO
    public static class GameLogic
    {
        const int ComputerPlayersCount = 3;
        private static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };

        public static void DisplayInitialMenu()
        {
            // Logo int[,]
            // Start game button
            // Options (sound on/off)
            // How to play / Game rules
            throw new NotImplementedException();
        }

        public static void DisplayIntro()
        {
            // Some ninja assassin story
            // http://en.wikipedia.org/wiki/League_of_Assassins
            // http://en.wikipedia.org/wiki/Ra%27s_al_Ghul
            throw new NotImplementedException();
        }
        
        public static void InitializeGame()
        {
            var deck = new Deck();
            var game = new Game(deck);

            for (int i = 0; i < ComputerPlayersCount; i++)
			{
                game.Players[i] = new Player(ComputerPlayersNames[i]);
			}

            string playerName = AskForUsername();
            game.Players[game.Players.Length - 1] = new Player(playerName);
        }

        public static void Play()
        {
            // TODO:
            // 1. Draw card
            // 2. Display card
            // 3. Decide what to do depending on card
            //  - save it to player's hand
            //  - play it (card.Action())
            //  - play another card (select a card from the hand)
            //  - game over (on Ninja Assassin and no save option)
            // 4. Change score depending on card (use Card.Value)
            // 5. Move to next player
            // 6. Computer players game logic - random decisions
            // 7. When all cards are drawn (re-fill deck? or end game?)
            throw new NotImplementedException();
        }

        public static void EndGame()
        {
            // TODO:
            // 1. Stop game cycle
            // 2. Add score to high-scores
            // 3. Display high-scores
            // 4. Display initial menu
            throw new NotImplementedException();
        }

        private static string AskForUsername()
        {
            // TODO : beautify
            Console.Write("Please enter your name: ");

            return Console.ReadLine();
        }
    }
}
