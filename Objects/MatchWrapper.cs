using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class MatchWrapper : ChallongeObjectWrapper<Match>
    {
        [JsonProperty("match")]
        internal override Match Item { get; set; }
    }
}
