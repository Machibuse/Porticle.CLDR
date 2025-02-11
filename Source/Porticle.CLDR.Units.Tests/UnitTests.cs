using System.Diagnostics;
using System.Runtime.CompilerServices;
using Porticle.CLDR.Units.Serialization;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units.Tests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var deserializer = new Deserializer();
        var x = new List<Unit>() { Unit.DurationWeek, Unit.DurationWeek, Unit.DurationWeek, Unit.AngleDegree, Unit.AngleRadian };
        foreach (var xx in x)
        {
            Stopwatch sw = Stopwatch.StartNew();
            var pluralPatternsForUnit = deserializer.Load(xx);
            Console.WriteLine(xx+" "+sw.ElapsedMilliseconds);
            sw = Stopwatch.StartNew();
            Check(pluralPatternsForUnit);
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }



    private static void Check(PluralPatternsForUnit p)
    {
        foreach (var pair in p.PluralPatternsForUnitByLanguage)
        {
            Check(pair.Value.Long);
            Check(pair.Value.Short);
            Check(pair.Value.Narrow);
                
            
        }
    }

    private static void Check(PluralPatternsForUnitLanguageAndLength value)
    {
        Assert.IsNotNull(value);
        
            
        Assert.IsNotNull(value.None);
            
        Check(value.None);
    }

    private static void Check(PluralPatternsForUnitLanguageLengthAndCaseBase? valueNone)
    {
        Assert.IsNotNull(valueNone.Other);
    }
}