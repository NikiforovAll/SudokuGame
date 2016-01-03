using System;

namespace Sudoku
{
    [Serializable]
    public class Cell
    {
        private int _value;
        private bool _isUnChangeable;
        public Cell()
        {
            _isUnChangeable = true;
        }
        public int CellVal
        {
            set
            {
                if (value < 10 && value >= 0)
                {
                    this._value = value;
                }
                else
                {
                    throw new Exception("Inappropriate  value of cell");
                }
            }
            get { return _value; }
        }

        public bool IsUnchangable
        {
            get { return _isUnChangeable; }
            set { _isUnChangeable = value; }
        }
    }
}
