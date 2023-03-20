using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using TileSpace;
using UtilitySpace;

namespace Services
{
    public abstract class BaseFS
    {
        protected List<Tuple<string, int, int>> path;
        protected List<Tile> tiles;
        protected List<Tile> treasure;
        protected Tile start;

        public BaseFS(Tiles input)
        {
            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
        }

        protected void appendPath(List<Tuple<string, int, int>> pathOther)
        {
            foreach (Tuple<string, int, int> tuple in pathOther)
            {
                path.Add(tuple);
            }
        }

        protected List<Tuple<string, int, int>> getAppendedPath()
        {
            return path;
        }


        public void printStep()
        {
            foreach (Tuple<string, int, int> tuple in path)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
            }
        }

        protected abstract void visit(Tile tile, string direction);

        /* reset all tile in tiles & clearing the stack */
        public abstract void refresh();
    }


    public class BFS : BaseFS
    {
        protected Queue<Tile> queue;

        public BFS(Tiles input) : base(input)
        {
            queue = new Queue<Tile>();
        }

        public override void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            queue = new Queue<Tile>();
        }

        protected override void visit(Tile tile, string direction)
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
                    queue.Enqueue(adjTile);
                    adjTile.hasVisited();
                    adjTile.addPath(tile, direction);
                }
            }
            catch (NullReferenceException ex)
            {

            }
        }

        public void run()
        {
            int count = this.treasure.Count;

            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(start);
                start.hasVisited();
                while (queue.Count != 0)
                {
                    Tile tile = queue.Dequeue();
                    tile.hasVisited();
                    if (treasure.Contains(tile))
                    {
                        List<Tuple<string, int, int>> path = tile.getPath();
                        treasure.Remove(tile);
                        if (treasure.Count == 0)
                        {
                            path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                        }
                        this.appendPath(path);
                        this.refresh();
                        this.start = tile;
                        break;
                    }

                    else
                    {
                        visit(tile, "Down");
                        visit(tile, "Right");
                        visit(tile, "Up");
                        visit(tile, "Left");
                    }
                }
            }
        }
    }

    public class DFS : BaseFS
    {
        protected Stack<Tile> stack;

        public DFS(Tiles input) : base(input)
        {
            stack = new Stack<Tile>();
        }

        public override void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            stack = new Stack<Tile>();
        }

        protected override void visit(Tile tile, string direction)
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

        public void run()
        {
            int count = this.treasure.Count;

            for (int i = 0; i < count; i++)
            {
                stack.Push(start);
                start.hasVisited();

                // if there is still element in stack
                while (stack.Count != 0)
                {
                    Tile tile = stack.Pop();
                    tile.hasVisited();

                    // if The Tile is Treasure
                    if (treasure.Contains(tile))
                    {
                        List<Tuple<string, int, int>> path = tile.getPath();
                        treasure.Remove(tile);

                        // Last Treasure
                        if (treasure.Count == 0)
                        {
                            path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                        }

                        this.appendPath(path);
                        this.refresh();
                        this.start = tile;
                        break;
                    }

                    // if The Tile is not Treasure, visit all the adjacent
                    else
                    {
                        visit(tile, "Left");
                        visit(tile, "Up");
                        visit(tile, "Right");
                        visit(tile, "Down");
                    }
                }
            }
        }
    }
}