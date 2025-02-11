using System.IO;

namespace Porticle.CLDR.Units.Serialization
{
    struct PluralFormPatternInfo
    {
        public PluralFormPatternInfo(BinaryReader br)
        {
            Language = br.ReadString();
            PluralFormLength = (PluralFormLength)br.ReadByte();
            GrammaticalCase = (GrammaticalCase)br.ReadByte();
            PluralCategory = (PluralCategory)br.ReadByte();
            Text = br.ReadString();
        }

        public string Language;
        public PluralFormLength PluralFormLength;
        public GrammaticalCase GrammaticalCase;
        public PluralCategory PluralCategory;
        public string Text;
    }
}