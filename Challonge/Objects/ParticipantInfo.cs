﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class ParticipantInfo : ChallongeObjectInfo
    {

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("challonge_username")]
        public string ChallongeUsername { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("seed")]
        public int? Seed { get; set; }

        [JsonProperty("misc")]
        public string Misc { get; set; }

        internal override Dictionary<string, object> ToDictionary(bool ignoreNulls)
        {
            return ToDictionaryWithKeyPrefix("participant", ignoreNulls);
        }
    }
}
