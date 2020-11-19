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

        public MatchAttachmentInfo(MatchAttachmentAsset asset, string description = null)
        {
            Asset = asset;
            Description = description;
        }

        public MatchAttachmentInfo(string url, string description = null)
        {
            Url = url;
            Description = description;
        }

        internal override Dictionary<string, object> GetCreateOrUpdateDictionary()
        {
            Dictionary<string, object> createOrUpdateDict = GetCreateOrUpdateDictionary("match_attachment");
            createOrUpdateDict["match_attachment[asset]"] = Asset;

            return createOrUpdateDict;
        }
    }
}
