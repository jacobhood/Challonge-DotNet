using Challonge.JsonConverters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class MatchInfo : ChallongeObjectInfo
    {
        [JsonProperty("scores_csv")]
        [JsonConverter(typeof(ScoresJsonConverter))]
        public IEnumerable<Score> Scores { get; set; }

        [JsonProperty("winner_id")]
        public long? WinnerId { get; set; }

        [JsonProperty("player1_votes")]
        public int? PlayerOneVotes { get; set; }

        [JsonProperty("player2_votes")]
        public int? PlayerTwoVotes { get; set; }

        [JsonIgnore]
        public bool ResultIsTie { get; }

        public MatchInfo(bool resultIsTie = false)
        {
            ResultIsTie = resultIsTie;
        }

        internal override Dictionary<string, object> ToDictionary(bool ignoreNulls)
        {
            Dictionary<string, object> dictionary = ToDictionaryWithKeyPrefix("match", ignoreNulls);

            if (ResultIsTie)
            {
                dictionary["match[winner_id]"] = "tie";
            }

            return dictionary;
        }
    }
}
