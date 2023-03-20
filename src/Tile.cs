using System;
using System.Collections.Generic;



namespace TileSpace
{
    public class TileClass
    {
        private int[] coordinate;
        private string value;
        private bool visited;
        private List<Tuple<string, int, int>> path;
        private TileClass Down;
        private TileClass Right;
        private TileClass Up;
        private TileClass Left;
        /* Constructor */
        public TileClass() {
            coordinate = new int[2];
            value = "X";
            visited = false;
            path = new List<Tuple<string, int, int>>();
            Down = null;
            Right = null;
            Up = null;
            Left = null;
        }

        /* Getter */
        public string getValue(){
            return value;
        }

        public bool isVisited(){
            return visited;
        }

        public List<Tuple<string, int, int>> getPath(){
            return path;
        }

        public int[] getCoordinate(){
            return coordinate;
        }
        public TileClass getDown(){
            return Down;
        }
        public TileClass getRight(){
            return Right;
        }
        public TileClass getUp(){
            return Up;
        }
        public TileClass getLeft(){
            return Left;
        }

        /* Setter */
        public void setCoordinate(int[] coor){
            if (coor != null)
            {
                coordinate = coor;
            }
            else
            {
                // Handle the case where the parameter is null
                // You could throw an exception or set a default value here
            }
        }

        public void hasVisited(){
            visited = true;
        }

        public void addPath(TileClass tile, string direction){
            List<Tuple<string, int, int>> path = tile.getPath();
            foreach(Tuple<string, int, int> tuple in path){
                this.path.Add(tuple);
            }
            this.path.Add(new Tuple<string, int, int>(direction, tile.getCoordinate()[0], tile.getCoordinate()[1]));
        }
        public void setValue(char val){
            value = val;
        }
        public void setDown(TileClass tile){
            Down = tile;
        }
        public void setRight(TileClass tile){
            Right = tile;
        }
        public void setUp(TileClass tile){
            Up = tile;
        }
        public void setLeft(TileClass tile){
            Left = tile;
        }


        /* Utility Function */

        public void printInfo(){
            Console.WriteLine("Coordinate: " + coordinate[0] + "," + coordinate[1]);
            Console.WriteLine("Value: " + value);
            Console.WriteLine("Visited: " + visited);
            Console.WriteLine("Path: ");
            if(path != null){
                foreach(Tuple<string, int, int> tuple in path){
                    Console.WriteLine(tuple.Item1 + " " + tuple.Item2 + "," + tuple.Item3);
                }
            }
            if(Down != null){
                Console.WriteLine("Down: " + Down.getCoordinate()[0] + "," + Down.getCoordinate()[1]);
            }
            if(Right != null){
                Console.WriteLine("Right: " + Right.getCoordinate()[0] + "," + Right.getCoordinate()[1]);
            }
            if(Up != null){
                Console.WriteLine("Up: " + Up.getCoordinate()[0] + "," + Up.getCoordinate()[1]);
            }
            if(Left != null){
                Console.WriteLine("Left: " + Left.getCoordinate()[0] + "," + Left.getCoordinate()[1]);
            }
        }

        public void reset(){
            visited = false;
            path = new List<Tuple<string, int, int>>();
        }



    }
}