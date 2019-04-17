using System;

namespace Battle
{
    public static class ConsoleExtras
    {
        /// <summary>
        /// Prints to console using color, with default being white.
        /// </summary>
        public static void ColorLine(string output, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ResetColor();
        }

        /// <summary>
        /// Returns wether parsing was successfull or not and parses the output string via the 'out' param
        /// </summary>
        public static bool GetIntInput(string input, out int output)
            => int.TryParse(input, out output);

        /// <summary>
        /// Returns wether parsing was successfull or not and parses the output string via the 'out' param
        /// </summary>
        public static bool GetBoolInput(string input, out bool output)
        {
            switch (input.ToLower())
            {
                case "yes":
                case "true":
                    output = true;
                    return true;

                case "no":
                case "false":
                    output = false;
                    return true;

                default:
                    output = false;
                    return false;
            }
        }

        /// <summary>
        /// Returns wether parsing was successfull or not and parses the output string via the 'out' param
        /// </summary>
        public static bool GetEnumInput(string input, out object output)
            => Enum.TryParse(typeof(Race), input, true, out output);
    }
}
