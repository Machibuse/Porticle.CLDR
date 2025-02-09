using Porticle.CLDR.Units.Serialization;

namespace Porticle.CLDR.Units.Tests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void TestMethod1()
    {
        var deserializer = new Deserializer();
        deserializer.Load(Unit.DurationWeek);
    }
}