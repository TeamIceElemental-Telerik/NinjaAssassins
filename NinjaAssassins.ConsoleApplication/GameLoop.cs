namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;
    using NinjaAssassins.Models.Cards;

    public class GameLoop
    {

        private static Game game;
        private static Deck deck;
        private static Player playerInTurn;
        private static Player[] allPlayers;

        static void Main()
        {
            
            GameVisualisation.DisplayInitialMenu();

            string playerName = GameVisualisation.AskForUsername();
            game = GameLogic.InitializeGame(playerName);

            playerInTurn = GameLogic.GetFirstPlayer();

            while (true)
            {
                if (game.GameState == GameState.Finished)
                {
                    GameLogic.SaveHighScore(allPlayers[3], Constants.HighScoreFilePath);
                    break;
                }

                GameVisualisation.DisplayGameBoard();

                deck = game.Deck;

                var card = GameLogic.DrawCard(deck, deck.Count - 1);

                if (game.GameState == GameState.YourTurn)
                {
                    var choice = GameVisualisation.GetPlayersChoice();
                    GameLogic.PlayCard(playerInTurn, card, choice);
                }
                else
                {
                    GameLogic.PlayByComputerLogic(playerInTurn, card);
                }

                GameLogic.ChangeScore(card, playerInTurn);

                playerInTurn = GameLogic.GetNextPlayer(playerInTurn);

                if (deck.Count == 0)
                {
                    game.GameState = GameState.Finished;
                }
            }
        }
    }
}
