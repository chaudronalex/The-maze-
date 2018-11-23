﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TheMaze
{
    public class Maze 
    {
        private int size;
        private List<Cell> maze;
        private Random rand;
      //  private Cell position;

        public Maze(int Size)
        {
            size = Size;
            rand = new Random();
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

            //int number = rand.Next(0, size * size);
            //while (maze[number].X == 0 || maze[number].X == size || maze[number].Y == 0 || maze[number].Y == size)
            //    number = rand.Next(0, size * size);

            //Cell current = maze[number];
            //current.Previous = new Cell(-1, -1); // for the stop condition in the Move function

            //Move(current, rand.Next(0, 3), null); // choosing a random neightbor to the cell to move to
            
            // shuffling the list to select a random cell to begin with
            var shuffled = maze.OrderBy(a => rand.Next());

            // moving in the maze until every cell is visited
            //foreach (Cell c in shuffled)
            //if (c.Visited == false)
            //Move(c);

            int number = rand.Next(0, size * size);
            while (maze[number].X == 0 || maze[number].X == size || maze[number].Y == 0 || maze[number].Y == size)
                number = rand.Next(0, size * size);

            Cell current = maze[number];
            Move(current);
        }

        public void Move(Cell currentCell)
        {
            // marking the cell as visited
            currentCell.Visited = true;

            // creation of a temporary list with the neighbours of the cell
            List<Cell> neighbour = new List<Cell>();

            if (Search(currentCell.X, currentCell.Y - 1) != null)
                neighbour.Add(Search(currentCell.X, currentCell.Y - 1));

            if (Search(currentCell.X, currentCell.Y + 1) != null)
                neighbour.Add(Search(currentCell.X, currentCell.Y + 1));

            if (Search(currentCell.X + 1, currentCell.Y) != null)
                neighbour.Add(Search(currentCell.X + 1, currentCell.Y));

            if (Search(currentCell.X - 1, currentCell.Y) != null)
                neighbour.Add(Search(currentCell.X - 1, currentCell.Y));
                
            // shuffling the list of neighbours to select a random one
            var shuffled = neighbour.OrderBy(a => rand.Next());
            //foreach(Cell c in shuffled)
            //{
            //    Console.Write("(" + c.X + "," + c.Y + ") ");
            //}
            //Console.WriteLine("");

            // Deep first search
            foreach (Cell c in shuffled)
            {
                if (c.Visited == false)
                {
                    currentCell.Neighbour.Add(c);
                    c.Neighbour.Add(currentCell);
                    Move(c);
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

            //if (Search(currentCell.X, currentCell.Y - 1) != null)
            //    neightbors.Add(Search(currentCell.X, currentCell.Y - 1));

            //if (Search(currentCell.X, currentCell.Y + 1) != null)
            //    neightbors.Add(Search(currentCell.X, currentCell.Y + 1));

            //if (Search(currentCell.X + 1, currentCell.Y) != null)
            //    neightbors.Add(Search(currentCell.X + 1, currentCell.Y));

            //if (Search(currentCell.X - 1, currentCell.Y) != null)
                //neightbors.Add(Search(currentCell.X - 1, currentCell.Y));


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

        public bool AreNeighbours(Cell c1, Cell c2) 
        {
            foreach (Cell c in c1.Neighbour)
                if (c == c2)
                    return true;

            return false;
        }

        public int [,] GraphToMatrix() // for the print function
        {
            int[,] maze2 = new int[size * 2, size * 2];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = i * 2;
                    int y = j * 2;
                    maze2[x, y] = 0;
                }
            }
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    int x = (i * 2) + 1;
                    int y = (j * 2);
                    if (AreNeighbours(Search(i, j), Search(i + 1, j)))
                        maze2[x, y] = 0;

                    else
                        maze2[x, y] = 1;
                }
            }
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    int x = (i * 2);
                    int y = (j * 2) + 1;
                    if (AreNeighbours(Search(i, j), Search(i, j + 1)))
                        maze2[x, y] = 0;

                    else
                        maze2[x, y] = 1;
                }
            }
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - 1; j++)
                {
                    int x = (i * 2) + 1;
                    int y = (j * 2) + 1;
                    maze2[x, y] = 1;
                }
            }

            return maze2;
        }

        public void Print()
        {
            for (int a = 0; a < (size * 2) + 1; a++)
                Console.Write("##");
            Console.Write("\n");

            for (int i = 0; i < (size * 2) - 1; i++)
            {
                Console.Write("##");
                for (int j = 0; j < (size * 2) - 1; j++)
                {
                    if (GraphToMatrix()[i, j] == 0)
                        Console.Write("  ");

                    else
                        Console.Write("##");
                }
                Console.Write("##");
                Console.Write("\n");
            }

            for (int a = 0; a < (size * 2) + 1; a++)
                Console.Write("##");
        }
    }
}
