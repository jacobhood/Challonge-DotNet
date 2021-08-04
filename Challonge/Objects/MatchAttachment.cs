using Newtonsoft.Json;
using System;

namespace Challonge.Objects
{
    public class MatchAttachment : ChallongeObject
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("match_id")]
        public long MatchId { get; private set; }

        [JsonProperty("user_id")]
        public long UserId { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("original_file_name")]
        public string OriginalFileName { get; private set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [JsonProperty("asset_file_name")]
        public string AssetFileName { get; private set; }

        [JsonProperty("asset_content_type")]
        public string AssetContentType { get; private set; }

        [JsonProperty("asset_file_size")]
        public long AssetFileSize { get; private set; }

        [JsonProperty("asset_url")]
        public string AssetUrl { get; private set; }

        internal MatchAttachment() { }
    }
}
