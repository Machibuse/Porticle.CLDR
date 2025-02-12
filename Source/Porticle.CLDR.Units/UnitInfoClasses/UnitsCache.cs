using System.Collections.Concurrent;
using System.Collections.Generic;
using Porticle.CLDR.Units.Serialization;

namespace Porticle.CLDR.Units.UnitInfoClasses
{
    internal static class UnitsCache
    {
        private static readonly ConcurrentDictionary<Unit, PatternsForUnit> UnitsDictionary = new ConcurrentDictionary<Unit, PatternsForUnit>();

        public static PatternsForUnit GetPatterns(Unit unit)
        {
            return UnitsDictionary.GetOrAdd(unit, u => LoadUnitDataFromResource(u));
        }

        private static PatternsForUnit LoadUnitDataFromResource(Unit unit1)
        {
            return new CldrResourceLoader().Load(unit1);
        }
    }
}