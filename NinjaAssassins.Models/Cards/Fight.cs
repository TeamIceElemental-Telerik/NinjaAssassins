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
            : base(filePath, rank, CardType.Fight, priority)
        {
        }

        public override void Action()
        {

        }
    }
}
