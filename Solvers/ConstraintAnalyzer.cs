using System;
using Fantasista.sudoku;

namespace Fantasista.sudoku.Solvers
{
    public class ConstraintAnalyzer : ISolver
    {

        private void SolveSquare(Square square)
        {
            var constraints = square.GetConstraints();
            constraints.Update();
            if (constraints.Count==1)
            {
                square.SetValue(constraints.Get(0).LegalValue);
            }
        }
        private void SolveRow(int y, Board board)
        {
            for (var x=0;x<board.Size;x++)
            {
                var square = board.Get(x,y);
                SolveSquare(square);
            }
        }
        
        public void Solve(Board board)
        {
            SolveInternal(board);
        }

        public void Init(Board board) 
        {
            // Do nothing
        }

        private void SolveInternal(Board board)
        {
            for (var y = 0;y<board.Size;y++)
            {
                SolveRow(y,board);
            }
        }


    }

} 