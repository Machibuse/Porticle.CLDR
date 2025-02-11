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
        var loader = new CldrResourceLoader();
 
        
        string result;
        
        
        
        
        var weekPluralizer = loader.Load(Unit.DurationWeek);
        
        result = weekPluralizer.GetFormat("de", 3, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Wochen");
        
        result = weekPluralizer.GetFormat("de", 1, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Woche");
    }

    [TestMethod]
    public void TestMethod2()
    {
        var deserializer = new CldrResourceLoader();
        var x = new List<Unit>() { Unit.DurationWeek, Unit.DurationWeek, Unit.DurationWeek, Unit.AngleDegree, Unit.AngleRadian };
        
        var pluralizer = deserializer.Load(Unit.DurationWeek);
        
        Check(pluralizer);
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