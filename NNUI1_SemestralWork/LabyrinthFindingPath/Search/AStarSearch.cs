using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthFindingPath.Search
{
    public class AStarSearch
    {
        public Labyrinth Labyrinth { get; }
        public Position StartPositioin { get; set; }
        public Position FinalPosition { get; set; }
        private SearchActionProcessor AStarSystemActionProcessor { get; }
        private SimplePriorityQueue<AStarNode> Fringe { get; }
        private IList<AStarNode> Explored { get; }

        public AStarSearch(Labyrinth labyrinth, Position startPosition, Position finalPosition)
        {
            Labyrinth = labyrinth;
            if (!CheckValidStartAndEndPoint(startPosition, finalPosition))
            {
                throw new LabyrinthException("Start or destination position is not valid position!");
            }
            AStarSystemActionProcessor = new SearchActionProcessor(labyrinth);
            Fringe = new SimplePriorityQueue<AStarNode>();
            Explored = new List<AStarNode>();
            StartPositioin = startPosition;
            FinalPosition = finalPosition;
        }
        public Stack<AStarNode> SearchPath(out int iteration)
        {
            iteration = 0;
            Fringe.Enqueue(new AStarNode(StartPositioin), 0);
            while (Fringe.Count != 0)
            {
                AStarNode node = Fringe.Dequeue();
                Explored.Add(node);
                ProcessChildrenNodes(AStarSystemActionProcessor.Successor(node));
                if (FinalPosition.EqualsCoordinates(node.Position))
                {
                    node.PathEval = EvaluateWithHeuristic(node.Position);
                    Stack<AStarNode> aStarNodePath = new Stack<AStarNode>();
                    ReconstructPath(node, aStarNodePath);
                    Labyrinth.ApplyPathOnAgent(aStarNodePath);
                    return aStarNodePath;
                }
                iteration++;
            }
            throw new SearchException("Unreachable position!");
        }
        private void ProcessChildrenNodes(IList<AStarNode> children)
        {
            foreach (var childNode in children)
            {
                if (!Explored.Any(nodeInCollection => childNode.Equals(nodeInCollection)) && !Fringe.Any(nodeInCollection => childNode.Equals(nodeInCollection)))
                {
                    childNode.PathEval = EvaluateWithHeuristic(childNode.Position);
                    Fringe.Enqueue(childNode, childNode.PathTotal);
                }
            }
        }
        private int EvaluateWithHeuristic(Position position)
        {
            return Math.Abs(position.Row - FinalPosition.Row) + Math.Abs(position.Column - FinalPosition.Column);
        }
        private void ReconstructPath(AStarNode node, Stack<AStarNode> path)
        {
            path.Push(node);
            if (node.Parent == null)
            {
                return;
            }
            ReconstructPath(node.Parent, path);
        }
        private bool CheckValidStartAndEndPoint(Position startPosition, Position finalPosition)
        {
            return Labyrinth.CheckValidPosition(startPosition.Row, startPosition.Column) && Labyrinth.CheckValidPosition(finalPosition.Row, finalPosition.Column);
        }
    }
}