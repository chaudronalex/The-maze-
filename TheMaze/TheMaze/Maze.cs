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
        private Random rand;

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
            // shuffling the list to select a random cell to begin with
            var shuffled = maze.OrderBy(a => rand.Next());

            // choosing a random cell to begin with that is not on the edge of the maze
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

            // Deep first search
            foreach (Cell c in shuffled)
            {
                if (!c.Visited)
                {
                    currentCell.Neighbours.Add(c);
                    c.Neighbours.Add(currentCell);
                    Move(c);
                }
            }
        }

        public Cell Search(int x, int y) // to get a cell using its coordinates
        {
            foreach (Cell c in maze)
                if (c.X == x && c.Y == y)
                    return c;

            return null;
        }

        public bool AreNeighbours(Cell c1, Cell c2) // verifying if two cells are neighbours in the graph (= no wall between them)
        {
            foreach (Cell c in c1.Neighbours)
                if (c == c2)
                    return true;

            return false;
        }

        public void ShortestPath() 
        {
            // printing an error message if no path was found
            if (!FindPath())
                Console.WriteLine("\nNo path found\n");

            List<Cell> path = new List<Cell>(); //tracing the path with help of the fathers
            Cell dest = Search(size - 1, size - 1);
            path.Add(dest);
            while(dest.Father != null)
            {
                path.Add(dest.Father);
                dest = dest.Father;
            }

            // for printing
            foreach (Cell c in path)
                c.State = Cell._state.path;
        }

        public bool FindPath()
        {
            Queue<Cell> queue = new Queue<Cell>(); 

            foreach (Cell c in maze) //setting again all the cells to unvisited
                c.Visited = false;

            Cell source = Search(0, 0); // we start at the entry of the maze
            source.Visited = true;
            source.Dist = 0;
            queue.Enqueue(source);

            while (queue.Count != 0) // breadth first search   
            {
                Cell current = queue.Peek();
                queue.Dequeue();
                foreach (Cell c in current.Neighbours)
                {
                    if (!c.Visited)
                    {
                        c.Visited = true;
                        c.Dist = current.Dist + 1;
                        c.Father = current;
                        queue.Enqueue(c);

                        if (c == Search(size - 1, size - 1)) // when we arrive to the destination
                            return true;
                    }
                }
            }

            return false;
        }

        public int [,] GraphToMatrix() // for the print function
        {
            // we create an element in the matrix for each edge in the graph, so the matrix is two times bigger than the graph
            int[,] maze2 = new int[size * 2, size * 2];

            // each cell is empty to begin with
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = i * 2;
                    int y = j * 2;
                    maze2[x, y] = 0;
                    // if the path function was called and the cell is on the path
                    if (Search(i, j).State == Cell._state.path)
                        maze2[x, y] = 3;
                }
            }
            // putting a wall between two cells if they aren't neighbours, or indicating if the cell is on the path (horizontally)
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = (i * 2) + 1;
                    int y = (j * 2);
                    if (!AreNeighbours(Search(i, j), Search(i + 1, j)))
                        maze2[x, y] = 1;

                    else 
                    {
                        if (Search(i, j).State == Cell._state.path && Search(i + 1, j).State == Cell._state.path)
                            maze2[x, y] = 3;
                    }
                }
            }
            // putting a wall between two cells if they aren't neighbours, or indicating if the cell is on the path (vertically)
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = (i * 2);
                    int y = (j * 2) + 1;
                    if (!AreNeighbours(Search(i, j), Search(i, j + 1)))
                        maze2[x, y] = 1;

                    else
                    {
                        if (Search(i, j).State == Cell._state.path && Search(i, j + 1).State == Cell._state.path)
                            maze2[x, y] = 3;
                    }
                }
            }
            // every element that isn't a cell nor an edge is a wall
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int x = (i * 2) + 1;
                    int y = (j * 2) + 1;
                    maze2[x, y] = 1;
                }
            }
            // setting the entrance and the exit 
            maze2[0, 0] = 2;
            maze2[(size * 2) - 2, (size * 2) - 2] = 2;

            return maze2;
        }

        public void Print()
        {
            int[,] matrix = GraphToMatrix();

            for (int a = 0; a < (size * 2) + 1; a++)
                Console.Write("##");
            Console.Write("\n");

            for (int i = 0; i < (size * 2) - 1; i++)
            {
                Console.Write("##");
                for (int j = 0; j < (size * 2) - 1; j++)
                {
                    switch(matrix[i, j])
                    {
                        case 0:
                            Console.Write("  ");
                            break;
                        case 1:
                            Console.Write("##");
                            break;
                        case 2:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("[]");
                            Console.ResetColor();
                            break;
                        case 3:
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write("**");
                            Console.ResetColor();
                            break;
                    }
                }
                Console.Write("##");
                Console.Write("\n");
            }

            for (int a = 0; a < (size * 2) + 1; a++)
                Console.Write("##");

            Console.Write("\n\n");
        }
    }
}
