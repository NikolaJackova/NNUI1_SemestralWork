using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthFindingPath.Search
{
    class AStarSearch
    {
        public SearchSystemController AStarSearchSystem { get; set; }
        public SimplePriorityQueue<AStarNode> Fringe { get; set; }
        public AStarNode InitNode { get; set; }
        public IList<AStarNode> Explored { get; set; }

        public AStarSearch(AStarNode initNode, Position finalPosition, Labyrinth labyrinth)
        {
            AStarSearchSystem = new SearchSystemController(finalPosition, labyrinth);
            InitNode = initNode;
            Fringe = new SimplePriorityQueue<AStarNode>();
            Explored = new List<AStarNode>();
        }

        public Stack<AStarNode> Search(out int iteration)
        {
            iteration = 0;
            Fringe.Enqueue(InitNode, InitNode.PathTotal);
            while (Fringe.Count != 0)
            {
                AStarNode node = Fringe.Dequeue();
                Explored.Add(node);
                IList<AStarNode> children = AStarSearchSystem.Successor(node);
                foreach (var item in children)
                {
                    if (!Explored.Any(nodeInCollection => item.Equals(nodeInCollection)) && !Fringe.Any(nodeInCollection => item.Equals(nodeInCollection)))
                    {
                        AStarSearchSystem.EvaluateNodeWithHeuristic(item);
                        Fringe.Enqueue(item, item.PathTotal);
                    }
                }
                if (AStarSearchSystem.IsFinalState(node))
                {
                    AStarSearchSystem.EvaluateNodeWithHeuristic(node);
                    Stack<AStarNode> aStarNodePath = new Stack<AStarNode>();
                    AStarSearchSystem.ReconstructPath(node, aStarNodePath);
                    return aStarNodePath;
                }
                iteration++;
            }
            throw new LabyrinthException("Unreachable position!");
        }
    }
}