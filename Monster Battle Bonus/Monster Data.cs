namespace Battle
{
    // These are the 'Monsters'
    // They are pretty much just data holders.
    public sealed class Goblin : Monster
    {
        public override Race Race => Race.Goblin;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (70, 10, 2, 8);
        public override (int health, int power, int defence, int speed) Max { get; } = (105, 15, 5, 12);

        public Goblin(int level = 1) : base(level) { }
    }

    public sealed class Troll : Monster
    {
        public override Race Race => Race.Troll;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (95, 7, 5, 2);
        public override (int health, int power, int defence, int speed) Max { get; } = (130, 12, 7, 6);

        public Troll(int level = 1) : base(level) { }
    }

    public sealed class Orc : Monster
    {
        public override Race Race => Race.Orc;
        public override string Name => GetType().Name;

        public override (int health, int power, int defence, int speed) Min { get; } = (120, 5, 7, 5);
        public override (int health, int power, int defence, int speed) Max { get; } = (155, 9, 10, 9);

        public Orc(int level = 1) : base(level) { }
    }
}
