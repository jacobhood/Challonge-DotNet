using Challonge.Exceptions;
using Challonge.Extensions;
using Challonge.Helpers;
using Challonge.Objects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using System.Threading.Tasks;

namespace Challonge.Api
{
    /// <inheritdoc cref="IChallongeClient"/>
    public class ChallongeClient : IChallongeClient
    {
        private readonly HttpClient _client;

        public ChallongeClient(IChallongeCredentials credentials, HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.challonge.com/v1/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        $"{credentials.Username}:{credentials.ApiKey}")));
            _client = client;
        }

        private async Task<TReturn> SendRequestAsync<TReturn>(string relativeUrl, HttpMethod method,
            IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            using HttpRequestMessage request = RequestBuilder.BuildRequest(relativeUrl, method, parameters);
            using HttpResponseMessage response = await _client.SendAsync(request);

            string responseText = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TReturn>(responseText);
            }
            else
            {
                throw new ChallongeException(responseText, response.StatusCode);
            }
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync(TournamentState? state = null,
            TournamentType? type = null, DateTime? createdAfter = null, DateTime? createdBefore = null,
            string subdomain = null)
        {
            Dictionary<string, object> parameters = new()
            {
                { "state", state?.GetEnumMemberValue() },
                { "type", type?.GetEnumMemberValue() },
                { "created_after", createdAfter?.ToString("yyyy-MM-dd") },
                { "created_before", createdBefore?.ToString("yyyy-MM-dd") },
                { "subdomain", subdomain }
            };

            IEnumerable<TournamentWrapper> wrappers = await SendRequestAsync<IEnumerable<TournamentWrapper>>(
                "tournaments.json", HttpMethod.Get, parameters);

            return wrappers.Select(w => w.Item);
        }

        public async Task<Tournament> CreateTournamentAsync(TournamentInfo tournamentInfo, bool ignoreNulls = true)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                "tournaments.json", HttpMethod.Post, tournamentInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task<Tournament> GetTournamentByUrlAsync(string url)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{url}.json", HttpMethod.Get);

            return wrapper.Item;
        }

        public Task<Tournament> GetTournamentByIdAsync(long id)
        {
            return GetTournamentByUrlAsync(id.ToString());
        }

        public async Task<Tournament> UpdateTournamentAsync(Tournament tournament, TournamentInfo tournamentInfo, 
            bool ignoreNulls = true)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}.json", HttpMethod.Put,
                tournamentInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task DeleteTournamentAsync(Tournament tournament)
        {
            await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}.json", HttpMethod.Delete);
        }

        public async Task<Tournament> ProcessTournamentCheckInsAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/process_check_ins.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Tournament> AbortTournamentCheckInAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/abort_check_in.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Tournament> StartTournamentAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/start.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Tournament> FinalizeTournamentAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/finalize.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Tournament> ResetTournamentAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/reset.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Tournament> OpenTournamentForPredictionsAsync(Tournament tournament)
        {
            TournamentWrapper wrapper = await SendRequestAsync<TournamentWrapper>(
                $"tournaments/{tournament.Id}/open_for_predictions.json", HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<IEnumerable<Participant>> GetParticipantsAsync(Tournament tournament)
        {
            IEnumerable<ParticipantWrapper> wrappers = await SendRequestAsync<IEnumerable<ParticipantWrapper>>(
                $"tournaments/{tournament.Id}/participants.json", HttpMethod.Get);

            return wrappers.Select(w => w.Item);
        }

        public async Task<Participant> CreateParticipantAsync(Tournament tournament, ParticipantInfo participantInfo, 
            bool ignoreNulls = true)
        {
            ParticipantWrapper wrapper = await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{tournament.Id}/participants.json", HttpMethod.Post,
                participantInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task<Participant> GetParticipantAsync(Tournament tournament, long participantId)
        {
            ParticipantWrapper wrapper = await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{tournament.Id}/participants/{participantId}.json", HttpMethod.Get);

            return wrapper.Item;
        }

        public async Task<Participant> UpdateParticipantAsync(Participant participant, ParticipantInfo participantInfo, 
            bool ignoreNulls = true)
        {
            ParticipantWrapper wrapper = await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{participant.TournamentId}/participants/{participant.Id}.json",
                HttpMethod.Put, participantInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task<Participant> CheckInParticipantAsync(Participant participant)
        {
            ParticipantWrapper wrapper = await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{participant.TournamentId}/participants/{participant.Id}/check_in.json",
                HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Participant> UndoCheckInParticipantAsync(Participant participant)
        {
            ParticipantWrapper wrapper = await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{participant.TournamentId}/participants/{participant.Id}/undo_check_in.json",
                HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task DeleteParticipantAsync(Participant participant)
        {
            await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{participant.TournamentId}/participants/{participant.Id}.json",
                HttpMethod.Delete);
        }

        public async Task ClearParticipantsAsync(Tournament tournament)
        {
            await SendRequestAsync<ParticipantWrapper>(
                $"tournaments/{tournament.Id}/participants/clear.json",
                HttpMethod.Delete);
        }

        public async Task<IEnumerable<Participant>> RandomizeParticipantsAsync(Tournament tournament)
        {
            IEnumerable<ParticipantWrapper> wrappers = await SendRequestAsync<IEnumerable<ParticipantWrapper>>(
                $"tournaments/{tournament.Id}/participants/randomize.json",
                HttpMethod.Post);

            return wrappers.Select(w => w.Item);
        }

        public async Task<IEnumerable<Match>> GetMatchesAsync(Tournament tournament, 
            MatchState matchState = MatchState.All, Participant participant = null)
        {
            Dictionary<string, object> parameters = new()
            {
                { "state", matchState.GetEnumMemberValue() },
                { "participant_id", participant?.Id }
            };

            IEnumerable<MatchWrapper> wrappers = await SendRequestAsync<IEnumerable<MatchWrapper>>(
                $"tournaments/{tournament.Id}/matches.json", HttpMethod.Get, parameters);

            return wrappers.Select(w => w.Item);
        }

        public async Task<Match> GetMatchAsync(Tournament tournament, long matchId)
        {
            MatchWrapper wrapper = await SendRequestAsync<MatchWrapper>(
                $"tournaments/{tournament.Id}/matches/{matchId}.json", HttpMethod.Get);

            return wrapper.Item;
        }

        public async Task<Match> UpdateMatchAsync(Match match, MatchInfo matchInfo, bool ignoreNulls = true)
        {
            MatchWrapper wrapper = await SendRequestAsync<MatchWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}.json",
                HttpMethod.Put, matchInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task<Match> ReopenMatchAsync(Match match)
        {
            MatchWrapper wrapper = await SendRequestAsync<MatchWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/reopen.json",
                HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Match> MarkMatchAsUnderwayAsync(Match match)
        {
            MatchWrapper wrapper = await SendRequestAsync<MatchWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/mark_as_underway.json",
                HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<Match> UnmarkMatchAsUnderwayAsync(Match match)
        {
            MatchWrapper wrapper = await SendRequestAsync<MatchWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/unmark_as_underway.json",
                HttpMethod.Post);

            return wrapper.Item;
        }

        public async Task<IEnumerable<MatchAttachment>> GetMatchAttachmentsAsync(Match match)
        {
            IEnumerable<MatchAttachmentWrapper> wrappers = await SendRequestAsync<IEnumerable<MatchAttachmentWrapper>>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/attachments.json", HttpMethod.Get);

            return wrappers.Select(w => w.Item);
        }

        public async Task<MatchAttachment> CreateMatchAttachmentAsync(Match match, 
            MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true)
        {
            MatchAttachmentWrapper wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/attachments.json",
                HttpMethod.Post, matchAttachmentInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task<MatchAttachment> GetMatchAttachmentAsync(Match match, long matchAttachmentId)
        {
            MatchAttachmentWrapper wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachmentId}.json",
                HttpMethod.Get);

            return wrapper.Item;
        }

        public async Task<MatchAttachment> UpdateMatchAttachmentAsync(Match match, MatchAttachment matchAttachment,
            MatchAttachmentInfo matchAttachmentInfo, bool ignoreNulls = true)
        {
            MatchAttachmentWrapper wrapper = await SendRequestAsync<MatchAttachmentWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachment.Id}.json",
                HttpMethod.Put, matchAttachmentInfo.ToDictionary(ignoreNulls));

            return wrapper.Item;
        }

        public async Task DeleteMatchAttachmentAsync(Match match, MatchAttachment matchAttachment)
        {
            await SendRequestAsync<MatchAttachmentWrapper>(
                $"tournaments/{match.TournamentId}/matches/{match.Id}/attachments/{matchAttachment.Id}.json",
                HttpMethod.Delete);
        }
    }
}
