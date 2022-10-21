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
    public class Model_Car {
        private Car car;
        [SetUp]
        public void Setup() {
            car = new Car();
        }
        [Test]
        public void PerformanceReturns10() {
            car.Performance = 10;
            Assert.AreEqual(10, car.Performance);
        }
        [Test]
        public void SpeedReturns10() {
            car.Speed = 10;
            Assert.AreEqual(10, car.Speed);
        }
        [Test]
        public void QualityReturns10() {
            car.Quality = 10;
            Assert.AreEqual(10, car.Quality);
        }
        [Test]
        public void IsBrokenReturnsTrue() {
            car.IsBroken = true;
            Assert.AreEqual(true, car.IsBroken);
        }
    }
}
