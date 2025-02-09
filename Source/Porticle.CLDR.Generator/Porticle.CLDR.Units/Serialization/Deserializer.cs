using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Porticle.CLDR.Units.Serialization
{
    public class Deserializer
    {
        public Deserializer()
        {
            var manifestResourceNames = typeof(Deserializer).Assembly.GetManifestResourceNames();
            1.ToString();
        }

        public void Load(Unit unit)
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
                        var genderInfosInfos = new List<UnitGenderInfo>(genderInfosCount);
                        for (int i = 0; i < genderInfosCount; i++)
                        {
                            genderInfosInfos.Add(new UnitGenderInfo(br));
                        }                    
                    
                        var unitExtraInfosCount = br.ReadInt32();
                        var unitExtraInfos = new List<UnitExtraInfo>(unitExtraInfosCount);
                        for (int i = 0; i < unitExtraInfosCount; i++)
                        {
                            unitExtraInfos.Add(new UnitExtraInfo(br));
                        }
                    }   
                }
            
            }
        }
    }
}