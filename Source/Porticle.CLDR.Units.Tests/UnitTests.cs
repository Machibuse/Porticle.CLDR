using Porticle.CLDR.Units.Serialization;

namespace Porticle.CLDR.Units.Tests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var loader = new CldrResourceLoader();


        var weekPluralizer = loader.Load(Unit.DurationWeek);

        var result = weekPluralizer.GetFormat("de", 3, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Wochen");

        result = weekPluralizer.GetFormat("de", 1, PluralFormLength.Long, GrammaticalCase.Accusative); // --> "{0} Wochen"
        Assert.AreEqual(result, "{0} Woche");
    }

    [TestMethod]
    public void TestAll()
    {
        HashSet<string> allLanguages = new HashSet<string>();

        foreach (var unit in Enum.GetValues<Unit>())
        {
            var cldrUnits = new CldrUnits(unit);
            foreach (var la in cldrUnits.GetAllSupportedLanguages()) allLanguages.Add(la);
        }

        foreach (var unit in Enum.GetValues<Unit>())
        {
            var cldrUnits = new CldrUnits(unit);

            foreach (var lang in allLanguages)
            {
                cldrUnits.GetUnitGender(lang);
                
                
                
                foreach (var pluralFormLength in Enum.GetValues<PluralFormLength>())
                {
                    cldrUnits.GetDisplayName(lang,pluralFormLength);
                    cldrUnits.GetPerUnitPattern(lang,pluralFormLength);

                    foreach (var grammaticalCase in Enum.GetValues<GrammaticalCase>())
                    {
                        foreach (var count in (int[])[0,1,2,3,11,21,111,121])
                        {
                            var result = cldrUnits.GetFormatString(lang, count, PluralFormLength.Long, grammaticalCase);
                            Assert.IsNotNull(result);
                            Assert.IsTrue(result.Length > 0);
                            if (count > 2)
                            {
                                Assert.IsTrue(result.Contains("{0}"));
                            }
                        }
                    }
                }
            }
        }
    }
}