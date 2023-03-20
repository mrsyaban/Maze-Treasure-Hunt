using System;
using System.Collections.Generic;

// is my namespace correct?
namespace obj.Tile
{
    public class Tile
    {
        private int[] coordinate;
        private char value;
        private bool visited;
        private List<Tile> path;
        private Tile Down;
        private Tile Right;
        private Tile Up;
        private Tile Left;

        /* Constructor */
        public Tile() {
            coordinate = new int[2];
            this.value = 'X';
            visited = false;
            path = new List<Tile>();
        }

        public Tile(char val)
        {
            coordinate = new int[2];
            this.value = val;
            visited = false;
            path = new List<Tile>();
        }

        /* Getter */
        public int getValue(){
            return value;
        }

        public bool isVisited(){
            return visited;
        }

        public List<Tile> getPath(){
            return path;
        }

        public int[] getCoordinate(){
            return coordinate;
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

        /* Setter */
        public void setCoordinate(int[] coor){
            coordinate[0] = coor[0]; 
            coordinate[1] = coor[1]; 
        }

        public void hasVisited(){
            visited = true;
        }

        public void addPath(Tile tile){
            path.Add(tile);
        }
        public void setValue(char val){
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


        /* Utility Function */

        public void printInfo(){
            Console.WriteLine("Coordinate: " + coordinate[0] + "," + coordinate[1]);
            Console.WriteLine("Value: " + value);
            Console.WriteLine("Visited: " + visited);
            Console.WriteLine("Path: ");
            foreach(Tile tile in path){
                Console.WriteLine(tile.getCoordinate()[0] + "," + tile.getCoordinate()[1]);
            }
            Console.WriteLine("Neighboor:");
            Console.WriteLine(Left.getCoordinate()[0] + "," + Left.getCoordinate()[1]);
            Console.WriteLine(Up.getCoordinate()[0] + "," + Up.getCoordinate()[1]);
            Console.WriteLine(Right.getCoordinate()[0] + "," + Right.getCoordinate()[1]);
            Console.WriteLine(Down.getCoordinate()[0] + "," + Down.getCoordinate()[1]);
        }



    }
}