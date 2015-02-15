namespace NinjaAssassins.Models
{
    public static class CardTypeExtension
    {
        const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";
        const string CardFileExtension = ".txt";

        public static string SetFilePath(this CardType cardType)
        {
            int cardTypeId = (int)cardType;

            switch (cardTypeId)
            {
                case 1:
                    return CardFilePath + "1" + CardFileExtension;
                case 2:
                    return CardFilePath + "2" + CardFileExtension;
                case 3:
                    return CardFilePath + "3" + CardFileExtension;
                case 4:
                    return CardFilePath + "4" + CardFileExtension;
                case 5:
                    return CardFilePath + "5" + CardFileExtension;
                case 6:
                    return CardFilePath + "6" + CardFileExtension;
                case 7:
                    return CardFilePath + "7" + CardFileExtension;
                case 8:
                    return CardFilePath + "8" + CardFileExtension;
                default:
                    return CardFilePath + "back" + CardFileExtension;
            }
        }

        public static int SetCardRank(this CardType cardType)
        {
            // TODO: set different values for each card
            int cardTypeId = (int)cardType;

            switch (cardTypeId)
            {
                case 1:
                    return 20;
                case 2:
                    return 20;
                case 3:
                    return 20;
                case 4:
                    return 20;
                case 5:
                    return 20;
                case 6:
                    return 20;
                case 7:
                    return 20;
                case 8:
                    return 20;
                default: 
                    return 0;
            }
        }

        public static void Action(this CardType cardType)
        {
            // TODO : set action for each card
            int cardTypeId = (int)cardType;

            switch (cardTypeId)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                default:
                    break;
            }

            throw new System.NotImplementedException();
        }
    }
}
