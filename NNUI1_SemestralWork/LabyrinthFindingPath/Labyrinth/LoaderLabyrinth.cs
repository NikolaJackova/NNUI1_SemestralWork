using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabyrinthFindingPath
{
    class LoaderLabyrinth
    {
        private Bitmap ImageLabyrinth { get; set; }

        public LoaderLabyrinth(Bitmap imageLabyrinth)
        {
            ImageLabyrinth = imageLabyrinth;
        }
        public bool[,] LoadLabyrinth()
        {
            bool[,] labyrinth = new bool[ImageLabyrinth.Height, ImageLabyrinth.Width];
            for (int x = 0; x < ImageLabyrinth.Width; x++)
            {
                for (int y = 0; y < ImageLabyrinth.Height; y++)
                {
                    if (ImageLabyrinth.GetPixel(x, y).ToArgb() == Color.White.ToArgb())
                    {
                        labyrinth[y, x] = true;
                    }
                }
            }
            return labyrinth;
        }
    }
}
