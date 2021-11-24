using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    class Program
    {
        public static void WritePath(Stack<Search.AStarNode> path)
        {
            foreach (var item in path)
            {
                Console.WriteLine(item.ToString()); ;
            }
        }
        static void Main(string[] args)
        {
            Labyrinth lab = new Labyrinth(Properties.Resources.Bludiste1, new Position(1,1));
            Search.AStarSearch aStar = new Search.AStarSearch(lab, new Position(7, 3));
            Stack<Search.AStarNode> path = aStar.SearchPath(out int iteration);
            Console.WriteLine(iteration);
            WritePath(path);
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(lab.ToString());
            Console.ReadKey();
        }
    }
}
