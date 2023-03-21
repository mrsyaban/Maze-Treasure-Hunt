using System;
using System.Collections.Generic;
using System.IO;
using TileSpace;
using UtilitySpace;

namespace DfsSpace
{
    class DfsClass
    {
        protected List<Tile> tiles;
        protected Stack<Tile> stack;
        protected List<Tile> treasure;
        protected List<Tuple<string, int, int>> path;
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

        public void setStart(Tile start)
        {
            this.start = start;
        }

        public void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            stack = new Stack<Tile>();
        }

        public void DFS()
        {
            stack.Push(start);
            start.hasVisited();
            while (stack.Count != 0)
            {
                Tile tile = stack.Pop();
                tile.hasVisited();
                if (treasure.Contains(tile))
                {
                    List<Tuple<string, int, int>> path = tile.getPath();
                    treasure.Remove(tile);
                    if (treasure.Count != 0)
                    {
                        this.appendPath(path);
                    }
                    else
                    {
                        path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                        this.appendPath(path);
                    }
                    this.refresh();
                    this.setStart(tile);
                    break;
                }
                else
                {
                    if (tile.getLeft() != null)
                    {
                        if (!tile.getLeft().isVisited())
                        {
                            stack.Push(tile.getLeft());
                            tile.getLeft().hasVisited();
                            tile.getLeft().addPath(tile, "Left");
                        }
                    }
                    if (tile.getUp() != null)
                    {
                        if (!tile.getUp().isVisited())
                        {
                            stack.Push(tile.getUp());
                            tile.getUp().hasVisited();
                            tile.getUp().addPath(tile, "Up");
                        }
                    }
                    if (tile.getRight() != null)
                    {
                        if (!tile.getRight().isVisited())
                        {
                            stack.Push(tile.getRight());
                            tile.getRight().hasVisited();
                            tile.getRight().addPath(tile, "Right");
                        }
                    }
                    if (tile.getDown() != null)
                    {
                        if (!tile.getDown().isVisited())
                        {
                            stack.Push(tile.getDown());
                            tile.getDown().hasVisited();
                            tile.getDown().addPath(tile, "Down");
                        }
                    }
                }
            }

        }

        public void startfind()
        {
            for (int i = 0; i < treasure.Count; i++)
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
            tiles.parserFile("test/input.txt");
            DfsClass DFS = new DfsClass(tiles);
            DFS.startfind();
            DFS.printStep();
        }
    }
}