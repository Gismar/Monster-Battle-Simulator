using System;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            IMonster player = InstantiatePlayer();
            IMonster enemy = InstantiateEnemy(player.Race);

            Console.Clear();

            for (int i = 0; player.IsAlive && enemy.IsAlive; i++)
            {
                ConsoleExtras.ColorLine("Select Option:\n\r1. Fight\n\r2. Check Stats");

                switch (ConsoleExtras.GetIntInput())
                {
                    case 1:
                        Attack(i, player, enemy);
                        break;
                    case 2:
                        ConsoleExtras.ColorLine($"\nYour monster's stats:\t{player}", ConsoleColor.Cyan);
                        ConsoleExtras.ColorLine($"Enemy's stats:\t{enemy}\n", ConsoleColor.Red);
                        break;
                    default:
                        ConsoleExtras.ColorLine("Invalid Selection", ConsoleColor.Red);
                        break;
                }

                Console.WriteLine();
            }

            if (player.IsAlive)
                ConsoleExtras.ColorLine("YOU WIN!", ConsoleColor.Cyan);
            else
                ConsoleExtras.ColorLine("YOU LOSE", ConsoleColor.Red);

            ConsoleExtras.ColorLine("Want to play again?");
            if (ConsoleExtras.GetBoolInput())
            {
                Console.Clear();
                Main(null);
            }
        }

        private static void Attack(int counter, IMonster player, IMonster enemy)
        {
            ConsoleExtras.ColorLine($"\nRound {counter + 1}:");

            if (player.Speed >= enemy.Speed)
            {
                player.Attack(enemy);
                if (enemy.IsAlive)
                    enemy.Attack(player);
            }
            else
            {
                enemy.Attack(player);
                if (player.IsAlive)
                    player.Attack(enemy);
            }
        }

        private static void GetStatInfo(IMonster monster, out int health, out int defence, out int power, out int speed)
        {
            ConsoleExtras.ColorLine($"Enter a value between {monster.Min.health} and {monster.Max.health} for health");
            health = ConsoleExtras.GetIntInput();

            ConsoleExtras.ColorLine($"Enter a value between {monster.Min.defence} and {monster.Max.defence} for defence");
            defence = ConsoleExtras.GetIntInput();

            ConsoleExtras.ColorLine($"Enter a value between {monster.Min.power} and {monster.Max.power} for power");
            power = ConsoleExtras.GetIntInput();

            ConsoleExtras.ColorLine($"Enter a value between {monster.Min.speed} and {monster.Max.speed} for speed");
            speed = ConsoleExtras.GetIntInput();
        }

        private static IMonster InstantiatePlayer()
        {
            ConsoleExtras.ColorLine("Enter a class: " + string.Join(", ", Enum.GetNames(typeof(Race))));
            IMonster player = GetMonster(ConsoleExtras.GetRaceInput());

            ConsoleExtras.ColorLine("\nWrite stat inputs for your monster.", ConsoleColor.Cyan);
            ConsoleExtras.ColorLine("TIP: if input is outside range, it will be automatically put within range", ConsoleColor.DarkYellow);
            GetStatInfo(player, out int health, out int defence, out int power, out int speed);

            return player.Initialize(health, defence, power, speed);
        }

        private static IMonster InstantiateEnemy(Race race)
        {
            var ran = new Random();
            int length = Enum.GetValues(typeof(Race)).Length;
            Race randomRace;

            do randomRace = (Race)ran.Next(0, length);
            while (randomRace == race);
            IMonster enemy = GetMonster(randomRace);

            ConsoleExtras.ColorLine($"\nWrite stat inputs for enemy monster ({enemy.Name}).", ConsoleColor.Red);
            ConsoleExtras.ColorLine("TIP: if input is outside range, it will be automatically put within range", ConsoleColor.DarkYellow);
            GetStatInfo(enemy, out int health, out int defence, out int power, out int speed);

            return enemy.Initialize(health, defence, power, speed);
        }

        private static IMonster GetMonster(Race race)
        {
            if (race == Race.Goblin)
                return new Goblin();
            else if (race == Race.Orc)
                return new Orc();
            else
                return new Troll();
        }
    }
}
