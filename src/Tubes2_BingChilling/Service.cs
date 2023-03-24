using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlTypes;
using System.IO;
using TileSpace;
using UtilitySpace;

namespace Services
{
    public abstract class BaseFS
    {
        protected List<Tuple<string, int, int>> path;
        protected List<Tuple<int,int,int>> pathHistory;
        protected List<Tile> tiles;
        protected List<Tile> treasure;
        protected Tile start;
        protected Tile home;
        
        /* Constructor */
        public BaseFS(string pathFile)
        {
            Tiles input = new Tiles();
            input.parserFile(pathFile);
            pathHistory = new List<Tuple<int, int, int>>();
            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
            home = start;
        }

        /* Getter */
        /* Method : return the result path with format (value, count, x, y)*/
        public List<Tuple<string, int, int, int>> getResultPath()
        {
            List<Tuple<string, int, int, int>> pathWithCount = new List<Tuple<string, int, int, int>>();
            foreach (Tuple<string, int, int> tuple in this.path)
            {
                int appearence = 0;
                for (int i = 0; i < pathWithCount.Count; i++)
                {
                    if (tuple.Item2 == pathWithCount[i].Item3 && tuple.Item3 == pathWithCount[i].Item4)
                    {
                        appearence++;
                    }
                }

                Tuple<string, int, int, int> temp = new Tuple<string, int, int, int>(tuple.Item1, appearence+1, tuple.Item2, tuple.Item3);
                pathWithCount.Add(temp);
            }
            return pathWithCount;
        }

        /* Method : return the result path with format (count, x, y)*/
        public List<Tuple<int,int,int>> getHistoryPath()
        {
            return this.pathHistory;
        }

        /* Method : add the tile to the path */
        public void addTile2History(Tile tile)
        {
            bool isExist = false;
            for(int i = 0; i < this.pathHistory.Count; i++)
            {
                if (this.pathHistory[i].Item2 == tile.getCoordinate(0) && this.pathHistory[i].Item3 == tile.getCoordinate(1))
                {
                    pathHistory.Add(new Tuple<int, int, int>(this.pathHistory[i].Item1 + 1, tile.getCoordinate(0), tile.getCoordinate(1)));
                    isExist = true;
                    break;
                }
            }
            if (!isExist)
            {
                pathHistory.Add(new Tuple<int, int, int>(1, tile.getCoordinate(0), tile.getCoordinate(1)));
            }
        }

        //public int getNodes()
        //{
        //    List<Tuple<string, int, int, int>> pathWithCount
        //}

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
            foreach(Tuple <string, int, int> tuple in this.path)
            {
                if(tuple.Item1 == "Found" && tuple.Item2 != home.getCoordinate(0) && tuple.Item3 != home.getCoordinate(1))
                {
                    Console.WriteLine("Back to Home");
                    this.path.Remove(tuple);
                    break;
                }
            }
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
                start.hasVisited();
                while (queue.Count != 0)
                {
                    Tile tile = queue.Dequeue(); // dequeue the tile
                    addTile2History(tile);
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

                    else // if the tile is not active treasure
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
        //private string lastTile;

        /* Constructor */
        public DFS(string inputPath) : base(inputPath)
        {
            stack = new Stack<Tile>();
            //lastTile = null;
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

        public void visitDFS(Tile tile, string direction)
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
                    adjTile.setOrigin(direction);
                }
            }
            catch (NullReferenceException ex) // if the tile is null , do nothing
            {
                // do nothing
            }
        }

        public void refreshPath()
        {
            foreach (Tile tile in tiles)
            {
                tile.resetPath();
            }
            stack = new Stack<Tile>();
        }

        /* I.S : stack is empty */
        /* F.S : stack is not empty */
        /* Method : run DFS, find path from KrustyKrab to all treasures with DFS algorithm */
        public override void run()
        {
            int count = this.treasure.Count; // count the number of treasure

            for (int i = 0; i < count; i++) // run BFS for each treasure
            {
                stack.Push(start); // input the start tile to stack
                start.hasVisited();
                while (stack.Count != 0)
                {
                    Tile tile = stack.Pop(); // pop the tile
                    tile.hasVisited();
                    if (tile != start)
                    {
                        tile.addPath(tile.getTileDirect(), tile.getDirect());
                    }

                    addTile2History(tile);

                    if (treasure.Contains(tile)) // if the tile is treasure
                    {
                        List<Tuple<string, int, int>> tempPath = tile.getPath(); // get the path from the tile
                        treasure.Remove(tile); // remove the treasure from the list
                        this.appendPath(tempPath); // add the path to the path

                        if (treasure.Count == 0) // if there is no treasure left
                        {
                            path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                        }

                        this.refreshPath();// refresh the tiles path
                        this.start = tile; // set the start tile to the treasure tile
                        break;
                    }


                    if (tile.getOrigin() == null)
                    {
                        visitDFS(tile, "Left");
                        visitDFS(tile, "Up");
                        visitDFS(tile, "Right");
                        visitDFS(tile, "Down");
                    } 
                    else
                    {
                        if (tile.getOrigin() == "Right")
                        {
                            inputTile(tile.getLeft());
                            tile.getLeft().setOrigin("Left");

                            visitDFS(tile, "Up");
                            visitDFS(tile, "Right");
                            visitDFS(tile, "Down");
                        } 

                        else if (tile.getOrigin() == "Down")
                        {
                            inputTile(tile.getUp());
                            tile.getUp().setOrigin("Up");

                            visitDFS(tile, "Left");
                            visitDFS(tile, "Right");
                            visitDFS(tile, "Down");
                        }

                        else if(tile.getOrigin() == "Left")
                        {
                            inputTile(tile.getRight());
                            tile.getRight().setOrigin("Right");

                            visitDFS(tile, "Left");
                            visitDFS(tile, "Up");
                            visitDFS(tile, "Down");
                        }

                        else if(tile.getOrigin() == "Up")
                        {
                            inputTile(tile.getDown());
                            tile.getDown().setOrigin("Down");

                            visitDFS(tile, "Left");
                            visitDFS(tile, "Up");
                            visitDFS(tile, "Right");
                        }
                    }

                }
            }
        }

    }
}