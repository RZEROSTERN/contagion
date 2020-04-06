using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contagion
{
    class Program
    {
        [STAThread]
        static void Main(String[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
}
