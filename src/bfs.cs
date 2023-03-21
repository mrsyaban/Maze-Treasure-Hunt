using System;
using System.Collections.Generic;
using TileSpace;
using UtilitySpace;

namespace bfsSpace{
    public class bfs{
        protected List<Tile> tiles;
        protected Queue<Tile> queue;
        protected Tile start;
        protected List<Tile> treasure;
        protected List<Tuple<string, int, int>> path;

        public bfs(Tiles input){
            queue = new Queue<Tile>();
            path = new List<Tuple<string, int, int>>();
            tiles = input.getTiles();
            treasure = input.getTreasure();
            start = input.getStart();
        }

        public int getTreasureCount(){
            return treasure.Count;
        }

        public void appendPath(List<Tuple<string,int,int>> pathOther){
            foreach(Tuple<string,int,int> tuple in pathOther){
                path.Add(tuple);
            }
        }

        public List<Tuple<string,int,int>> getAppendedPath(){
            return path;
        }

        public void setStart(Tile start){
            this.start = start;
        }

        public void refresh(){
            foreach(Tile tile in tiles){
                tile.reset();
            }
            queue = new Queue<Tile>();
        }

        

        public void BFS(){
            queue.Enqueue(start);
            start.hasVisited();
            while(queue.Count != 0){
                Tile tile = queue.Dequeue();
                tile.hasVisited();
                if(treasure.Contains(tile)){
                    List<Tuple<string,int,int>> path = tile.getPath();
                    treasure.Remove(tile);
                    if(treasure.Count == 0){
                        path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate(0), tile.getCoordinate(1)));
                    }
                    this.appendPath(path);
                    this.refresh();
                    this.setStart(tile);
                    break;         
                }else{
                    if(tile.getDown() != null){
                        if(!tile.getDown().isVisited()){
                            queue.Enqueue(tile.getDown());
                            tile.getDown().hasVisited();
                            tile.getDown().addPath(tile,"Down");
                        }

                    }
                    if(tile.getRight() != null){
                        if(!tile.getRight().isVisited()){
                            queue.Enqueue(tile.getRight());
                            tile.getRight().hasVisited();
                            tile.getRight().addPath(tile,"Right");
                        }
                    }
                    if(tile.getUp() != null){
                        if(!tile.getUp().isVisited()){
                            queue.Enqueue(tile.getUp());
                            tile.getUp().hasVisited();
                            tile.getUp().addPath(tile,"Up");
                        }
                    }
                    if(tile.getLeft() != null){
                        if(!tile.getLeft().isVisited()){
                            queue.Enqueue(tile.getLeft());
                            tile.getLeft().hasVisited();
                            tile.getLeft().addPath(tile,"Left");
                        }
                    }
                }
            }
        }

        public void startfind(){
            int count = this.getTreasureCount();
            for(int i = 0; i < count; i++){
                this.BFS();
            }
        }

        public void printStep(){
            foreach (Tuple<string, int, int> tuple in path)
            {
                Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + " " + tuple.Item3);
            }
        }

        public static void Main(string[] args){
            Tiles tiles = new Tiles();
            tiles.parserFile("test/input2.txt");
            bfs BFS = new bfs(tiles);
            BFS.startfind();
            BFS.printStep();
        }
    }
}