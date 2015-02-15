namespace NinjaAssassins.ConsoleApplication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NinjaAssassins.GameLogic;
    using NinjaAssassins.Models;

    public class GameCycle
    {
        static void Main()
        {
            // TODO
            string playerName = GameVisualisation.AskForUsername();
            GameLogic.InitializeGame(playerName);
        }
    }
}
