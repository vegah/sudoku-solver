namespace Fantasista.sudoku
{
    public class Constraint
    {
        private int _legal_value;
        public Constraint(int legal_value)
        {
            _legal_value=legal_value;
        }

        public int LegalValue 
        {
            get { return _legal_value;}    
        } 
    }
}