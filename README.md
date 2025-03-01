# Unit CLDR Data for C#

[![Build and Release](https://github.com/Machibuse/Porticle.CLDR/actions/workflows/release.yaml/badge.svg)](https://github.com/Machibuse/Porticle.CLDR/actions/workflows/release.yaml)  

This library allows you to format units with numeric values, such as `1 byte` or `42 bytes`, ensuring the correct grammatical forms using CLDR data.

## Features

- **Fully Functional Out of the Box**  
  No additional CLDR data download is required; everything is included in the package.

- **Embedded CLDR Data**  
  The latest version of CLDR unit data is included as compressed embedded resources within the library.  

- **Optimized Compression**  
  Each unit has its own compressed resource — for example, separate ones for kilograms, days, weeks, months, and years.

- **Efficient Decompression**  
  Only the required units are decompressed when needed, while all others remain in embedded resources to avoid unnecessary memory usage.

- **Versioning**
  I will always try to ensure that the version of this library corresponds to the version of the CLDR library.


## Getting Started

**Porticle.CLDR.Units** is available via the **NuGet** package manager. You can find the package here: [NuGet - Porticle.CLDR.Units](https://www.nuget.org/packages/Porticle.CLDR.Units/).

State on nuget.org [![NuGet Latest Version](https://img.shields.io/nuget/v/Porticle.CLDR.Units.svg)](https://www.nuget.org/packages/Porticle.CLDR.Units/)   [![NuGet Downloads](https://img.shields.io/nuget/dt/Porticle.CLDR.Units.svg)](https://www.nuget.org/packages/Porticle.CLDR.Units/)  

To install **Porticle.CLDR.Units** manually, use the following command in the Package Manager Console:

```
Install-Package Porticle.CLDR.Units
```

## Support

If you find this project useful and would like to support its development, consider buying me a coffee. Your support helps me keep improving and maintaining this library. Thank you! ☕  

[![Buy Me A Coffee](https://img.shields.io/badge/Buy%20Me%20A%20Coffee-Support%20Me-blue?style=flat&logo=buy-me-a-coffee)](https://buymeacoffee.com/CarstenJendro)

Bugs and feature requests are also welcome.

# Example

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
