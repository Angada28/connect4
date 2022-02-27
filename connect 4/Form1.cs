using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace connect_4
{
    public partial class Form1 : Form
    {
        int red = 0, black = 0;
        string horizontal = "", vertical = "", p1 = "", p2 = "", win = "";
        bool currentTurn = true;
        Random box = new Random();
        string[,] spots =
        {
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
            {"e","e","e","e","e","e" },
        };
        public Form1(string x, string y)
        {
            InitializeComponent();
            p1 = x;
            p2 = y;
            move1();
            startingposition();
        }
        private void startingposition()
        {
            Point startingPosition = this.Location;
            startingPosition.Offset(10, 10);
            Cursor.Position = PointToScreen(startingPosition);
        }
        private void move1()
        {
            switch (box.Next(2))
            {
                case 0:
                    currentTurn = true;
                    MessageBox.Show(p1 + " will go first");
                    break;
                case 1:
                    currentTurn = false;
                    MessageBox.Show(p2 + " will go first");
                    break;
            }
        }
        private void messageRed()
        {
            MessageBox.Show("Congrats " + win + " wins"); if (MessageBox.Show("Do you want to try again?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                Application.Exit();
            }
        }

        private void showcolour(object sender, EventArgs e)
        {
            PictureBox picked = (PictureBox)sender;
            char[] broken = picked.Name.ToCharArray();
            int num = int.Parse(broken[1].ToString());
            if (currentTurn) this.Controls["d" + num].BackgroundImage = Properties.Resources.connect4_new_red_piece;
            else this.Controls["d" + num].BackgroundImage = Properties.Resources.connect4_black_piece;
            this.Controls["a" + num].BackgroundImage = Properties.Resources.arrow_down_transparent_4;
        }
        private void d0_MouseLeave(object sender, EventArgs e)
        {
            PictureBox picked = (PictureBox)sender;
            char[] broken = picked.Name.ToCharArray();
            int num = int.Parse(broken[1].ToString());
            this.Controls["d" + num].BackgroundImage = null;
            this.Controls["a" + num].BackgroundImage = null;
        }
        private int blankcheck(int col)
        {
            int blanks = 0;
            for (int i = 0; i < 6; i++)
            {
                if (spots[col, i] == "e") blanks++;
            }
            return blanks;
        }
        private void dropit(object sender, EventArgs e)
        {
            PictureBox picked = (PictureBox)sender;
            char[] broken = picked.Name.ToCharArray();
            int num = int.Parse(broken[1].ToString());
            int dropspot = blankcheck(num) - 1;
            this.Controls["d" + num].BackgroundImage = null;
            this.Controls["a" + num].BackgroundImage = null;
            if (dropspot > -1)
            {
                if (currentTurn)
                {
                    red++;
                    panel1.Controls["b" + num + dropspot].BackgroundImage = Properties.Resources.connect4_new_red_piece;
                    spots[num, dropspot] = "r";
                    currentTurn = false;
                    win = p1;
                }
                else if (!currentTurn)
                {
                    black++;
                    panel1.Controls["b" + num + dropspot].BackgroundImage = Properties.Resources.connect4_black_piece;
                    spots[num, dropspot] = "b";
                    currentTurn = true;
                    win = p2;
                }
                wincheck();
                startingposition();
            }
            else MessageBox.Show("this collumn is full");
        }
        private void wincheck()
        {
            horizontal = "";
            vertical = "";
            //horizontal check
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    horizontal = horizontal + spots[j, i];
                }
                horizontal = horizontal + "----";
            }
            //vertical check
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    vertical = vertical + spots[i, j];
                }
                vertical = vertical + "----";
            }
            //ascending diagonal check
            for (int i = 3; i < 7; i++)
            {
                for (int j = 0; j < 6 - 3; j++)
                {
                    if ((spots[i, j] == "r" && spots[i - 1, j + 1] == "r" && spots[i - 2, j + 2] == "r" && spots[i - 3, j + 3] == "r") || (spots[i, j] == "b" && spots[i - 1, j + 1] == "b" && spots[i - 2, j + 2] == "b" && spots[i - 3, j + 3] == "b"))
                    {
                        messageRed();
                    }
                }
            }
            //descending vertical check
            for (int i = 3; i < 7; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    if ((spots[i, j] == "r" && spots[i - 1, j - 1] == "r" && spots[i - 2, j - 2] == "r" && spots[i - 3, j - 3] == "r") || (spots[i, j] == "b" && spots[i - 1, j - 1] == "b" && spots[i - 2, j - 2] == "b" && spots[i - 3, j - 3] == "b"))
                        messageRed();
                }
            }
            if ((vertical.Contains("rrrr")) || (horizontal.Contains("rrrr"))|| (vertical.Contains("bbbb")) || (horizontal.Contains("bbbb")))
            {
                messageRed();
            }
            //tie scenario
            else if (red == 21 && black == 21)
            {
                MessageBox.Show("No one wins"); if (MessageBox.Show("Do you want to try again?", "Continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }             
    }
}