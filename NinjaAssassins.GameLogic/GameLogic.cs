namespace NinjaAssassins.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using NinjaAssassins.Helper;
    using NinjaAssassins.Models;
    using System.Text;

    public class GameLogic
    {
        private static Random random = new Random();

        private static Game game;
        private static Player[] allPlayers;

        public static Game InitializeGame(string playerName)
        {
            var deck = new Deck();
            game = new Game(deck);

            game.Deck = deck;

            for (int i = 0; i < Constants.TotalPlayers - 1; i++)
            {
                game.Players[i] = new Player(Constants.ComputerPlayersNames[i]);
            }

            game.Players[game.Players.Length - 1] = new Player(playerName);
            allPlayers = game.Players;

            SetFirstPlayer();

            if (game.PlayerInTurn == game.Players[game.Players.Length - 1])
            {
                game.GameState = GameState.YourTurn;
            }
            else
            {
                game.GameState = GameState.ComputerTurn;
            }

            File.Create(Constants.PlayerMoves).Close();

            return game;
        }

        public static int GetNumberOfCardsToDraw(Game game)
        {
            int cardsToDraw = 1;
            if (game.PlayerInTurn.DrawDouble == true)
            {
                cardsToDraw = 2;
                game.PlayerInTurn.DrawDouble = false;
            }

            return cardsToDraw;
        }

        public static void EndGame()
        {
            ReduceScoresWithCardsInHand(game);
            SaveHighScore(allPlayers[3], Constants.HighScoreFilePath);
        }

        public static Card DrawCard(Deck deck, int positionInDeck = 0)
        {
            Card card = deck[positionInDeck];
            deck.RemoveCardFromDeck(card);
            return card;
        }

        public static void SetFirstPlayer()
        {
            int playerId = random.Next(0, allPlayers.Length);
            var player = game.Players[playerId];
            game.PlayerInTurn = player;

            int nextPlayerId = (playerId + 1) % game.Players.Length;
            game.NextPlayer = game.Players[nextPlayerId];

            game.GameState = playerId < 3 ? GameState.ComputerTurn : GameState.YourTurn;
        }

        public static void SetNextPlayer(Player currentPlayer)
        {
            int playerId = (currentPlayer.Id + 1) % game.Players.Length;
            var player = game.Players[playerId];
            game.PlayerInTurn = player;

            int nextPlayerId = (playerId + 1) % game.Players.Length;
            game.NextPlayer = game.Players[nextPlayerId];

            game.GameState = playerId < 3 ? GameState.ComputerTurn : GameState.YourTurn;
        }

        public static void PlayByComputerLogic(Player currentPlayer, Card card)
        {
            if (card.CardType != CardType.NinjaAssassin)
            {
                currentPlayer.Hand.Add(card);
                Card bestCard;
                int maxPriority = -1;

                foreach (var c in currentPlayer.Hand)
                {
                    maxPriority = c.Priority > maxPriority && c.Priority <= (int)CardType.Attack ? c.Priority : maxPriority;
                }

                if (maxPriority > -1)
                {
                    bestCard = currentPlayer.Hand.FirstOrDefault(c => c.Priority == maxPriority);

                    if (currentPlayer.Hand.Count < Constants.HandCount)
                    {
                        // 50-50 chance
                        bool play = random.Next(1, 3) == 1 ? true : false;

                        if (play)
                        {
                            bestCard.Action(game);
                            currentPlayer.Hand.Remove(bestCard);
                            game.IsCardPlayed = true;
                        }
                    }
                    else
                    {
                        bestCard.Action(game);
                        currentPlayer.Hand.Remove(bestCard);
                        game.IsCardPlayed = true;
                    }
                }
                else
                {
                    if (currentPlayer.Hand.Count > Constants.HandCount)
                    {
                        int minPriority = int.MaxValue;

                        foreach (var c in currentPlayer.Hand)
                        {
                            minPriority = c.Priority < minPriority && c.Priority > (int)CardType.Attack ? c.Priority : minPriority;
                        }

                        bestCard = currentPlayer.Hand.FirstOrDefault(c => c.Priority == minPriority);
                        bestCard.Action(game);
                        currentPlayer.Hand.Remove(bestCard);
                        game.IsCardPlayed = true;
                    }
                }
            }
            else
            {
                HandleNinjaAssasin(game, game.PlayerInTurn, card);
            }
        }

        public static void PlayCard(Game game, Player currentPlayer, Card card, PlayersChoice choice = PlayersChoice.NotSelected)
        {
            switch (choice)
            {
                case PlayersChoice.PlayCard:
                    card.Action(game);
                    game.IsCardPlayed = true;
                    break;
                case PlayersChoice.SaveToHand:
                    if (currentPlayer.Hand.Count < 3)
                    {
                        currentPlayer.Hand.Add(card);
                    }
                    else
                    {
                        choice = PlayersChoice.PlayDifferentCard;
                        PlayCard(game, currentPlayer, card, choice);
                    }

                    break;
                case PlayersChoice.PlayDifferentCard:
                    currentPlayer.Hand.Add(card);

                    if (currentPlayer.Hand.Count > 0)
                    {
                        DisplaySelectFromHand(currentPlayer.Hand, Constants.RightBorderX + 3, 27);
                        card = SelectCardFromHand(currentPlayer.Hand);
                        currentPlayer.Hand.Remove(card);
                        choice = PlayersChoice.PlayCard;
                        PlayCard(game, currentPlayer, card, choice);
                    }

                    break;
            }
        }

        public static void HandleNinjaAssasin(Game game, Player currentPlayer, Card card)
        {
            var saviourCards = new List<Card>();
            if (currentPlayer.Hand.Count > 0)
            {
                foreach (var c in currentPlayer.Hand)
                {
                    if (c.SaviourType)
                    {
                        saviourCards.Add(c);
                    }
                }
            }

            if (saviourCards.Count > 0)
            {
                if (game.GameState == GameState.YourTurn)
                {
                    DisplaySelectFromHand(saviourCards, Constants.RightBorderX + 3, 27);
                    card = SelectCardFromHand(saviourCards);
                    currentPlayer.Hand.Remove(card);
                }
                else
                {
                    int maxPriority = -1;

                    foreach (var c in saviourCards)
                    {
                        maxPriority = c.Priority > maxPriority ? c.Priority : maxPriority;
                    }

                    card = currentPlayer.Hand.FirstOrDefault(c => c.Priority == maxPriority);
                    currentPlayer.Hand.Remove(card);
                }
            }

            card.Action(game);
        }

        public static void DisplaySelectFromHand(IList<Card> hand, int x, int y)
        {
            ExtensionMethods.ClearConsolePart(x, y, 40, 5);

            var text = "Select a card from your hand:";
            ExtensionMethods.PrintOnPosition(x, y, text, ConsoleColor.White);

            for (int i = 0; i < hand.Count; i++)
            {
                string key = i == 0 ? "A" : i == 1 ? "S" : i == 2 ? "D" : "F";

                ExtensionMethods.PrintOnPosition(x, y + i + 1, key + ": ", ConsoleColor.Yellow);
                ExtensionMethods.PrintOnPosition(x + 3, y + i + 1, hand[i].ToString(), ConsoleColor.White);
            }
        }

        public static Card SelectCardFromHand(IList<Card> hand)
        {
            if (hand.Count < 1)
            {
                throw new ArgumentException("The hand cannot be empty.");
            }

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            Card card = hand[0];
            switch (pressedKey.Key)
            {
                case ConsoleKey.A:
                    card = hand[0];
                    break;
                case ConsoleKey.S:
                    if (hand.Count > 1)
                    {
                        card = hand[1];
                    }
                    else
                    {
                        SelectCardFromHand(hand);
                    }

                    break;
                case ConsoleKey.D:
                    if (hand.Count > 2)
                    {
                        card = hand[2];
                    }
                    else
                    {
                        SelectCardFromHand(hand);
                    }

                    break;
                case ConsoleKey.F:
                    if (hand.Count > 3)
                    {
                        card = hand[3];
                    }
                    else
                    {
                        SelectCardFromHand(hand);
                    }

                    break;
                default:
                    SelectCardFromHand(hand);
                    break;
            }

            return card;
        }

        public static void ChangeScore(Card card, Player player)
        {
            player.Score += card.Rank;
        }

        public static void ReduceScoresWithCardsInHand(Game game)
        {
            foreach (var player in game.Players)
            {
                int scoreToSubtract = 0;
                foreach (var c in player.Hand)
                {
                    scoreToSubtract += c.Rank;
                }

                player.Score -= scoreToSubtract;
            }
        }

        public static KeyValuePair<string, int> GetWinner(Game game)
        {
            int maxScore = 0;
            var player = string.Empty;
            for (int i = 0; i < game.Players.Length; i++)
            {
                if (game.Players[i].Score > maxScore)
                {
                    maxScore = game.Players[i].Score;
                    player = game.Players[i].Name;
                }
            }

            return new KeyValuePair<string, int>(player, maxScore);
        }

        public static void SaveHighScore(Player player, string path)
        {
            try
            {
                var writer = new StreamWriter(path, true, Encoding.GetEncoding("Windows-1251"));
                using (writer)
                {
                    writer.WriteLine(player.Score + "|" + player.Name);
                }
            }
            catch (Exception e)
            {
                ExtensionMethods.HandleExceptions(e, Constants.RightBorderX + 3, Console.WindowHeight - 9, ConsoleColor.White);
            }
        }

        public static Dictionary<string, int> GetHighScores(StreamReader reader, int highScoresCount)
        {
            var highScores = new Dictionary<string, int>();
            using (reader)
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    int separatorIndex = line.IndexOf('|');

                    int score = int.Parse(line.Substring(0, separatorIndex).Trim());
                    string playerName = line.Substring(separatorIndex + 1, line.Length - (separatorIndex + 1)).Trim();

                    if (!highScores.ContainsKey(playerName))
                    {
                        highScores.Add(playerName, score);
                    }

                    highScores[playerName] = score;
                    
                    line = reader.ReadLine();
                }
            }

            int scoreCount = highScores.Count >= highScoresCount ? highScoresCount : highScores.Count;

            return highScores.OrderByDescending(s => s.Value).Take(scoreCount).ToDictionary(s => s.Key, s => s.Value);
        }

        public static void SaveMoves(Game game, string path)
        {
            var writer = new StreamWriter(path, true, Encoding.GetEncoding("Windows-1251"));
            using (writer)
            {
                if (game.IsCardPlayed)
                {
                    writer.WriteLine(game.PlayerInTurn.Name + "| played " + game.CurrentCard.CardType);
                    game.IsCardPlayed = false;
                }
                else if (game.PlayerInTurn.IsDead)
                {
                    game.Log = game.PlayerInTurn.Name + "| was killed by a " + game.CurrentCard.CardType;
                }
                else
                {
                    game.Log = game.PlayerInTurn.Name + "| saved a card to their hand.";
                }

                writer.WriteLine(game.Log);
            }
        }

        public static List<string> GetLastNMoves(string path, int movesCount)
        {
            List<string> moves = new List<string>();

            try
            {
                var reader = new StreamReader(path, Encoding.GetEncoding("Windows-1251"));
                using (reader)
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        moves.Add(line);
                        line = reader.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                ExtensionMethods.HandleExceptions(e, Constants.RightBorderX + 3, Console.WindowHeight - 9, ConsoleColor.White);
            }

            return moves.Skip(moves.Count - movesCount)
                .Take(movesCount)
                .ToList();
        }
    }
}
