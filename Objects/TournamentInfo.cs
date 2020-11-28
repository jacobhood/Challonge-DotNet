using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class TournamentInfo : ChallongeObjectInfo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tournament_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TournamentType? TournamentType { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("open_signup")]
        public bool? OpenSignup { get; set; }

        [JsonProperty("hold_third_place_match")]
        public bool? HoldThirdPlaceMatch { get; set; }

        [JsonProperty("pts_for_match_win")]
        public double? PtsForMatchWin { get; set; }

        [JsonProperty("pts_for_match_tie")]
        public double? PtsForMatchTie { get; set; }

        [JsonProperty("pts_for_game_win")]
        public double? PtsForGameWin { get; set; }

        [JsonProperty("pts_for_game_tie")]
        public double? PtsForGameTie { get; set; }

        [JsonProperty("pts_for_bye")]
        public double? PtsForBye { get; set; }

        [JsonProperty("swiss_rounds")]
        public int? SwissRounds { get; set; }

        [JsonProperty("ranked_by")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RankingMethod? RankedBy { get; set; }

        [JsonProperty("rr_pts_for_match_win")]
        public double? RRPtsForMatchWin { get; set; }

        [JsonProperty("rr_pts_for_match_tie")]
        public double? RRPtsForMatchTie { get; set; }
         
        [JsonProperty("rr_pts_for_game_win")]
        public double? RRPtsForGameWin { get; set; }

        [JsonProperty("rr_pts_for_game_tie")]
        public double? RRPtsForGameTie { get; set; }

        [JsonProperty("accept_attachments")]
        public bool? AcceptAttachments { get; set; }

        [JsonProperty("hide_forum")]
        public bool? HideForum { get; set; }

        [JsonProperty("show_rounds")]
        public bool? ShowRounds { get; set; }

        [JsonProperty("private")]
        public bool? Private { get; set; }

        [JsonProperty("notify_users_when_matches_open")]
        public bool? NotifyUsersWhenMatchesOpen { get; set; }

        [JsonProperty("notify_users_when_the_tournament_ends")]
        public bool? NotifyUsersWhenTheTournamentEnds { get; set; }

        [JsonProperty("sequential_pairings")]
        public bool? SequentialPairings { get; set; }

        [JsonProperty("signup_cap")]
        public int? SignupCap { get; set; }

        [JsonProperty("start_at")]
        public DateTime? StartAt { get; set; }

        [JsonProperty("check_in_duration")]
        public int? CheckInDuration { get; set; }

        [JsonProperty("grand_finals_modifier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GrandFinalsModifier? GrandFinalsModifier { get; set; }

        public TournamentInfo(string name)
        {
            Name = name;
        }

        internal override Dictionary<string, object> ToDictionary(bool ignoreNulls)
        {
            return BuildDictionary("tournament", ignoreNulls);
        }
    }
}
