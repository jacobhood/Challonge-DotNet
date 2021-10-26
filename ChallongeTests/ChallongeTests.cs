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
        private const string _testTournamentSuffix = "-_-_Challonge-DotNetTest-_-_";
        private static readonly ChallongeClient _client = new(new HttpClient(),
            new ChallongeCredentials(
                Environment.GetEnvironmentVariable("CHALLONGE_USERNAME"),
                Environment.GetEnvironmentVariable("CHALLONGE_API_KEY")));

        [TestMethod]
        public async Task TestTournamentCreationDeletion()
        {
            TournamentInfo[] tis = new TournamentInfo[]
            {
                new(){ Name = "Test1" + _testTournamentSuffix },
                new(){ Name = "Test2" + _testTournamentSuffix },
                new(){ Name = "Test3" + _testTournamentSuffix }
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
        public async Task TestTournamentUpdates()
        {
            Tournament t = await _client.CreateTournamentAsync(
                new TournamentInfo() { Name = "UpdateTest" + _testTournamentSuffix });
            string newDescription = "An updated description";

            t = await _client.UpdateTournamentAsync(t, 
                new TournamentInfo()
                {
                    AcceptAttachments = true,
                    Description = newDescription,
                    PredictionMethod = PredictionMethod.ExponentialScoring
                });

            Assert.IsTrue(t.AcceptAttachments);
            Assert.AreEqual(newDescription, t.Description);
            Assert.AreEqual(PredictionMethod.ExponentialScoring, t.PredictionMethod);

            await _client.CreateParticipantsAsync(
                t, new ParticipantInfo[]
                {
                    new() { Name = "player1" },
                    new() { Name = "player2" }
                });

            t = await _client.OpenTournamentForPredictionsAsync(t);

            Assert.IsTrue(t.AcceptingPredictions);
            Assert.IsNotNull(t.PredictionsOpenedAt);
            Assert.AreEqual(TournamentState.AcceptingPredictions, t.State);

            t = await _client.StartTournamentAsync(t);

            Assert.AreEqual(TournamentState.Underway, t.State);

            Match m = (await _client.GetMatchesAsync(t)).FirstOrDefault();
            await _client.UpdateMatchAsync(m,
                new MatchInfo()
                {
                    Scores = new Score[] { new(3, 0) },
                    WinnerId = m.Player1Id
                });

            t = await _client.ResetTournamentAsync(t);

            Assert.IsNull(t.StartedAt);
            Assert.IsNull(t.PredictionsOpenedAt);
            Assert.IsFalse(t.AcceptingPredictions);
            Assert.AreEqual(TournamentState.Pending, t.State);
            Assert.AreEqual(0, t.ProgressMeter);
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
            t = await _client.GetTournamentByIdAsync(t.Id);
            
            Assert.AreEqual(count, participants.Count());
            Assert.AreEqual(count, t.ParticipantsCount);

            t = await _client.StartTournamentAsync(t);

            Assert.IsNotNull(t.StartedAt);
            Assert.AreEqual(TournamentState.Underway, t.State);

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

            t = await _client.FinalizeTournamentAsync(t);

            Assert.IsNotNull(t.CompletedAt);
            Assert.AreEqual(TournamentState.Complete, t.State);
            Assert.AreEqual(100, t.ProgressMeter);
        }

        [TestMethod]
        public async Task TestParticipants()
        {
            TournamentInfo ti = new()
            {
                Name = "ParticipantsTest" + _testTournamentSuffix,
                CheckInDuration = 30,
                StartAt = DateTime.Now.AddMinutes(1)
            };

            Tournament t = await _client.CreateTournamentAsync(ti);
            int count = 5;

            // tests basic participant creation
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

            Participant p0 = participants.First();
            await _client.DeleteParticipantAsync(p0);

            await Assert.ThrowsExceptionAsync<ChallongeException>(() => _client.GetParticipantAsync(t, p0.Id));
            
            participants = await _client.GetParticipantsAsync(t);
            
            Assert.AreEqual(count - 1, participants.Count());
            
            p0 = participants.First();
            string newName = "UPDATED PLAYER";
            
            ParticipantInfo newPi = new()
            {
                Name = newName
            };
            
            await _client.UpdateParticipantAsync(p0, newPi);
            p0 = await _client.GetParticipantAsync(t, p0.Id);
            
            Assert.AreEqual(newName, p0.Name);

            // tests participants bulk add
            List<ParticipantInfo> participantInfos = new();
            for (int i = count + 1; i <= count + count; i++)
            {
                string name = $"player{i}";
                ParticipantInfo pi = new()
                {
                    Name = name,
                    Seed = i - 1
                };
                participantInfos.Add(pi);
            }

            List<Participant> result = (await _client.CreateParticipantsAsync(t, participantInfos)).ToList();
            
            for (int i = 0; i < count; i++)
            {
                Assert.AreEqual(participantInfos[i].Name, result[i].Name);
                Assert.AreEqual(participantInfos[i].Seed, result[i].Seed);
            }
            
            await _client.ClearParticipantsAsync(t);

            IEnumerable<Participant> noParticipants = await _client.GetParticipantsAsync(t);

            Assert.AreEqual(0, noParticipants.Count());
        }

        [TestMethod]
        public async Task TestParticipantCheckIns()
        {
            Tournament t = await _client.CreateTournamentAsync(
                new TournamentInfo()
                {
                    Name = "ParticipantCheckInsTest" + _testTournamentSuffix,
                    CheckInDuration = 60,
                    StartAt = DateTime.Now
                });

            Participant p = await _client.CreateParticipantAsync(
                t, new ParticipantInfo()
                {
                    Name = "player1"
                });

            p = await _client.CheckInParticipantAsync(p);

            Assert.IsTrue(p.CheckedIn);
            Assert.IsNotNull(p.CheckedInAt);
            Assert.IsTrue(p.CheckInOpen);

            p = await _client.UndoCheckInParticipantAsync(p);

            Assert.IsFalse(p.CheckedIn);
            Assert.IsNull(p.CheckedInAt);
            Assert.IsTrue(p.CanCheckIn);
        }

        [TestMethod]
        public async Task TestTournamentCheckIns()
        {
            Tournament t = await _client.CreateTournamentAsync(
                new TournamentInfo()
                {
                    Name = "TournamentCheckInsTest" + _testTournamentSuffix,
                    CheckInDuration = 60,
                    StartAt = DateTime.Now.AddMinutes(1)
                });

            Assert.IsNotNull(t.StartedCheckingInAt);
            Assert.AreEqual(TournamentState.CheckingIn, t.State);

            IEnumerable<Participant> participants = await _client.CreateParticipantsAsync(
                t, new ParticipantInfo[]
                {
                    new() { Name = "player1" },
                    new() { Name = "player2" }
                });

            foreach (Participant p in participants)
            {
                await _client.CheckInParticipantAsync(p);
            }

            t = await _client.ProcessTournamentCheckInsAsync(t);

            Assert.AreEqual(TournamentState.CheckedIn, t.State);

            t = await _client.AbortTournamentCheckInAsync(t);

            Assert.IsNull(t.StartedCheckingInAt);
            Assert.AreEqual(TournamentState.Pending, t.State);

            foreach (Participant p in await _client.GetParticipantsAsync(t))
            {
                Assert.IsFalse(p.CheckedIn);
                Assert.IsNull(p.CheckedInAt);
            }
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
            Assert.IsTrue(t.CreatedByApi);
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
        }

        [TestMethod]
        public async Task TestMatches()
        {
            Tournament t = await _client.CreateTournamentAsync(
                new TournamentInfo()
                {
                    Name = "MatchTest" + _testTournamentSuffix,
                    AcceptAttachments = true
                });

            await _client.CreateParticipantsAsync(t,
                new ParticipantInfo[]
                {
                    new() { Name = "player1" },
                    new() { Name = "player2" }
                });

            t = await _client.StartTournamentAsync(t);
            IEnumerable<Match> matches = await _client.GetMatchesAsync(t);

            Assert.AreEqual(1, matches.Count());

            Match m = matches.First();
            Score[] scores = new Score[] { new(3, 0), new(3, 0) };
            m = await _client.UpdateMatchAsync(m, 
                new MatchInfo()
                {
                    Scores = scores,
                    WinnerId = m.Player1Id
                });

            Assert.IsTrue(Enumerable.SequenceEqual(scores, m.Scores));
            Assert.AreEqual(m.Player1Id, m.WinnerId);
            Assert.AreEqual(MatchState.Complete, m.State);
            Assert.IsNotNull(m.CompletedAt);

            m = await _client.ReopenMatchAsync(m);

            Assert.AreEqual(MatchState.Open, m.State);
            Assert.IsNull(m.CompletedAt);
            Assert.IsNull(m.UnderwayAt);

            m = await _client.MarkMatchAsUnderwayAsync(m);

            Assert.IsNotNull(m.UnderwayAt);

            m = await _client.UnmarkMatchAsUnderwayAsync(m);

            Assert.IsNull(m.UnderwayAt);
        }

        [TestMethod]
        public async Task TestMatchAttachments()
        {
            Tournament t = await _client.CreateTournamentAsync(
                new TournamentInfo()
                {
                    Name = "MatchAttachmentTest" + _testTournamentSuffix,
                    AcceptAttachments = true
                });

            await _client.CreateParticipantsAsync(t, 
                new ParticipantInfo[]
                {
                    new() { Name = "player1" },
                    new() { Name = "player2" }
                });

            t = await _client.StartTournamentAsync(t);

            Match m = (await _client.GetMatchesAsync(t)).First();

            string description = "An attachment test";
            string fileName = "attachmenttest.jpg";
            MatchAttachment ma = await _client.CreateMatchAttachmentAsync(m, new()
            {
                Asset = new MatchAttachmentAsset(
                    TestImageGenerator.GenerateTestPngBytes(), fileName),
                Description = description
            });

            m = await _client.GetMatchAsync(t, m.Id);

            Assert.AreEqual(description, ma.Description);
            Assert.AreEqual(fileName, ma.OriginalFileName);
            Assert.AreEqual(1, m.AttachmentCount);

            description = "A new description";

            ma = await _client.UpdateMatchAttachmentAsync(
                m, ma, new MatchAttachmentInfo()
                {
                    Description = description
                });

            Assert.AreEqual(description, ma.Description);

            await _client.DeleteMatchAttachmentAsync(m, ma);

            Assert.IsFalse((await _client.GetMatchAttachmentsAsync(m)).Any());
            await Assert.ThrowsExceptionAsync<ChallongeException>(() => 
                _client.GetMatchAttachmentAsync(m, ma.Id));
        }

        [ClassCleanup]
        public static async Task Cleanup()
        {
            foreach (Tournament t in await _client.GetTournamentsAsync())
            {
                if (t.Name.EndsWith(_testTournamentSuffix))
                {
                    await _client.DeleteTournamentAsync(t);
                }
            }
        }
    }
}
