using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class Agent
    {
        public Position Position { get; set; }
        public static int CostOfRotate90 { get; set; }
        public static int CostOfRotate180 { get; set; }
        public static int CostOfStepForward { get; set; }

        public Agent(Position position)
        {
            Position = position;
            CostOfRotate90 = 2;
            CostOfRotate180 = 3;
            CostOfStepForward = 5;
        }
    }
}