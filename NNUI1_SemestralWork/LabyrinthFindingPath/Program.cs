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
            Labyrinth l = new Labyrinth(Properties.Resources.Bludiste1, new int[] { 1,1}, new int[] {1,3});
            Search.AStarSearch aStar = new Search.AStarSearch(new Search.AStarNode(null, new Position(1, 1, Direction.NORTH), null, 0), new Position(1, 20), l);
            Stack<Search.AStarNode> path = aStar.Search(out int iteration);
            Console.WriteLine(iteration);
            WritePath(path);
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(l.ToString());
            Console.ReadKey();
        }
    }
}
