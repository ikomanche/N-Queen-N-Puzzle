/*1600002148
 İlker Kılınçarslan*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightQueen
{
    class HillClimbing
    {        
        private static int stepsClimbedAfterLastRestart = 0;
        private static int stepsClimbed = 0;
        private static int heuristic = 0;
        private static int randomRestarts = 0;

        public HillClimbing()
        {
            this.run();
        }

        public static Queen[] Startboard()
        {
            Queen[] startboard = new Queen[8];

            //  Row and Column numbers must between [0 <= x <= 7]

            startboard[0] = new Queen(2, 0);
            startboard[1] = new Queen(5, 0);
            startboard[2] = new Queen(7, 0);
            startboard[3] = new Queen(4, 0);
            startboard[4] = new Queen(3, 0);
            startboard[5] = new Queen(1, 0);
            startboard[6] = new Queen(6, 0);
            startboard[7] = new Queen(0, 0);


            return startboard;
        }
        public static Queen[] generateBoard()
        {
            Queen[] newBoard = new Queen[8];
            Random rnd = new Random();
            
            for(int i = 0; i < 8; i++)
            {
                newBoard[i] = new Queen(rnd.Next(8), i);
            }

            return newBoard;
        }

        public static void PrintState(Queen[] state)
        {
            int[,] tempBoard = new int[8,8];
            for(int i = 0; i < 8; i++)
            {
                //Get position of Queen from the present board
                tempBoard[state[i].getRow(), state[i].getColumn()] = 1;
            }
            Console.WriteLine();
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(tempBoard[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static int findHeuristic(Queen[] state)
        {
            int heuristic = 0;
            for(int i = 0; i < 8; i++)
            {
                for(int j = i + 1; j < 8; j++)
                {
                    if (state[i].ifConflict(state[j]))
                        heuristic++;

                }
            }
            return heuristic;
        }

        public static Queen[] nextBoard(Queen[] presentBoard)
        {
            Queen[] nextBoard = new Queen[8];
            Queen[] tmpBoard = new Queen[8];

            int presentHeuristic = findHeuristic(presentBoard);
            int bestHeuristic = presentHeuristic;
            int tempH;

            for(int i = 0; i < 8; i++)
            {
                nextBoard[i] = new Queen(presentBoard[i].getRow(), presentBoard[i].getColumn());
                tmpBoard[i] = nextBoard[i];
            }

            for(int i = 0; i < 8; i++)
            {
                if (i > 0)
                    tmpBoard[i - 1] = new Queen(presentBoard[i - 1].getRow(), presentBoard[i - 1].getColumn());
                tmpBoard[i] = new Queen(0, tmpBoard[i].getColumn());

                for(int j = 0; j < 8; j++)
                {
                    tempH = findHeuristic(tmpBoard);

                    if(tempH < bestHeuristic)
                    {
                        bestHeuristic = tempH;

                        for(int k = 0; k < 8; k++)
                        {
                            nextBoard[k] = new Queen(tmpBoard[k].getRow(), tmpBoard[k].getColumn());
                        }
                    }

                    if (tmpBoard[i].getRow() != 7)
                        tmpBoard[i].Move();
                }
            }

            if (bestHeuristic == presentHeuristic)
            {
                randomRestarts++;
                stepsClimbedAfterLastRestart = 0;
                nextBoard = generateBoard();
                heuristic = findHeuristic(nextBoard);
            }
            else
                heuristic = bestHeuristic;

            stepsClimbed++;
            stepsClimbedAfterLastRestart++;

            return nextBoard;
        }
        
        public void run()
        {
            int presentHeuristic;

            Console.WriteLine("Solution to 8 Queen problem using Hill Climbing:");
            Queen[] presentBoard = Startboard();
            //Queen[] presentBoard = generateBoard(); // To get random numbers for the initial board, use this line instead of above.
            
            presentHeuristic = findHeuristic(presentBoard);

            while (presentHeuristic != 0)
            {
                presentBoard = nextBoard(presentBoard);
                presentHeuristic = heuristic;
            }

            PrintState(presentBoard);
            Console.WriteLine("\nTotal number of steps climbed: {0}", stepsClimbed);
            Console.WriteLine("Total number of restarts: {0}", randomRestarts);
            Console.WriteLine("Number of steps climbed after last restart: {0}", stepsClimbedAfterLastRestart); 
        }
    }
}
