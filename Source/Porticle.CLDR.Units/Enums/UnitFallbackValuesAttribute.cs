using System;

namespace Porticle.CLDR.Units
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UnitFallbackValuesAttribute : Attribute
    {
        public string Long { get; }
        public string Short { get; }
        public string Narrow { get; }

        public UnitFallbackValuesAttribute(string longFallback, string shortFallback, string narrowFallback)
        {
            Long = longFallback;
            Short = shortFallback;
            Narrow = narrowFallback;
        }
    }
}