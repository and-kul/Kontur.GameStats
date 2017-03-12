using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kontur.GameStats.Server
{
    public class CamelCaseJsonSerializer : JsonSerializer
    {
        public CamelCaseJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            DateFormatHandling = DateFormatHandling.IsoDateFormat;
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
            Formatting = Formatting.Indented;
            
        }
        
    }
    
}
