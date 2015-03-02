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

            game.Log = game.PlayerInTurn + "| has nothing to hide from.";

            if (nextPlayer.Hand.Contains(card))
            {
                game.Log = game.PlayerInTurn + "| hid behind " + game.NextPlayer + " and was saved.";
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
                    game.Log = game.PlayerInTurn + "| tried to hide but the " + game.CurrentCard.CardType + " found them anyway.";
                    game.PlayerInTurn.IsDead = true;
                }

                return;
            }
        }
    }
}
