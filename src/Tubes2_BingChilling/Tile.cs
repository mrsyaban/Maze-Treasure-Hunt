using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace TileSpace
{
    public class Tile
    {
        private int[] coordinate; // [x,y]
        private string value; // R, K, or T
        private bool visited;
        private List<Tuple<string, int, int>> path; // Tile's path from current position
        private string origin;
        private string direct;


        /* Adjacency Tiles */
        private Tile Down; 
        private Tile Right;
        private Tile Up;
        private Tile Left;

        /* Constructor */
        public Tile() {
            coordinate = new int[2];
            value = "X";
            origin = null;
            visited = false;
            path = new List<Tuple<string, int, int>>();
            Down = null;
            Right = null;
            Up = null;
            Left = null;
        }

        public Tile(string value, int x, int y)
        {
            coordinate = new int[2];
            coordinate[0] = x; 
            coordinate[1] = y;
            this.value = value;
            visited = false;
            origin = null;
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

        public int getCoordinate(int i){
            return coordinate[i];
        }

        public Tile getDown(){
            return Down;
        }
        public Tile getRight(){
            return Right;
        }
        public Tile getUp(){
            return Up;
        }
        public Tile getLeft(){
            return Left;
        }

        public string getOrigin()
        {
            return origin;
        }

        public string getDirect()
        {
            return direct;
        }


        public Tile getTileDirect()
        {
            if (direct == "Left")
            {
                return this.Right;
            }
            else if (direct == "Right")
            {
                return this.Left;
            }
            else if (direct == "Up")
            {
                return this.Down;
            }
            else
            {
                return this.Up;
            }
        }
        //public string getOrigin()
        //{
        //    string origin = path[path.Count - 1].Item1;
        //    if (origin == "Left")
        //    {
        //        return "Right";
        //    } 
        //    else if (origin == "Right")
        //    {
        //        return "Left";
        //    }
        //    else if (origin ==  "Up")
        //    {
        //        return "Down";
        //    }
        //    else
        //    {
        //        return "Up";
        //    }
        //}

        /* Setter */
        //private string getOpposite(string orig)
        //{
        //    if (orig == "Left")
        //    {
        //        return "Right";
        //    }
        //    else if (orig == "Right")
        //    {
        //        return "Left";
        //    }
        //    else if (orig == "Up")
        //    {
        //        return "Down";
        //    }
        //    else
        //    {
        //        return "Up";
        //    }
        //}
        public void setOrigin(string orig)
        {
            if (visited == false)
            {
                this.origin = orig;
            }
            this.direct = orig;
        }

        public void setDirect(string direct)
        {
            this.direct = direct;
        }

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

        public void setValue(string val){
            value = val;
        }

        public void setDown(Tile tile){
            Down = tile;
        }

        public void setRight(Tile tile){
            Right = tile;
        }

        public void setUp(Tile tile){
            Up = tile;
        }

        public void setLeft(Tile tile){
            Left = tile;
        }

        public void hasVisited(){
            visited = true;
        }


        /*** * Utility Methods * ***/
        public void addPath(Tile tile, string direction){
            List<Tuple<string, int, int>> tempPath = tile.getPath();
            foreach(Tuple<string, int, int> tuple in tempPath){
                this.path.Add(tuple);
            }
            this.path.Add(new Tuple<string, int, int>(direction, tile.getCoordinate(0), tile.getCoordinate(1)));
        }

        public void reset(){
            visited = false;
            path = new List<Tuple<string, int, int>>();
        }

        public void resetPath()
        {
            this.path = new List<Tuple<string, int, int>>();
        }

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
                Console.WriteLine("Down: " + Down.getCoordinate(0) + "," + Down.getCoordinate(1));
            }
            if(Right != null){
                Console.WriteLine("Right: " + Right.getCoordinate(0) + "," + Right.getCoordinate(1));
            }
            if(Up != null){
                Console.WriteLine("Up: " + Up.getCoordinate(0) + "," + Up.getCoordinate(1));
            }
            if(Left != null){
                Console.WriteLine("Left: " + Left.getCoordinate(0) + "," + Left.getCoordinate(1));
            }
        }

    }
}