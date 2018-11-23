using System;
using System.Collections.Generic;

namespace TheMaze
{
    public class Cell
    {
        private int x; // position x in the maze
        private int y; // position y
        private List<Cell> neighbour;
//        private Cell previous; // for the creation of the maze
        private bool visited;
  //      private bool [] walls; // 0 for north, 1 for south, 2 east, 3 for west

        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            visited = false;
   //         previous = null;
            neighbour = new List<Cell>();
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public List<Cell> Neighbour { get => neighbour; set => neighbour = value; }
        public bool Visited { get => visited; set => visited = value; }
 //       public Cell Previous { get => previous; set => previous = value; }
    }
}
