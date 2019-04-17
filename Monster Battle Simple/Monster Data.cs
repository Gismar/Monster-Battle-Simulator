namespace Battle
{
    // These are the 'Monsters'
    // They are pretty much just data holders.

    public class Troll : Monster
    {
        public override Race Race => Race.Troll;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (90, 20, 0, 5);
        public override (int health, int power, int defence, int speed) Max { get; } = (110, 25, 5, 8);
    }

    public class Goblin : Monster
    {
        public override Race Race => Race.Goblin;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (75, 10, 5, 7);
        public override (int health, int power, int defence, int speed) Max { get; } = (125, 25, 10, 12);
    }
    public class Orc : Monster
    {
        public override Race Race => Race.Orc;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (125, 5, 10, 2);
        public override (int health, int power, int defence, int speed) Max { get; } = (175, 10, 20, 4);
    }
}
