using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthFindingPath.Search
{
    class SearchSystemController
    {
        public readonly int Rows;
        public readonly int Columns;
        public Position FinalPosition { get; set; }
        public Labyrinth Labyrinth { get; set; }
        public Action[] Actions { get; set; }

        public SearchSystemController(Position finalPosition, Labyrinth labyrinth)
        {
            Labyrinth = labyrinth;
            FinalPosition = finalPosition;
            Rows = labyrinth.Rows;
            Columns = labyrinth.Columns;
            Actions = (Action[])Enum.GetValues(typeof(Action));
        }
        public bool IsFinalState(AStarNode node)
        {
            return FinalPosition.EqualsCoordinates(node.Position);
        }
        public bool IsValidAction(Action action, Position position)
        {
            if (action == Action.STEP_FORWARD)
            {
                switch (position.Direction)
                {
                    case Direction.NORTH:
                        return Labyrinth.LabyrinthMap[position.Row - 1, position.Column];
                    case Direction.EAST:
                        return Labyrinth.LabyrinthMap[position.Row, position.Column + 1];
                    case Direction.SOUTH:
                        return Labyrinth.LabyrinthMap[position.Row + 1, position.Column];
                    case Direction.WEST:
                        return Labyrinth.LabyrinthMap[position.Row, position.Column - 1];
                    default:
                        throw new SearchException("Unknown direction!");
                }
            }
            return true;
        }

        public AStarNode ApplyAction(Action action, AStarNode nodeOrigin)
        {
            AStarNode node = new AStarNode(nodeOrigin);
            switch (action)
            {
                case Action.ROTATE_90_LEFT:
                    node.Position.Rotate90DegreesLeft();
                    node.PathCost += Agent.CostOfRotate90;
                    break;
                case Action.ROTATE_90_RIGHT:
                    node.Position.Rotate90DegreesRight();
                    node.PathCost += Agent.CostOfRotate90;
                    break;
                case Action.ROTATE_180:
                    node.Position.Rotate180Degrees();
                    node.PathCost += Agent.CostOfRotate180;
                    break;
                case Action.STEP_FORWARD:
                    node.Position.StepForward();
                    node.PathCost += Agent.CostOfStepForward;
                    break;
                default:
                    break;
            }
            return node;
        }
        public IList<AStarNode> Successor(AStarNode node)
        {
            IList<AStarNode> nodes = new List<AStarNode>();
            for (int i = 0; i < Actions.Length; i++)
            {
                Action action = Actions[i];
                if (IsValidAction(action, node.Position))
                {
                    nodes.Add(ApplyAction(action, node));
                }
            }
            return nodes;
        }

        public void ReconstructPath(AStarNode node, Stack<AStarNode> path)
        {
            path.Push(node);
            if (node.Parent == null)
            {
                return;
            }
            ReconstructPath(node.Parent, path);
        }
        public void EvaluateNodeWithHeuristic(AStarNode item)
        {
            int distance = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    distance += Math.Abs(i - FinalPosition.Row) + Math.Abs(j - FinalPosition.Column);
                }
            }
            item.PathEval = distance;
        }
    }
}