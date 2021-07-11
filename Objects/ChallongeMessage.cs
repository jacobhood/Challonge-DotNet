using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class ChallongeMessage
    {
        [JsonProperty("message")]
        internal string Message { get; set; }
    }
}
