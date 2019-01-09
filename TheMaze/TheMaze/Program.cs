using System;

namespace TheMaze
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string input;
            int size;
            do
            {
                Console.WriteLine("Welcome ! Please enter the size for the maze :");
                input = Console.ReadLine();
            } while (!Int32.TryParse(input, out size));

            Maze laby = new Maze(size);
            laby.Print();
            Console.WriteLine("\n");

            ConsoleKey response;
            do
            {
                Console.Write("Do you want to see the solution of the maze ? [y/n]\n");
                response = Console.ReadKey(false).Key;   
            } while (response != ConsoleKey.Y && response != ConsoleKey.N);

            if(response == ConsoleKey.Y)
            {
                Console.WriteLine();
                laby.ShortestPath();
                laby.Print();
            }
            else
            {
                Console.WriteLine("\nBye !");
            }
            // test
        }
    }
}
