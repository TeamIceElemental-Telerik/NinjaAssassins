namespace NinjaAssassins.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using NinjaAssassins.Models;
    using NinjaAssassins.Helper;

    public class GameLogic
    {
        private static Random random = new Random();

        private static Game game;
        private static Player[] allPlayers;
        private static Player playerInTurn;
        private static int score;

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
            //throw new NotImplementedException();
        }

        public static void EndGame()
        {
            // TODO:
            // 1. Stop game loop - set gamestate to finished
            // 2. Add score to high-scores
            // 3. Display high-scores
            // 4. Display initial menu
            game.GameState = GameState.Finished;
            GameLogic.SaveHighScore(allPlayers[3], Constants.HighScoreFilePath);
            Console.WriteLine("Dead");
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

        public static Card SelectCardFromHand(IList<Card> hand)
        {
            if (hand.Count < 1)
            {
                throw new ArgumentException("The hand cannot be empty.");
            }

            ExtensionMethods.ClearConsolePart(Console.WindowWidth - 45, 0, 40, 5);

            var text = "Select a card from your hand:";
            ExtensionMethods.PrintOnPosition(Console.WindowWidth - 45, 0, text, ConsoleColor.Green);

            for (int i = 0; i < hand.Count; i++)
            {
                string key = i == 0 ? "A" : i == 1 ? "S" : i == 2 ? "D" : "F";
                string cardInHand = key + ": " + hand[i];

                ExtensionMethods.PrintOnPosition(Console.WindowWidth - 45, i + 1, cardInHand, ConsoleColor.Green);
            }

            Console.CursorVisible = false;

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
            var player = "";
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
                var writer = new StreamWriter(path, true);
                using (writer)
                {
                    writer.WriteLine(player.Score + "|" + player.Name);
                }
            }
            catch (Exception e)
            {
                ExtensionMethods.HandleExceptions(e);
            }
        }

        public static void SaveMoves(Game game, string path)
        {
            var writer = new StreamWriter(path, true);
            using (writer)
            {
                if (game.IsCardPlayed)
                {
                    writer.WriteLine(game.PlayerInTurn.Name + "| played " + game.CurrentCard.CardType);
                    game.IsCardPlayed = false;
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
                var reader = new StreamReader(path);
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
                ExtensionMethods.HandleExceptions(e);
            }

            return moves.Skip(moves.Count - movesCount)
                .Take(movesCount)
                .ToList();
        }
    }
}
