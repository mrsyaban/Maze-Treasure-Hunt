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
        /* Constructor */
        public BaseFS(Tiles input)
        {
            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
        }
        /* Getter */
        /* Method : return the path */
        protected List<Tuple<string, int, int>> getAppendedPath()
        {
            return path;
        }
        /* Method : add the path from other path */
        protected void appendPath(List<Tuple<string, int, int>> pathOther)
        {
            foreach (Tuple<string, int, int> tuple in pathOther)
            {
                path.Add(tuple);
            }
        }
        /* Method : print the path */
        public void printStep()
        {
            foreach (Tuple<string, int, int> tuple in path)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
            }
        }
        /* Method : visit the tile  while checking if it is visited or not */
        public void visit(Tile tile, string direction)
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
                    inputTile(adjTile);
                    adjTile.hasVisited();
                    adjTile.addPath(tile, direction);
                }
            }
            catch (NullReferenceException ex)
            {

            }
        }
        /* Method : add tile to stack */
        public abstract void inputTile(Tile tile);

        /* Method : reset all tile in tiles & clearing the stack */
        public abstract void refresh();
    }


    public class BFS : BaseFS
    {
        protected Queue<Tile> queue;
        /* Constructor */
        public BFS(Tiles input) : base(input)
        {
            queue = new Queue<Tile>();
        }
        /* I.S : queue is not empty */
        /* F.S : queue is empty */
        /* Method : reset all tile in tiles & clearing the queue */
        public override void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            queue = new Queue<Tile>();
        }
        /* I.S : queue is not empty */
        /* F.S : queue is not empty */
        /* Method : add tile to queue */
        public override void inputTile(Tile tile)
        {
            queue.Enqueue(tile);
        }
        /* I.S : queue is empty, start and treasure have defined */
        /* F.S : queue is empty */
        /* Methode : find path from KrustyKrab to all treasures with BFS algorithm */
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
        private Stack<Tile> stack;
        /* Constructor */
        public DFS(Tiles input) : base(input)
        {
            stack = new Stack<Tile>();
        }
        /* I.S : stack is not empty */
        /* F.S : stack is empty */
        /* Method : reset all tile in tiles & clearing the stack */
        public override void refresh()
        {
            foreach (Tile tile in tiles)
            {
                tile.reset();
            }
            stack = new Stack<Tile>();
        }
        /* I.S : stack is empty */
        /* F.S : stack is not empty */
        /* Method : input tile to stack */
        public override void inputTile(Tile tile)
        {
            stack.Push(tile);
        }

        /* I.S : stack is empty */
        /* F.S : stack is not empty */
        /* Method : run DFS, find path from KrustyKrab to all treasures with DFS algorithm */
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

    public class TSP : BFS
    {
        private Tile home;
        /* Constructor */
        public TSP(Tiles input) : base(input)
        {
            this.home = this.start;
        }
        /* I.S : home is the start tile */
        /* F.S : home is the end tile */
        /* Method : back to home */
        public void backtoHome()
        {
            this.treasure.Add(home);
            this.run();
        }
        /* I.S : home is the end tile */
        /* F.S : home is the start tile */
        /* Method : run TSP , find path from KrustyKrab to all treasures and back to KrustyKrab with BFS algorithm */
        public void runTSP()
        {
            // run BFS
            this.run();
            // back to home
            this.backtoHome();
        }

    }
}