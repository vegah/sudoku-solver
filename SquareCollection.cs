using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Fantasista.sudoku
{
    public class SquareCollection : IEnumerable<Square>
    {
        private readonly Square[] _rowData;

        public SquareCollection(Square[] rowData)
        {
            _rowData = rowData;
        }

        public Square Get(int index) 
        {
            return _rowData[index];
        }

        public IEnumerator<Square> GetEnumerator()
        {
            return _rowData.Cast<Square>().GetEnumerator();
        }

        public int GetErrors()
        {
            return 9-(_rowData.Where(x=>x.Value!=0).Select(x=>x.Value).Distinct().Count());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}