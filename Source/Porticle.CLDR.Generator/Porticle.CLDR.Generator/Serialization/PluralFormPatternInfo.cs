using Porticle.CLDR.Units;

namespace Porticle.CLDR.Generator.Serialization
{
    readonly struct PluralFormPatternInfo(string language, PluralFormLength pluralFormLength, GrammaticalCase grammaticalCase, PluralCategory pluralCategory, string text)
    {
        public readonly string Language = language;
        public readonly PluralFormLength PluralFormLength = pluralFormLength;
        public readonly GrammaticalCase GrammaticalCase = grammaticalCase;
        public readonly PluralCategory PluralCategory = pluralCategory;
        public readonly string Text = text;

        public void Serialize(BinaryWriter bw)
        {
            bw.Write(Language);
            bw.Write((byte)PluralFormLength);
            bw.Write((byte)GrammaticalCase);
            bw.Write((byte)PluralCategory);
            bw.Write(Text);
        }
    }
}