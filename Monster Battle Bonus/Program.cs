using System;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            bool @continue;
            
            var engine = new Engine();
            engine.Start();

            Console.Clear();

            while (engine.Continue)
                engine.Update();

            ConsoleExtras.ColorLine("Want to play again?");
            @continue = ConsoleExtras.GetBoolInput();
            Console.Clear();

            if (@continue)
                Main(null);
        }
    }
}
