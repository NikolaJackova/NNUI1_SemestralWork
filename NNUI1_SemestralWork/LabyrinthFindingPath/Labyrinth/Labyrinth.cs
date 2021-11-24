using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class Labyrinth
    {
        private LoaderLabyrinth LoaderLabyrinth { get; }
        public bool[,] LabyrinthMap { get; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Agent Agent { get; set; }
        public Labyrinth(Bitmap image, Position startPosition)
        {
            LoaderLabyrinth = new LoaderLabyrinth(image);
            LabyrinthMap = LoaderLabyrinth.LoadLabyrinth();
            SetAttributes(startPosition);
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    builder.Append(LabyrinthMap[i, j] ? "  " : "▓▓");
                }
                builder.Append("\n");
            }
            return builder.ToString();
        }
        private void SetAttributes(Position startPosition)
        {
            if (!CheckValidPosition(startPosition))
            {
                throw new LabyrinthException("Start or destination position is not valid position!");
            }
            Agent = new Agent(startPosition);
            Rows = LabyrinthMap.GetLength(0);
            Columns = LabyrinthMap.GetLength(1);
        }

        private bool CheckValidPosition(Position position)
        {
            return LabyrinthMap[position.Row, position.Column] != false;
        }
    }
}
