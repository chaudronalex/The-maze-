using System;
using System.Collections.Generic;

namespace TheMaze
{
    public class Cell
    {
        private int x; // position x in the maze
        private int y; // position y
        private List<Cell> neighbour;
        private bool visited;
        private Cell father;
        private _state state;

        public enum _state 
        {
            entrance, exit, current, path, normal,
        }

        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            visited = false;
            neighbour = new List<Cell>();
            father = null;
            state = _state.normal;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public List<Cell> Neighbour { get => neighbour; set => neighbour = value; }
        public bool Visited { get => visited; set => visited = value; }
        public Cell Father { get => father; set => father = value; }
        public _state State { get => state; set => state = value; }
    }
}
