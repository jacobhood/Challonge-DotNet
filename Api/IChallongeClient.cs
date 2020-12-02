using Challonge.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challonge.Api
{
    public interface IChallongeClient
    {
        public Task<IEnumerable<Tournament>> GetTournamentsAsync(TournamentState? state = null, TournamentType? type = null,
            DateTime? createdAfter = null, DateTime? createdBefore = null, string subdomain = null);

        public Task<Tournament> CreateTournamentAsync(TournamentInfo tournamentInfo, bool ignoreNulls = true);

        public Task<Tournament> GetTournamentByUrlAsync(string url);

        public Task<Tournament> GetTournamentByIdAsync(long id);

        public Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInfo tournamentInfo, bool ignoreNulls = true);

        public Task DeleteTournamentAsync(Tournament tournament);

        public Task<Tournament> ProcessTournamentCheckInsAsync(Tournament tournament);

        public Task<Tournament> AbortTournamentCheckInAsync(Tournament tournament);

        public Task<Tournament> StartTournamentAsync(Tournament tournament);

        public Task<Tournament> FinalizeTournamentAsync(Tournament tournament);

        public Task<Tournament> ResetTournamentAsync(Tournament tournament);

        public Task<Tournament> OpenTournamentForPredictionsAsync(Tournament tournament);

        public Task<IEnumerable<Participant>> GetParticipantsAsync(Tournament tournament);

        public Task<Participant> CreateParticipantAsync(Tournament tournament, ParticipantInfo participantInfo, bool ignoreNulls = true);

        public Task<Participant> GetParticipantAsync(Tournament tournament, long participantId);

        public Task<Participant> UpdateParticipantAsync(Participant participant, ParticipantInfo participantInfo, bool ignoreNulls = true);

        public Task<Participant> CheckInParticipantAsync(Participant participant);

        public Task<Participant> UndoCheckInParticipantAsync(Participant participant);

        public Task DeleteParticipantAsync(Participant participant);

        public Task ClearParticipantsAsync(Tournament tournament);

        public Task<IEnumerable<Participant>> RandomizeParticipantsAsync(Tournament tournament);

        public Task<IEnumerable<Match>> GetMatchesAsync(Tournament tournament, MatchState matchState = MatchState.All,
            Participant participant = null);

        public Task<Match> GetMatchAsync(Tournament tournament, long matchId);

        public Task<Match> UpdateMatchAsync(Match match, MatchInfo matchInfo, bool ignoreNulls = true);

        public Task<Match> ReopenMatchAsync(Match match);

        public Task<Match> MarkMatchAsUnderwayAsync(Match match);

        public Task<Match> UnmarkMatchAsUnderwayAsync(Match match);

        public Task<IEnumerable<MatchAttachment>> GetMatchAttachmentsAsync(Match match);

        public Task<MatchAttachment> CreateMatchAttachmentAsync(Match match, MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true);

        public Task<MatchAttachment> GetMatchAttachmentAsync(Match match, long matchAttachmentId);

        public Task<MatchAttachment> UpdateMatchAttachmentAsync(Match match,
            MatchAttachment matchAttachment, MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true);

        public Task DeleteMatchAttachmentAsync(Match match, MatchAttachment matchAttachment);
    }
}
