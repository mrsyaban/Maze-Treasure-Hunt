using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreasureHunterAlgo
{
    public class Maze
    {
        private List<List<string>> mazeContents;
        private (int i, int j) startTile;
        private int length;
        private int width;
        private int countTreasure;
        public Maze()
        {
            mazeContents = new List<List<string>>();
            startTile = (0, 0);
            length = 0;
            width = 0;
            countTreasure = 0;
        }
        public Maze(string txtFile)
        {
            var lines = File.ReadAllLines(txtFile);
            mazeContents = new List<List<string>>();
            countTreasure = 0;
            foreach (var line in lines)
            {
                string[] curLine = line.Split(' ');
                List<string> characters = new List<string>();
                foreach (var info in curLine)
                {
                    if (info == "T")
                    {
                        this.countTreasure++;
                    }
                    characters.Add(info);
                }
                mazeContents.Add(characters);
            }

            this.width = lines.Length;
            this.length = (this.mazeContents)[0].Count();

            int startN = 0, startM = 0;
            bool found = false;
            foreach (var column in mazeContents)
            {
                startM = 0;
                foreach (var info in column)
                {
                    if (info == "K")
                    {
                        found = true;
                        break;
                    }
                    startM++;
                }
                if (found)
                {
                    break;
                }
                startN++;
            }

            this.startTile = (startN, startM);
        }

        public List<List<string>> Content
        {
            get { return mazeContents; }
            set { mazeContents = value; }
        }
        public (int i, int j) Start
        {
            get { return startTile; }
            set { startTile = value; }
        }
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Treasure
        {
            get { return countTreasure; }
            set { countTreasure = value; }
        }
    }
}
