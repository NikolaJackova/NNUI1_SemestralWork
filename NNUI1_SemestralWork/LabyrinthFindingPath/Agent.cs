using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class Agent
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Direction Direction { get; set; }
        public int CostOfRotate90 { get; set; }
        public int CostOfRotate180 { get; set; }
        public int CostOfStepForward { get; set; }

        public Agent(int row, int column, Direction direction = Direction.NORTH)
        {
            Row = row;
            Column = column;
            Direction = direction;
            CostOfRotate90 = 2;
            CostOfRotate180 = 3;
            CostOfStepForward = 5;
        }
        public int StepForward(int row, int column)
        {
            Row = row;
            Column = column;
            return CostOfStepForward;
        }
        public int Rotate90DegreesLeft()
        {
            RotateAgent(-90);
            return CostOfRotate90;

        }
        public int Rotate90DegreesRight()
        {
            RotateAgent(90);
            return CostOfRotate90;
        }
        public int Rotate180Degrees()
        {
            RotateAgent(180);
            return CostOfRotate180;
        }
        private void RotateAgent(int degrees)
        {
            Direction = (Direction)(((int)Direction + (degrees % 360) + 360) % 360);
        }
    }
}
