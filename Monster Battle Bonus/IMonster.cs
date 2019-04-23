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
        int Level { get; }
        int Experience { get; }
        int Speed { get; }
        float Health { get; }
        float Power { get; }
        float Defence { get; }
        bool IsAlive { get; }

        /// <summary>
        /// Lower bounds for setting a stat value
        /// </summary>
        (int health, int power, int defence, int speed) Min { get; }
        /// <summary>
        /// Upper bounds for setting a stat value
        /// </summary>
        (int health, int power, int defence, int speed) Max { get; }

        /// <summary>
        /// Takes in raw damage and calculates actual damage dealt with defence.
        /// </summary>
        /// <param name="damage">Raw damage</param>
        /// <param name="attacker">Mosnter attacking</param>
        /// <param name="isTrueDamage">Ignore defence calculation</param>
        void TakeDamage(float damage, IMonster attacker, bool isTrueDamage = false);

        /// <summary>
        /// Attacks the target using random number generated to change damage dealt and type of damage.
        /// </summary>
        /// <param name="target">IMonster target to attack</param>
        void Attack(IMonster target);

        /// <summary>
        /// Adds experience to monster and checks for level ups.
        /// </summary>
        /// <param name="experience">Amount of experience to add</param>
        void AddExperience(int experience);
    }
}
