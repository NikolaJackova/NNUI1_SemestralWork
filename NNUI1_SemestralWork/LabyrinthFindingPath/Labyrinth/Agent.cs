using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class Agent
    {
        private Position position;
        public Position Position { get { return position; } set { position = value; OnChangePosition(); } }
        public static int CostOfRotate90 { get; set; }
        public static int CostOfRotate180 { get; set; }
        public static int CostOfStepForward { get; set; }

        public event ChangePositionEventHandler ChangePosition;
        public delegate void ChangePositionEventHandler(object sender, EventArgs e);
        public Agent(Position position)
        {
            Position = position;
            CostOfRotate90 = 2;
            CostOfRotate180 = 3;
            CostOfStepForward = 5;
        }

        protected virtual void OnChangePosition()
        {
            ChangePosition?.Invoke(this, new EventArgs());
        }
    }
}