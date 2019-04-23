using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Battle
{
    /// <summary>
    /// Runs X simulations for each race type and computes their winrates
    /// </summary>
    public class BalanceTester
    {
        private Dictionary<Race, List<(int round, int wave, int totalRounds)>> _stats;
        private readonly int _testAmount;

        public BalanceTester(int testAmount)
        {
            _testAmount = testAmount;
            _stats = new Dictionary<Race, List<(int round, int wave, int totalRounds)>>();
            foreach (Race race in Enum.GetValues(typeof(Race)))
                _stats.Add(race, new List<(int round, int wave, int totalRounds)>());
        }

        public void TestMonsters()
        {
            Console.WriteLine("Testing: ");
            Console.WriteLine("Round: ");
            Console.WriteLine("Wave: ");
            int counter = 0;
            foreach(KeyValuePair<Race, List<(int round, int wave, int totalRounds)>> stat in _stats)
            {
                for (int i = 0; i < _testAmount; i++)
                {
                    Console.SetCursorPosition(10, 0);
                    Console.WriteLine($"{i + (counter * _testAmount) + 1} / {_testAmount * _stats.Count}");
                    IMonster tester = Engine.GetMonster(stat.Key, 1);
                    Thread.Sleep(10); // Seperate the seed for System.Random's algorithm.
                    IMonster enemy = Engine.InstantiateEnemy(tester.Race, tester.Level);
                    var random = new Random();
                    int totalRounds = 0;

                    (int round, int wave) = (0, 1);
                    do
                    {
                        round++;
                        totalRounds++;
                        Console.SetCursorPosition(8, 1);
                        Console.Write(round);
                        Console.SetCursorPosition(6, 2);
                        Console.WriteLine($"{wave,-2}");
                        Engine.Attack(tester, enemy);

                        if (!enemy.IsAlive)
                        {
                            enemy = Engine.InstantiateEnemy(tester.Race, random.Next(tester.Level - 1, tester.Level + 1));
                            round = 0;
                            wave++;
                        }
                    } while (tester.IsAlive);

                    stat.Value.Add((round, wave, totalRounds));
                }
                counter++;
            }

            DisplayStats();
        }

        public void DisplayStats()
        {
            string display = "Average rounds and waves:\n\r";
            foreach(KeyValuePair<Race, List<(int round, int wave, int totalRounds)>> stats in _stats)
            {
                double averageTotalRound = stats.Value.Average(s => s.totalRounds);
                double averageRound = stats.Value.Average(s => s.round);
                double averageWave = stats.Value.Average(s => s.wave);
                display += $" {stats.Key,-10}\tRounds: {averageRound:0.0}\t Waves: {averageWave:0.0}\t Total Rounds: {averageTotalRound}\n";
            }

            Console.WriteLine($"\n{display}");
        }
    }
}
