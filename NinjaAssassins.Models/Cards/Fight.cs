namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Fight : Card
    {
        public Fight(string filePath, int rank, int priority)
            : base(filePath, rank, CardType.Fight, priority, true)
        {
        }

        public override void Action(Game game)
        {
            Random random = new Random();

            int ninjaRandom = random.Next(1, 100);
            int yourRandom = random.Next(1, 100);

            if (yourRandom > ninjaRandom)
            {                
                return;
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
