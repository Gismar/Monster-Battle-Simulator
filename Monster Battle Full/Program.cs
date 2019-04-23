using System;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleExtras.ColorLine("What would you like to do?\n1. Test Monsters\n2. Start Simulation");
            bool @continue;
            switch (ConsoleExtras.GetIntInput())
            {
                case 1:
                {
                    ConsoleExtras.ColorLine("How many tests per monster? (1-100)");
                    int amount = Math.Min(Math.Max(1, ConsoleExtras.GetIntInput()), 100);
                    Console.Clear();
                    new BalanceTester(amount).TestMonsters();
                    Console.WriteLine();
                    @continue = true;
                    break;
                }
                case 2:
                {
                    var engine = new Engine();
                    engine.Start();

                    Console.Clear();

                    while (engine.Continue)
                        engine.Update();

                    ConsoleExtras.ColorLine("Want to play again?");
                    @continue = ConsoleExtras.GetBoolInput();
                    Console.Clear();
                    break;
                }
                default:
                    ConsoleExtras.ColorLine("INVALID OPTION", ConsoleColor.Red);
                    @continue = true;
                    break;
            }

            if (@continue)
                Main(null);
        }
    }
}
