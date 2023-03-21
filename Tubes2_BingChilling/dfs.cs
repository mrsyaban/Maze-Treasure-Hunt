using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using TileSpace;
using UtilitySpace;

namespace DfsSpace
{
    class DfsClass
    {
        protected Stack<Tile> stack;
        protected List<Tuple<string, int, int>> path;
        protected List<Tile> tiles;
        protected List<Tile> treasure;
        protected Tile start;

        public DfsClass(Tiles input) {
            stack = new Stack<Tile>();
            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
        }

        public void appendPath(List<Tuple<string, int, int>> pathOther)
        {
            foreach (Tuple<string, int, int> tuple in pathOther)
            {
                path.Add(tuple);
            }
        }

        public List<Tuple<string, int, int>> getAppendedPath()
        {
            return path;
        }

        /* reset all tile in tiles & clearing the stack */
        private void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            stack = new Stack<Tile>();
        }

        private void visit(Tile tile, string direction)
        {
            try
            {
                Tile adjTile;
                if (direction == "Down")
                {
                    adjTile = tile.getDown();

                }
                else if (direction == "Right")
                {
                    adjTile = tile.getRight();

                }
                else if (direction == "Left")
                {
                    adjTile = tile.getLeft();

                }
                else
                {
                    adjTile = tile.getUp();
                }

                if (!adjTile.isVisited())
                {
                    stack.Push(adjTile);
                    adjTile.hasVisited();
                    adjTile.addPath(tile, direction);
                }
            }
            catch (NullReferenceException ex) 
            { 
            
            }
        }

        public void DFS()
        {
            stack.Push(start);
            start.hasVisited();

            // if there is still element in stack
            while (stack.Count != 0)
            {
                Tile tile = stack.Pop();
                tile.hasVisited();

                // if The Tile is Treasure
                if (treasure.Contains(tile)) {
                    List<Tuple<string, int, int>> path = tile.getPath();
                    treasure.Remove(tile);

                    // Last Treasure
                    if (treasure.Count == 0)
                    {
                        path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                    }
                    if (tile.getDown() != null)
                    {
                        Console.WriteLine("Mulai");
                        Console.Write("Sebelum : "+tile.getValue() + " " + tile.isVisited() + "\n");
                        Console.Write(tile.getDown().getUp().getValue() + " " + tile.isVisited() + "\n");
                    }

                    this.appendPath(path);
                    this.refresh();
                    if (tile.getDown() != null)
                    {
                        Console.Write(tile.getValue() + " " + tile.isVisited() + "\n");
                        Console.Write(tile.getDown().getUp().getValue() + " " + tile.isVisited() + "\n");
                        Console.WriteLine("Selesai");
                    }
                    this.start = start;
                    break;

                // if The Tile is not Treasure, visit all the adjacent
                } else {
                    visit(tile, "Left");
                    visit(tile, "Up");
                    visit(tile, "Right");
                    visit(tile, "Down");
                }
            }

        }

        public void startfind()
        {
            int count = this.treasure.Count;
            for (int i = 0; i < count; i++)
            {
                this.DFS();
            }
        }

        public void printStep()
        {
            foreach (Tuple<string, int, int> tuple in path)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
            }
        }

        public static void Main(string[] args)
        {

            Tiles tiles = new Tiles();
            tiles.parserFile("test/input2.txt");
            tiles.printMatrix();
            DfsClass DFS = new DfsClass(tiles);
            DFS.startfind();
            DFS.printStep();
        }
    }
}