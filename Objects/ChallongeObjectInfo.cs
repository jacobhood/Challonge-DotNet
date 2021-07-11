using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Challonge.Objects
{
    public abstract class ChallongeObjectInfo
    {
        internal abstract bool Validate();
        internal abstract Dictionary<string, object> ToDictionary(bool ignoreNulls);
        private protected Dictionary<string, object> ToDictionaryWithKeyPrefix(string prefix, bool ignoreNulls)
        {
            JsonSerializerSettings settings = new();
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

            if (ignoreNulls)
            {
                settings.NullValueHandling = NullValueHandling.Ignore;
            }

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(
                JsonConvert.SerializeObject(this, settings))
                .ToDictionary(kv => $"{prefix}[{kv.Key}]", kv => kv.Value);
        }
    }
}