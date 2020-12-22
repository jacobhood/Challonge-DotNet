using Challonge.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challonge.Api
{
    /// <summary>
    /// Contains all methods for interacting with the Challonge API.
    /// </summary>
    public interface IChallongeClient
    {
        /// <summary>
        /// Retrieves all tournaments created with your account that meet the specified criteria.
        /// </summary>
        /// <param name="state">The state of tournaments to retrieve.</param>
        /// <param name="type">The type of tournaments to retrieve.</param>
        /// <param name="createdAfter">The date after which the tournaments to retrieve have been created.</param>
        /// <param name="createdBefore">The date before which the tournaments to retrieve have been created.</param>
        /// <param name="subdomain">The subdomain to which the tournaments to retrieve have been published.</param>
        /// <returns>A task representing the tournaments.</returns>
        public Task<IEnumerable<Tournament>> GetTournamentsAsync(TournamentState? state = null, TournamentType? type = null,
            DateTime? createdAfter = null, DateTime? createdBefore = null, string subdomain = null);

        /// <summary>
        /// Creates a tournament.
        /// </summary>
        /// <param name="tournamentInfo">The details of the new tournament.</param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of tournamentInfo should be sent to Challonge.</param>
        /// <returns>A task representing the new tournament.</returns>
        public Task<Tournament> CreateTournamentAsync(TournamentInfo tournamentInfo, bool ignoreNulls = true);

        /// <summary>
        /// Gets a tournament given its url.
        /// </summary>
        /// <param name="url">The tournament's URL.</param>
        /// <returns>A task representing the tournament.</returns>
        public Task<Tournament> GetTournamentByUrlAsync(string url);

        /// <summary>
        /// Gets a tournament given its ID.
        /// </summary>
        /// <param name="id">The tournament's ID.</param>
        /// <returns>A task representing the tournament.</returns>
        public Task<Tournament> GetTournamentByIdAsync(long id);

        /// <summary>
        /// Updates an existing tournament.
        /// </summary>
        /// <param name="tournament">The tournament to update.</param>
        /// <param name="tournamentInfo">The new details of the tournament.</param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of tournamentInfo should be sent to Challonge.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInfo tournamentInfo, bool ignoreNulls = true);

        /// <summary>
        /// Deletes a tournament.
        /// </summary>
        /// <param name="tournament">The tournament to delete.</param>
        /// <returns>An empty task.</returns>
        public Task DeleteTournamentAsync(Tournament tournament);

        /// <summary>
        /// Processes a tournament's check-ins:
        /// <list type="number">
        /// <item>Marks participants who have not checked in as inactive.</item>
        /// <item>Moves inactive participants to bottom seeds, ordered by original seed.</item>
        /// <item>Transitions the tournament state from "checking_in" to "checked_in."</item>
        /// </list>
        /// </summary>
        /// <param name="tournament">The tournament for which to process check-ins.</param>
        /// <returns>A task representing the the updated tournament.</returns>
        public Task<Tournament> ProcessTournamentCheckInsAsync(Tournament tournament);

        /// <summary>
        /// Aborts a tournament's check-in:
        /// <list type="number">
        /// <item>Makes all participants active and clears their "checked_in_at" times.</item>
        /// <item>Transitions the tournament state from "checking_in" or "checked_in" to "pending."</item>
        /// </list>
        /// </summary>
        /// <param name="tournament">The tournament for which to cancel check-in.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> AbortTournamentCheckInAsync(Tournament tournament);

        /// <summary>
        /// Starts a tournament.
        /// </summary>
        /// <param name="tournament">The tournament to start.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> StartTournamentAsync(Tournament tournament);

        /// <summary>
        /// Finalizes a tournament's results.
        /// </summary>
        /// <param name="tournament">The tournament to finalize.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> FinalizeTournamentAsync(Tournament tournament);

        /// <summary>
        /// Reverts a tournament back to its original state, clearing all scores and attachments.
        /// </summary>
        /// <param name="tournament">The tournament to reset.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> ResetTournamentAsync(Tournament tournament);

        /// <summary>
        /// Allows a tournament to start accepting predictions.
        /// </summary>
        /// <param name="tournament">The tournament to open for predictions.</param>
        /// <returns>A task representing the updated tournament.</returns>
        public Task<Tournament> OpenTournamentForPredictionsAsync(Tournament tournament);

        /// <summary>
        /// Gets all of a tournament's participants.
        /// </summary>
        /// <param name="tournament">The tournament whose participants are to be retrieved.</param>
        /// <returns>A task representing the tournament's participants.</returns>
        public Task<IEnumerable<Participant>> GetParticipantsAsync(Tournament tournament);

        /// <summary>
        /// Adds a new participant to a tournament.
        /// </summary>
        /// <param name="tournament">The tournament to which to add the new participant.</param>
        /// <param name="participantInfo">The details of the new participant.</param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of participantInfo should be sent to Challonge.</param>
        /// <returns>A task representing the new participant.</returns>
        public Task<Participant> CreateParticipantAsync(Tournament tournament, ParticipantInfo participantInfo, bool ignoreNulls = true);

        /// <summary>
        /// Gets a participant.
        /// </summary>
        /// <param name="tournament">The participant's tournament.</param>
        /// <param name="participantId">The participant's ID.</param>
        /// <returns>A task representing the participant.</returns>
        public Task<Participant> GetParticipantAsync(Tournament tournament, long participantId);

        /// <summary>
        /// Updates an existing participant.
        /// </summary>
        /// <param name="participant">The participant to update.</param>
        /// <param name="participantInfo">The new details of the participant.</param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of participantInfo should be sent to Challonge.</param>
        /// <returns>A task representing the updated participant.</returns>
        public Task<Participant> UpdateParticipantAsync(Participant participant, ParticipantInfo participantInfo, bool ignoreNulls = true);

        /// <summary>
        /// Checks a participant in.
        /// </summary>
        /// <param name="participant">The participant to check in.</param>
        /// <returns>A task representing the updated participant.</returns>
        public Task<Participant> CheckInParticipantAsync(Participant participant);

        /// <summary>
        /// Undoes a participant's check-in.
        /// </summary>
        /// <param name="participant">The participant for whom to undo check-in.</param>
        /// <returns>A task representing the updated participant.</returns>
        public Task<Participant> UndoCheckInParticipantAsync(Participant participant);

        /// <summary>
        /// Removes a participant from their tournament if that tournament has not yet started,
        /// otherwise marks the participant as inactive and forfeits their remaining matches.
        /// </summary>
        /// <param name="participant">The participant to remove.</param>
        /// <returns>An empty task.</returns>
        public Task DeleteParticipantAsync(Participant participant);

        /// <summary>
        /// Deletes all participants of a tournament if the tournament has not yet started.
        /// </summary>
        /// <param name="tournament">The tournament whose participants are to be deleted.</param>
        /// <returns>An empty task.</returns>
        public Task ClearParticipantsAsync(Tournament tournament);

        /// <summary>
        /// Randomizes a tournament's seeds if it has not yet started.
        /// </summary>
        /// <param name="tournament">The tournament for which to randomize seeds.</param>
        /// <returns>A task representing the updated participants.</returns>
        public Task<IEnumerable<Participant>> RandomizeParticipantsAsync(Tournament tournament);

        /// <summary>
        /// Gets all of a tournament's matches that meet the specified criteria.
        /// </summary>
        /// <param name="tournament">The tournament whose matches are to be retrieved.</param>
        /// <param name="matchState">The state of matches to retrieve.</param>
        /// <param name="participant">The participant whose matches are to be retrieved.</param>
        /// <returns>A task representing the tournament's matches.</returns>
        public Task<IEnumerable<Match>> GetMatchesAsync(Tournament tournament, MatchState matchState = MatchState.All,
            Participant participant = null);

        /// <summary>
        /// Gets a match.
        /// </summary>
        /// <param name="tournament">The match's tournament.</param>
        /// <param name="matchId">The match's ID.</param>
        /// <returns>A task representing the match.</returns>
        public Task<Match> GetMatchAsync(Tournament tournament, long matchId);

        /// <summary>
        /// Updates a match.
        /// </summary>
        /// <param name="match">The match to update.</param>
        /// <param name="matchInfo">The new details of the match.param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of matchInfo should be sent to Challonge.</param>
        /// <returns>A task representing the updated match.</returns>
        public Task<Match> UpdateMatchAsync(Match match, MatchInfo matchInfo, bool ignoreNulls = true);

        /// <summary>
        /// Reopens an already completed match, resetting matches that follow.
        /// </summary>
        /// <param name="match">The match to reopen.</param>
        /// <returns>A task representing the updated match.</returns>
        public Task<Match> ReopenMatchAsync(Match match);

        /// <summary>
        /// Sets "underway_at" to the current date/time and highlights the match in the bracket.
        /// </summary>
        /// <param name="match">The match to mark as underway.</param>
        /// <returns>A task representing the updated match.</returns>
        public Task<Match> MarkMatchAsUnderwayAsync(Match match);

        /// <summary>
        /// Clears "underway_at" and unhighlights the match in the bracket.
        /// </summary>
        /// <param name="match">The match to unmark as underway.</param>
        /// <returns>A task representing the updated match.</returns>
        public Task<Match> UnmarkMatchAsUnderwayAsync(Match match);

        /// <summary>
        /// Gets all of a match's attachments.
        /// </summary>
        /// <param name="match">The match whose attachments are to be retrieved.</param>
        /// <returns>A task representing the match's attachments.</returns>
        public Task<IEnumerable<MatchAttachment>> GetMatchAttachmentsAsync(Match match);

        /// <summary>
        /// Creates a match attachment.
        /// </summary>
        /// <param name="match">The match to which to add the new attachment.</param>
        /// <param name="matchAttachmentInfo">The details of the new attachment.</param>
        /// <param name="ignoreNulls">Indicates wheter null-valued properties of matchAttachmentInfo should be sent to Challonge.</param>
        /// <returns>A task representing the new attachment.</returns>
        public Task<MatchAttachment> CreateMatchAttachmentAsync(Match match, MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true);

        /// <summary>
        /// Gets a match attachment.
        /// </summary>
        /// <param name="match">The attachment's match.</param>
        /// <param name="matchAttachmentId">The attachment's ID.</param>
        /// <returns>A task representing the attachment.</returns>
        public Task<MatchAttachment> GetMatchAttachmentAsync(Match match, long matchAttachmentId);

        /// <summary>
        /// Updates an existing match attachment.
        /// </summary>
        /// <param name="match">The attachment's match.</param>
        /// <param name="matchAttachment">The attachment to update.</param>
        /// <param name="matchAttachmentInfo">The new details of the attachment.</param>
        /// <param name="ignoreNulls">Indicates whether null-valued properties of matchAttachmentInfo should be sent to Challonge.</param>
        /// <returns>A task representing the updated attachment..</returns>
        public Task<MatchAttachment> UpdateMatchAttachmentAsync(Match match,
            MatchAttachment matchAttachment, MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true);

        /// <summary>
        /// Deletes a match attachment.
        /// </summary>
        /// <param name="match">The attachment's match.</param>
        /// <param name="matchAttachment">The attachment to delete.</param>
        /// <returns>An empty task.</returns>
        public Task DeleteMatchAttachmentAsync(Match match, MatchAttachment matchAttachment);
    }
}
