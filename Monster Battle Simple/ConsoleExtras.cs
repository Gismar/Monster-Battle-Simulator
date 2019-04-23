using System;

namespace Battle
{
    public static class ConsoleExtras
    {
        public static void ColorLine(string output, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ResetColor();
        }

        public static int GetIntInput()
        {
            int result;
            while(!int.TryParse(Console.ReadLine(), out result))
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;
        }

        public static Race GetRaceInput()
        {
            Race result;
            while(!Enum.TryParse(Console.ReadLine(), true, out result))
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;
        }

        public static bool GetBoolInput()
        {
            bool result;
            while(!CheckResult(Console.ReadLine(), out result))
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;

            bool CheckResult(string input, out bool output)
            {
                switch (input.ToLower())
                {
                    case "yes":
                    case "1":
                    case "true":
                        output = true;
                        return true;

                    case "no":
                    case "0":
                    case "false":
                        output = false;
                        return true;

                    default:
                        output = false;
                        return false;
                }
            }
        }
    }
}
