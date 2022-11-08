using Controller;
using Model;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
namespace ControllerTest {
    [TestFixture]
    internal class Controller_Data {
        
        [SetUp]
        public void Setup() {
            Data.Initialize(true);
        }

        [Test]
        public void Data_isInitalized() {
            Assert.IsNotNull(Data.Competition);
        }
        [Test]
        public void Data_addParticipatns_adds_participants() {
            Assert.IsNotNull(Data.Competition.Participants);
        }
        [Test]
        public void Data_addTracks_adds_tracks() {
            Assert.IsNotNull(Data.Competition.Tracks);
        }
        [Test]
        public void Data_Nextrace() {
            Race previousRace = Data.CurrentRace;
            Data.NextRace();
            Assert.AreNotEqual(previousRace, Data.CurrentRace);
        }
    }
}
