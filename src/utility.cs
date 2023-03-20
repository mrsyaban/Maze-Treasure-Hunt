using System;
using System.Collections.Generic;
using TileSpace;

namespace UtilitySpace
{
    public class Matrix
    {
        private string[,] matrix;

        public Matrix(){
            matrix = null;
        }

        public Matrix(string[,] matrix){
            this.matrix = matrix;
        }

        public void setMatrix(string[,] matrix){
            this.matrix = matrix;
        }

        public string[,] getMatrix(){
            return matrix;
        }



        public void printMatrix(){

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        // public static void Main(string[] args){
        //     Utility utility = new Utility();
        //     utility.parserFile("../test/input.txt");
        //     utility.printMatrix();

        // }
    }

    public class Tiles
    {
        private List<Tile> tiles;
        private string[,] matrix;
        
        /* constructor */
        public Tiles() {
            tiles = new List<Tile>();
            matrix = null;
        }

        /* Getter */
        Tile getTile(int i) { 
            return tiles[i];
        }

        public string[,] getMatrix()
        {
            return matrix;
        }

        public string getMatrix(int i, int j)
        {
            return matrix[i,j];
        }

        /* Setter */
        public void addTile(Tile tile) { 
            tiles.Add(tile);
        }

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
        }

        public void setAdjacency()
        {
            foreach (Tile tile in tiles)
            {
                int[] coor = tile.getCoordinate();
                int x = coor[0];
                int y = coor[1];
                foreach (Tile t in tiles)
                {
                    int x2 = t.getCoordinate(0);
                    int y2 = t.getCoordinate(1);
                    if (x2 == x + 1 && y2 == y)
                    {
                        tile.setDown(t);
                    }
                    else if (x2 == x && y2 == y + 1)
                    {
                        tile.setRight(t);
                    }
                    else if (x2 == x - 1 && y2 == y)
                    {
                        tile.setUp(t);
                    }
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
    }
}