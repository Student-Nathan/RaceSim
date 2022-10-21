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
    public class Model_Driver {
        private Driver driver;
        [SetUp]
        public void Init() {
            driver = new Driver("Naam",TeamColor.Blue);
        }

        [Test]
        public void naamReturnsNaam() {
            Assert.AreEqual(driver.Name, "Naam");
        }
        [Test]
        public void teamColorReturnsBlue() {
            Assert.AreEqual(driver.TeamColor, TeamColor.Blue);
        }
    }
}
