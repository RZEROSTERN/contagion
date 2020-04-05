using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Contagion
{
    static class Global
    {
        public static Level1 game;
        public static Random random = new Random();
        public static string levelName;

        public static void Initialize(Level1 inputGame)
        {
            game = inputGame;
        }
    }
}
