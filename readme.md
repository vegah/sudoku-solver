# Sudoku solver
Different algorithms for solving sudoku puzzles.  Written for educational purposes, not optimized for speed.
The code provides a framework for working with sudokus, so you dont need to create all the classes and functionality on you own.  Feel free to use the code to whatever you like.  

This is not finished program to solve sudokus, and it is in no way optimized.  It's just classes for testing algorithms and a couple of algorithms that solves sudokus.

## Algorithms provided
### "Constraint analyzer"
This is a simple algorithm that solves a lot of sudokus in a very short time.  It just purely follows the logic of a sudoku using the constraints that are the rules of Sudoku: 
 * Each square must have a number
 * Each row, column and "square" must have 9 unique numbers.  
The algorithm works in this way:  
 * For every square, find all the other numbers on the same row, the same column and the same square.  All the numbers from 1-9 that are missing are available for this square.  If just one number is missing, this has to be the number in the square.
Lets look at one of the most used sudoku examples:    
 ``` 
5 3 0 | 0 7 0 | 0 0 0
6 0 0 | 1 9 5 | 0 0 0
0 9 8 | 0 0 0 | 0 6 0
---------------------
8 0 0 | 0 6 0 | 0 0 3
4 0 0 | 8 0 3 | 0 0 1
7 0 0 | 0 2 0 | 0 0 6
---------------------
0 6 0 | 0 0 0 | 2 8 0
0 0 0 | 4 1 9 | 0*0*5
0 0 0 | 0 8 0 | 0 7 9
```
0 means empty square.  
Now lets look at the square at 8,8 (7,7 0-indexed).  I have marked this with stars around it.  
The column have these values:  6,8,7  
The row have these values: 4,1,9,5  
And the square have these values:  2,8,7,9  
This means 1,2,4,5,6,7,8,9 all are impossible.  The _only_ number that can be in this square is 3.  So we can safely set 3 in this square.  
You do this for all squares, and when you have done that - do the same again.  Since some squares that didnt have value, now have value, more values are filled in.  The sudoku over will be solved 100% by just doing this 5 times. 
  
The catch is that not all sudokus are possible to solve this way.  

### Algorithm loosely based on Alldifferent-Tabu Search
This algorithm is very loosely based on the article "A Hybrid alldifferent-Tabu Search Algorithm for Solving Sudoku Puzzles" by Ricardo Soto, Broderick Crawford, Cristian Galleguillos, Fernando Paredes and Enrique Norero.  You can find the article here:  
https://www.hindawi.com/journals/cin/2015/286354/  
The idea is simple.  We put random values on the board, and then we score the board.  Now we change _one_ value and see if the number of errors is lower than the current board.  If it is, then we keep it.  In other words, we can call the error count "a score" for a certain instance of a board. The thing is that if we just do this, then some hard sudoku puzzles sometimes get stuck in a local optimum (meaning that there are some errors it is impossible to get out of, the board is "almost" correct).  To avoid this, we introduce a socalled Tabu search:
Instead of making one suggested change, we create multiple alternatives.    
Instead of keeping the board whenever the score is better than the current score, we always keep the best of the alternatives - but we add the resulting board to a tabu-table.  This tabu table is then checked whenever we create alternatives.  If the resulting board is in the list, we ignore it.  
This means that the score can go up and down, but it will - sooner or later - reach 0 (meaning that the puzzle is solved).  

Please note that this implementation is very slow, but that is due to the implementation - not the algorithm.  For example the error count method is slow (it always check the whole board).  As I wrote above, this is meant as a test of algorithms - not a speed test.  But don't blame the algorithm for non-optimized code.
  
  

