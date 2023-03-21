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

            Console.WriteLine("=============");
            Console.WriteLine("     DFS     ");
            Console.WriteLine("=============");

            Tiles tiles = new Tiles();
            tiles.parserFile("test/input2.txt");

            DFS dfs = new DFS(tiles);
            dfs.run();
            dfs.printStep();


            Console.WriteLine("=============");
            Console.WriteLine("     BFS     ");
            Console.WriteLine("=============");
            
            tiles = new Tiles();
            tiles.parserFile("test/input2.txt");

            BFS bfs = new BFS(tiles);
            bfs.run();
            bfs.printStep();

            Console.WriteLine("=============");
            Console.WriteLine("     TSP     ");
            Console.WriteLine("=============");

            tiles = new Tiles();
            tiles.parserFile("test/input2.txt");

            TSP tsp = new TSP(tiles);
            tsp.runTSP();
            tsp.printStep();
        }
    }
}
