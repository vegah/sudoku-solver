using System;
using System.Collections.Generic;
using System.Linq;
using Fantasista.sudoku.Solvers.Helpers;

namespace Fantasista.sudoku.Solvers
{
    public class AllDifferentWithTabuSearch : ISolver
    {

        private class Patch
        {
            private readonly ConstraintCollection _constraints;
            private int _oldValue;
            private int _newValue;
            private static Random rand = new Random();

            public Patch(Square square, ConstraintCollection constraints)
            {
                Square = square;
                _constraints = constraints;
                _oldValue = square.Value;
                _newValue = _constraints.Get(rand.Next(_constraints.Count)).LegalValue;
            }

            public void ApplyPatch()
            {
                Square.SetValue(_newValue);
            }

            public void UndoPatch()
            {
                Square.SetValue(_oldValue);
            }
            public void ScorePatch(Board board,TabuList tabulist)
            {
                ApplyPatch();
                if (tabulist.Exist(board)) Score=0x1ffff;
                else Score = board.GetErrorCount();
                UndoPatch();
            }

            public override string ToString()
            {
                return "Score " + Score;
            }
  
            public Square Square { get; private set; }
            public int Score { get; private set; }
        }



        private SquareCollection _empty_squares;
        private Random _rnd;
        private List<ConstraintCollection> _constraints;
        private TabuList _tabu_list; 
        public AllDifferentWithTabuSearch() 
        {
            _tabu_list=new TabuList();
            _constraints = new List<ConstraintCollection>();
            _rnd=new Random();
        }
        private int GetNextIndex(int[] frequencies)
        {
            for (var i=0;i<frequencies.Length;i++)
            {
                if (frequencies[i]!=9) return i;
            }
            return 0;
        }
        // Fill in the board with the right numbers.  This can probably be done more intelligently than this.
        // Currently we just make sure that all the numbers are there.  9 of every number.  From here, we will
        // switch numbers...
        public void Init(Board board)
        {
            // Get the frequencies of numbers... Every number should be 9 times when filled in.
            var frequencies = board.GetFrequencies();
            _empty_squares = board.GetCurrentlyEmpty();
            foreach (var square in _empty_squares)
            {
                var constraints = square.GetConstraints();
                constraints.Update();
                _constraints.Add(constraints);
            }
            foreach (var square in _empty_squares)
            {
                var index = GetNextIndex(frequencies);
                square.SetValue(index+1);
                frequencies[index]++;
            }
        }

        private Patch CreatePatch() {
            var square1 = _rnd.Next(_empty_squares.Count());
            return new Patch(_empty_squares.Get(square1),_constraints[square1]);
        }

        private IEnumerable<Patch> CreatePatches(int number_of_patches)
        {
            for (var i=0;i<number_of_patches;i++)
                yield return CreatePatch();
        } 


        public void Solve(Board board)
        {
            var patches_to_test = CreatePatches(100).ToArray();
            foreach (var patch in patches_to_test)
            {
                patch.ScorePatch(board,_tabu_list);
            }
            var filter_out_invalid_patches = patches_to_test.Where(x=>x.Score<0xffff);
            var best_alternative = filter_out_invalid_patches.OrderBy(x=>x.Score).FirstOrDefault();
            if (best_alternative!=null)
            {
                best_alternative.ApplyPatch();
                _tabu_list.Add(board);
            }
                

        }
    }
}