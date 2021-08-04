using Microsoft.VisualStudio.TestTools.UnitTesting;
using Challonge.Api;
using System;
using Challonge.Objects;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Challonge.Exceptions;
using System.Net.Http;

namespace ChallongeTests
{
    [TestClass]
    public class ChallongeTests
    {
        private readonly ChallongeClient _client;
        private readonly string _testTournamentSuffix;
        public ChallongeTests()
        {
            string username = Environment.GetEnvironmentVariable("CHALLONGE_USERNAME");
            string key = Environment.GetEnvironmentVariable("CHALLONGE_API_KEY");

            _testTournamentSuffix = "--__-_--__-_";
            _client = new ChallongeClient(new HttpClient(), new ChallongeCredentials(username, key));
        }

        [TestMethod]
        public async Task TestTournamentCreationDeletion()
        {
            TournamentInfo[] tis = new TournamentInfo[]
            {
                new(){ Name = "Test1" + _testTournamentSuffix},
                new(){ Name = "Test2" + _testTournamentSuffix},
                new(){ Name = "Test3" + _testTournamentSuffix}
            };

            List<Tournament> testTournaments = new();

            foreach (TournamentInfo ti in tis)
            {
                testTournaments.Add(await _client.CreateTournamentAsync(ti));
            }

            IEnumerable<Tournament> allTournaments = await _client.GetTournamentsAsync();
            Assert.IsTrue(testTournaments.All(test => 
                allTournaments.Select(t => t.Id).Contains(test.Id)));

            foreach (Tournament t in testTournaments)
            {
                await _client.DeleteTournamentAsync(t);
                await Assert.ThrowsExceptionAsync<ChallongeException>(() =>
                    _client.GetTournamentByIdAsync(t.Id));
            }

        }
        [TestMethod]
        public async Task TestTournamentUpdate()
        {
            Tournament t = await _client.CreateTournamentAsync(new() { Name = "UpdateTest" + _testTournamentSuffix });
            string newDescription = "An updated description";
            TournamentInfo ti = new()
            {
                AcceptAttachments = true,
                Description = newDescription,
            };
            t = await _client.UpdateTournamentAsync(t, ti);

            Assert.IsTrue(t.AcceptAttachments);
            Assert.AreEqual(newDescription, t.Description);

            await _client.DeleteTournamentAsync(t);
        }

        [TestMethod]
        public async Task TestFullTournamentRun()
        {
            TournamentInfo ti = new()
            {
                Name = "Full Tournament Test" + _testTournamentSuffix
            };
            Tournament t = await _client.CreateTournamentAsync(ti);
            int count = 5;

            for (int i = 1; i <= count; i++)
            {
                string name = $"player{i}";
                ParticipantInfo pi = new()
                {
                    Name = name,
                    Seed = i
                };

                Participant p = await _client.CreateParticipantAsync(t, pi);

                Assert.AreEqual(name, p.Name);
                Assert.AreEqual(i, p.Seed);
            }

            IEnumerable<Participant> participants = await _client.GetParticipantsAsync(t);
            Assert.AreEqual(count, participants.Count());

            t = await _client.StartTournamentAsync(t);
            IEnumerable<Match> matches = await _client.GetMatchesAsync(t, MatchState.Open);
            while (matches.Any())
            {
                foreach (Match m in matches)
                {
                    MatchInfo mi = new()
                    {
                        Scores = new Score[]
                        {
                            new(3, 0)
                        },
                        WinnerId = m.Player1Id
                    };

                    Match updated = await _client.UpdateMatchAsync(m, mi);
                    Assert.AreEqual(mi.WinnerId, updated.WinnerId);
                }
                matches = await _client.GetMatchesAsync(t, MatchState.Open);
            }

            await _client.FinalizeTournamentAsync(t);
            t = await _client.GetTournamentByIdAsync(t.Id);

            Assert.IsNotNull(t.CompletedAt);
            Assert.AreEqual(TournamentState.Complete, t.State);

            await _client.DeleteTournamentAsync(t);
        }

        [TestMethod]
        public async Task TestParticipants()
        {
            TournamentInfo ti = new()
            {
                Name = "ParticipantsTest" + _testTournamentSuffix,
                CheckInDuration = 30,
                StartAt = DateTime.Now
            };
            Tournament t = await _client.CreateTournamentAsync(ti);

            int count = 5;

            for (int i = 1; i <= count; i++)
            {
                string name = $"player{i}";
                ParticipantInfo pi = new()
                {
                    Name = name,
                    Seed = i
                };

                Participant p = await _client.CreateParticipantAsync(t, pi);

                Assert.AreEqual(name, p.Name);
                Assert.AreEqual(i, p.Seed);

                await _client.CheckInParticipantAsync(p);
                p = await _client.GetParticipantAsync(t, p.Id);
                Assert.IsTrue(p.CheckedIn);
                p = await _client.UndoCheckInParticipantAsync(p);
                Assert.IsFalse(p.CheckedIn);
            }

            IEnumerable<Participant> participants = await _client.GetParticipantsAsync(t);
            Assert.AreEqual(count, participants.Count());

            Participant p0 = participants.ElementAt(0);
            await _client.DeleteParticipantAsync(p0);
            await Assert.ThrowsExceptionAsync<ChallongeException>(() => _client.GetParticipantAsync(t, p0.Id));
            participants = await _client.GetParticipantsAsync(t);
            Assert.AreEqual(count - 1, participants.Count());
            p0 = participants.ElementAt(0);
            string newName = "UPDATED PLAYER";

            ParticipantInfo newPi = new()
            {
                Name = newName
            };

            await _client.UpdateParticipantAsync(p0, newPi);
            p0 = await _client.GetParticipantAsync(t, p0.Id);
            Assert.AreEqual(newName, p0.Name);

            await _client.ClearParticipantsAsync(t);

            IEnumerable<Participant> noParticipants = await _client.GetParticipantsAsync(t);

            Assert.AreEqual(0, noParticipants.Count());

            await _client.DeleteTournamentAsync(t);
        }

        [TestMethod]
        public async Task TestTournamentAttributes()
        {
            int checkInDuration = 30;
            string description = "A test tournament";
            string name = "TestAttributes" + _testTournamentSuffix;
            int signupCap = 100;
            DateTime startAt = DateTime.Now.AddDays(1).Date;

            TournamentInfo ti = new()
            {
                AcceptAttachments = true,
                CheckInDuration = checkInDuration,
                Description = description,
                GrandFinalsModifier = GrandFinalsModifier.Skip,
                HideForum = false,
                HoldThirdPlaceMatch = false,
                Name = name,
                NotifyUsersWhenMatchesOpen = true,
                NotifyUsersWhenTheTournamentEnds = true,
                OpenSignup = true,
                Private = false,
                RankedBy = RankingMethod.MatchWins,
                SequentialPairings = false,
                ShowRounds = true,
                SignupCap = signupCap,
                TournamentType = TournamentType.DoubleElimination,
                StartAt = startAt
            };
            Tournament t = await _client.CreateTournamentAsync(ti);

            Assert.IsTrue(t.AcceptAttachments);
            Assert.AreEqual(checkInDuration, t.CheckInDuration);
            Assert.AreEqual(description, t.Description);
            Assert.AreEqual(GrandFinalsModifier.Skip, t.GrandFinalsModifier);
            Assert.IsFalse(t.HideForum);
            Assert.IsFalse(t.HoldThirdPlaceMatch);
            Assert.AreEqual(name, t.Name);
            Assert.IsTrue(t.NotifyUsersWhenMatchesOpen);
            Assert.IsTrue(t.NotifyUsersWhenTheTournamentEnds);
            Assert.IsTrue(t.OpenSignup);
            Assert.IsFalse(t.Private);
            Assert.AreEqual(RankingMethod.MatchWins, t.RankedBy);
            Assert.IsFalse(t.SequentialPairings);
            Assert.IsTrue(t.ShowRounds);
            Assert.AreEqual(signupCap, t.SignupCap);
            Assert.AreEqual(TournamentType.DoubleElimination, t.TournamentType);
            Assert.AreEqual(startAt, t.StartAt);

            await _client.DeleteTournamentAsync(t);
        }

        [TestMethod]
        public async Task TestMatches()
        {
            Tournament t = await _client.CreateTournamentAsync(new() { 
                Name = "MatchTest" + _testTournamentSuffix, 
                AcceptAttachments = true 
            });
            Participant p1 = await _client.CreateParticipantAsync(t, new() { Name = "player1" });
            await _client.CreateParticipantAsync(t, new() { Name = "player2" });

            t = await _client.StartTournamentAsync(t);

            IEnumerable<Match> matches = await _client.GetMatchesAsync(t, MatchState.Open);

            Assert.AreEqual(1, matches.Count());

            Match m = matches.ElementAt(0);
            string description = "An attachment test";
            string fileName = "attachmenttest.jpg";

            MatchAttachment ma = await _client.CreateMatchAttachmentAsync(m, new()
            {
                Asset = new(TestImageGenerator.GenerateTestPngBytes(), fileName),
                Description = description
            });

            Assert.AreEqual(description, ma.Description);
            Assert.AreEqual(fileName, ma.OriginalFileName);

            Score[] scores = m.Player1Id == p1.Id ? 
                new[] { new Score(3, 0), new Score(3, 0) } :
                new[] { new Score(0, 3), new Score(0, 3) };

            m = await _client.UpdateMatchAsync(m, new()
            {
                Scores = scores,
                WinnerId = p1.Id
            });

            Assert.IsTrue(Enumerable.SequenceEqual(scores, m.Scores));
            Assert.AreEqual(p1.Id, m.WinnerId);
            Assert.AreEqual(MatchState.Complete, m.State);
            await _client.DeleteTournamentAsync(t);
        }
    }
}
