using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;


namespace ControllerTest {
    [TestFixture]
    public class Model_SectionData {
        SectionData data;
        Driver LeftDriver = new Driver("LeftDriver", TeamColor.Blue);
        Driver RightDriver = new Driver("RightDriver", TeamColor.Red);
        [SetUp]
        public void Setup() {
            data = new SectionData();
            data.Left =LeftDriver;
            data.Right = RightDriver;
            data.DistanceRight = 100;
        }
        [Test]
        public void LeftReturnsLeftDriver() {
            Assert.AreEqual(data.Left, LeftDriver);
        }
        [Test]
        public void RightReturnsRigthDriver() {
            Assert.AreEqual(data.Right, RightDriver);
        }
        [Test]
        public void RightDistanceIs100() {
            Assert.AreEqual(data.DistanceRight, 100);
        }
        [Test]
        public void LeftDistanceIs0() {
            Assert.AreEqual(data.DistanceLeft, 0);
        }
    }
}
