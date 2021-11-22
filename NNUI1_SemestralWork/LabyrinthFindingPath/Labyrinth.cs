using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    class Labyrinth
    {
        private LoaderLabyrinth LoaderLabyrinth { get; }
        public bool[,] LabyrinthMap { get; set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Agent Agent { get; set; }
        public int[] DestinationPosition { get; set; } 
        public Labyrinth(Bitmap image, int[] startPosition, int[] destinationPosition)
        {
            LoaderLabyrinth = new LoaderLabyrinth(image);
            Load();
            SetAttributes(startPosition, destinationPosition);
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
        private void Load()
        {
            LabyrinthMap = LoaderLabyrinth.LoadLabyrinth();
        }
        private void SetAttributes(int[] startPosition, int[] destinationPosition)
        {
            if (!CheckValidPosition(startPosition) || !CheckValidPosition(destinationPosition))
            {
                throw new LabyrinthException("Start or destination position is not valid position!");
            }
            Agent = new Agent(startPosition[0], startPosition[1]);
            DestinationPosition = destinationPosition;
            Rows = LabyrinthMap.GetLength(0);
            Columns = LabyrinthMap.GetLength(1);
        }

        private bool CheckValidPosition(int[] position)
        {
            if (LabyrinthMap[position[0], position[1]] == false)
            {
                return false;
            }
            return true;
        }
    }
}
