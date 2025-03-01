# Porticle.CLDR

[![Build and Release](https://github.com/Machibuse/Porticle.CLDR/actions/workflows/release.yaml/badge.svg)](https://github.com/Machibuse/Porticle.CLDR/actions/workflows/release.yaml) 
[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Support%20Me-blue?style=flat&logo=buy-me-a-coffee)](https://buymeacoffee.com/CarstenJendro)


Unit CLDR Data for C# to format values like numbers, dates, and times.  

```csharp
using System;
using System.Globalization;
using Porticle.CLDR.Units;

class Program
{
    static void Main()
    {
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
    }
}
```
