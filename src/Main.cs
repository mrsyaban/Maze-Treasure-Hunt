using System;
using UtilitySpace;
using Services;

namespace main
{
    class main
    {
        public static void Main(string[] args)
        {

            //Tiles tiles2 = (Tiles)tiles.Clone();
            string pathFile = "test/sampel-1.txt";

            Console.WriteLine("=============");
            Console.WriteLine("     DFS     ");
            Console.WriteLine("=============");

            Tiles tiles = new Tiles();
            tiles.parserFile(pathFile);
            tiles.printMatrix();

            DFS dfs = new DFS(tiles);
            dfs.run();
            dfs.printStep();


            Console.WriteLine("=============");
            Console.WriteLine("     BFS     ");
            Console.WriteLine("=============");
            
            tiles = new Tiles();
            tiles.parserFile(pathFile);

            BFS bfs = new BFS(tiles);
            bfs.run();
            bfs.printStep();

            Console.WriteLine("=============");
            Console.WriteLine("   TSP BFS   ");
            Console.WriteLine("=============");

            tiles = new Tiles();
            tiles.parserFile(pathFile);

            BFS bfs_tsp = new BFS(tiles);
            bfs_tsp.runTSP();
            bfs_tsp.printStep();


            Console.WriteLine("=============");
            Console.WriteLine("   TSP BFS   ");
            Console.WriteLine("=============");

            tiles = new Tiles();
            tiles.parserFile(pathFile);

            DFS dfs_tsp = new DFS(tiles);
            dfs_tsp.runTSP();
            dfs_tsp.printStep();
        }
    }
}
