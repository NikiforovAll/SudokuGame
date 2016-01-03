using System;

namespace Sudoku
{
    [Serializable]
    public class SubGrid
    {
        private readonly Cell[,] _arr = new Cell[3, 3];

        public SubGrid()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    _arr[i, j] = new Cell();
        }

        public Cell[,] GetCells => _arr;

        public void SetCell(int x, int y, int value)
        {
            if (x < 3 && y < 3 && x >= 0 && y >= 0)
            {
                _arr[x, y].CellVal = value;
            }
        }

        public bool IsInSubGrid(int val)
        {
            int foo;
            return IsInSubGrid(val, out foo, out foo);
        }

        public bool IsInSubGrid(int val, out int posY, out int posX)
        {
            posY = -1;
            posX = -1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_arr[i, j].CellVal == val)
                    {
                        posY = i;
                        posX = j;
                        return true;
                    }

                }
            }
            return false;
        }

        public void SwapValue(int x, int y)
        {
            int xi, xj, yi, yj, tmpVal;
            if (this.IsInSubGrid(x, out xi, out xj) && this.IsInSubGrid(y, out yi, out yj))
            {
                tmpVal = _arr[xi, xj].CellVal;
                _arr[xi, xj].CellVal = _arr[yi, yj].CellVal;
                _arr[yi, yj].CellVal = tmpVal;
            }
        }

    }
}
