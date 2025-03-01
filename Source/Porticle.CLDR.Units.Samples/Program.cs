using System.Globalization;
using Porticle.CLDR.Units;

var speed = new CldrUnits(Unit.SpeedKilometerPerHour);

Console.WriteLine(speed.GetFormatString(new CultureInfo("en-GB"), 0, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(speed.GetFormatString(new CultureInfo("en-GB"), 1, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(speed.GetFormatString(new CultureInfo("en-GB"), 2, PluralFormLength.Long, GrammaticalCase.Accusative));

// Output will be format strings only:
// {0} kilometres per hour
// {0} kilometre per hour
// {0} kilometres per hour

var microsecond = new CldrUnits(Unit.DurationMicrosecond);
Console.WriteLine(microsecond.FormatUnit(new CultureInfo("de"), 1, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(microsecond.FormatUnit(new CultureInfo("de"), 42, PluralFormLength.Long, GrammaticalCase.Accusative));
Console.WriteLine(microsecond.FormatUnit(new CultureInfo("de"), 42000, PluralFormLength.Short, GrammaticalCase.Accusative));

// Output will be directly formatted as numbers with the given CultureInfo:
// 1 Mikrosekunde
// 42 Mikrosekunden
// 42000 µs

Console.WriteLine(microsecond.FormatUnit(new CultureInfo("de"), 42000, PluralFormLength.Short, GrammaticalCase.Accusative, "N0"));

// output will be directly formatted as numbers with the given CultureInfo and format string "N0" (number without decimal places but with thousands separator):
// 42.000 µs


// Formatting of "liters per 100 kilometers" in japanese
var consumption = new CldrUnits(Unit.ConsumptionLiterPer100Kilometer);
Console.WriteLine(consumption.GetDisplayName(new CultureInfo("ja"),PluralFormLength.Long));

// Display name:
// リットル毎100キロメートル

Console.WriteLine(consumption.FormatUnit(new CultureInfo("ja"), 42, PluralFormLength.Long, GrammaticalCase.Oblique));
Console.WriteLine(consumption.FormatUnit(new CultureInfo("ja"), 42, PluralFormLength.Short, GrammaticalCase.Oblique));

// output will be directly formatted as numbers with the given CultureInfo:
// 42 リットル毎100キロメートル
// 42 L/100km
