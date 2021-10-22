using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace CVU.ERP.Module.API.Settings
{
    public class CamelCaseContractResolverJsonSettings
    {
        public JsonSerializerSettings GetJSONSettings(bool ignoreNull, ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore, bool useCamelCaseNaming = true)
        {
            return new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = useCamelCaseNaming ? new CamelCasePropertyNamesContractResolver() :  new DefaultContractResolver(),
                Converters = new List<JsonConverter> { new StringEnumConverter() },
                NullValueHandling = ignoreNull ? NullValueHandling.Ignore : NullValueHandling.Include,
                ReferenceLoopHandling = referenceLoopHandling
            };
        }
    }
}