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
            using(Level1 engine = new Level1()) // Change to title
            {
                engine.Run();
            }
        }
    }
}
