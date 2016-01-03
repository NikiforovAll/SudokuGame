using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    [Serializable]
    public class Cell
    {
        private int value;
        private bool isUnChangeable;
        public Cell()
        {
            isUnChangeable = true;            

        }
        public int CellVal
        {
            set
            {
                if (value < 10 && value >= 0)
                {
                    this.value = value;
                }
                else
                {
                    throw new Exception("Inappropriate  value of cell");
                }
            }
            get { return value; }
        }

        public bool IsUnchangable
        {
            get { return isUnChangeable; }
            set { isUnChangeable = value; }
        }
    }
}
