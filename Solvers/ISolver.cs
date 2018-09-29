using Fantasista.sudoku;

namespace Fantasista.sudoku.Solvers
{
    public interface ISolver
    {
        void Solve(Board board);
        void Init(Board board);
    }

} 