using System;

namespace Battle
{
    public abstract class Monster : IMonster
    {
        public float Health { get; private set; }
        public float Power { get; private set; }
        public float Defence { get; private set; }
        public float Speed { get; private set; }
        public bool IsAlive => Health > 1f; // Margin because floating point precision.

        public abstract Race Race { get; }
        public abstract string Name { get; }
        public abstract (int health, int power, int defence, int speed) Min { get; }
        public abstract (int health, int power, int defence, int speed) Max { get; }

        private Random _random;

        /// <summary>
        /// Custome constructor, used for initializing data after creating object
        /// </summary>
        public virtual IMonster Initialize(float health, float power, float defence, float speed)
        {
            Health = Math.Max(Math.Min(health, Max.health), Min.health);
            Power = Math.Max(Math.Min(power, Max.power), Min.power);
            Defence = Math.Max(Math.Min(defence, Max.defence), Min.defence);
            Speed = Math.Max(Math.Min(speed, Max.speed), Min.speed);
            _random = new Random();

            return this;
        }

        /// <summary>
        /// Attacks target with random probability.
        /// </summary>
        public virtual void Attack(IMonster target)
        {
            if (target.IsAlive)
            {
                int damageModifier = _random.Next(74, 127);
                float damage = (damageModifier / 100f) * Power;

                switch (damageModifier)
                {
                    case int n when n == 126:
                        ConsoleExtras.ColorLine($"{Name} just dealt {Power:0.0} true damage to {target.Name}".ToUpper(), ConsoleColor.Cyan);
                        target.TakeDamage(Power, this, true);
                        break;

                    case int n when n == 74:
                        ConsoleExtras.ColorLine($"{Name} critically missed and dealt {Power:0.0} true damage to itself", ConsoleColor.Red);
                        TakeDamage(Power, this, true);
                        break;

                    case int n when n >= 115:
                        ConsoleExtras.ColorLine($"{Name} dealt {damage:0.0} critical damage to {target.Name}", ConsoleColor.Yellow);
                        target.TakeDamage(damage, this, false);
                        break;

                    case int n when n <= 85:
                        ConsoleExtras.ColorLine($"{Name} fumbled and dealt {damage:0.0} damage to {target.Name}", ConsoleColor.DarkYellow);
                        target.TakeDamage(damage, this, false);
                        break;

                    default:
                        ConsoleExtras.ColorLine($"{Name} dealt {damage:0.0} damage to {target.Name}");
                        target.TakeDamage(damage, this, false);
                        break;
                }
            }
        }

        /// <summary>
        /// Takes damage.
        /// </summary>
        public virtual void TakeDamage(float damage, IMonster attacker, bool isTrue)
        {
            float mitigateDamage = isTrue ? damage : damage * (100f / (100f + Defence));
            Health = Math.Max(Health - mitigateDamage, 0f);

            if (!IsAlive)
                ConsoleExtras.ColorLine($"{attacker.Name} HAS SLAIN {Name}!!", ConsoleColor.DarkRed);
        }
         /// <summary>
         /// Custom .ToString() method
         /// </summary>
        public virtual string GetStats()
            => $"{Name}: {Health:0.0} Health, {Power:0.0} Power, {Defence:0.0} Defence, {Speed:0.0} Speed";

    }
}
