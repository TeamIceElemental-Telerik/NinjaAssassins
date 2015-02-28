namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Escape : Card
    {
        public Escape(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Escape, priority)
        {
        }

        public override void Action(Game game)
        {
            Card card = game.Deck[game.Deck.Count - 1];
            game.Deck.RemoveCardFromDeck(card);

            if (Constants.SaviourTypes.Contains(card.CardType))
            {
                card.Action(game);
            }
            else
            {
                if (game.CurrentCard.CardType == CardType.NinjaAssassin)
                {
                    game.PlayerInTurn.IsDead = true;
                }

                return;
            }
        }
    }
}
