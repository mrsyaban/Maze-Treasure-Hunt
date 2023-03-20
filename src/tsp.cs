using bfsSpace;
using TileSpace;
using UtilitySpace;

namespace tspSpace{
    public class tsp : bfs {
        private Tile home;

        public tsp(Tiles tiles) : base(tiles) {
            home = start;
        }

        public void setHome(Tile home){
            this.home = home;
        }

        public Tile getHome(){
            return home;
        }

        public void backtoHome(){
           this.treasure.Add(home);
           this.BFS();
        }

        public void startTsp(){
            this.startfind();
            this.backtoHome();
        }

        // public static void Main(string[] args){
        //     Tiles tiles = new Tiles();
        //     tiles.parserFile("test/input.txt");
        //     tsp tsp = new tsp(tiles);
        //     tsp.startTsp();
        //     tsp.printStep();

        // }

    }
}