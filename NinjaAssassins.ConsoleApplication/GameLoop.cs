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
    using System.Threading;
    using NinjaAssassins.Helper;

    public class GameLoop
    {
        private static Game game;
        private static Deck deck;
        private static Player playerInTurn;
        private static Player[] allPlayers;

        static void Main()
        {
            Console.BufferWidth = Console.WindowWidth = 125;
            Console.BufferHeight = Console.WindowHeight = 35;

            // test image visualization
            //string path = @"C:\Users\svetla.ivanova\Desktop\badges\twenty-something.jpg";
            //GameVisualisation.DisplayImageOnTheConsole(path);

            // test game end
            // playerInTurn = new Player("Maria");
            // playerInTurn.Score = 0;
            // GameVisualisation.DisplayEndGame(playerInTurn);

            var reader = new StreamReader(Constants.GameBoard);

            GameVisualisation.DisplayInitialMenu();

            string playerName = GameVisualisation.AskForUsername();
            game = GameLogic.InitializeGame(playerName);

            Console.Clear();

            while (true)
            {
                Console.CursorVisible = false;

                playerInTurn = game.PlayerInTurn;

                GameVisualisation.DisplayGameBoard(game);

                int cardsToDraw = 1;
                if (playerInTurn.DrawDouble == true)
                {
                    cardsToDraw = 2;
                    playerInTurn.DrawDouble = false;
                }

                for (int i = 0; i < cardsToDraw; i++)
                {
                    var moves = GameLogic.GetLastNMoves(Constants.PlayerMoves, 13);

                    int x = Constants.xRightBorder + 3;
                    int y = 1;

                    GameVisualisation.DisplayCurrentGameState(game, x, y);
                    GameVisualisation.DisplayPlayerMoves(moves, game, x, y + 7);

                    if (playerInTurn.SkipTurn)
                    {
                        playerInTurn.SkipTurn = false;
                        continue;
                    }

                    if (playerInTurn.IsDead)
                    {
                        continue;
                    }

                    var card = GameLogic.DrawCard(game.Deck, game.Deck.Count - 1);
                    game.CurrentCard = card;

                    try
                    {
                        if (game.GameState == GameState.YourTurn)
                        {
                            x = 30;
                            y = 7;

                            var cardReader = new StreamReader(card.FilePath);
                            GameVisualisation.DisplayCard(cardReader, card, x, y);
                        }
                    }
                    catch
                    {
                        x = 30;
                        y = 7;
                        ExtensionMethods.PrintOnPosition(x, y, card.ToString(), ConsoleColor.Green);
                    }

                    if (game.GameState == GameState.YourTurn)
                    {
                        if (card.CardType != CardType.NinjaAssassin)
                        {
                            y = 27;
                            x = Constants.xRightBorder + 3;
                            GameVisualisation.DisplayPlayersChoiceOptions(x, y);

                            var choice = PlayersChoice.NotSelected;
                            bool wrongChoice = true;

                            while (wrongChoice)
                            {
                                try
                                {
                                    choice = GameVisualisation.GetPlayersChoice();

                                    GameLogic.PlayCard(game, playerInTurn, card, choice);
                                    wrongChoice = false;
                                }
                                catch (ArgumentException e)
                                {
                                    wrongChoice = true;
                                    ExtensionMethods.PrintOnPosition(Constants.xRightBorder + 3, Console.WindowHeight - 4, e.Message, ConsoleColor.White);
                                }
                                catch (InvalidOperationException e)
                                {
                                    wrongChoice = true;
                                    ExtensionMethods.PrintOnPosition(Constants.xRightBorder + 3, Console.WindowHeight - 4, e.Message, ConsoleColor.White);
                                }
                            }
                        }
                        else
                        {
                            GameLogic.HandleNinjaAssasin(game, playerInTurn, card);
                        }
                    }
                    else
                    {
                        if (card.CardType != CardType.NinjaAssassin)
                        {
                            Thread.Sleep(1000);
                            GameLogic.PlayByComputerLogic(playerInTurn, card);
                        }
                        else
                        {
                            Thread.Sleep(1000);
                            GameLogic.HandleNinjaAssasin(game, playerInTurn, card);
                        }
                    }

                    GameLogic.SaveMoves(game, Constants.PlayerMoves);
                    GameLogic.ChangeScore(card, playerInTurn);
                }

                // test game state
                //Console.WriteLine(new string('-', 60));
                //Console.WriteLine("In turn: {0}", playerInTurn.Name);
                //Console.WriteLine("In turn: {0}", playerInTurn.Id);
                //Console.WriteLine("Next: {0}", game.NextPlayer.Name);
                //Console.WriteLine("Deck count: {0}", game.Deck.Count);
                //Console.WriteLine("Deck: {0}", string.Join(" ", game.Deck));
                //Console.WriteLine("State: {0}", game.GameState);
                //Console.WriteLine("Skip? {0}", playerInTurn.SkipTurn);
                //Console.WriteLine("Is dead? {0}", playerInTurn.IsDead);
                //Console.WriteLine("Draw double? {0}", playerInTurn.DrawDouble);
                //Console.WriteLine("Hand: {0}", string.Join(" ", playerInTurn.Hand));
                //Console.WriteLine("Players: {0}", string.Join(" ", game.Players.ToList()));
                //Console.WriteLine(new string('-', 60));

                if (game.Deck.Count == 0 || game.Players[3].IsDead)
                {
                    game.GameState = GameState.Finished;
                    // TODO: get winner

                }

                if (game.GameState == GameState.Finished)
                {
                    GameLogic.ReduceScoresWithCardsInHand(game);

                    // test
                    Console.WriteLine("The end");

                    var winner = GameLogic.GetWinner(game);

                    // Console.WriteLine("Winner : {0} {1}", winner.Key, winner.Value);
                    // GameVisualisation.DisplayEndGame(game.Players[3]);

                    break;
                }

                GameLogic.SetNextPlayer(playerInTurn);
            }



        }
    }
}
