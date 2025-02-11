namespace Porticle.CLDR.Generator.Serialization
{
    internal readonly struct UnitGenderInfo(string language, UnitGender unitGender)
    {
        public readonly string Language = language;
        public readonly UnitGender UnitGender = unitGender;
        
        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Language);
            bw.Write((byte)UnitGender);
        }
    }
}
