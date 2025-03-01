using System.IO;

namespace Porticle.CLDR.Units.Serialization
{
    internal struct UnitExtraInfo
    {
        public UnitExtraInfo(BinaryReader br) : this()
        {
            Language = br.ReadString();
            Length = (PluralFormLength)br.ReadByte();
            DisplayName = br.ReadString();
            PerUnitPattern = br.ReadString();
        }

        public string Language { get; }
        public PluralFormLength Length { get; }
        public string DisplayName { get; }
        public string PerUnitPattern { get; }
    }
}