namespace NinjaAssassins.Models
{
    using System;
    using System.IO;

    public class Card : ICard
    {
        private string filePath;
        private int rank;
        private CardType cardType;

        public Card(CardType cardType)
        {
            this.Id++;
            this.CardType = cardType;
            this.FilePath = CardTypeExtension.SetFilePath(cardType);
            this.Rank = CardTypeExtension.SetCardRank(cardType);
        }

        public int Id { get; private set; }

        public CardType CardType
        {
            get
            {
                return this.cardType;
            }

            private set
            {
                this.cardType = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this.filePath;
            }

            protected set
            {
                this.filePath = value;
            }
        }

        public int Rank
        {
            get
            {
                return this.rank;
            }

            protected set
            {
                this.rank = value;
            }
        }

        public void Action()
        {
            // TODO
            CardTypeExtension.Action(this.CardType);
        }

        public override string ToString()
        {
            // TODO
            return this.CardType.ToString();
        }
    }
}
