using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath.Search
{
    class AStarNode
    {
        private static int id = 1;
        public int Id { get; set; }
        public AStarNode Parent { get; set; }
        public Action Action { get; set; }
        public Position Position { get; set; }
        public int Depth { get; set; }
        public int PathEval { get; set; }
        public int PathCost { get; set; }
        public int PathTotal { get { return PathCost + PathEval; } }
        public AStarNode(AStarNode parent, Position position, Action action, int depth)
        {
            Id = id++;
            Parent = parent;
            Position = position;
            Action = action;
            Depth = depth;
        }
        public AStarNode(AStarNode node)
        {
            Id = id++;
            Parent = node;
            Position = node.Position;
            Depth = node.Depth + 1;
        }
    }
}
