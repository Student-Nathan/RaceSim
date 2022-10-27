//using Model;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Assert = NUnit.Framework.Assert;

//namespace ControllerTest {
//    [TestFixture]
//    public class Model_RaceContext {
//        RaceContext context;
//        [SetUp]
//        public void Setup() {
//            context = new RaceContext();
//        }
//        [Test]
//        public void EventUpdatesProperly() {
//            DriversChangedEventArgs e = new DriversChangedEventArgs(new Track("Naam", new SectionTypes[] {SectionTypes.Finish}, 3));
//            context.OnDriversChanged(null, e);
//            Assert.AreEqual(context.publicTrackName, e.Track.Name);
//        }
//    }
//}
