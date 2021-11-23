using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public struct Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Direction Direction { get; set; }

        public Position(int row, int column, Direction direction)
        {
            Row = row;
            Column = column;
            Direction = direction;
        }
        public void StepForward()
        {
            switch (Direction)
            {
                case Direction.NORTH:
                    Row--;
                    break;
                case Direction.EAST:
                    Column++;
                    break;
                case Direction.SOUTH:
                    Row++;
                    break;
                case Direction.WEST:
                    Column--;
                    break;
                default:
                    throw new LabyrinthException("Unknow direction!");
            }
        }
        public void Rotate90DegreesLeft()
        {
            Rotate(-90);

        }
        public void Rotate90DegreesRight()
        {
            Rotate(90);
        }
        public void Rotate180Degrees()
        {
            Rotate(180);
        }
        private void Rotate(int degrees)
        {
            Direction = (Direction)(((int)Direction + (degrees % 360) + 360) % 360);
        }
    }
}
