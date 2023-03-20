using System;
using System.Collections;
using System.IO;
using System.Runtime.Intrinsics.X86;
using System.Windows.Documents;
using TileSpace;
using UtilitySpace;

namespace DfsSpace
{
    class DfsClass
    {
        protected Stack<Tile> stack;
        protected List<Tile> treasure;
        protected List<Tuple<string, int, int>> path; 

        public DfsClass(Tiles tiles) {
            queue = new Queue<Tile>();
            treasure = new List<Tile>();
            path = new List<Tuple<string, int, int>>();
                
            int rows = tiles.getMatrix().GetLength(0);
            int cols = tiles.getMatrix().GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (tiles.getMatrix(i,j) != "X")
                    {
                        Tile tile = new Tile(tiles.getMatrix(i,j), i, j)
                        tiles.addTile(tile);

                        if (tiles.matrix[i, j] == "K")
                        {
                            start = tile;
                        }
                        else if (tiles.matrix[i, j] == "T")
                        {
                            treasure.Add(tile);
                        }
                        else
                        {
                            /* Do nothing */
                        }
                    }
                }
            }
        }

        public void DFS()
        {
            

        }
    }
}