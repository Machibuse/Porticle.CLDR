using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Porticle.CLDR.Units.UnitInfoClasses;

namespace Porticle.CLDR.Units.Serialization
{
    public class Deserializer
    {
        public Deserializer()
        {
            var manifestResourceNames = typeof(Deserializer).Assembly.GetManifestResourceNames();
            1.ToString();
        }

        public PluralPatternsForUnit Load(Unit unit)
        {
            var resourceName = "Porticle.CLDR.Units.Data."+unit.ToString("D")+".bin";
            using (var manifestResourceStream = typeof(Deserializer).Assembly.GetManifestResourceStream(resourceName))
            {
                if (manifestResourceStream == null)
                {
                    throw new FileNotFoundException("Embedded Resource " + resourceName + " not found");
                }
            
                using (var ds = new DeflateStream(manifestResourceStream,CompressionMode.Decompress))
                {
                    using (var br = new BinaryReader(ds))
                    {
                        var pluralFormPatternCount = br.ReadInt32();
                        var pluralFormPatternInfos = new List<PluralFormPatternInfo>(pluralFormPatternCount);
                        for (int i = 0; i < pluralFormPatternCount; i++)
                        {
                            pluralFormPatternInfos.Add(new PluralFormPatternInfo(br));
                        }
                    
                        var genderInfosCount = br.ReadInt32();
                        var genderInfos = new List<UnitGenderInfo>(genderInfosCount);
                        for (int i = 0; i < genderInfosCount; i++)
                        {
                            genderInfos.Add(new UnitGenderInfo(br));
                        }                    
                    
                        var unitExtraInfosCount = br.ReadInt32();
                        var unitExtraInfos = new List<UnitExtraInfo>(unitExtraInfosCount);
                        for (int i = 0; i < unitExtraInfosCount; i++)
                        {
                            unitExtraInfos.Add(new UnitExtraInfo(br));
                        }

                        return CreatePluralPatternsForCasesForLanguages(unit, pluralFormPatternInfos, genderInfos, unitExtraInfos);
                    }   
                }
            }
        }

        private PluralPatternsForUnit CreatePluralPatternsForCasesForLanguages(Unit unit, List<PluralFormPatternInfo> pluralFormInfo, List<UnitGenderInfo> genderInfosInfos,
            List<UnitExtraInfo> unitExtraInfos)
        {
            PluralPatternsForUnit p = new PluralPatternsForUnit();

            foreach (var pi in pluralFormInfo)
            {
                PluralPatternsForUnitAndLanguage pil;
                if (!p.PluralPatternsForUnitByLanguage.TryGetValue(pi.Language, out pil))
                {
                    pil = new PluralPatternsForUnitAndLanguage();
                    p.PluralPatternsForUnitByLanguage.Add(pi.Language, pil);
                }

                var pill = pil.GetOrAdd(pi.PluralFormLength);

                var pillc = pill.GetOrAdd(pi.GrammaticalCase);

                switch (pi.PluralCategory)
                {
                    case PluralCategory.Other:
                        pillc.Other = pi.Text;
                        break;
                    case PluralCategory.One:
                        pillc.One = pi.Text;
                        break;
                    case PluralCategory.Zero:
                        pillc.Zero = pi.Text;
                        break;
                    case PluralCategory.Two:
                        pillc.Two = pi.Text;
                        break;
                    case PluralCategory.Few:
                        pillc.Few = pi.Text;
                        break;
                    case PluralCategory.Many:
                        pillc.Many = pi.Text;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var pi in unitExtraInfos)
            {
                PluralPatternsForUnitAndLanguage pil;
                if (!p.PluralPatternsForUnitByLanguage.TryGetValue(pi.Language, out pil))
                {
                    pil = new PluralPatternsForUnitAndLanguage();
                    p.PluralPatternsForUnitByLanguage.Add(pi.Language, pil);
                    
                    var pill = pil.GetOrAdd(pi.Length);

                    pill.DisplayName = pi.DisplayName;
                    pill.PerUnitPattern = pi.PerUnitPattern;
                }
            }
            
            foreach (var pi in genderInfosInfos)
            {
                PluralPatternsForUnitAndLanguage pil;
                if (!p.PluralPatternsForUnitByLanguage.TryGetValue(pi.Language, out pil))
                {
                    pil = new PluralPatternsForUnitAndLanguage();
                    p.PluralPatternsForUnitByLanguage.Add(pi.Language, pil);

                    pil.Gender = pi.UnitGender;
                }
            }

            return p;
        }


    }
}