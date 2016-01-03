using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Sudoku
{
    [Serializable]
    public class SudokuGrid
    {

        public enum GameDifficulty { Easy = 10, Medium = 25, Hard = 50 }
        private SubGrid[,] _arr = new SubGrid[3, 3];
        private SubGrid[,] _arrAnswer = new SubGrid[3, 3];

        public SudokuGrid(GameDifficulty diff = GameDifficulty.Easy)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    _arr[i, j] = new SubGrid();
            }
            Init();
            Shuffle();
            _arrAnswer = ((SubGrid[,])DeepClone(_arr));
            SetDifficulty(diff);
        }

        public Cell[,] GetAnswer()
        {
            Cell[,] ans = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    ans[i, j] = _arrAnswer[i / 3, j / 3].GetCells[i % 3, j % 3];
                }
            }
            return ans;
        }



        public bool IsFinish()
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
            get { return _arr[y / 3, x / 3].GetCells[y % 3, x % 3]; }
            set { _arr[y / 3, x / 3].GetCells[y % 3, x % 3] = value; }
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

        private void Shuffle(int shuffleDepth = 15)
        {
            if (shuffleDepth-- > 0)
            {
                Random r = new Random(Guid.NewGuid().GetHashCode());
                int val1, val2;

                val1 = r.Next(1, 10);
                val2 = r.Next(1, 10);

                foreach (SubGrid sg in _arr)
                {
                    sg.SwapValue(val1, val2);
                }
                Shuffle(shuffleDepth);
            }

        }

        public void SetDifficulty(GameDifficulty diff)
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
        public bool MakeMove(int y, int x, int val)
        {

            if (y >= 0 && y < 9 && x >= 0 && x < 9 && val < 10 && val > 0)
            {
                if (!this[y, x].IsUnchangable)
                {
                    if (CheckColumn(y, val) && CheckRow(x, val) && !_arr[y / 3, x / 3].IsInSubGrid(val))
                    {
                        this[y, x].CellVal = val;
                        return true;
                    }
                }
            }
            return false;

        }
        public void MakeWrongMove(int y, int x)
        {
            if (y >= 0 && y < 9 && x >= 0 && x < 9)
            {
                if (!this[y, x].IsUnchangable)
                {
                    this[y, x].CellVal = 0;
                }
            }
        }

        private bool CheckRow(int column, int val)
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
        private bool CheckColumn(int row, int val)
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
