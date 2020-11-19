using Newtonsoft.Json;

namespace Challonge.Objects
{
    internal class MatchAttachmentWrapper : ChallongeObjectWrapper<MatchAttachment>
    {
        [JsonProperty("match_attachment")]
        internal override MatchAttachment Item { get; set; }
    }
}
