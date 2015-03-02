namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Helper;
    using NinjaAssassins.Models;

    public static class GameLoop
    {
        public static void Play(Game game)
        {
            Console.Clear();

            while (true)
            {
                Console.CursorVisible = false;

                GameVisualisation.DisplayGameBoard(game);

                for (int i = 0; i < GameLogic.GetNumberOfCardsToDraw(game); i++)
                {
                    var moves = GameLogic.GetLastNMoves(Constants.PlayerMoves, Constants.PlayerMovesCount);

                    GameVisualisation.DisplayPlayerMoves(moves, game, Constants.PlayerMovesX, Constants.PlayerMovesY);
                    GameVisualisation.DisplayCurrentGameState(game, Constants.GameStateX, Constants.GameStateY);

                    if (game.PlayerInTurn.SkipTurn)
                    {
                        game.PlayerInTurn.SkipTurn = false;
                        continue;
                    }

                    if (game.PlayerInTurn.IsDead)
                    {
                        continue;
                    }

                    var card = GameLogic.DrawCard(game.Deck, game.Deck.Count - 1);
                    game.CurrentCard = card;

                    try
                    {
                        if (game.GameState == GameState.YourTurn)
                        {
                            var cardReader = new StreamReader(card.FilePath);
                            GameVisualisation.DisplayCard(cardReader, card, Constants.CardX, Constants.CardY);
                        }
                    }
                    catch
                    {
                        ExtensionMethods.PrintOnPosition(Constants.CardX, Constants.CardY, card.ToString(), ConsoleColor.Green);
                    }

                    if (game.GameState == GameState.YourTurn)
                    {
                        GameVisualisation.DisplayPlayersChoiceOptions(Constants.PlayersChoiceOptionsX, Constants.PlayersChoiceOptionsY);

                        var choice = PlayersChoice.NotSelected;
                        bool wrongChoice = true;

                        while (wrongChoice)
                        {
                            try
                            {
                                choice = GameVisualisation.GetPlayersChoice();
                                GameLogic.PlayCard(game, game.PlayerInTurn, card, choice);
                                wrongChoice = false;
                            }
                            catch (ArgumentException e)
                            {
                                wrongChoice = true;
                                ExtensionMethods.PrintOnPosition(Constants.ExceptionMessageX, Constants.ExceptionMesssageWrongChoiceY, e.Message, ConsoleColor.White);
                            }
                            catch (InvalidOperationException e)
                            {
                                wrongChoice = true;
                                ExtensionMethods.PrintOnPosition(Constants.ExceptionMessageX, Constants.ExceptionMesssageWrongChoiceY, e.Message, ConsoleColor.White);
                            }
                        }
                    }
                    else
                    {
                        GameLogic.PlayByComputerLogic(game.PlayerInTurn, card);
                        Thread.Sleep(1000);
                    }

                    GameLogic.SaveMoves(game, Constants.PlayerMoves);
                    GameLogic.ChangeScore(card, game.PlayerInTurn);
                }

                if (game.Deck.Count == 0 || game.Players[3].IsDead)
                {
                    game.GameState = GameState.Finished;
                }

                if (game.GameState == GameState.Finished)
                {
                    GameLogic.EndGame();

                    var highScores = new List<string>();
                    try
                    {
                        var highScoreReader = new StreamReader(Constants.HighScoreFilePath);
                        highScores = GameLogic.GetHighScores(highScoreReader, Constants.HighScoresCount);
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        ExtensionMethods.HandleExceptions(e, Constants.ExceptionMessageX, Constants.ExceptionMesssageY, ConsoleColor.White);
                    }

                    var winner = GameLogic.GetWinner(game);
                    GameVisualisation.DisplayGameEnd(game.Players[3], winner, highScores);
                    break;
                }

                GameLogic.SetNextPlayer(game.PlayerInTurn);
            }
        }
    }
}
