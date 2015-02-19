namespace NinjaAssassins.GameLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;

    using NinjaAssassins.Models;

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

            for (int i = 0; i < Constants.TotalPlayers - 1; i++)
            {
                game.Players[i] = new Player(Constants.ComputerPlayersNames[i]);
            }

            game.Players[game.Players.Length - 1] = new Player(playerName);
            allPlayers = game.Players;

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
            Console.WriteLine("Dead");
        }

        public static Card DrawCard(Deck deck, int positionInDeck = 0)
        {
            // TODO:
            // get card from deck by position
            // remove card from deck
            throw new NotImplementedException();
        }

        public static Player GetFirstPlayer()
        {
            // TODO:
            // get a random player id
            // select player from allPlayers based on that id;
            // set GameState to either ComputerTurn or YourTurn based on the id
            throw new NotImplementedException();
        }

        public static Player GetNextPlayer(Player currentPlayer)
        {
            // TODO:
            // get currentPlayer id + 1 unless it's the last player - then select 0;
            // select player from allPlayers using the id;
            // set playerInTurn to be the new current player 
            // set GameState to either ComputerTurn or YourTurn based on the id
            throw new NotImplementedException();
        }

        public static void PlayByComputerLogic(Player currentPlayer, Card card)
        {

        }
        public static void PlayCard(Player currentPlayer, Card card, PlayersChoice choice = PlayersChoice.NotSelected)
        {
            
        }

        public static Card SelectCardFromHand(IList<Card> hand)
        {
            throw new NotImplementedException();
        }

        public static void ChangeScore(Card card, Player player)
        {
            player.Score += card.Rank;
        }

        public static void SaveHighScore(Player player, string path)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(player.Name + "|" + player.Score);
            writer.Close();
        }

        public static void SaveMoves(Player player, Card card, string path)
        {
            StreamWriter writer = new StreamWriter(path , true);
            writer.WriteLine(player.Name + "|" + player.Score);
            writer.Close();
        }

        public static List<string> GetLastNMoves(string path, int movesCount)
        {
            List<string> moves = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (reader.EndOfStream == false)
                    {
                        string line = reader.ReadLine();
                        moves.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return moves.Skip(moves.Count - movesCount)
                .Take(movesCount)
                .ToList();
        }
    }
}
