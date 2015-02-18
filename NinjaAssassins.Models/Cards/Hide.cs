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
            : base(filePath, rank, CardType.Hide, priority)
        {
        }

        public override void Action()
        {

        }
    }
}
