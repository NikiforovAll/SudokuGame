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

namespace SudokuForms
{
   
    public class SudokuTableGraphic:TableLayoutPanel
    {
        public  SudokuGrid sudokuGrid;
        public bool isFinish;
        public SudokuTableGraphic(SudokuGrid sudokuGrid)
        {
            this.sudokuGrid = sudokuGrid;


            this.AutoSize = true;
            this.Top = 60;
            this.Left = 80;
          //  this.Width = 150;
           
           // this.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuCell sc = new SudokuCell(sudokuGrid[i,j],j,i);
                    if(!sudokuGrid[i,j].IsUnchangable)
                    sc.render(sudokuGrid[i,j].CellVal);
                    this.Controls.Add(sc, j, i);
                }
            }

           
         
        }

        public void fillBoard()
        {
            if(!isFinish)
            {
                this.Controls.Clear();
            Cell[,] ans = sudokuGrid.GetAnswer();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    
                    SudokuCell sc = new SudokuCell(ans[i,j], j, i);
                    this.Controls.Add(sc, j, i);
                }
            }
            isFinish = true;
            Form1.sudokuGrid = new SudokuGrid();
            }

            
        }

        
    }
}
