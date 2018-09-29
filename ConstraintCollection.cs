using System.Collections.Generic;
using System.Linq;
namespace Fantasista.sudoku
{
    public class ConstraintCollection
    {
        private readonly Square _square;
        private List<Constraint> _constraints;
        public ConstraintCollection(Square square)
        {
            _square = square;
            _constraints = new List<Constraint>();

        }

        public Constraint Get(int index) {
            return _constraints[index];
        }

        public bool IsValid(int value)
        {
            return _constraints.Where(x=>x.LegalValue==value).Count()==1;
        }

        public int Count 
        {
            get{return _constraints.Count;}
        }
        private void CreateConstraints(IEnumerable<int> values)
        {
            for (var x=1;x<=9;x++) {
                var val = values.Count(z=>z==x);
                if (val==0) _constraints.Add(new Constraint(x));  
            }
        }

        public void Update()
        {
            _constraints.Clear();
            if (_square.Value!=0) return;
            var x=new [] {0,1,2,3,4,5,6,7,8,9};
            var col =_square.GetParentColumn();
            var row = _square.GetParentRow();
            var square = _square.GetParentSquare();
            var all=col.Concat(col).Concat(row).Concat(square).Select(z=>z.Value).Distinct();
            CreateConstraints(all);
        }
    }
}