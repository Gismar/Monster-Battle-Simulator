using System;

namespace Battle
{
    public abstract class Monster : IMonster
    {
        public bool IsAlive => Health > 1f; // Margin because floating point precision.
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int Speed { get; private set; }
        public float Health { get; private set; }
        public float Power { get; private set; }
        public float Defence { get; private set; }

        public abstract Race Race { get; }
        public abstract string Name { get; }
        public abstract (int health, int power, int defence, int speed) Min { get; }
        public abstract (int health, int power, int defence, int speed) Max { get; }

        private Random _random;
        private int _nextLevelExperience;

        public Monster(int level)
        {
            _random = new Random();
            Level = level;
            UpdateStates();
        }

        private void UpdateStates()
        {
            Health = _random.Next(Min.health, Max.health) * Level;
            Power = _random.Next(Min.power, Max.power) * Level;
            Defence = _random.Next(Min.defence, Max.defence) * Level;
            Speed = _random.Next(Min.speed, Max.speed);
            _nextLevelExperience = Level * Level;
        }

        public virtual void Attack(IMonster target)
        {
            int damageModifier = _random.Next(74, 127);
            float damage = (damageModifier / 100f) * Power;
            float defence = 100f / (100f + target.Defence);

            switch (damageModifier)
            {
                case int n when n == 126:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} just dealt {Power:0.0} true damage to {target.Name}".ToUpper(),
                        color: ConsoleColor.Cyan);
                    target.TakeDamage(Power, this, true);
                    break;

                case int n when n == 74:
                    ConsoleExtras.ColorLine(
                        output: $"{Name} critically missed and dealt {Power:0.0} true damage to itself",
                        color: ConsoleColor.Red);
                    TakeDamage(Power, this, true);
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

        public virtual void TakeDamage(float damage, IMonster attacker, bool isTrueDamage = false)
        {
            float mitigateDamage = isTrueDamage ? damage : damage * (100f / (100f + Defence));
            Health = Math.Max(Health - mitigateDamage, 0f);

            if (!IsAlive && attacker != null)
            {
                ConsoleExtras.ColorLine($"{attacker.Name} HAS SLAIN {Name}!!", ConsoleColor.DarkRed);
                attacker.AddExperience(Level * 2);
            }
        }

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
            => $"{Name,-7}: {Health,5:0.0} Health, {Power,4:0.0} Power, {Defence,4:0.0} Defence, " +
            $"{Speed,2} Speed | Level: {Level,-2}, Exp: {Experience}/{_nextLevelExperience}";
    }
}
