using Newtonsoft.Json;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class MatchAttachmentInfo : ChallongeObjectInfo
    {
        [JsonIgnore]
        public MatchAttachmentAsset Asset { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        public MatchAttachmentInfo(MatchAttachmentAsset asset)
        {
            Asset = asset;
        }

        public MatchAttachmentInfo(string url)
        {
            Url = url;
        }

        internal override Dictionary<string, object> ToDictionary(bool ignoreNulls)
        {
            Dictionary<string, object> dictionary = BuildDictionary("match_attachment", ignoreNulls);
            dictionary["match_attachment[asset]"] = Asset;

            return dictionary;
        }
    }
}
