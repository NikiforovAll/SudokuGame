using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace SudokuForms
{
    [Serializable]
    public static class MoveList
    {
        public static Dictionary<Point, int> moveLog = new Dictionary<Point, int>();

        public static void addMove(Point p, int val)
        {
            if (moveLog.ContainsKey(p))
            {
                moveLog[p] = val;
            }
            else
            {
                moveLog.Add(p, val);
            }

        }
    }
}
