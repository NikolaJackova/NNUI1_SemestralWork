using System;
using LabyrinthFindingPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthFindingPathTests
{
    [TestClass]
    public class AgentTests
    {
        [TestMethod]
        public void Rotate180DegreesNorthToSouth()
        {
            Position position = new Position(0, 0, Direction.NORTH);
            position.Rotate180Degrees();
            Assert.AreEqual(Direction.SOUTH, position.Direction);
        }
        [TestMethod]
        public void Rotate180DegreesEastToWest()
        {
            Position position = new Position(0, 0, Direction.EAST);
            position.Rotate180Degrees();
            Assert.AreEqual(Direction.WEST, position.Direction);
        }
        [TestMethod]
        public void Rotate90DegreesRightNorthToEast()
        {
            Position position = new Position(0, 0, Direction.NORTH);
            position.Rotate90DegreesRight();
            Assert.AreEqual(Direction.EAST, position.Direction);
        }
        [TestMethod]
        public void Rotate90DegreesRightWestToNorth()
        {
            Position position = new Position(0, 0, Direction.WEST);
            position.Rotate90DegreesRight();
            Assert.AreEqual(Direction.NORTH, position.Direction);
        }
        [TestMethod]
        public void Rotate90DegreesLeftNorthToWest()
        {
            Position position = new Position(0, 0, Direction.NORTH);
            position.Rotate90DegreesLeft();
            Assert.AreEqual(Direction.WEST, position.Direction);
        }
        [TestMethod]
        public void Rotate90DegreesLeftWestToSouth()
        {
            Position position = new Position(0, 0, Direction.WEST);
            position.Rotate90DegreesLeft();
            Assert.AreEqual(Direction.SOUTH, position.Direction);
        }
    }
}
