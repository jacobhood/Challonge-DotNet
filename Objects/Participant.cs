using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Challonge.Objects
{
    public class Participant : ChallongeObject
    {
        [JsonProperty("id")]
        public long Id { get; private set; }

        [JsonProperty("tournament_id")]
        public long TournamentId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("seed")]
        public int Seed { get; private set; }

        [JsonProperty("active")]
        public bool Active { get; private set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; private set; }

        [JsonProperty("invite_email")]
        public string InviteEmail { get; private set; }

        [JsonProperty("final_rank")]
        public int? FinalRank { get; private set; }

        [JsonProperty("misc")]
        public string Misc { get; private set; }

        [JsonProperty("icon")]
        public string Icon { get; private set; }

        [JsonProperty("on_waiting_list")]
        public bool OnWaitingList { get; private set; }

        [JsonProperty("invitation_id")]
        public long? InvitationId { get; private set; }

        [JsonProperty("group_id")]
        public long? GroupId { get; private set; }

        [JsonProperty("checked_in_at")]
        public DateTime? CheckedInAt { get; private set; }

        [JsonProperty("ranked_member_id")]
        public long? RankedMemberId { get; private set; }

        [JsonProperty("custom_field_response")]
        public string CustomFieldResponse { get; private set; }

        [JsonProperty("clinch")]
        public string Clinch { get; private set; }

        [JsonProperty("integration_uids")]
        public IEnumerable<string> IntegrationUids { get; private set; }

        [JsonProperty("challonge_username")]
        public string ChallongeUsername { get; private set; }

        [JsonProperty("challonge_email_address_verified")]
        public bool? ChallongeEmailAddressVerified { get; private set; }

        [JsonProperty("removable")]
        public bool Removable { get; private set; }

        [JsonProperty("participatable_or_invitation_attached")]
        public bool ParticipatableOrInvitationAttached { get; private set; }

        [JsonProperty("confirm_remove")]
        public bool ConfirmRemove { get; private set; }

        [JsonProperty("invitation_pending")]
        public bool InvitationPending { get; private set; }

        [JsonProperty("display_name_with_invitation_email_address")]
        public string DisplayNameWithInvitationEmailAddress { get; private set; }

        [JsonProperty("email_hash")]
        public string EmailHash { get; private set; }

        [JsonProperty("username")]
        public string Username { get; private set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; private set; }

        [JsonProperty("attached_participatable_portrait_url")]
        public string AttachedParticipatablePortraitUrl { get; private set; }

        [JsonProperty("can_check_in")]
        public bool CanCheckIn { get; private set; }

        [JsonProperty("checked_in")]
        public bool CheckedIn { get; private set; }

        [JsonProperty("reactivatable")]
        public bool Reactivatable { get; private set; }

        [JsonProperty("check_in_open")]
        public bool CheckInOpen { get; private set; }

        [JsonProperty("group_player_ids")]
        public IEnumerable<int> GroupPlayerIds { get; private set; }

        [JsonProperty("has_irrelevant_seed")]
        public bool HasIrrelevantSeed { get; private set; }

        internal Participant() { }
    }
}