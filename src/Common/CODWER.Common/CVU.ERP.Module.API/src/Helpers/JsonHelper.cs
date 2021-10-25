using CVU.ERP.Module.API.Extensions.Json;
using CVU.ERP.Module.API.Settings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CVU.ERP.Module.API.Helpers {
    public class JsonHelper {
        public static JsonSerializerSettings GetJsonSettings (bool ignoreNull = true, ReferenceLoopHandling referenceLoopHandling = ReferenceLoopHandling.Ignore, bool useCamelCaseNaming = true) {
            return new CamelCaseContractResolverJsonSettings ().GetJSONSettings (ignoreNull, referenceLoopHandling, useCamelCaseNaming);
        }

        public static JToken RemoveEmptyChildren (JToken token) {
            if (token.Type == JTokenType.Object) {
                JObject copy = new JObject ();
                foreach (JProperty prop in token.Children<JProperty> ()) {
                    JToken child = prop.Value;
                    if (child.HasValues) {
                        child = RemoveEmptyChildren (child);
                    }

                    if (!child.IsNullOrEmpty ()) {
                        copy.Add (prop.Name, child);
                    }
                }
                return copy;
            } else if (token.Type == JTokenType.Array) {
                JArray copy = new JArray ();
                foreach (JToken item in token.Children ()) {
                    JToken child = item;
                    if (child.HasValues) {
                        child = RemoveEmptyChildren (child);
                    }

                    if (!child.IsNullOrEmpty ()) {
                        copy.Add (child);
                    }
                }
                return copy;
            }
            return token;
        }

    }
}