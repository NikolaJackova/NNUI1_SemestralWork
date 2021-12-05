using LabyrinthFindingPath;
using LabyrinthFindingPath.Search;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthGUI
{
    public class FindingPath
    {
        public Labyrinth Labyrinth { get; set; }
        public AStarSearch AStarSearch { get; private set; }
        public Stack<AStarNode> Path { get; private set; }
        public int Iteration { get; private set; }
        private Stack<AStarNode> CopyPath { get; set; }
        public FindingPath(Bitmap image)
        {
            Labyrinth = new Labyrinth(image);
        }
        public void SetSearch(PointOfInterest start, PointOfInterest end)
        {
            Position startPosition = new Position(start.Row, start.Column);
            Position endPosition = new Position(end.Row, end.Column);
            Labyrinth.Agent = new Agent(startPosition);
            AStarSearch = new AStarSearch(Labyrinth, startPosition, endPosition);
        }
        public AStarNode GetPathItem()
        {
            if (CopyPath.Count==0)
            {
                CopyPath = Path;
                return null;
            } else
            {
                return CopyPath.Pop();
            }
        }
        public void Search()
        {
            Path = AStarSearch.SearchPath(out int iteration);
            CopyPath = Path;
            Iteration = iteration;
        }

        public void CheckValidPosition(int row, int column)
        {
            Labyrinth.CheckValidPosition(row, column);
        }
    }
}
