using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Challonge.Objects
{
    public abstract class ChallongeObjectInfo
    {
        internal abstract Dictionary<string, object> ToDictionary();

        private protected Dictionary<string, object> ToDictionary(string prefix)
        {
            JsonSerializerSettings settings = new()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local,
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(
                JsonConvert.SerializeObject(this, settings))
                .ToDictionary(kv => $"{prefix}[{kv.Key}]", kv => kv.Value);
        }
    }
}