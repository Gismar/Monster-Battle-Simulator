using System;
using System.Threading;

namespace Battle
{
    public class Engine
    {
        public bool Continue { get; private set; }

        private IMonster _player;
        private IMonster _enemy;
        private Random _random;
        private int _round;
        private int _wave;

        public void Start()
        {
            Console.WriteLine("Enter a class: " + string.Join(", ", Enum.GetNames(typeof(Race))));

            _player = GetMonster(ConsoleExtras.GetRaceInput(), 1);
            Thread.Sleep(10); // Seperate the seed for System.Random's algorithm.
            _enemy = InstantiateEnemy(_player.Race, _player.Level);
            _random = new Random();

            Continue = true;
            _round = 0;
            _wave = 1;
        }

        public void Update()
        {
            _round++;
            ConsoleExtras.ColorLine("Select Option:\n\r1. Fight\n\r2. Check Stats\n\r3. Quit");

            switch (ConsoleExtras.GetIntInput(5))
            {
                case 1:
                    ConsoleExtras.ColorLine($"\nRound {_round}, Wave {_wave}:");
                    Attack(_player, _enemy);
                    break;
                case 2:
                    ConsoleExtras.ColorLine($"\nYour stats:\n{_player}", ConsoleColor.Cyan);
                    ConsoleExtras.ColorLine($"Enemy's stats:\n{_enemy}\n", ConsoleColor.Red);
                    break;
                case 3:
                    Continue = false;
                    return;
            }

            if (!_enemy.IsAlive)
            {
                ConsoleExtras.ColorLine("New Enemy has appeared!", ConsoleColor.DarkRed);
                _enemy = InstantiateEnemy(_player.Race, _random.Next(_player.Level - 1, _player.Level + 1));
                _round = 0;
                _wave++;
            }

            if (!_player.IsAlive)
                Continue = false;

            Console.WriteLine();
        }

        public static void Attack(IMonster a, IMonster b)
        {
            if (a.Speed >= b.Speed)
            {
                a.Attack(b);
                if (b.IsAlive)
                    b.Attack(a);
            }
            else
            {
                b.Attack(a);
                if (a.IsAlive)
                    a.Attack(b);
            }
        }

        public static IMonster GetMonster(Race race, int level)
        {
            switch (race)
            {
                case Race.Goblin: return new Goblin(level);
                case Race.Orc: return new Orc(level);
                case Race.Troll: return new Troll(level);
                default: return null;
            }
        }

        public static IMonster InstantiateEnemy(Race race, int level)
        {
            var ran = new Random();
            int length = Enum.GetValues(typeof(Race)).Length;
            Race randomRace;

            do randomRace = (Race)ran.Next(0, length);
            while (randomRace == race);
            return GetMonster(randomRace, level);
        }
    }
}
