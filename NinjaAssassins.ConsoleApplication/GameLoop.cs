﻿namespace NinjaAssassins.ConsoleApplication
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
            Console.BufferWidth = Console.WindowWidth = 130;
            //Console.BufferHeight = Console.WindowHeight = 40;

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
                //Console.Clear();
                playerInTurn = game.PlayerInTurn;

                Console.WriteLine(playerInTurn.Name);

                //GameVisualisation.DisplayGameBoard(reader);

                int cardsToDraw = 1;
                if (playerInTurn.DrawDouble == true)
                {
                    cardsToDraw = 2;
                    playerInTurn.DrawDouble = false;
                }

                for (int i = 0; i < cardsToDraw; i++)
                {
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
                        var cardReader = new StreamReader(card.FilePath);
                        GameVisualisation.DisplayCard(cardReader, card);
                    }
                    catch
                    {
                        Console.WriteLine(card.ToString());
                    }

                    if (game.GameState == GameState.YourTurn)
                    {
                        if (card.CardType != CardType.NinjaAssassin)
                        {
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
                                    Console.WriteLine(e.Message);
                                }
                                catch (InvalidOperationException e)
                                {
                                    wrongChoice = true;
                                    Console.WriteLine(e.Message);
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
                            GameLogic.PlayByComputerLogic(playerInTurn, card);
                        }
                        else
                        {
                            GameLogic.HandleNinjaAssasin(game, playerInTurn, card);
                        }
                    }

                    GameLogic.ChangeScore(card, playerInTurn);
                }

                // test game state
                Console.WriteLine(new string('-', 60));
                Console.WriteLine("In turn: {0}", playerInTurn.Name);
                Console.WriteLine("In turn: {0}", playerInTurn.Id);
                Console.WriteLine("Next: {0}", game.NextPlayer.Name);
                Console.WriteLine("Deck count: {0}", game.Deck.Count);
                Console.WriteLine("Deck: {0}", string.Join(" ", game.Deck));
                Console.WriteLine("State: {0}", game.GameState);
                Console.WriteLine("Skip? {0}", playerInTurn.SkipTurn);
                Console.WriteLine("Is dead? {0}", playerInTurn.IsDead);
                Console.WriteLine("Draw double? {0}", playerInTurn.DrawDouble);
                Console.WriteLine("Hand: {0}", string.Join(" ", playerInTurn.Hand));
                Console.WriteLine("Players: {0}", string.Join(" ", game.Players.ToList()));
                Console.WriteLine(new string('-', 60));

                if (game.Deck.Count == 0 || game.Players[3].IsDead)
                {
                    game.GameState = GameState.Finished;
                    // TODO: get winner
                }

                if (game.GameState == GameState.Finished)
                {
                    GameLogic.ReduceScoresWithCardsInHand(game);

                    Console.WriteLine("The end");
                    //GameVisualisation.DisplayEndGame(game.Players[3]);
                    break;
                }

                GameLogic.SetNextPlayer(playerInTurn);
            }
        }
    }
}
