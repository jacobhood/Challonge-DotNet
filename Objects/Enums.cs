using System.Runtime.Serialization;

namespace Challonge.Objects
{
    public enum TournamentState
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "in_progress")]
        InProgress,
        [EnumMember(Value = "underway")]
        Underway,
        [EnumMember(Value = "ended")]
        Ended,
        [EnumMember(Value = "checking_in")]
        CheckingIn,
        [EnumMember(Value = "checked_in")]
        CheckedIn
    }
    public enum TournamentType
    {
        [EnumMember(Value = "single elimination")]
        SingleElimination,
        [EnumMember(Value = "double elimination")]
        DoubleElimination,
        [EnumMember(Value = "round robin")]
        RoundRobin,
        [EnumMember(Value = "swiss")]
        Swiss,
        [EnumMember(Value = "free for all")]
        FreeForAll
    }
    public enum RankingMethod
    {
        [EnumMember(Value = "match wins")]
        MatchWins,
        [EnumMember(Value = "game wins")]
        GameWins,
        [EnumMember(Value = "points scored")]
        PointsScored,
        [EnumMember(Value = "points difference")]
        PointsDifference,
        [EnumMember(Value = "custom")]
        Custom
    }
    public enum PredictionMethod
    {
        Default,
        ExponentialScoring,
        LinearScoring
    }
    public enum GrandFinalsModifier
    {
        [EnumMember(Value = "single match")]
        SingleMatch,
        [EnumMember(Value = "skip")]
        Skip
    }
    public enum MatchState
    {
        [EnumMember(Value = "all")]
        All,
        [EnumMember(Value = "pending")]
        Pending,
        [EnumMember(Value = "open")]
        Open,
        [EnumMember(Value = "complete")]
        Complete
    }
}
