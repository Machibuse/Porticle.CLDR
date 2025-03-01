using System.Globalization;
using Porticle.CLDR.Units;

var weekUnits = new CldrUnits(Unit.MassTonne);

Console.WriteLine(weekUnits.GetFormatString(new CultureInfo("en-GB"), 0, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(weekUnits.GetFormatString(new CultureInfo("en-GB"), 1, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(weekUnits.GetFormatString(new CultureInfo("en-GB"), 2, PluralFormLength.Long, GrammaticalCase.Accusative));

Console.WriteLine(weekUnits.FormatUnit(new CultureInfo("it"), 0, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(weekUnits.FormatUnit(new CultureInfo("it"), 1, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(weekUnits.FormatUnit(new CultureInfo("it"), 2, PluralFormLength.Long, GrammaticalCase.Accusative));


// This code produces the following output:

// {0} tonnes
// {0} tonne
// {0} tonnes
// 0 tonnellate metriche
// 1 tonnellata metrica
// 2 tonnellate metriche


 