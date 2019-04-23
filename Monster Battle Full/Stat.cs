namespace Battle
{
    public enum StatType
    {
        Health,
        Power,
        Speed,
        Defence
    }
    public struct Stat
    {
        public StatType Type { get; }
        public float Base { get; }
        public float Additional { get; }
        public float Total => Base + Additional;

        public Stat(float baseValue, StatType type, float additionalValue = 0)
        {
            Base = baseValue;
            Additional = additionalValue;
            Type = type;
        }

        public override string ToString() => Total + "";
        public static Stat operator +(Stat stat, float value) => new Stat(stat.Base, stat.Type, stat.Additional + value);
        public static Stat operator -(Stat stat, float value) => new Stat(stat.Base, stat.Type, stat.Additional - value);
    }
}
