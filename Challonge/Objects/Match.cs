using Challonge.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class Match : ChallongeObject
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("tournament_id")]
        public long TournamentId { get; private set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MatchState State { get; private set; }

        [JsonProperty("player1_id")]
        public long? Player1Id { get; private set; }

        [JsonProperty("player2_id")]
        public long? Player2Id { get; private set; }

        [JsonProperty("player1_prereq_match_id")]
        public long? Player1PrereqMatchId { get; private set; }

        [JsonProperty("player2_prereq_match_id")]
        public long? Player2PrereqMatchId { get; private set; }

        [JsonProperty("player1_is_prereq_match_loser")]
        public bool Player1IsPrereqMatchLoser { get; private set; }

        [JsonProperty("player2_is_prereq_match_loser")]
        public bool Player2IsPrereqMatchLoser { get; private set; }

        [JsonProperty("winner_id")]
        public long? WinnerId { get; private set; }

        [JsonProperty("loser_id")]
        public long? LoserId { get; private set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; private set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [JsonProperty("identifier")]
        public string Identifier { get; private set; }

        [JsonProperty("has_attachment")]
        public bool HasAttachment { get; private set; }

        [JsonProperty("round")]
        public int Round { get; private set; }

        [JsonProperty("player1_votes")]
        public int? Player1Votes { get; private set; }

        [JsonProperty("player2_votes")]
        public int? Player2Votes { get; private set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; private set; }

        [JsonProperty("attachment_count")]
        public int? AttachmentCount { get; private set; }

        [JsonProperty("scheduled_time")]
        public DateTime? ScheduledTime { get; private set; }

        [JsonProperty("location")]
        public string Location { get; private set; }

        [JsonProperty("underway_at")]
        public DateTime? UnderwayAt { get; private set; }

        [JsonProperty("optional")]
        public bool? Optional { get; private set; }

        [JsonProperty("rushb_id")]
        public long? RushbId { get; private set; }

        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; private set; }

        [JsonProperty("suggested_play_order")]
        public int? SuggestedPlayOrder { get; private set; }

        [JsonProperty("forfeited")]
        public bool? Forfeited { get; private set; }

        [JsonProperty("open_graph_image_file_name")]
        public string OpenGraphImageFileName { get; private set; }

        [JsonProperty("open_graph_image_content_type")]
        public string OpenGraphImageContentType { get; private set; }

        [JsonProperty("open_graph_image_file_size")]
        public double? OpenGraphImageFileSize { get; private set; }

        [JsonProperty("prerequisite_match_ids_csv")]
        [JsonConverter(typeof(PrerequisiteMatchIdsJsonConverter))]
        public IEnumerable<long> PrerequisiteMatchIds { get; private set; }

        [JsonProperty("scores_csv")]
        [JsonConverter(typeof(ScoresJsonConverter))]
        public IEnumerable<Score> Scores { get; private set; }

        internal Match() { }
    }
}
