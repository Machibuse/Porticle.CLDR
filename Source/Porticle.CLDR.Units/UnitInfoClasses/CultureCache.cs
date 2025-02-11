using System.Collections.Concurrent;
using System.Collections.Generic;
using Porticle.CLDR.Units.Serialization;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    public static class CldrUnitData
    {
        private static ConcurrentDictionary<Unit, PluralPatternsForUnit> UnitsDictionary = new ConcurrentDictionary<Unit, PluralPatternsForUnit>();

        public static PluralPatternsForUnit GetPatterns(Unit unit)
        {
            return UnitsDictionary.GetOrAdd(unit, u => LoadUnitDataFromResource(u));
        }

        private static PluralPatternsForUnit LoadUnitDataFromResource(Unit unit1)
        {
            return new Deserializer().Load(unit1);
        }
    }
}