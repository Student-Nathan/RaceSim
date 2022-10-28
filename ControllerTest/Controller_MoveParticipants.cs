using Controller;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace ControllerTest {
    [TestFixture]
    internal class Controller_MoveParticipants {
        Race race;
        Section section1;
        Section section2;
        Section section3;
        Track track;


        [SetUp]
        public void Setup() {
            track = new Track("UnitTest", new SectionTypes[] { SectionTypes.StartGrid,SectionTypes.Straight,SectionTypes.RightCorner},3);
            section1 = track.Sections.ElementAt(0);
            section2 = track.Sections.ElementAt(1);
            section3 = track.Sections.ElementAt(2);

            race = new Race(track, true);
        }
        [Test]
        public void Race_initialised() {
            Assert.AreEqual(track, race.Track);
        }
        [Test]
        public void NextSection_returns_section2() {
            Section test = TestFindNextSection(section1, 1);
            Assert.AreEqual(test, section2);
        }
        [Test]
        public void NextSection_returns_section3() {
            Section test = TestFindNextSection(section1, 2);
            Assert.AreEqual(section3, test);
        }

        [Test]
        public void NextSection_returns_section1() {
            Section test = TestFindNextSection(section1, 3);
            Assert.AreEqual(section1, test);
        }


        public Section TestFindNextSection(Section section, int sections) {
            Section next = section;
            for (int i = 0; i < sections; i++) {
                if (next.Equals(track.Sections.Last?.Value)) {
                    Console.WriteLine("Last detected");
                    next = track.Sections.First.Value;
                } else {
                    next = track.Sections.Find(next).Next.Value;
                }
            }
            return next;
        }
    }
}
