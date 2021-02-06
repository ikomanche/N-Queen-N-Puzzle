/*
 *      1600002148 İlker Kılınçarslan
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] puzzle =
            {
                1,4,2,
                3,7,5,
                6,0,8
            };

            Node root = new Node(puzzle);
            UninformedSearch uis = new UninformedSearch();
            string choice = "0";
            int choiceInt = Convert.ToInt32(choice);

            while(choiceInt < 6)
            {
                Console.WriteLine("Select one of the search methods below:\n" +
                "1) Breadth First Search\n" +
                "2) Depth First Search\n" +
                "3) Depth Limited Search\n" +
                "4) Iterative Deepening Search\n" +
                "5) Exit.\n");

                choice = Console.ReadLine();
                choiceInt = Convert.ToInt32(choice);

                if(choiceInt < 6 && choiceInt > 0)
                {
                    Console.WriteLine("you choose {0}", choice);

                    List<Node> solution;
                    string depth;
                    int depthInt;
                    switch (choiceInt)
                    {
                        case 1:
                            Console.WriteLine("Breadth First Search runnnig...\n");
                            solution = uis.BreadthFirstSearch(root);
                            PrintSolution(solution);
                            break;
                        case 2:
                            Console.WriteLine("Depth First Search runnnig...\n");
                            solution = uis.DepthFirstSearch(root);
                            PrintSolution(solution);
                            break;
                        case 3:
                            Console.WriteLine("Depth Limited Search runnnig...\n");
                            Console.WriteLine("Enter a depth limit :");
                            depth = Console.ReadLine();
                            depthInt = Convert.ToInt32(depth);
                            solution = uis.DepthLimitedSearch(root, depthInt);
                            PrintSolution(solution);
                            break;
                        case 4:
                            Console.WriteLine("Iterative Deepening Search runnnig...\n");
                            Console.WriteLine("Enter a depth limit :");
                            depth = Console.ReadLine();
                            depthInt = Convert.ToInt32(depth);
                            solution = uis.IterativeDeepeningSearch(root, depthInt);
                            PrintSolution(solution);
                            break;
                        case 5:
                            Console.WriteLine("Quiting... Press Enter to exit.");
                            choiceInt = 6;
                            break;
                    }
                }

                else
                {
                    Console.WriteLine("You choose invalid option. Be sure you entered a number between 1-5 !\n");
                    choiceInt = 0;
                }
                
            }
            
            Console.Read();
        }

        private static void PrintSolution(List<Node> solution)
        {
            if (solution.Count > 0)
            {
                solution.Reverse();
                for (int i = 0; i < solution.Count; i++)
                {
                    solution[i].PrintPuzzle();
                }
            }
            else
            {
                Console.WriteLine("No path solution is found.\n");
            }
        }
    }
}
