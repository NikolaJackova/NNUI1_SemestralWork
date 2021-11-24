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
        public int Id { get; }
        public AStarNode Parent { get; }
        public Action? Action { get; }
        public Position Position { get; }
        public int Depth { get; }
        public int PathEval { get; set; }
        public int PathCost { get; set; }
        public int PathTotal { get { return PathCost + PathEval; } }
        public AStarNode(Position position)
        {
            Id = id++;
            Position = position;
            Parent = null;
            Action = null;
            Depth = 0;
        }
        public AStarNode(AStarNode node, Action? action)
        {
            Id = id++;
            Parent = node;
            Position = new Position(node.Position);
            Depth = node.Depth + 1;
            Action = action;
            PathCost = node.PathCost;
        }
        public bool Equals(AStarNode node)
        {
            return Position.Equals(node.Position);
        }
        public override string ToString()
        {
            return "{" + Action + ": " + Position.ToString() + "\n" + "Path eval: " + PathEval + ", path cost: " + PathCost + ", path total: " + PathTotal + "}";
        }
    }
}
