using System;
using LabyrinthFindingPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthFindingPathTests
{
    [TestClass]
    public class PositionTests
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
        [TestMethod]
        public void StepForwardToNorth()
        {
            Position position = new Position(2, 1, Direction.NORTH);
            position.StepForward();
            Position expectedPosition = new Position(1, 1, Direction.NORTH);
            Assert.AreEqual(true, position.Equals(expectedPosition));
        }
        [TestMethod]
        public void StepForwardToEast()
        {
            Position position = new Position(1, 1, Direction.EAST);
            position.StepForward();
            Position expectedPosition = new Position(1, 2, Direction.EAST);
            Assert.AreEqual(true, position.Equals(expectedPosition));
        }
        [TestMethod]
        public void StepForwardToSouth()
        {
            Position position = new Position(1, 1, Direction.SOUTH);
            position.StepForward();
            Position expectedPosition = new Position(2, 1, Direction.SOUTH);
            Assert.AreEqual(true, position.Equals(expectedPosition));
        }
        [TestMethod]
        public void StepForwardToWest()
        {
            Position position = new Position(1, 2, Direction.WEST);
            position.StepForward();
            Position expectedPosition = new Position(1, 1, Direction.WEST);
            Assert.AreEqual(true, position.Equals(expectedPosition));
        }
    }
}
