namespace NinjaAssassins.Models
{
    using System;
    using System.IO;

    public class Card : ICard
    {
        const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";

        private string filePath;
        private int rank;

        public Card(CardType cardType)
        {
            this.Id++;
            this.CardType = cardType;
            this.FilePath = CardTypeExtension.SetFilePath(cardType);
            this.Rank = CardTypeExtension.SetCardRank(cardType);
        }

        public int Id { get; private set; }

        public CardType CardType { get; private set; }

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

        public void Display()
        {
            try
            {
                using (StreamReader sr = new StreamReader(this.filePath))
                {
                    // TODO : change color
                    string fileContents = sr.ReadToEnd();
                    Console.WriteLine(fileContents);
                }
            }
            catch (Exception e)
            {
                // TODO: Display card name instead
                Console.WriteLine("The file could not be read.");
                Console.WriteLine(e.Message);
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
