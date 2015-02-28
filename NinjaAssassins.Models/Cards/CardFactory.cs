namespace NinjaAssassins.Models.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CardFactory
    {
        public static Card Get(CardType cardType)
        {
            int cardTypeId = (int)cardType;
            string filePath = Constants.CardFilePath + cardTypeId + Constants.CardFileExtension;
            int priority = cardTypeId;
            int rank = cardTypeId * 5;

            switch (cardTypeId)
            {
                case 1:
                    return new NinjaAssassin(filePath, rank, priority);
                case 2:
                    return new Shuffle(filePath, rank, priority);
                case 3:
                    return new SkipTurn(filePath, rank, priority);
                case 4:
                    return new Attack(filePath, rank, priority);
                case 5:
                    return new Hide(filePath, rank, priority);
                case 6:
                    return new Escape(filePath, rank, priority);
                case 7:
                    return new Fight(filePath, rank, priority);
                case 8:
                    return new GreenNinja(filePath, rank, priority);
                default:
                    return null;
            }
        }
    }
}
