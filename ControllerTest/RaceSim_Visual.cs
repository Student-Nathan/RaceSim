using Controller;
using Model;
using RaceSim;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;
using NUnit.Framework.Constraints;

namespace ControllerTest {
    [TestFixture]
    internal class RaceSim_Visual {
        




        [TestCase(SectionTypes.Straight, Rotation.SouthNorth, new string[] { "|  |", "|12|", "|  |", "|  |" })]
        [TestCase(SectionTypes.Straight, Rotation.NorthSouth, new string[] { "|  |", "|12|", "|  |", "|  |" })]
        [TestCase(SectionTypes.Straight, Rotation.EastWest, new string[] { "----", "  1 ", "  2 ", "----" })]
        [TestCase(SectionTypes.Straight, Rotation.WestEast, new string[] { "----", "  1 ", "  2 ", "----" })]
        [TestCase(SectionTypes.Finish, Rotation.SouthNorth, new string[] { "|--|", "|21|", "|##|", "|  |" })]
        [TestCase(SectionTypes.Finish, Rotation.NorthSouth, new string[] { "|--|", "|##|", "|12|", "|  |" })]
        [TestCase(SectionTypes.Finish, Rotation.EastWest, new string[] { "----", " #1|", " #2|", "----" })]
        [TestCase(SectionTypes.Finish, Rotation.WestEast, new string[] { "----", "|1# ", "|2# ", "----" })]
        [TestCase(SectionTypes.StartGrid, Rotation.SouthNorth, new string[] { "|  |", "|‾‾|", "|12|", "|  |" })]
        [TestCase(SectionTypes.StartGrid, Rotation.NorthSouth, new string[] { "|  |", "|21|", "|__|", "|  |" })]
        [TestCase(SectionTypes.StartGrid, Rotation.EastWest, new string[] { "----", " [1 ", " [2  ", "----" })]
        [TestCase(SectionTypes.StartGrid, Rotation.WestEast, new string[] { "----", " 1] ", " 2]  ", "----" })]
        [TestCase(SectionTypes.LeftCorner, Rotation.NorthSouth, new string[] { @"|  \", @"| 1 ", @"\2  ", @" \--" })]
        [TestCase(SectionTypes.LeftCorner, Rotation.EastWest, new string[] { @" /--", @"/1  ", @"| 2 ", @"|  /" })]
        [TestCase(SectionTypes.LeftCorner, Rotation.SouthNorth, new string[] { @"--\", @" 1 \", @"2  |", @"\  |" })]
        [TestCase(SectionTypes.LeftCorner, Rotation.WestEast, new string[] { @"/  |", @" 1 |", @"  2/", @"--/ " })]
        [TestCase(SectionTypes.RightCorner, Rotation.NorthSouth, new string[] { @"/  |", @" 1 |", @"  2/", @"--/ " })]
        [TestCase(SectionTypes.RightCorner, Rotation.EastWest, new string[] { @"|  \", @"| 1 ", @"\2  ", @" \--" })]
        [TestCase(SectionTypes.RightCorner, Rotation.SouthNorth, new string[] { @" /--", @"/1  ", @"| 2 ", @"|  /" })]
        [TestCase(SectionTypes.RightCorner, Rotation.WestEast, new string[] { @"--\", @" 1 \", @"2  |", @"\  |" })]
        public void Section_To_Graphics_String(SectionTypes type, Rotation rotation, string[] expectedStrings) {

            var result = Visual.GetGraphics(new Section(type),rotation);

            Assert.AreEqual(expectedStrings, result);
        }


        [TestCase(new string[] { "----", "|1# ", "|2# ", "----" }, new string[] { "----", "| # ", "| # ", "----" })]
        [TestCase(new string[] { "|  |", "|12|", "|  |", "|  |" }, new string[] { "|  |", "|  |", "|  |", "|  |" })]

        public void replacePlaceHolders_replaces_all(string[] unprocessedStrings, string[] expectedStrings) {
            string[] processedStrings = new string[unprocessedStrings.Length];
            for(int i = 0; i<unprocessedStrings.Length;i++) {
                processedStrings[i] = Visual.ReplacePlaceholders(unprocessedStrings[i], null, null);
            }
            Assert.AreEqual(processedStrings,expectedStrings);
        }

        [TestCase(new string[] { "----", "|1# ", "|2# ", "----" }, new string[] { "----", "|t# ", "| # ", "----" })]
        [TestCase(new string[] { "|  |", "|12|", "|  |", "|  |" }, new string[] { "|  |", "|t |", "|  |", "|  |" })]
        public void replacePlaceHolders_only_replaces_2(string[] unprocessedStrings, string[] expectedStrings) {
            string[] processedStrings = new string[unprocessedStrings.Length];
            for (int i = 0; i < unprocessedStrings.Length; i++) {
                processedStrings[i] = Visual.ReplacePlaceholders(unprocessedStrings[i], new Driver("test",TeamColor.Red), null);
            }
            Assert.AreEqual(processedStrings, expectedStrings);
        }

        [TestCase(new string[] { "----", "|1# ", "|2# ", "----" }, new string[] { "----", "|t# ", "|D# ", "----" })]
        [TestCase(new string[] { "|  |", "|12|", "|  |", "|  |" }, new string[] { "|  |", "|tD|", "|  |", "|  |" })]
        public void replacePlaceHolders_replaces_both(string[] unprocessedStrings, string[] expectedStrings) {
            string[] processedStrings = new string[unprocessedStrings.Length];
            for (int i = 0; i < unprocessedStrings.Length; i++) {
                processedStrings[i] = Visual.ReplacePlaceholders(unprocessedStrings[i], new Driver("test", TeamColor.Red), new Driver("DestTriver",TeamColor.Green));
            }
            Assert.AreEqual(processedStrings, expectedStrings);
        }

        [TestCase(new string[] { "----", "|1# ", "|2# ", "----" }, new string[] { "----", "|X# ", "|X# ", "----" })]
        [TestCase(new string[] { "|  |", "|12|", "|  |", "|  |" }, new string[] { "|  |", "|XX|", "|  |", "|  |" })]
        public void replacePlaceHolders_replaces_broken(string[] unprocessedStrings, string[] expectedStrings) {
            string[] processedStrings = new string[unprocessedStrings.Length];
            Driver driver1 = new Driver("test", TeamColor.Red);
            Driver driver2 = new Driver("DestTriver", TeamColor.Green);
            driver1.Equipment.IsBroken = true;
            driver2.Equipment.IsBroken = true;
            for (int i = 0; i < unprocessedStrings.Length; i++) {
                processedStrings[i] = Visual.ReplacePlaceholders(unprocessedStrings[i],driver1 ,driver2 );
            }
            Assert.AreEqual(processedStrings, expectedStrings);
        }

    }
}
