using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Controller;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ControllerTest {
    [TestFixture]
    public class Model_Competition_NextTrackShould {
        private Competition _competition;
        [SetUp]
        public void SetUp() {
            _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull() {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack() {
            Track track = new Track("testTrack", new SectionTypes[] { SectionTypes.Straight }, 3);
            _competition.Tracks.Enqueue(track);
            Track result = _competition.NextTrack();
            Assert.AreEqual(result, track);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue() {
            Track track = new Track("testTrack", new SectionTypes[] { SectionTypes.Straight }, 3);
            _competition.Tracks.Enqueue(track);
            _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack() {
            Track track = new Track("testTrack", new SectionTypes[] { SectionTypes.Straight },3);
            Track track2 = new Track("testTrack2", new SectionTypes[] { SectionTypes.Straight },3);
            _competition.Tracks.Enqueue(track);
            _competition.Tracks.Enqueue(track2);
            _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.AreEqual(result, track2);

        }
    }
}
