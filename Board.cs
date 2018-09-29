using System;
using System.Linq;
using System.Text;

namespace Fantasista.sudoku
{

    public class Board 
    {
        private readonly Square[][] _board;
        private readonly int _board_size;

        public Board(int [][] board)
        {
            _board=board.Select((x,idxy)=>x.Select((y,idxx)=>new Square(y,idxx,idxy,this)).ToArray()).ToArray();
            _board_size = _board.Length;
        }

        public SquareCollection GetRow(int y)
        {
            return new SquareCollection(_board[y]);
        }

        public int[] GetFrequencies()
        {
            var frequency_array = new int[] {0,0,0,0,0,0,0,0,0};
            for (var y=0;y<_board_size;y++)
            {
                for (var x=0;x<_board_size;x++)
                {
                    var val = Get(x,y);
                    if (!val.IsEmpty()) frequency_array[val.Value-1]++;
                }
            }
            return frequency_array;
        }

        public Square Get(int x,int y) {
            return _board[y][x];
        }

        public SquareCollection GetColumn(int x)
        {
            return new SquareCollection(_board.Select(col=>col[x]).ToArray());
        }

        public SquareCollection GetSquare(int square)
        {
            var ys=new int[] {1,1,1,4,4,4,7,7,7};
            var xs=new int[]{1,4,7,1,4,7,1,4,7};
            var y=ys[square];
            var x=xs[square];
            var array = new Square[] {
                _board[y-1][x-1],
                _board[y-1][x],
                _board[y-1][x+1],

                _board[y][x-1],
                _board[y][x],
                _board[y][x+1],

                _board[y+1][x-1],
                _board[y+1][x],
                _board[y+1][x+1],
            };
            return new SquareCollection(array);
        }

        public string ToShortString() 
        {
            var sb = new StringBuilder();
            for (var y=0;y<_board_size;y++)
            {
                var short_string = _board[y].Select(x=>x.Value.ToString());
                sb.Append(string.Join('-',short_string.ToArray()));
            }
            return sb.ToString();
        }

        public override string ToString() 
        {
            var sb = new StringBuilder();
            for (var y=0;y<_board_size;y++)
            {
                for (var x=0;x<_board_size;x++)
                {
                    sb.Append(_board[y][x]+" ");

                   if  ((x+1)==_board_size) sb.Append(" === "+GetRow(y).GetErrors());
                   else if ((x+1)%3==0) sb.Append(" | ");
                }
                sb.Append(System.Environment.NewLine);
                if ((y+1)==_board_size) 
                {
                    sb.Append("".PadLeft(_board_size,'='));
                    sb.Append(System.Environment.NewLine);
                }
                else if ((y+1)%3==0) sb.Append("".PadLeft(_board_size*2+2,'-')+System.Environment.NewLine);
            }
            return sb.ToString();
        }

        public SquareCollection GetCurrentlyEmpty()
        {
            return new SquareCollection(All.Where(x=>x.IsEmpty()).ToArray());
        }

        public int Size 
        {
            get { return _board_size;}
        }

        public int GetErrorCount()
        {
            var errors = 0;
            for (var x=0;x<_board_size;x++) errors+=GetColumn(x).GetErrors();
            for (var x=0;x<_board_size;x++) errors+=GetRow(x).GetErrors();
            for (var x=0;x<_board_size;x++) errors+=GetSquare(x).GetErrors();
            return errors;
        }

        private Square[] All
        {
            get
            {
                return _board.SelectMany(x=>x).ToArray();
            }
        }

        

    }
}