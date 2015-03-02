namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Hide : Card
    {
        public Hide(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Hide, priority, true)
        {
        }

        public override void Action(Game game)
        {
            Player currentPlayer = game.PlayerInTurn;
            Player nextPlayer = game.NextPlayer;
            Random random = new Random();
            var card = CardFactory.Get(CardType.GreenNinja);

            if (nextPlayer.Hand.Contains(card))
            {
                nextPlayer.Hand.Remove(card);
                currentPlayer.Hand.Add(card);

                nextPlayer.SkipTurn = true;

                int index = random.Next(0, game.Deck.Count);
                game.Deck.Insert(index, card);

                index = random.Next(0, game.Deck.Count);
                game.Deck.Insert(index, game.CurrentCard);
            }
            else
            {
                if (game.CurrentCard.CardType == CardType.NinjaAssassin)
                {
                    game.PlayerInTurn.IsDead = true;
                }
                // Console.WriteLine("Sorry, no place to hide.");
                return;
            }
        }
    }
}
