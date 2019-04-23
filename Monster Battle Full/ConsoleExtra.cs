using System;

namespace Battle
{
    public class ConsoleExtras
    {
        /// <summary>
        /// Outputs "<paramref name="output"/>" to console with color.
        /// </summary>
        /// <param name="output">String to output to console</param>
        /// <param name="color">Color to set forgound color to</param>
        public static void ColorLine(string output, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ResetColor();
        }

        /// <summary>
        /// Parses input from Console.ReadLine() into int
        /// </summary>
        public static int GetIntInput()
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result))
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;
        }

        /// <summary>
        /// Parses input from Console.ReadLine() into int and makes sure is under <paramref name="max"/> but above 0
        /// </summary>
        /// <param name="max">Inclusive maximum value</param>
        public static int GetIntInput(int max)
        {
            int result;
            while (!int.TryParse(Console.ReadLine(), out result) || result >= max || result <= 0)
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;
        }

        /// <summary>
        /// Parses input from Console.ReadLine() and returns value from Race enum
        /// </summary>
        public static Race GetRaceInput()
        {
            Race result;
            while (!Enum.TryParse(Console.ReadLine(), true, out result))
                ColorLine("Invalid Input", ConsoleColor.Red);

            return result;
        }

        /// <summary>
        /// Parses input from Console.ReadLine() and returns a boolean value
        /// </summary>
        public static bool GetBoolInput()
        {
            bool result;
            while (!CheckResult(Console.ReadLine(), out result))
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
