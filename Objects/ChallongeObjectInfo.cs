using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Challonge.Objects
{
    public abstract class ChallongeObjectInfo
    {
        internal abstract Dictionary<string, object> GetCreateOrUpdateDictionary();

        private protected Dictionary<string, object> GetCreateOrUpdateDictionary(string prefix)
        {
            JsonSerializerSettings settings = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(
                JsonConvert.SerializeObject(this, settings))
                .ToDictionary(kv => $"{prefix}[{kv.Key}]", kv => kv.Value);
        }
    }
}