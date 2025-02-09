using System.IO;

namespace Porticle.CLDR.Units.Serialization
{
    struct UnitGenderInfo
    {
        public UnitGenderInfo(BinaryReader br) : this()
        {
            Language = br.ReadString();
            UnitGender = (UnitGender)br.ReadByte();
        }          

        public string Language { get; }
        public UnitGender UnitGender { get; }
    }
}
