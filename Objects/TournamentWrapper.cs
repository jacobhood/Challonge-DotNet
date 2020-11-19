using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class TournamentWrapper : ChallongeObjectWrapper<Tournament>
    {
        [JsonProperty("tournament")]
        internal override Tournament Item { get; set; }

    }
}
