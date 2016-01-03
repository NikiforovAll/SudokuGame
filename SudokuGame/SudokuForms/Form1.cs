using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sudoku;
using System.Drawing.Drawing2D;
using CodeProject;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SudokuForms
{
    public partial class Form1 : Form
    {
        public static SudokuTableGraphic gameBoard;
        public static SudokuGrid sudokuGrid;
        public static Dictionary<Point, int> moveLog;
        IFormatter objBinaryFormatter = new BinaryFormatter();

        public Form1()
        {
            InitializeComponent();
            graphicalOverlay1.Owner = this;
            graphicalOverlay1.Paint += graphicalOverlay1_Paint;
            this.FormClosed += Form1_FormClosed;

        }
        public void DrawBoard()
        {
            if (!(gameBoard == null))
            {
                gameBoard.Dispose();
            }
            SudokuGrid.GameDifficulty diff;
            if (radioButton1.Checked)
            {
                diff = SudokuGrid.GameDifficulty.Hard;
            }
            else if (radioButton2.Checked)
            {
                diff = SudokuGrid.GameDifficulty.Medium;
            }
            else
            {
                diff = SudokuGrid.GameDifficulty.Easy;
            }
            sudokuGrid = new SudokuGrid(diff);
            gameBoard = new SudokuTableGraphic(sudokuGrid);
            moveLog = new Dictionary<Point, int>();

            this.Controls.Add(gameBoard);


        }
        void graphicalOverlay1_Paint(object sender, PaintEventArgs e)
        {
            Pen myPen = new Pen(Color.FromArgb(105, 139, 34), 6);
            e.Graphics.DrawLine(myPen, new Point(80, 60), new Point(580, 60));
            e.Graphics.DrawLine(myPen, new Point(80, 60), new Point(80, 560));
            e.Graphics.DrawLine(myPen, new Point(80, 560), new Point(580, 560));
            e.Graphics.DrawLine(myPen, new Point(581, 560), new Point(581, 60));
            Pen innerTable = new Pen(Color.FromArgb(238, 201, 0), (float)6.0);

            e.Graphics.DrawLine(innerTable, new Point(248, 60), new Point(248, 560));
            e.Graphics.DrawLine(innerTable, new Point(416, 60), new Point(416, 560));
            e.Graphics.DrawLine(innerTable, new Point(80, 227), new Point(580, 227));
            e.Graphics.DrawLine(innerTable, new Point(80, 396), new Point(580, 396));


        }
        private void button1_Click(object sender, EventArgs e)
        {
            DrawBoard();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //  deserialization
            Stream objstreamdeserialize_SudokuGrid = new FileStream("data.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Stream objstreamdeserialize_moveLog = new FileStream("data1.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                sudokuGrid = (SudokuGrid)objBinaryFormatter.Deserialize(objstreamdeserialize_SudokuGrid);
                moveLog = (Dictionary<Point, int>)objBinaryFormatter.Deserialize(objstreamdeserialize_moveLog);
                foreach (KeyValuePair<Point, int> kvp in moveLog)
                {
                    sudokuGrid.MakeMove(kvp.Key.X, kvp.Key.Y, kvp.Value);//y,x
                }
                gameBoard = new SudokuTableGraphic(sudokuGrid);
                this.Controls.Add(gameBoard);
            }
            catch (Exception ea)
            {
                DrawBoard();
            }
            finally
            {
                objstreamdeserialize_SudokuGrid.Close();
                objstreamdeserialize_moveLog.Close();
            }
        }
        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // serialization 
            Stream objStream = new FileStream("data.bin", FileMode.Create, FileAccess.ReadWrite, FileShare.None);

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    if (!sudokuGrid[i, j].IsUnchangable)
                        sudokuGrid[i, j].CellVal = 0;
            }
            objBinaryFormatter.Serialize(objStream, sudokuGrid);
            objStream.Close();
            Stream objStream1 = new FileStream("data1.bin", FileMode.Create, FileAccess.ReadWrite, FileShare.None);
            objBinaryFormatter.Serialize(objStream1, MoveList.moveLog);
            objStream1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            gameBoard.fillBoard();
        }

    }
}
