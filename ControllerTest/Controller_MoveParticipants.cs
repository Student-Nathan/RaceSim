using Controller;
using Model;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace ControllerTest {
    [TestFixture]
    internal class Controller_MoveParticipants {
        Race race;
        Section section1;
        Section section2;
        Section section3;
        Driver driver1;
        Driver driver2;
        Driver driver3;
        Track track;


        [SetUp]
        public void Setup() {

            track = new Track("UnitTest", new SectionTypes[] { SectionTypes.StartGrid,SectionTypes.Straight,SectionTypes.RightCorner},3);
            section1 = track.Sections.ElementAt(0);
            section2 = track.Sections.ElementAt(1);
            section3 = track.Sections.ElementAt(2);
            driver1 = new Driver("Driver1",TeamColor.Red);
            driver2 = new Driver("Driver2", TeamColor.Green);
            driver3 = new Driver("Driver1", TeamColor.Yellow);
            race = new Race(track, new List<IParticipant>() {driver1,driver2,driver3 },true);
        }
        [Test]
        public void NextSection_returns_section2() {
            Section test = race.FindNextSection(section1, 1);
            Assert.AreEqual(test, section2);
        }
        [Test]
        public void NextSection_returns_section3() {
            Section test = race.FindNextSection(section1, 2);
            Assert.AreEqual(section3, test);
        }

        [Test]
        public void NextSection_returns_section1() {
            Section test = race.FindNextSection(section1, 3);
            Assert.AreEqual(section1, test);
        }
        [Test]
        public void placeDriverData_places_driver1_in_section2_left() {
            race.PlaceDriverData(section1, section2, driver1, 10, 1);
            SectionData sectionData = race.GetSectionData(section2);
            Assert.AreEqual(sectionData.Left, driver1);
        }
        [Test]
        public void placeDriverData_places_driver2_in_section2_right() {
            race.PlaceDriverData(section1, section2, driver1, 10, 1);
            race.PlaceDriverData(section1, section2, driver2, 10, 1);
            SectionData sectionData = race.GetSectionData(section2);
            Assert.AreEqual(sectionData.Right, driver2);
        }
        [Test]
        public void placeDriverData_throws_exception() {
            race.PlaceDriverData(section1, section2, driver1, 10, 1);
            race.PlaceDriverData(section1, section2, driver2, 10, 1);
            Assert.That(() => race.PlaceDriverData(section1, section2, driver3, 10, 1), Throws.Exception);
        }
        [Test]
        public void removeDriverData_removes_section2_left() {
            SectionData sectionData = race.GetSectionData(section2);
            race.PlaceDriverData(section1, section2, driver1, 10, 1);
            race.PlaceDriverData(section1, section2, driver2, 10, 1);
            race.RemoveDriverData(sectionData, true);
            Assert.IsNull(sectionData.Left);
            Assert.IsNotNull(sectionData.Right);
        }
        [Test]
        public void removeDriverData_removes_section2_right() {
            SectionData sectionData = race.GetSectionData(section2);
            race.PlaceDriverData(section1, section2, driver1, 10, 1);
            race.PlaceDriverData(section1, section2, driver2, 10, 1);
            race.RemoveDriverData(sectionData, false);
            Assert.IsNull(sectionData.Right);
            Assert.IsNotNull(sectionData.Left);
        }
    }
}
