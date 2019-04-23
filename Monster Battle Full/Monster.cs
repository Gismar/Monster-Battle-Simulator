using System;
using System.Collections.Generic;

namespace Battle
{
    public abstract class Monster : IMonster
    {
        public Dictionary<ItemType, Item> Equipment { get; set; }
        public bool IsAlive => CurrentHealth > 1f; // Margin because floating point precision.
        public float CurrentHealth { get; private set; }
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public Stat Speed { get; private set; }
        public Stat MaxHealth { get; private set; }
        public Stat Power { get; private set; }
        public Stat Defence { get; private set; }

        public abstract Race Race { get; }
        public abstract string Name { get; }
        public abstract (int health, int power, int defence, int speed) Min { get; }
        public abstract (int health, int power, int defence, int speed) Max { get; }

        private Random _random;
        private int _nextLevelExperience;

        public Monster(int level)
        {
            Equipment = new Dictionary<ItemType, Item>();
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
                Equipment.Add(type, null);

            _random = new Random();
            Level = level;
            UpdateStates();
        }

        public void ApplyItemStats()
        {
            foreach (Item equip in Equipment.Values)
            {
                if (equip == null) continue;

                foreach (KeyValuePair<StatType, int> stat in equip.Stats)
                {
                    switch (stat.Key)
                    {
                        case StatType.Health: MaxHealth += stat.Value; break;
                        case StatType.Speed: Speed += stat.Value; break;
                        case StatType.Power: Power += stat.Value; break;
                        case StatType.Defence: Defence += stat.Value; break;
                    }
                }
            }

            if (CurrentHealth == MaxHealth.Base)
                CurrentHealth = MaxHealth.Total;
        }

        private void UpdateStates()
        {
            MaxHealth = new Stat(_random.Next(Min.health, Max.health) * Level, StatType.Health);
            CurrentHealth = MaxHealth.Total;
            Power = new Stat(_random.Next(Min.power, Max.power) * Level, StatType.Power);
            Defence = new Stat(_random.Next(Min.defence, Max.defence) * Level, StatType.Defence);
            Speed = new Stat(_random.Next(Min.speed, Max.speed), StatType.Speed);
            _nextLevelExperience = Level * Level;
            ApplyItemStats();
        }

        public void DisplayEquipment()
        {
            ConsoleExtras.ColorLine($"{Name}'s Equipments:");
            foreach (KeyValuePair<ItemType, Item> items in Equipment)
                if (items.Value != null)
                    ConsoleExtras.ColorLine(items.Value.ToString());
            Console.WriteLine();

            ConsoleExtras.ColorLine("Total Stats Gained:" +
                $" {MaxHealth.Additional, 4} Health, {Power.Additional, 3} Power, {Defence.Additional, 3} Defence, {Speed, 2} Speed");
        }

        /// <summary>
        /// Attacks target with random probability.
        /// </summary>
        public virtual void Attack(IMonster target)
        {
            int damageModifier = _random.Next(74, 127);
            float damage = (damageModifier / 100f) * Power.Total;
            float defence = 100f / (100f + target.Defence.Total);

            switch (damageModifier)
            {
                case int n when n == 126:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} just dealt {Power:0.0} true damage to {target.Name}".ToUpper(), 
                        color: ConsoleColor.Cyan);
                    target.TakeDamage(Power.Total, this, true);
                    break;

                case int n when n == 74:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} critically missed and dealt {Power:0.0} true damage to itself",
                        color: ConsoleColor.Red);
                    TakeDamage(Power.Total, this, true);
                    break;

                case int n when n >= 115:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} dealt {damage * defence:0.0} critical damage to {target.Name}",
                        color: ConsoleColor.Yellow);
                    target.TakeDamage(damage, this);
                    break;

                case int n when n <= 85:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} fumbled and dealt {damage * defence:0.0} damage to {target.Name}",
                        color: ConsoleColor.DarkYellow);
                    target.TakeDamage(damage, this);
                    break;

                default:
                    ConsoleExtras.ColorLine($"{Name} dealt {damage * defence:0.0} damage to {target.Name}");
                    target.TakeDamage(damage, this);
                    break;
            }
        }

        /// <summary>
        /// Calculates how much damage will be dealt with defence.
        /// </summary>
        /// <param name="damage">Raw, unmitigated damage</param>
        /// <param name="attacker">IMonster that is dealing the damage</param>
        /// <param name="isTrueDamage">Ignore Defence calculation</param>
        public virtual void TakeDamage(float damage, IMonster attacker, bool isTrueDamage = false)
        {
            float mitigateDamage = isTrueDamage ? damage : damage * (100f / (100f + Defence.Total));
            CurrentHealth = Math.Max(CurrentHealth - mitigateDamage, 0f);

            if (!IsAlive && attacker != null)
            {
                ConsoleExtras.ColorLine($"{attacker.Name} HAS SLAIN {Name}!!", ConsoleColor.DarkRed);
                attacker.AddExperience(Level * 2);
            }
        }

        /// <summary>
        /// Adds experience to monster and levels up with excess experience
        /// </summary>
        /// <param name="experience">Experience the monster gains</param>
        public void AddExperience(int experience)
        {
            Experience += experience;
            while (Experience >= _nextLevelExperience)
            {
                ConsoleExtras.ColorLine($"{Name} has just leveled up! ({Level} -> {++Level})", ConsoleColor.Blue);
                Experience -= _nextLevelExperience;
                UpdateStates();
            }
        }

        public override string ToString()
            => $"{Name,-7}: {$"{CurrentHealth:0.0} Health", 11} | {$"{Power:0.0} Power", 9} | {$"{Defence:0.0} Defence", 11}" +
            $" | {$"{Speed:0.0} Speed", 8} | {$"Level: {Level}", -9} | Exp: {Experience}/{_nextLevelExperience}";
    }
}
