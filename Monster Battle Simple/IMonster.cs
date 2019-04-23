namespace Battle
{
    public enum Race
    {
        Orc,
        Troll,
        Goblin
    }

    public interface IMonster
    {
        Race Race { get; }
        string Name { get; }
        float Health { get; }
        float Power { get; }
        float Defence { get; }
        float Speed { get; }
        bool IsAlive { get; }

        /// <summary>
        /// Lower bounds for setting a stat value
        /// </summary>
        (int health, int power, int defence, int speed) Min { get; }
        /// <summary>
        /// Upper bounds for setting a stat value
        /// </summary>
        (int health, int power, int defence, int speed) Max { get; }

        void TakeDamage(float damage, IMonster attacker, bool isTrueDamage = false);
        void Attack(IMonster target);
        IMonster Initialize(float health, float power, float defence, float speed);
    }
}
