using LabyrinthFindingPath.Search;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public delegate void ApplyActionOnAgentDelegate(Position position);
    public class Labyrinth
    {
        public ApplyActionOnAgentDelegate ApplyAction { get; set; }
        private LoaderLabyrinth LoaderLabyrinth { get; }
        public bool[,] LabyrinthMap { get; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public Agent Agent { get; set; }
        public AStarSearch AStarSearch { get; set; }
        public Labyrinth(Bitmap image)
        {
            LoaderLabyrinth = new LoaderLabyrinth(image);
            LabyrinthMap = LoaderLabyrinth.LoadLabyrinth();
            Rows = LabyrinthMap.GetLength(0);
            Columns = LabyrinthMap.GetLength(1);
        }
        public void CheckValidPosition(int row, int column)
        {
            if (!LabyrinthMap[row, column])
            {
                throw new LabyrinthException("Start or destination position is not valid position!");
            }
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
        public void ApplyPathOnAgent(Stack<AStarNode> aStarNodePath)
        {
            foreach (var item in aStarNodePath)
            {
                Agent.Position = item.Position;
            }
        }
    }
}
