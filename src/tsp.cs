using bfs = bfsSpace.bfsClass;
using Tile = TileSpace.TileClass;
using Utility = UtilitySpace.Matrix;

namespace tspSpace{
    public class tspBfs : bfs {
        private Tile home;
        
        public tspBfs(string[,] matrix) : base(matrix) {
            home = start;
            // Do nothing
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

        public static void Main(string[] args){
            Utility utility = new Utility();
            utility.parserFile("../test/input.txt");
            utility.printMatrix();
            tspBfs tspBfs = new tspBfs(utility.getMatrix());
            tspBfs.startTsp();
            tspBfs.printStep();
        }

    }
}