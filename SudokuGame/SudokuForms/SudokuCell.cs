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
    [Serializable]
    class SudokuCell:Panel
    {
        //class CoordNumeric:NumericUpDown
        //{
        //    public int coordx;
        //    public int coordy;
            
        //}
        private Label textLabel;
    //    CoordNumeric numeric;
        TextBox textBoxValue; 
           
        public int x, y;
        public SudokuCell(Cell cell, int x , int y)
        {
            this.Width = 50;
            this.Height = 50;
            this.x = x;
            this.y = y;
          
            textLabel = new Label();
            textLabel.TextAlign = ContentAlignment.BottomCenter;
            textLabel.Width = 50;
            textLabel.Height = 50;
            textLabel.Font = new Font(textLabel.Font.FontFamily, 14, FontStyle.Bold);
            textLabel.BackColor = Color.Gray;
            Label label2 = new Label();
            label2.Top = 45;
            label2.Width = 50;
            label2.Height = 4;
            label2.BackColor = Color.FromArgb(123,123,1);
            textLabel.BackColor = Color.FromArgb(70, 198, 60);
            
            
            if(cell.IsUnchangable)
            {
                setCellVal(cell.CellVal);
                textLabel.BackColor = Color.FromArgb(70, 198, 60);
                textLabel.TextAlign = ContentAlignment.MiddleCenter;

            }
            else
            {
                textBoxValue = new TextBox();
                textBoxValue.Top = this.Height / 6;
                //textBoxValue.Left = 20;
                textBoxValue.Height = this.Height;
                textBoxValue.MaxLength = 1;
                textBoxValue.Width = this.Width/2;
                textBoxValue.Left = this.Width / 4;
                textBoxValue.TextAlign = HorizontalAlignment.Center;
                textBoxValue.Font = new Font(textBoxValue.Font.FontFamily, 16);
                textBoxValue.BackColor = textLabel.BackColor;
                
                textBoxValue.KeyPress += textBoxValue_KeyPress;
                textBoxValue.TextChanged += textBoxValue_TextChanged;
                textBoxValue.Click += textBoxValue_Click;
                this.Controls.Add(textBoxValue);
                //numeric = new CoordNumeric();
                //numeric.coordx = x;
                //numeric.coordy = y;
                //numeric.ValueChanged += numeric_ValueChanged;
                //numeric.Width = 30;
                //numeric.Left = 10;
                //numeric.BackColor = Color.LightGray;
                //numeric.Maximum = 9;
                //this.Controls.Add(numeric);
                
              
            }
           
            this.Controls.Add(label2);
            this.Controls.Add(textLabel);

           

        }

        void textBoxValue_Click(object sender, EventArgs e)
        {
            this.textBoxValue.SelectionStart = 0;
            this.textBoxValue.SelectionLength = this.textBoxValue.Text.Length;
        }

        void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            TextBox tmp = (TextBox)sender;
            int val;
            Int32.TryParse(tmp.Text, out val);
            MoveList.addMove(new Point(this.y, this.x), val);
            if(Form1.sudokuGrid.makeMove(y,x,val))
           {
                this.textLabel.BackColor = Color.Yellow;
                this.textBoxValue.BackColor = Color.Yellow;
              //  setCellVal(val);
            }
            else
            {
                if(tmp.Text != String.Empty)
                {
                    Form1.sudokuGrid.makeWrongMove(y, x);
                    this.textLabel.BackColor = Color.Red; ;
                    this.textBoxValue.BackColor = Color.Red; ;
                }
                else
                {
                    this.textLabel.BackColor = Color.FromArgb(123, 123, 1);
                    this.textBoxValue.BackColor = Color.Gray; ;
                }
            }
            if(Form1.sudokuGrid.isFinish())
            {
                MessageBox.Show("You WON! ");
                Form1.gameBoard.fillBoard();
            }
        }

        void textBoxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsDigit(e.KeyChar)||e.KeyChar==(char)Keys.Back);
        }

        
        public void render(int val)
        {
            if(val!=0)
            {
                this.textBoxValue.Text = val.ToString();
                this.textLabel.BackColor = Color.Yellow;
                this.textBoxValue.BackColor = Color.Yellow;
            }
            
            

           // this.numeric.Value = val;
           // numeric_ValueChanged(this.numeric, new EventArgs());
        }
        //
        //void numeric_ValueChanged(object sender, EventArgs e)
        //{
        //    int val = (int)((NumericUpDown)sender).Value;
        //    int y = ((CoordNumeric)sender).coordy;
        //    int x = ((CoordNumeric)sender).coordx;
        //    MoveList.addMove(new Point(y,x),val);
        //    if(Form1.sudokuGrid.makeMove(y,x,val))
        //    {
        //        this.textLabel.BackColor = Color.Yellow;
        //        setCellVal(val);
        //    }
        //    else
        //    {
        //        setCellVal(0);
        //        Form1.sudokuGrid.makeWrongMove(y, x);
        //        this.textLabel.BackColor = Color.Red;
        //    }
        //    if(Form1.sudokuGrid.isFinish())
        //    {
        //        MessageBox.Show("You WON! ");
        //        Form1.gameBoard.fillBoard();
        //    }
        //}

        public void setCellVal(int val)
        {
            if(val!=0)
                this.textLabel.Text = val.ToString();
            else
            {
                
                textLabel.Text = "";
            }
        }

    }
}
