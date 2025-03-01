using System.IO;

namespace Porticle.CLDR.Units.Serialization
{
    internal struct PluralFormPatternInfo
    {
        public PluralFormPatternInfo(BinaryReader br)
        {
            Language = br.ReadString();
            PluralFormLength = (PluralFormLength)br.ReadByte();
            GrammaticalCase = (GrammaticalCase)br.ReadByte();
            PluralCategory = (PluralCategory)br.ReadByte();
            Text = br.ReadString();
        }

        public readonly string Language;
        public readonly PluralFormLength PluralFormLength;
        public readonly GrammaticalCase GrammaticalCase;
        public readonly PluralCategory PluralCategory;
        public readonly string Text;
    }
}