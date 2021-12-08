using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace LabyrinthGUI
{
    public class PointOfInterest
    {
        public Rectangle POI { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public PointOfInterest(Rectangle poi, int row, int column)
        {
            POI = poi;
            Row = row;
            Column = column;
        }
    }
}
