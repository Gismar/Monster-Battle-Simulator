using System.Collections.Generic;

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
        Dictionary<ItemType, Item> Equipment { get; set; }
        string Name { get; }
        int Level { get; }
        int Experience { get; }
        float CurrentHealth { get; }
        Stat Speed { get; }
        Stat MaxHealth { get; }
        Stat Power { get; }
        Stat Defence { get; }
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
        void AddExperience(int experience);
        void ApplyItemStats();
        void DisplayEquipment();
    }
}
