using System;
using LabyrinthFindingPath;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabyrinthFindingPathTests
{
    [TestClass]
    public class AgentTests
    {
        [TestMethod]
        public void RotateAgent180DegreesNorthToSouth()
        {
            Agent agent = new Agent(0, 0);
            agent.Rotate180Degrees();
            Assert.AreEqual(Direction.SOUTH, agent.Direction);
        }
        [TestMethod]
        public void RotateAgent180DegreesEastToWest()
        {
            Agent agent = new Agent(0, 0, Direction.EAST);
            agent.Rotate180Degrees();
            Assert.AreEqual(Direction.WEST, agent.Direction);
        }
        [TestMethod]
        public void RotateAgent90DegreesRightNorthToEast()
        {
            Agent agent = new Agent(0, 0);
            agent.Rotate90DegreesRight();
            Assert.AreEqual(Direction.EAST, agent.Direction);
        }
        [TestMethod]
        public void RotateAgent90DegreesRightWestToNorth()
        {
            Agent agent = new Agent(0, 0, Direction.WEST);
            agent.Rotate90DegreesRight();
            Assert.AreEqual(Direction.NORTH, agent.Direction);
        }
        [TestMethod]
        public void RotateAgent90DegreesLeftNorthToWest()
        {
            Agent agent = new Agent(0, 0);
            agent.Rotate90DegreesLeft();
            Assert.AreEqual(Direction.WEST, agent.Direction);
        }
        [TestMethod]
        public void RotateAgent90DegreesLeftWestToSouth()
        {
            Agent agent = new Agent(0, 0, Direction.WEST);
            agent.Rotate90DegreesLeft();
            Assert.AreEqual(Direction.SOUTH, agent.Direction);
        }
    }
}
