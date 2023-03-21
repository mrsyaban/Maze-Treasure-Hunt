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

            DFS dfs = new DFS(pathFile);
            dfs.run();
            dfs.getresultPath();
            dfs.printStep();


            Console.WriteLine("=============");
            Console.WriteLine("     BFS     ");
            Console.WriteLine("=============");
           

            BFS bfs = new BFS(pathFile);
            bfs.run();
            bfs.printStep();

            Console.WriteLine("=============");
            Console.WriteLine("   TSP BFS   ");
            Console.WriteLine("=============");

            BFS bfs_tsp = new BFS(pathFile);
            bfs_tsp.runTSP();
            bfs_tsp.printStep();


            Console.WriteLine("=============");
            Console.WriteLine("   TSP BFS   ");
            Console.WriteLine("=============");

            DFS dfs_tsp = new DFS(pathFile);
            dfs_tsp.runTSP();
            dfs_tsp.printStep();
        }
    }
}
