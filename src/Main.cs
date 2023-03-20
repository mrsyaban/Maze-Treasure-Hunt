using System;
using UtilitySpace;
using Services;

namespace main
{
    class main
    {
        public static void Main(string[] args)
        {
            Tiles tiles = new Tiles();
            tiles.parserFile("test/input.txt");

            //Tiles tiles2 = (Tiles)tiles.Clone();

            Console.WriteLine("=============");
            Console.WriteLine("     DFS     ");
            Console.WriteLine("=============");

            DFS dfs = new DFS(tiles);
            dfs.run();
            dfs.printStep();

            tiles = new Tiles();
            tiles.parserFile("test/input.txt");

            Console.WriteLine("=============");
            Console.WriteLine("     BFS     ");
            Console.WriteLine("=============");

            BFS bfs = new BFS(tiles);
            bfs.run();
            bfs.printStep();
        }
    }
}
