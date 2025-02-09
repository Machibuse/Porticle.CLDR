using Porticle.CLDR.Units;

namespace Porticle.CLDR.Generator.Serialization
{
    struct UnitExtraInfo(string language, PluralFormLength length, string displayName, string perUnitPattern)
    {
        public string Language { get; } = language;
        public PluralFormLength Length { get; } = length;
        public string DisplayName { get; } = displayName;
        public string PerUnitPattern { get; } = perUnitPattern;

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Language);
            bw.Write((Byte)Length);
            bw.Write(DisplayName);
            bw.Write(PerUnitPattern);
        }
    }
}