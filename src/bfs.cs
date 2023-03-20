using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TileSpace;
using UtilitySpace;

namespace bfsSpace{
    public class bfsClass{
        protected List<Tile> tiles;
        protected Queue<Tile> queue;
        protected Tile start;
        protected List<Tile> treasure;
        protected List<Tuple<string, int, int>> path;

        public bfsClass(string[,] matrix){
            tiles = new List<Tile>();
            queue = new Queue<Tile>();
            treasure = new List<Tile>();
            path = new List<Tuple<string, int, int>>();

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for(int i = 0; i < rows; i++){
                for(int j = 0; j < cols; j++){
                    if(matrix[i,j] != "X"){
                        Tile tile = new Tile(matrix[i,j], i, j);
                        tiles.Add(tile);
                        if(matrix[i,j] == "K"){
                            start = tile;
                        }else if(matrix[i,j] == "T"){
                            treasure.Add(tile);
                        }else{
                            /* Do nothing */X500DistinguishedName
                        }
                    }
                }
            }   
        }

        public void setTile(){
            foreach(Tile tile in tiles){
                int[] coor = tile.getCoordinate();
                int x = coor[0];
                int y = coor[1];
                foreach(Tile t in tiles){
                    int[] coor2 = t.getCoordinate();
                    int x2 = coor2[0];
                    int y2 = coor2[1];
                    if(x2 == x+1 && y2 == y){
                        tile.setDown(t);
                    }else if(x2 == x && y2 == y+1){
                        tile.setRight(t);
                    }else if(x2 == x-1 && y2 == y){
                        tile.setUp(t);
                    }else if(x2 == x && y2 == y-1){
                        tile.setLeft(t);
                    }else{
                        /* Do nothing */
                    }
                }
            }
        }

        public void printTile(){
            foreach(Tile tile in tiles){
                Console.WriteLine("Coordinate: " + tile.getCoordinate()[0] + "," + tile.getCoordinate()[1]);
                Console.WriteLine("Value: " + tile.getValue());
                if(tile.getDown() != null){
                    Console.WriteLine("Down: " + tile.getDown().getCoordinate()[0] + "," + tile.getDown().getCoordinate()[1]);
                }
                if(tile.getRight() != null){
                    Console.WriteLine("Right: " + tile.getRight().getCoordinate()[0] + "," + tile.getRight().getCoordinate()[1]);
                }
                if(tile.getUp() != null){
                    Console.WriteLine("Up: " + tile.getUp().getCoordinate()[0] + "," + tile.getUp().getCoordinate()[1]);
                }
                if(tile.getLeft() != null){
                    Console.WriteLine("Left: " + tile.getLeft().getCoordinate()[0] + "," + tile.getLeft().getCoordinate()[1]);
                }
                Console.WriteLine();
            }
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
                    if(treasure.Count != 0){
                        this.appendPath(path);
                    }else{
                        path.Add(new Tuple<string, int, int>("Found", tile.getCoordinate()[0], tile.getCoordinate()[1]));
                        this.appendPath(path);
                    }
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
            this.setTile();
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

            Matrix utility = new Matrix();
            utility.parserFile("test/input.txt");
            utility.printMatrix();

            bfsClass BFS = new bfsClass(utility.getMatrix());
            BFS.startfind();
            BFS.printStep();
        }
    }
}