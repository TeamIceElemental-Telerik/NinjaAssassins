namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NinjaAssassin : Card
    {
        public NinjaAssassin(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.NinjaAssassin, priority)
        {
        }

        public override void Action(Game game)
        {
            game.PlayerInTurn.IsDead = true;
        }
    }
}
