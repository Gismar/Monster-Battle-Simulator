using System;
using System.Collections.Generic;

namespace Battle
{
    public enum ItemType
    {
        Sword,
        LongSword,
        Shield,
        Helmet,
        Armor,
        Leggings,
        Gloves,
        Boots
    }

    public class Item
    {
        public ItemType Type { get; }
        public Dictionary<StatType, int> Stats { get; }

        public Item(int level)
        {
            var ran = new Random();
            Stats = new Dictionary<StatType, int>();
            Type = (ItemType)ran.Next(0, Enum.GetValues(typeof(ItemType)).Length);
            switch (Type)
            {
                case ItemType.Sword:
                    Stats.Add(StatType.Power, ran.Next(5, 10) * level);
                    break;
                case ItemType.LongSword:
                    Stats.Add(StatType.Power, ran.Next(7, 15) * level);
                    Stats.Add(StatType.Speed, ran.Next(-3, -1) * level);
                    break;
                case ItemType.Shield:
                    Stats.Add(StatType.Power, ran.Next(-4, -2) * level);
                    Stats.Add(StatType.Defence, ran.Next(10, 20) * level);
                    break;
                case ItemType.Helmet:
                    Stats.Add(StatType.Health, ran.Next(5, 10) * level);
                    Stats.Add(StatType.Defence, ran.Next(2, 4) * level);
                    break;
                case ItemType.Armor:
                    Stats.Add(StatType.Health, ran.Next(10, 20) * level);
                    Stats.Add(StatType.Defence, ran.Next(5, 10) * level);
                    break;
                case ItemType.Leggings:
                    Stats.Add(StatType.Health, ran.Next(5, 10) * level);
                    Stats.Add(StatType.Defence, ran.Next(5, 10) * level);
                    break;
                case ItemType.Gloves:
                    Stats.Add(StatType.Power, ran.Next(5, 10) * level);
                    Stats.Add(StatType.Speed, ran.Next(5, 10) * level);
                    break;
                case ItemType.Boots:
                    Stats.Add(StatType.Speed, ran.Next(5, 10) * level);
                    break;
            }
        }

        public override string ToString()
        {
            string output = $"{Type}: ";
            foreach (KeyValuePair<StatType, int> stat in Stats)
                output += $"{stat.Value,-3} {stat.Key} ";

            return output;
        }
    }
}
