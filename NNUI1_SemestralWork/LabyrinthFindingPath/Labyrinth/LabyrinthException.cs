using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    public class LabyrinthException : Exception
    {
        public LabyrinthException(string message) : base(message) { }
    }
}
