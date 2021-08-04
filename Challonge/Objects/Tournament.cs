using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class Tournament : ChallongeObject
    {
        public class NonEliminationData
        {
            [JsonProperty("current_round")]
            public int? CurrentRound { get; private set; }
            
            [JsonProperty("participants_per_match")]
            public int? ParticipantsPerMatch { get; private set; }
            
            internal NonEliminationData() { }
        }
        
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("url")]
        public string Url { get; private set; }

        [JsonProperty("description")]   
        public string Description { get; private set; }

        [JsonProperty("tournament_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TournamentType TournamentType { get; private set; }

        [JsonProperty("started_at")]
        public DateTime? StartedAt { get; private set; }

        [JsonProperty("completed_at")]
        public DateTime? CompletedAt { get; private set; }

        [JsonProperty("require_score_agreement")]
        public bool RequireScoreAgreement { get; private set; }

        [JsonProperty("notify_users_when_matches_open")]
        public bool NotifyUsersWhenMatchesOpen { get; private set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TournamentState State { get; private set; }

        [JsonProperty("open_signup")]
        public bool OpenSignup { get; private set; }

        [JsonProperty("notify_users_when_the_tournament_ends")]
        public bool NotifyUsersWhenTheTournamentEnds { get; private set; }

        [JsonProperty("progress_meter")]
        public int ProgressMeter { get; private set; }

        [JsonProperty("quick_advance")]
        public bool QuickAdvance { get; private set; }

        [JsonProperty("hold_third_place_match")]
        public bool HoldThirdPlaceMatch { get; private set; }

        [JsonProperty("pts_for_game_win")]
        public double PtsForGameWin { get; private set; }

        [JsonProperty("pts_for_game_tie")]
        public double PtsForGameTie { get; private set; }

        [JsonProperty("pts_for_match_win")]
        public double PtsForMatchWin { get; private set; }

        [JsonProperty("pts_for_match_tie")]
        public double PtsForMatchTie { get; private set; }

        [JsonProperty("pts_for_bye")]
        public double PtsForBye { get; private set; }

        [JsonProperty("swiss_rounds")]
        public int SwissRounds { get; private set; }

        [JsonProperty("private")]
        public bool Private { get; private set; }

        [JsonProperty("ranked_by")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RankingMethod? RankedBy { get; private set; }

        [JsonProperty("show_rounds")]
        public bool ShowRounds { get; private set; }

        [JsonProperty("hide_forum")]
        public bool HideForum { get; private set; }

        [JsonProperty("sequential_pairings")]
        public bool SequentialPairings { get; private set; }

        [JsonProperty("accept_attachments")]
        public bool AcceptAttachments { get; private set; }

        [JsonProperty("rr_pts_for_game_win")]
        public double RRPtsForGameWin { get; private set; }

        [JsonProperty("rr_pts_for_game_tie")]
        public double RRPtsForGameTie { get; private set; }

        [JsonProperty("rr_pts_for_match_win")]
        public double RRPtsForMatchWin { get; private set; }

        [JsonProperty("rr_pts_for_match_tie")]
        public double RRPtsForMatchTie { get; private set; }

        [JsonProperty("created_by_api")]
        public bool CreatedByApi { get; private set; }

        [JsonProperty("credit_capped")]
        public bool CreditCapped { get; private set; }

        [JsonProperty("category")]
        public string Category { get; private set; }

        [JsonProperty("hide_seeds")]
        public bool HideSeeds { get; private set; }

        [JsonProperty("prediction_method")]
        public PredictionMethod PredictionMethod { get; private set; }

        [JsonProperty("predictions_opened_at")]
        public DateTime? PredictionsOpenedAt { get; private set; }

        [JsonProperty("anonymous_voting")]
        public bool AnonymousVoting { get; private set; }

        [JsonProperty("max_predictions_per_user")]
        public int MaxPredictionsPerUser { get; private set; }

        [JsonProperty("signup_cap")]
        public int? SignupCap { get; private set; }

        [JsonProperty("game_id")]
        public long? GameId { get; private set; }

        [JsonProperty("participants_count")]
        public int ParticipantsCount { get; private set; }

        [JsonProperty("group_stages_enabled")]
        public bool GroupStagesEnabled { get; private set; }

        [JsonProperty("allow_participant_match_reporting")]
        public bool AllowParticipantMatchReporting { get; private set; }

        [JsonProperty("teams")]
        public bool? Teams { get; private set; }

        [JsonProperty("check_in_duration")]
        public int? CheckInDuration { get; private set; }

        [JsonProperty("start_at")]
        public DateTime? StartAt { get; private set; }

        [JsonProperty("started_checking_in_at")]
        public DateTime? StartedCheckingInAt { get; private set; }

        [JsonProperty("tie_breaks")]
        public IEnumerable<string> TieBreaks { get; private set; }

        [JsonProperty("locked_at")]
        public DateTime? LockedAt { get; private set; }

        [JsonProperty("event_id")]
        public long? EventId { get; private set; }

        [JsonProperty("public_predictions_before_start_time")]
        public bool? PublicPredictionsBeforeStartTime { get; private set; }

        [JsonProperty("ranked")]
        public bool Ranked { get; private set; }

        [JsonProperty("grand_finals_modifier")]
        [JsonConverter(typeof(StringEnumConverter))]
        public GrandFinalsModifier? GrandFinalsModifier { get; private set; }

        [JsonProperty("predict_the_losers_bracket")]
        public bool? PredictTheLosersBracket { get; private set; }

        [JsonProperty("spam")]
        public string Spam { get; private set; }

        [JsonProperty("ham")]
        public string Ham { get; private set; }

        [JsonProperty("rr_iterations")]
        public int? RRIterations { get; private set; }

        [JsonProperty("tournament_registration_id")]
        public long? TournamentRegistrationId { get; private set; }

        [JsonProperty("donation_contest_enabled")]
        public bool? DonationContestEnabled { get; private set; }

        [JsonProperty("mandatory_donation")]
        public bool? MandatoryDonation { get; private set; }

        [JsonProperty("non_elimination_tournament_data")]
        public NonEliminationData NonEliminationTournamentData { get; private set; }

        [JsonProperty("auto_assign_stations")]
        public bool? AutoAssignStations { get; private set; }

        [JsonProperty("only_start_matches_with_stations")]
        public bool? OnlyStartMatchesWithStations { get; private set; }

        [JsonProperty("registration_fee")]
        public double RegistrationFee { get; private set; }

        [JsonProperty("registration_type")]
        public string RegistrationType { get; private set; }

        [JsonProperty("split_participants")]
        public bool SplitParticipants { get; private set; }

        [JsonProperty("allowed_regions")]
        public IEnumerable<string> AllowedRegions { get; private set; }

        [JsonProperty("show_participant_country")]
        public bool? ShowParticipantCountry { get; private set; }

        [JsonProperty("description_source")]
        public string DescriptionSource { get; private set; }

        [JsonProperty("subdomain")]
        public string Subdomain { get; private set; }

        [JsonProperty("full_challonge_url")]
        public string FullChallongeUrl { get; private set; }

        [JsonProperty("live_image_url")]
        public string LiveImageUrl { get; private set; }

        [JsonProperty("sign_up_url")]
        public string SignUpUrl { get; private set; }

        [JsonProperty("review_before_finalizing")]
        public bool ReviewBeforeFinalizing { get; private set; }

        [JsonProperty("accepting_predictions")]
        public bool AcceptingPredictions { get; private set; }

        [JsonProperty("participants_locked")]
        public bool ParticipantsLocked { get; private set; }

        [JsonProperty("game_name")]
        public string GameName { get; private set; }

        [JsonProperty("participants_swappable")]
        public bool ParticipantsSwappable { get; private set; }

        [JsonProperty("team_convertable")]
        public bool TeamConvertable { get; private set; }

        [JsonProperty("group_stages_were_started")]
        public bool GroupStagesWereStarted { get; private set; }

        internal Tournament() { }
    }
}