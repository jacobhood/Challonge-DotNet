using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class ChallongeMessage : ChallongeObject
    {
        [JsonProperty("message")]
        internal string Message { get; set; }
    }
}
