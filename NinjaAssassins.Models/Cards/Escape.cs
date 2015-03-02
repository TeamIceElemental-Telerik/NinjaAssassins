namespace NinjaAssassins.Models.Cards
{
    using NinjaAssassins.Helper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Escape : Card
    {
        public Escape(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Escape, priority, true)
        {
        }

        public override void Action(Game game)
        {
            Card card = game.Deck[game.Deck.Count - 1];
            game.Deck.RemoveCardFromDeck(card);

            game.Log = game.PlayerInTurn + "| ran just for the sake of it.";
            if (card.SaviourType)
            {
                game.Log = game.PlayerInTurn + "| ran for their lives!";
                card.Action(game);
            }
            else
            {
                if (game.CurrentCard.CardType == CardType.NinjaAssassin)
                {
                    game.Log = game.PlayerInTurn + "| tried to escape but couldn't.";
                    game.PlayerInTurn.IsDead = true;
                }

                return;
            }
        }
    }
}
