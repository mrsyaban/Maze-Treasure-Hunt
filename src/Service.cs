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
        protected Tile home;
        
        /* Constructor */
        public BaseFS(string path)
        {
            Tiles input = new Tiles();
            input.parserFile(pathFile);

            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
            home = start;
        }

        /* Getter */
        /* Method : return the path */
        public List<Tuple<string, int, int, int>> getResultPath()
        {
            List<Tuple<string, int, int, int>> pathWithCount = new List<Tuple<string, int, int, int>>();
            foreach (Tuple<string, int, int> tuple in this.path)
            {
                bool isExist = false;
                for (int i = 0; i < pathWithCount.Count; i++)
                {
                    if (tuple.Item2 == pathWithCount[i].Item3 && tuple.Item3 == pathWithCount[i].Item4)
                    {
                        isExist = true;
                    }

                    if (isExist)
                    {
                        pathWithCount[i] = new Tuple<string, int, int, int>(pathWithCount[i].Item1, pathWithCount[i].Item2 + 1, pathWithCount[i].Item3, pathWithCount[i].Item4);
                        break;

                    }

                }
                if (!isExist)
                {
                    Tuple<string, int, int, int> tempCount = new Tuple<string, int, int, int>(tuple.Item1, 1, tuple.Item2, tuple.Item3);
                    pathWithCount.Add(tempCount);
                }
            }
            return pathWithCount;
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
        /* Method : visit the tile while checking if it is visited or not */
        public void visit(Tile tile, string direction)
        {
            try
            {
                Tile adjTile;
                if (direction == "Down") // if direction is down , move to the down tile
                {
                    adjTile = tile.getDown();

                }
                else if (direction == "Right") // if direction is right , move to the right tile
                {
                    adjTile = tile.getRight();

                }
                else if (direction == "Left") // if direction is left , move to the left tile
                {
                    adjTile = tile.getLeft();

                }
                else // if direction is up , move to the up tile
                {
                    adjTile = tile.getUp();
                }

                if (!adjTile.isVisited()) // if the tile is not visited , visit it
                {
                    inputTile(adjTile); // input the tile to the stack or queue
                    adjTile.hasVisited(); // mark the tile as visited
                    adjTile.addPath(tile, direction); // add the path to the tile
                }
            }
            catch (NullReferenceException ex) // if the tile is null , do nothing
            {
                // do nothing
            }
        }

        /* Abstract Method : add tile to stack */
        public abstract void inputTile(Tile tile);

        /* Abstract Method : reset all tile in tiles & clearing the stack */
        public abstract void refresh();

        /* Abstract Method : searching algorithm */
        public abstract void run();


        /* TSP Algorithm : back to home after get all the treasure */
        public void backtoHome()
        {
            this.treasure.Add(home);
            this.refresh();
            this.run();
        }

        public void runTSP()
        {
            // run dfs/bfs
            this.run();
            // back to home
            this.backtoHome();
        }
    }


    public class BFS : BaseFS
    {
        protected Queue<Tile> queue;
        /* Constructor */
        public BFS(string inputPath) : base(inputPath)
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
        public override void run()
        {
            int count = this.treasure.Count; // count the number of treasure

            for (int i = 0; i < count; i++) // run BFS for each treasure
            {
                queue.Enqueue(start); // input the start tile to queue
                start.hasVisited(); // mark the start tile as visited
                while (queue.Count != 0)
                {
                    Tile tile = queue.Dequeue(); // dequeue the tile
                    if (treasure.Contains(tile)) // if the tile is treasure
                    {
                        List<Tuple<string, int, int>> path = tile.getPath(); // get the path from the tile
                        treasure.Remove(tile); // remove the treasure from the list
                        if (treasure.Count == 0) // if there is no treasure left
                        {
                            path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                        }
                        this.appendPath(path); // add the path to the path
                        this.refresh(); // refresh the tiles
                        this.start = tile; // set the start tile to the treasure tile
                        break;
                    }

                    else // if the tile is not treasure
                    {
                        visit(tile, "Down"); // visit the down tile
                        visit(tile, "Right"); // visit the right tile
                        visit(tile, "Up"); // visit the up tile
                        visit(tile, "Left"); // visit the left tile
                    }
                }
            }
        }
    }

    public class DFS : BaseFS
    {
        /* Attribute */
        private Stack<Tile> stack;

        /* Constructor */
        public DFS(string inputPath) : base(inputPath)
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
        public override void run()
        {
            int count = this.treasure.Count; // count the number of treasure

            for (int i = 0; i < count; i++)
            {
                stack.Push(start); // input the start tile to stack
                start.hasVisited(); // mark the start tile as visited

                // if there is still element in stack
                while (stack.Count != 0)
                {
                    Tile tile = stack.Pop(); // pop the tile
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