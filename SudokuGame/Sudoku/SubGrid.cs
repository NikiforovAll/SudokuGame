using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    [Serializable]
    public class SubGrid
    {

        Cell[,] Arr = new Cell[3, 3];

        public SubGrid()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Arr[i, j] = new Cell();
        }

        public Cell[,] GetCells
        {
            get { return Arr; }
        }

        public void setCell(int x, int y, int value)
        {
            if (x < 3 && y < 3 && x >= 0 && y >= 0)
            {
                Arr[x, y].CellVal = value;
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
                    if (Arr[i, j].CellVal == val)
                    {
                        posY = i;
                        posX = j;
                        return true;
                    }

                }
            }
            return false;
        }


        public void swapValue(int x, int y)
        {
            int xi, xj, yi, yj, tmpVal;
            if (this.IsInSubGrid(x, out xi, out xj) && this.IsInSubGrid(y, out yi, out yj))
            {
                tmpVal = Arr[xi, xj].CellVal;
                Arr[xi, xj].CellVal = Arr[yi, yj].CellVal;
                Arr[yi, yj].CellVal = tmpVal;
            }
        }

    }
}
