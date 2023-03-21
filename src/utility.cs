using System;
using System.Collections.Generic;
using TileSpace;

namespace UtilitySpace
{
    public class Tiles : ICloneable
    {
        private List<Tile> tiles;
        private Tile start;
        private List<Tile> treasure;
        private string[,] matrix;
        
        /* constructor */
        public Tiles() {
            tiles = new List<Tile>();
            treasure = new List<Tile>();
            matrix = null;
            start = null;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /* Getter */
        public List<Tile> getTiles() { 
            return tiles;
        }

        public List<Tile> getTreasure(){
            return treasure;
        }

        public Tile getStart(){
            return start;
        }

        public string[,] getMatrix(){
            return matrix;
        }

        /* Setter */
        public void addTile(Tile tile) { 
            tiles.Add(tile);
        }

        /*** * Utility Methods * ***/
        /* Parsing input file to list of tile (Tiles)*/
        public void parserFile(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            int row = lines.Length;
            int col = lines[0].Split(' ').Length;
            matrix = new string[row, col];
            for (int i = 0; i < row; i++)
            {
                string[] line = lines[i].Split(' ');
                for (int j = 0; j < col; j++)
                {
                    matrix[i, j] = line[j];
                }
            }
            convMatrix();
            setAdjacency();
        }

        /* convert input files into matrix of string */
        public void convMatrix(){
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for(int i = 0; i < rows; i++){
                for(int j = 0; j < cols; j++){
                    
                    // construct all tiles except "X"
                    if(matrix[i,j] != "X"){
                        Tile tile = new Tile(matrix[i,j], i, j);
                        tiles.Add(tile);
                        if(matrix[i,j] == "K"){
                            start = tile;
                        }else if(matrix[i,j] == "T"){
                            treasure.Add(tile);
                        }else{
                            // Do Nothing.
                        }
                    }
                }
            }   
        }

        /* set the adjacencyof the tiles */
        public void setAdjacency()
        {
            foreach (Tile tile in tiles)
            {
                int x = tile.getCoordinate(0);
                int y = tile.getCoordinate(1);
                foreach (Tile t in tiles)
                {
                    int x2 = t.getCoordinate(0);
                    int y2 = t.getCoordinate(1);

                    // t is below tile
                    if (x2 == x + 1 && y2 == y)
                    {
                        tile.setDown(t);
                    }

                    // t is rightside tile
                    else if (x2 == x && y2 == y + 1)
                    {
                        tile.setRight(t);
                    }

                    // t is upside tile
                    else if (x2 == x - 1 && y2 == y)
                    {
                        tile.setUp(t);
                    }

                    // t is leftside tile
                    else if (x2 == x && y2 == y - 1)
                    {
                        tile.setLeft(t);
                    }
                    else
                    {
                        /* Do nothing */
                    }
                }
            }
        }
        public void printMatrix(){
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for(int i = 0; i < rows; i++){
                for(int j = 0; j < cols; j++){
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
        }
        public void printTile(){
            foreach(Tile tile in tiles){
                Console.WriteLine("Coordinate: " + tile.getCoordinate(0) + "," + tile.getCoordinate(1));
                Console.WriteLine("Value: " + tile.getValue());
                if(tile.getDown() != null){
                    Console.WriteLine("Down: " + tile.getDown().getCoordinate(0) + "," + tile.getDown().getCoordinate(1));
                }
                if(tile.getRight() != null){
                    Console.WriteLine("Right: " + tile.getRight().getCoordinate(0) + "," + tile.getRight().getCoordinate(1));
                }
                if(tile.getUp() != null){
                    Console.WriteLine("Up: " + tile.getUp().getCoordinate(0) + "," + tile.getUp().getCoordinate(1));
                }
                if(tile.getLeft() != null){
                    Console.WriteLine("Left: " + tile.getLeft().getCoordinate(0) + "," + tile.getLeft().getCoordinate(1));
                }
                Console.WriteLine();
            }
        }
    }
}