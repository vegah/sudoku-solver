using System;
using System.Diagnostics;
using Fantasista.sudoku.Solvers;

/*
/*

Setting value 2 to 1,4
Setting value 6 to 2,4
Setting value 5 to 4,4
Setting value 9 to 6,4
Setting value 3 to 4,6
Setting value 1 to 5,6
Setting value 7 to 6,7

Applying  5  to  4 4
Applying  7  to  5 6
Applying  4  to  8 6
Applying  3  to  7 7

*/
namespace Fantasista.sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
            // Standard sudoku
            var setup = new int[][] {
                new [] {5,3,0,0,7,0,0,0,0},
                new [] {6,0,0,1,9,5,0,0,0},
                new [] {0,9,8,0,0,0,0,6,0},
                new [] {8,0,0,0,6,0,0,0,3},
                new [] {4,0,0,8,0,3,0,0,1},
                new [] {7,0,0,0,2,0,0,0,6},
                new [] {0,6,0,0,0,0,2,8,0},
                new [] {0,0,0,4,1,9,0,0,5},
                new [] {0,0,0,0,8,0,0,7,9}
            }; */

             
            // Hidden singles setup
            var setup = new int [][] {
                new int[] {0,0,2,0,3,0,0,0,8},
                new int[] {0,0,0,0,0,8,0,0,0},
                new int[] {0,3,1,0,2,0,0,0,0},
                new int[] {0,6,0,0,5,0,2,7,0},
                new int[] {0,1,0,0,0,0,0,5,0},
                new int[] {2,0,4,0,6,0,0,3,1},
                new int[] {0,0,0,0,8,0,6,0,5},
                new int[] {0,0,0,0,0,0,0,1,3},
                new int[] {0,0,5,3,1,0,4,0,0}
            };

            /*var setup = new int [][] {
                new int[] {6,7,2,4,3,5,1,9,8},
                new int[] {5,4,9,1,7,8,3,6,2},
                new int[] {0,3,1,0,2,0,0,0,0},
                new int[] {0,6,0,0,5,0,2,7,0},
                new int[] {0,1,0,0,0,0,0,5,0},
                new int[] {2,0,4,0,6,0,0,3,1},
                new int[] {0,0,0,0,8,0,6,0,5},
                new int[] {0,0,0,0,0,0,0,1,3},
                new int[] {0,0,5,3,1,0,4,0,0}
            };*/


            var board = new Board(setup);
            var solver = new ConstraintAnalyzer();
            var errors = board.GetErrorCount();
            var new_errors=board.Size;
            var clock = new Stopwatch(); 
            clock.Start();
            while (errors!=new_errors)
            {
               errors=new_errors;
               solver.Solve(board);
               new_errors = board.GetErrorCount();
               Console.WriteLine("Errors :"+new_errors);
            }
            clock.Stop();

            var solver2 = new AllDifferentWithTabuSearch();
            solver2.Init(board);
            var iterations = 0;
            while(errors!=0)
            {
                solver2.Solve(board);
                errors = board.GetErrorCount();
                iterations++;
                if (iterations%1000==0)
                {
                    Console.WriteLine("Errors :"+errors);
                    Console.WriteLine(board);
                }
                
            }
            Console.WriteLine(board);
            Console.WriteLine($"Took {clock.ElapsedMilliseconds} ms");
        }
    }
}
