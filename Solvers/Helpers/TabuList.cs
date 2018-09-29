using System.Collections.Generic;

namespace Fantasista.sudoku.Solvers.Helpers
{
    public class TabuList
    {
        private Dictionary<string,bool> _tabu_dictionary;
        private List<string> _history;
        public TabuList() 
        {
            _tabu_dictionary=new Dictionary<string, bool>();
            _history = new List<string>();
        }

        public void Add(Board board) 
        {
            var hash = board.ToShortString();
            _tabu_dictionary.Add(hash,true);
            _history.Add(hash);
            if (_history.Count>1000)
            {
                var remove_hash = _history[0];
                _history.RemoveAt(0);
                _tabu_dictionary.Remove(remove_hash);
            }
        }

        public bool Exist(Board board) 
        {
            var hash = board.ToShortString();
            return _tabu_dictionary.ContainsKey(hash);
        }

        public int Count
        {
            get => _history.Count;
        }
    }
}