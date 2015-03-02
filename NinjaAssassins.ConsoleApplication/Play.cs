namespace NinjaAssassins.ConsoleApplication
{
    using System;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;

    public class Play
    {
        public static void Main()
        {
            GameVisualisation.SetInitialConsoleSize();
            GameVisualisation.DisplayInitialMenu();

            string playerName = GameVisualisation.AskForUsername();
            Game game = GameLogic.InitializeGame(playerName);
          
            GameLoop.Play(game);
        }
    }
}