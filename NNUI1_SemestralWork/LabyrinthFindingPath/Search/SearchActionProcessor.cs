using System;
using System.Collections.Generic;
using System.Linq;

namespace LabyrinthFindingPath.Search
{
    class SearchActionProcessor
    {
        public Action[] Actions { get; }
        private Labyrinth Labyrinth { get; }

        public SearchActionProcessor(Labyrinth labyrinth)
        {
            Labyrinth = labyrinth;
            Actions = (Action[])Enum.GetValues(typeof(Action));
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
        private bool IsValidAction(Action action, Position position)
        {
            Position tempPosition = new Position(position);
            switch (action)
            {
                case Action.ROTATE_90_LEFT:
                    tempPosition.Rotate90DegreesLeft();
                    return IsStepValidInDirection(tempPosition);
                case Action.ROTATE_90_RIGHT:
                    tempPosition.Rotate90DegreesRight();
                    return IsStepValidInDirection(tempPosition);
                case Action.ROTATE_180:
                    tempPosition.Rotate180Degrees();
                    return IsStepValidInDirection(tempPosition);
                case Action.STEP_FORWARD:
                    return IsStepValidInDirection(tempPosition);
                default:
                    throw new SearchException("Unknown action!");
            }
        }
        private AStarNode ApplyAction(Action action, AStarNode nodeOrigin)
        {
            AStarNode node = new AStarNode(nodeOrigin, action);
            switch (action)
            {
                case Action.ROTATE_90_LEFT:
                    node.Position.Rotate90DegreesLeft();
                    node.PathCost += Agent.CostOfRotate90;
                    node.Position.StepForward();
                    break;
                case Action.ROTATE_90_RIGHT:
                    node.Position.Rotate90DegreesRight();
                    node.PathCost += Agent.CostOfRotate90;
                    node.Position.StepForward();
                    break;
                case Action.ROTATE_180:
                    node.Position.Rotate180Degrees();
                    node.PathCost += Agent.CostOfRotate180;
                    node.Position.StepForward();
                    break;
                case Action.STEP_FORWARD:
                    node.Position.StepForward();
                    node.PathCost += Agent.CostOfStepForward;
                    break;
                default:
                    throw new SearchException("Unknown action!");
            }
            return node;
        }
        private bool IsStepValidInDirection(Position position)
        {
            switch (position.Direction)
            {
                case Direction.NORTH:
                    if (position.Row - 1 >= 0)
                    {
                        return Labyrinth.LabyrinthMap[position.Row - 1, position.Column];
                    }
                    break;
                case Direction.EAST:
                    if (position.Column + 1 < Labyrinth.Columns)
                    {
                        return Labyrinth.LabyrinthMap[position.Row, position.Column + 1];
                    }
                    break;
                case Direction.SOUTH:
                    if (position.Row + 1 < Labyrinth.Rows)
                    {
                        return Labyrinth.LabyrinthMap[position.Row + 1, position.Column];
                    }
                    break;
                case Direction.WEST:
                    if (position.Column - 1 >= 0)
                    {
                        return Labyrinth.LabyrinthMap[position.Row, position.Column - 1];
                    }
                    break;
                default:
                    throw new SearchException("Unknown direction!");
            }
            return false;
        }
    }
}