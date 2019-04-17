using System;

namespace Battle
{
    class Program
    {
        static void Main(string[] args)
        {
            bool play;
            do
            {
                Race race = GetPlayerInputRace();

                InstantiatePlayerAndEnemy(race, out IMonster player, out IMonster enemy);

                ConsoleExtras.ColorLine("\nWrite stat inputs for your monster.", ConsoleColor.Cyan);
                ConsoleExtras.ColorLine("TIP: if input is outside range, it will be automatically put within range", ConsoleColor.DarkYellow);

                GetStatInfo(player, out int health, out int defence, out int power, out int speed);
                player.Initialize(health, power, defence, speed);

                ConsoleExtras.ColorLine("\nWrite stat inputs for enemy monster", ConsoleColor.Red);
                GetStatInfo(enemy, out health, out defence, out power, out speed);
                enemy.Initialize(health, power, defence, speed);

                Console.Clear();

                ConsoleExtras.ColorLine($"Your monster's stats:\t{player?.GetStats()}", ConsoleColor.Cyan);
                ConsoleExtras.ColorLine($"Enemy's stats:\t{enemy?.GetStats()}", ConsoleColor.Red);

                int counter = 0;
                while (player.IsAlive && enemy.IsAlive)
                {
                    ConsoleExtras.ColorLine("\nSelect Option:\n\r1. Fight\n\r2. Check Stats");
                    if (ConsoleExtras.GetIntInput(Console.ReadLine(), out int option))
                    {
                        if (option == 1)
                        {
                            counter++;
                            Attack(counter, player, enemy);
                            continue;
                        }
                        else if (option == 2)
                        {
                            ConsoleExtras.ColorLine($"\nYour monster's stats:\t{player?.GetStats()}", ConsoleColor.Cyan);
                            ConsoleExtras.ColorLine($"Enemy's stats:\t{enemy?.GetStats()}\n", ConsoleColor.Red);
                            continue;
                        }
                    }
                    ConsoleExtras.ColorLine("Invalid Selection", ConsoleColor.Red);
                }

                if (player.IsAlive)
                    ConsoleExtras.ColorLine("YOU WIN!", ConsoleColor.Cyan);
                else
                    ConsoleExtras.ColorLine("YOU LOSE", ConsoleColor.Red);

                ConsoleExtras.ColorLine("Want to play again?");

                while(!ConsoleExtras.GetBoolInput(Console.ReadLine(), out play))
                    ConsoleExtras.ColorLine("Invalid Selection", ConsoleColor.Red);

            } while (play);
        }

        private static void Attack(int counter, IMonster player, IMonster enemy)
        {
            ConsoleExtras.ColorLine($"\nRound {counter}:");

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
            health = GetPlayerStatInput($"Enter a value between {monster.Min.health} and {monster.Max.health} for health");
            defence = GetPlayerStatInput($"Enter a value between {monster.Min.defence} and {monster.Max.defence} for defence");
            power = GetPlayerStatInput($"Enter a value between {monster.Min.power} and {monster.Max.power} for power");
            speed = GetPlayerStatInput($"Enter a value between {monster.Min.speed} and {monster.Max.speed} for speed");
        }

        private static void InstantiatePlayerAndEnemy(Race race, out IMonster player, out IMonster enemy)
        {
            player = null;
            enemy = null;
            switch (race)
            {
                case Race.Goblin:
                    player = new Goblin();
                    enemy = GetRandomEnemy();
                    break;
                case Race.Troll:
                    player = new Troll();
                    enemy = GetRandomEnemy();
                    break;
                case Race.Orc:
                    player = new Orc();
                    enemy = GetRandomEnemy();
                    break;
            }

            IMonster GetRandomEnemy()
            {
                var ran = new Random();
                Race randomRace;
                do
                {
                    randomRace = (Race)ran.Next(0, Enum.GetValues(typeof(Race)).Length);
                } while (randomRace == race);

                if (randomRace == Race.Goblin)
                    return new Goblin();
                else if (randomRace == Race.Orc)
                    return new Orc();
                else
                    return new Troll();
            }
        }

        public static Race GetPlayerInputRace()
        {
            bool worked;
            Race option;
            do
            {
                ConsoleExtras.ColorLine("Choose Race: Troll, Orc, Goblin");
                worked = ConsoleExtras.GetEnumInput(Console.ReadLine(), out Race output);
                option = worked ? output : Race.Goblin;

                if (!worked)
                    ConsoleExtras.ColorLine("Invalid Input", ConsoleColor.Red);
            } while (!worked);

            return option;
        }

        public static int GetPlayerStatInput(string prompt)
        {
            bool worked;
            int result;
            do
            {
                ConsoleExtras.ColorLine(prompt);
                worked = ConsoleExtras.GetIntInput(Console.ReadLine(), out int output);
                result = output;

                if (!worked)
                    ConsoleExtras.ColorLine("Invalid Input", ConsoleColor.Red);
            } while (!worked);

            return result;
        }
    }
}
