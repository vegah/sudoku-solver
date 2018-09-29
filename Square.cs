using System;

namespace Fantasista.sudoku
{
    public class Square 
    {
        private int _value;
        private readonly int _x;
        private readonly int _y;
        private readonly Board _board;

        private readonly int _original_value;
        public Square(int value,int x, int y, Board board)
        {
            _value=value; 
            _x=x;
            _y=y;
            _board = board;
            _original_value = value;
        }

        public void SetValue(int value)
        {
            _value=value;
        }

        public SquareCollection GetParentRow()
        {
            return _board.GetRow(_y);
        }

        public SquareCollection GetParentColumn()
        {
            return _board.GetColumn(_x);
        }

        public SquareCollection GetParentSquare()
        {
            return _board.GetSquare(((_y/3)*3)+(_x/3));
        }

        public ConstraintCollection GetConstraints()
        {
            return new ConstraintCollection(this);
        }

        public int Value 
        {
            get { return _value;}
        }

        public override string ToString(){
            return _value.ToString();
        }

        public bool IsEmpty(){
            return _value==0;
        }

    }
}