using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Sudoku
{
    [Serializable]
    public class SudokuGrid
    {
        
        public enum GameDifficulty { Easy = 10, Medium = 25, Hard = 50 }
        private SubGrid[,] Arr = new SubGrid[3, 3];
        private SubGrid[,] ArrAnswer = new SubGrid[3, 3];

        public SudokuGrid(GameDifficulty diff = GameDifficulty.Easy)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    Arr[i, j] = new SubGrid();
            }
            Init();
            Shuffle();
            ArrAnswer = ((SubGrid[,])DeepClone(Arr));
            setDifficulty(diff);
           
        }

        public Cell[,] GetAnswer()
        {
            Cell[,] ans = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ans[i, j] = ArrAnswer[i / 3, j / 3].GetCells[i % 3, j % 3];
                }
            }
            return ans;
        }



        public bool isFinish()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    if (this[i, j].CellVal == 0)
                        return false;
                }
            return true;
        }
        public Cell this[int y, int x]
        {
            get { return Arr[y / 3, x / 3].GetCells[y % 3, x % 3]; }
            set { Arr[y / 3, x / 3].GetCells[y % 3, x % 3] = value; }
        }


        // initialization of the seed
        #region initSeed
        private void Init()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this[i, j].CellVal = (i * 3 + i / 3 + j) % 9 + 1;
                }
            }
        }

        private void Shuffle(int ShuffleDepth = 15)
        {
            if (ShuffleDepth-- > 0)
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int val1, val2;

                val1 = r.Next(1, 10);
                val2 = r.Next(1, 10);

                foreach (SubGrid sg in Arr)
                {
                    sg.swapValue(val1, val2);
                }
                Shuffle(ShuffleDepth);
            }

        }

        public void setDifficulty(GameDifficulty diff)
        {
            int numBlankCell = (int)diff;
            Random r = new Random(Guid.NewGuid().GetHashCode());
            int val1, val2;

            while (numBlankCell > 0)
            {
                val1 = r.Next(0, 9);
                val2 = r.Next(0, 9);
                if (this[val1, val2].IsUnchangable)
                {
                    this[val1, val2].IsUnchangable = false;
                    this[val1, val2].CellVal = 0;
                    numBlankCell--;
                }
            }
        }
        #endregion

        // make move
        public bool makeMove(int y, int x, int val)
        {
           
            if (y >= 0 && y < 9 && x >= 0 && x < 9 && val < 10 && val > 0 )
            {
                if (!this[y, x].IsUnchangable)
                {
                    if (checkColumn(y, val) && checkRow(x, val) && !Arr[y / 3, x / 3].IsInSubGrid(val))
                    {
                        this[y, x].CellVal = val;
                        return true;
                    }
                }
            }
            return false;

        }
        public void makeWrongMove(int y, int x)
        {
            if (y >= 0 && y < 9 && x >= 0 && x < 9)
            {
                if (!this[y, x].IsUnchangable)
                {
                    this[y, x].CellVal = 0;
                }
            }
        }

        private bool checkRow(int column, int val)
        {
            for (int i = 0; i < 9; i++)
            {
                if (this[i, column].CellVal == val)
                {
                    return false;
                }
            }
            return true;
        }
        private bool checkColumn(int row, int val)
        {
            for (int i = 0; i < 9; i++)
            {
                if (this[row, i].CellVal == val)
                {
                    return false;
                }
            }
            return true;
        }

        private static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;
                return (T)formatter.Deserialize(ms);
            }
        }

    }
}
