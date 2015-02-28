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
            // test image visualization
            //string path = @"C:\Users\svetla.ivanova\Desktop\badges\twenty-something.jpg";
            //GameVisualisation.DisplayImageOnTheConsole(path);
            Console.WindowWidth = 130;
            Console.WindowHeight = 40;

            GameVisualisation.DisplayInitialMenu();

            string playerName = GameVisualisation.AskForUsername();
            game = GameLogic.InitializeGame(playerName);

            while (true)
            {
                playerInTurn = game.PlayerInTurn;

                Console.WriteLine(playerInTurn.Name);

                GameVisualisation.DisplayGameBoard();

                int cardsToDraw = 1;
                if (playerInTurn.DrawDouble == true)
                {
                    cardsToDraw = 2;
                    playerInTurn.DrawDouble = false;
                }

                for (int i = 0; i < cardsToDraw; i++)
                {
                    if (playerInTurn.SkipTurn == true)
                    {
                        playerInTurn.SkipTurn = false;
                        continue;
                    }

                    var card = GameLogic.DrawCard(game.Deck, game.Deck.Count - 1);
                    GameVisualisation.DisplayCard(card);

                    if (game.GameState == GameState.YourTurn)
                    {
                        if (card.CardType != CardType.NinjaAssassin)
                        {
                            var choice = PlayersChoice.NotSelected;
                            try
                            {
                                choice = GameVisualisation.GetPlayersChoice();
                                GameLogic.PlayCard(game, playerInTurn, card, choice);
                            }
                            catch (InvalidOperationException e)
                            {
                                Console.WriteLine(e.Message);
                                choice = GameVisualisation.GetPlayersChoice();
                                GameLogic.PlayCard(game, playerInTurn, card, choice);
                            }
                            catch (ArgumentException e)
                            {
                                Console.WriteLine(e.Message);
                            }                       
                        }
                        else
                        {
                            GameLogic.HandleNinjaAssasin(game, playerInTurn, card);
                        }
                    }
                    else
                    {
                        GameLogic.PlayByComputerLogic(playerInTurn, card);
                    }

                    GameLogic.ChangeScore(card, playerInTurn);
                }

                // test game state
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("In turn: {0}", playerInTurn.Name);
                Console.WriteLine("Next: {0}", game.NextPlayer.Name);
                Console.WriteLine("Deck count: {0}", game.Deck.Count);
                Console.WriteLine("Deck: {0}", string.Join(" ", game.Deck));
                Console.WriteLine("State: {0}", game.GameState);
                Console.WriteLine("Skip? {0}", playerInTurn.SkipTurn);
                Console.WriteLine("Draw double? {0}", playerInTurn.DrawDouble);
                Console.WriteLine("Hand: {0}", string.Join(" ", playerInTurn.Hand));
                Console.WriteLine(new string('-', 60));

                if (game.Deck.Count == 0)
                {
                    game.GameState = GameState.Finished;
                }

                if (game.GameState == GameState.Finished)
                {
                    GameVisualisation.DisplayEndGame(game.Players[3]);
                    break;
                }

                GameLogic.SetNextPlayer(playerInTurn);
            }
        }
    }
}
