namespace NinjaAssassins.Models
{
    public abstract class Constants
    {
        public const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";
        public const string CardFileExtension = ".txt";
        public const int CardsInDeck = 32;
        public const int TotalPlayers = 4;
        public const string HighScoreFilePath = "../../../NinjaAssassins.Models/HighScores.txt";
        public const string PlayerMoves = "../../../NinjaAssassins.Models/PlayerMoves.txt";

        public static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };
    }
}
