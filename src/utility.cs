using System;
namespace Utility{
    class Utility{
        private string[,] matrix;

        public Utility(){
            matrix = null;
        }

        public Utility(string[,] matrix){
            this.matrix = matrix;
        }

        public void setMatrix(string[,] matrix){
            this.matrix = matrix;
        }

        public string[,] getMatrix(){
            return matrix;
        }

        public void parserFile(string path){
            string[] lines = System.IO.File.ReadAllLines(path);
            int row = lines.Length;
            int col = lines[0].Split(' ').Length;
            matrix = new string[row,col];
            for(int i = 0; i < row; i++){
                string[] line = lines[i].Split(' ');
                for(int j = 0; j < col; j++){
                    matrix[i,j] = line[j];
                }
            }
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
}