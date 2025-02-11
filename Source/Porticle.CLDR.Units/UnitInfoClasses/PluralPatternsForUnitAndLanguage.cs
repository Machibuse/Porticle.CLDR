using System;
using System.Diagnostics.CodeAnalysis;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    public class PluralPatternsForUnitAndLanguage
    {
        public PluralPatternsForUnitLanguageAndLength Long { get; internal set; } 
        public PluralPatternsForUnitLanguageAndLength Short { get;  internal set;} 
        public PluralPatternsForUnitLanguageAndLength Narrow { get;  internal set;}
        public UnitGender Gender { get; internal set; }

        [SuppressMessage("ReSharper", "NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract")]
        internal PluralPatternsForUnitLanguageAndLength GetOrAdd(PluralFormLength piPluralFormLength)
        {
            switch (piPluralFormLength)
            {
                case PluralFormLength.Long:
                    return Long ??= new PluralPatternsForUnitLanguageAndLength();
                    break;
                case PluralFormLength.Short:
                    return Short ??= new PluralPatternsForUnitLanguageAndLength();
                case PluralFormLength.Narrow:
                    return Narrow ??= new PluralPatternsForUnitLanguageAndLength();
                default:
                    throw new ArgumentOutOfRangeException(nameof(piPluralFormLength), piPluralFormLength, null);
            }
        }
    }
}