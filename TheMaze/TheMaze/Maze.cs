using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TheMaze
{
    public class Maze 
    {
        private int size;
        private List<Cell> maze;
      //  private Cell position;

        public Maze(int Size)
        {
            size = Size;
            maze = new List<Cell>();
            Generate();
        }

        public void Generate()
        {
            // creating all the cells with no connections between them yet
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cell node = new Cell(i, j);
                    maze.Add(node);
                }
            }
            // choosing a random cell to begin with that is not on the edge of the maze
            Random rand = new Random();

            //int number = rand.Next(0, size * size);
            //while (maze[number].X == 0 || maze[number].X == size || maze[number].Y == 0 || maze[number].Y == size)
            //    number = rand.Next(0, size * size);

            //Cell current = maze[number];
            //current.Previous = new Cell(-1, -1); // for the stop condition in the Move function

            //Move(current, rand.Next(0, 3), null); // choosing a random neightbor to the cell to move to
            var shuffled = maze.OrderBy(a => rand.Next());

            foreach (Cell c in shuffled)
            {
                if (c.Visited == false)
                    Move(c, null);
            }
        }

        public void Move(Cell currentCell, List<Cell> neightbors)
        {
            currentCell.Visited = true;

            Random rand = new Random();

            // creation of a temporary list with the neightbors of the cell
            neightbors = new List<Cell>();

            if (Search(currentCell.X, currentCell.Y - 1) != null)
                neightbors.Add(Search(currentCell.X, currentCell.Y - 1));

            if (Search(currentCell.X, currentCell.Y + 1) != null)
                neightbors.Add(Search(currentCell.X, currentCell.Y + 1));

            if (Search(currentCell.X + 1, currentCell.Y) != null)
                neightbors.Add(Search(currentCell.X + 1, currentCell.Y));

            if (Search(currentCell.X - 1, currentCell.Y) != null)
                neightbors.Add(Search(currentCell.X - 1, currentCell.Y));

            var shuffled = neightbors.OrderBy(a => rand.Next());

            foreach (Cell c in shuffled)
            {
                if(c.Visited == false)
                {
                    Move(c, null);
                }
            }
        }

        public Cell Search(int x, int y)
        {
            foreach (Cell c in maze)
                if (c.X == x && c.Y == y)
                    return c;

            return null;
        }

        //public Cell Move2(Cell currentCell, int pick, List<Cell> neightbors)
        //{
        //    Random rand = new Random();

        //    // creation of a temporary list with the neightbors of the cell
        //    neightbors = new List<Cell>();

        //    if (Search(currentCell.X, currentCell.Y - 1) != null)
        //        neightbors.Add(Search(currentCell.X, currentCell.Y - 1));

        //    if (Search(currentCell.X, currentCell.Y + 1) != null)
        //        neightbors.Add(Search(currentCell.X, currentCell.Y + 1));

        //    if (Search(currentCell.X + 1, currentCell.Y) != null)
        //        neightbors.Add(Search(currentCell.X + 1, currentCell.Y));

        //    if (Search(currentCell.X - 1, currentCell.Y) != null)
        //        neightbors.Add(Search(currentCell.X - 1, currentCell.Y));


        //    // verifying that all neighbors hasn't been visited yet
        //    bool visit = true;

        //    foreach (Cell c in neightbors)
        //    {
        //        if (c.Visited == false)
        //        {
        //            visit = false;
        //        }
        //    }

        //    // if so, going back to the previous cell
        //    if (visit == true)
        //    {
        //        return Move2(currentCell.Previous, rand.Next(0, 4), null);
        //    }
            
        //    else
        //    {
        //        // moving to the selected neightbor, creating a path between the two cells 
        //        // only if the cell wasn't visited yet
        //        if (neightbors[pick].Visited == false)
        //        {
        //            if (pick > neightbors.Count)
        //                pick = rand.Next(0, 3);

        //            if (pick > neightbors.Count)
        //                pick = rand.Next(0, 2);

        //            currentCell.Neightbors.Add(neightbors[pick]);
        //            currentCell.Visited = true;
        //            neightbors[pick].Previous = currentCell;
        //            return Move2(neightbors[pick], rand.Next(0, 4), null); // repeating with the new cell
        //        }

        //        else
        //        {
        //            neightbors.RemoveAt(pick);
        //            return Move2(currentCell, rand.Next(0, neightbors.Count - 1), neightbors); // choosing an another unvisited neightbor
        //        }
        //    }
        //}

        public void Print()
        {

        }
    }
}
