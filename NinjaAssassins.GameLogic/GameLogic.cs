namespace NinjaAssassins.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NinjaAssassins.Models;
    
    public class GameLogic
    {
        const int ComputerPlayersCount = 3;
        private static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };

        public static void InitializeGame(string playerName)
        {
            var deck = new Deck();
            var game = new Game(deck);

            for (int i = 0; i < ComputerPlayersCount; i++)
            {
                game.Players[i] = new Player(ComputerPlayersNames[i]);
            }

            game.Players[game.Players.Length - 1] = new Player(playerName);
        }

        public static void Play()
        {
            // TODO : separate methods:
            // 1. Draw a card
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
    }
}
