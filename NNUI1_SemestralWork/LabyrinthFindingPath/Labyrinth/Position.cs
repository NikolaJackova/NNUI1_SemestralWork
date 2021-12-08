using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Direction Direction { get; set; }

        public Position(int row, int column, Direction direction = default)
        {
            Row = row;
            Column = column;
            Direction = direction;
        }
        public Position(Position position)
        {
            Row = position.Row;
            Column = position.Column;
            Direction = position.Direction;
        }
        public bool Equals(Position position)
        {
            return Row == position.Row && Column == position.Column && Direction == position.Direction;
        }
        public bool EqualsCoordinates(Position position)
        {
            return Row == position.Row && Column == position.Column;
        }
        public override string ToString()
        {
            return "[" + Row + ", " + Column + "], " + Direction;
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
