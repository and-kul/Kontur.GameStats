using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Formatting = Formatting.Indented;
            
        }


    }
}
