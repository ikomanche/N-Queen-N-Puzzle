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
    class UninformedSearch
    {
        public UninformedSearch()
        {

        }

        public List<Node> BreadthFirstSearch(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>(); 
            
            
            OpenList.Add(root);
            bool goalFound = false;
            int expanded = 0;

            while(OpenList.Count > 0 && !goalFound)
            {
                Node currentNode = OpenList[0];                
                ClosedList.Add(currentNode);
                OpenList.RemoveAt(0);

                currentNode.ExpandMove();
                expanded++;
                //currentNode.PrintPuzzle();

                for(int i = 0; i < currentNode.children.Count; i++)
                {
                    Node currentChild = currentNode.children[i];
                    if (currentChild.GoalTest())
                    {
                        Console.WriteLine("Goal Found. {0} nodes expanded",expanded);
                        goalFound = true;
                        //  trace path to root node
                        PathTrace(PathToSolution, currentChild);
                    }

                    if(!Contains(OpenList,currentChild) && !Contains(ClosedList,currentChild))
                    {
                        OpenList.Add(currentChild);
                    }
                }
            }

            return PathToSolution;
        }
        
        public List<Node> DepthFirstSearch(Node root)
        {
            List<Node> PathToSolution = new List<Node>();
            Stack<Node> Fringe = new Stack<Node>();
            List<Node> ClosedList = new List<Node>();

            Fringe.Push(root);
            bool goalFound = false;
            int expanded = 0;

            while (Fringe.Count > 0 && !goalFound)
            {
                Node currentNode = Fringe.ElementAt(0);
                ClosedList.Add(currentNode);
                Fringe.Pop();

                currentNode.ExpandMove();
                expanded++;

                for (int i = currentNode.children.Count-1; i >= 0; i--)
                {
                    Node currentChild = currentNode.children[i];
                    if (currentChild.GoalTest())
                    {
                        Console.WriteLine("Goal Found. {0} nodes expanded", expanded);
                        goalFound = true;
                        //  trace path to root node
                        PathTrace(PathToSolution, currentChild);
                    }

                    if (!Contains(Fringe, currentChild) && !Contains(ClosedList, currentChild))
                    {
                        Fringe.Push(currentChild);
                    }
                }
            }
            return PathToSolution;
        }

        bool limitFound = false;    //to check iterative deepening

        public List<Node> DepthLimitedSearch(Node root, int depth)
        {
            List<Node> PathToSolution = new List<Node>();
            Stack<Node> Fringe = new Stack<Node>();
            List<Node> ClosedList = new List<Node>();

            Fringe.Push(root);
            bool goalFound = false;
            int expanded = 0;

            while (Fringe.Count > 0 && !goalFound)
            {
                Node currentNode = Fringe.ElementAt(0);
                ClosedList.Add(currentNode);
                Fringe.Pop();

                if(expanded < depth)
                {
                    currentNode.depth = expanded;
                    currentNode.ExpandMove();
                    expanded++;                    
                }
                else
                {   
                    if(Fringe.Count != 0)
                    {
                        currentNode = Fringe.ElementAt(0);
                        ClosedList.Add(currentNode);
                        Fringe.Pop();
                        currentNode.depth = expanded;
                    }                    

                    if(currentNode.children.Count == 0 && currentNode.depth < depth)
                    {
                        currentNode.ExpandMove();
                        expanded++;
                    }
                }

                for (int i = currentNode.children.Count - 1; i >= 0; i--)
                {
                    Node currentChild = currentNode.children[i];
                    if (currentChild.GoalTest())
                    {                        
                        Console.WriteLine("Goal Found. {0} nodes expanded", expanded);
                        goalFound = true;
                        limitFound = true;
                        //  trace path to root node
                        PathTrace(PathToSolution, currentChild);
                    }

                    if (!Contains(Fringe, currentChild) && !Contains(ClosedList, currentChild))
                    {
                        Fringe.Push(currentChild);
                    }                    
                }
            }
            if (!goalFound)
            {
                Console.WriteLine("Goal NOT found for {0} depth limit. Try another", depth);
            }            
            return PathToSolution;
        }

        public List<Node> IterativeDeepeningSearch(Node root,int depth)
        {
            limitFound = false;
            List<Node> solution = new List<Node>();
            while (!limitFound)
            {
                solution = DepthLimitedSearch(root, depth);
                if (!limitFound)
                {
                    depth++;
                    Console.WriteLine("Increasing depth limit to {0}\n", depth);
                }                
            }
            limitFound = false;

            return solution;
        }

        public void PathTrace(List<Node> path, Node n)
        {
            Console.WriteLine("Tracing path...");
            Node current = n;
            path.Add(current);

            while(current.parent != null)
            {
                current = current.parent;
                path.Add(current);
            }
        }

        public static bool Contains(List<Node> list, Node c)
        {
            bool contains = false;
            for(int i = 0; i < list.Count; i++)
            {
                if (list[i].IsSamePuzzle(c.puzzle))
                {
                    contains = true;
                }
            }
            return contains;
        }

        public static bool Contains(Stack<Node> list, Node c)   //overload for dfs
        {
            bool contains = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).IsSamePuzzle(c.puzzle))
                {
                    contains = true;
                }
            }
            return contains;
        }
    }
}
