using System;
using System.Collections.Generic;



namespace Tile
{
    class Tile
    {
        int[] coordinate;
        int value;
        bool visited;
        List<Tile> path;

        /* Constructor */
        public Tile() {
            coordinate = new int[2];
            value = 0;
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


        /* Setter */
        public void setCoordinate(int[] coor){
            coordinate[0] = coor[0]; 
            coordinate[1] = coor[1]; 
        }

        public void hasVisited(){
            visited = true;
        }

        public void addPath(Tile tile){

        }
    }
}