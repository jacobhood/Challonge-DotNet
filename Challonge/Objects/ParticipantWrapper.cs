using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class ParticipantWrapper : ChallongeObjectWrapper<Participant>
    {
        [JsonProperty("participant")]
        internal override Participant Item { get; set; }
    }
}
