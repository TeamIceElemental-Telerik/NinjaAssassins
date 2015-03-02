namespace NinjaAssassins.Helper
{
    using System.Collections.Generic;

    public abstract class Constants
    {
        public const int CardsInDeck = 32;
        public const int TotalPlayers = 4;
        public const int HandCount = 3;

        public const string CardFilePath = "../../../NinjaAssassins.Models/CardFaces/";
        public const string CardFileExtension = ".txt";

        public const string HighScoreFilePath = "../../../NinjaAssassins.Models/HighScores.txt";
        public const string PlayerMoves = "../../../NinjaAssassins.Models/PlayerMoves.txt";
        public const string GameIntro = "../../../NinjaAssassins.Models/GameIntro.txt";
        public const string GameBoard = "../../../NinjaAssassins.Models/GameBoard.txt";

        public static readonly string[] ComputerPlayersNames = { "Pesho", "Geri", "Stamat" };

        public const int xRightBorder = 81;
    }
}
