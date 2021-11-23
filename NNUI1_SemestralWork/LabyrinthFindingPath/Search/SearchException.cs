using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath.Search
{
    class SearchException : Exception
    {
        public SearchException(string message) : base(message) { }
    }
}
