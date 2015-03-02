namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GreenNinja : Card
    {
        public GreenNinja(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.GreenNinja, priority, true)
        {
        }

        public override void Action(Game game)
        {
            game.Log = game.PlayerInTurn + "| was saved by a " + this.CardType + "! Hurray!";
            return;
        }
    }
}
