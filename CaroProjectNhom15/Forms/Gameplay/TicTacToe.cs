using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms.Forms.Gameplay
{
    public partial class TicTacToe : Form
    {
        bool turn = true; //true = O, flase = X
        int turn_played = 0;

        public TicTacToe()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game made by Tri", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_CLick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                b.Text = "O";
            }
            else
            {
                b.Text = "X";
            }
            turn = !turn;
            turn_played++;
            b.Enabled = false;
            checkWinner();
        }

        private void checkWinner()
        {
            bool thereIsWinner = false;
            //horizontal
            if (Btn_A1.Text == Btn_A2.Text && Btn_A2.Text == Btn_A3.Text && (!Btn_A1.Enabled))
                thereIsWinner = true;
            else if (Btn_B1.Text == Btn_B2.Text && Btn_B2.Text == Btn_B3.Text && (!Btn_B1.Enabled))
                thereIsWinner = true;
            else if (Btn_C1.Text == Btn_C2.Text && Btn_C2.Text == Btn_C3.Text && (!Btn_C1.Enabled))
                thereIsWinner = true;
            //vertical
            else if (Btn_A1.Text == Btn_B1.Text && Btn_B1.Text == Btn_C1.Text && (!Btn_A1.Enabled))
                thereIsWinner = true;
            else if (Btn_A2.Text == Btn_B2.Text && Btn_B2.Text == Btn_C2.Text && (!Btn_A2.Enabled))
                thereIsWinner = true;
            else if (Btn_A3.Text == Btn_B3.Text && Btn_B3.Text == Btn_C3.Text && (!Btn_A3.Enabled))
                thereIsWinner = true;
            //diagonal
            else if (Btn_A1.Text == Btn_B2.Text && Btn_B2.Text == Btn_C3.Text && (!Btn_A1.Enabled))
                thereIsWinner = true;
            else if (Btn_A3.Text == Btn_B2.Text && Btn_B2.Text == Btn_C1.Text && (!Btn_A3.Enabled))
                thereIsWinner = true;

            if (thereIsWinner)
            {
                String winner = "";
                if (turn)
                    winner = "X";
                else
                    winner = "O";
                MessageBox.Show("Winner is " + winner + "!");
                disableButtons();
            }
            else if (thereIsWinner == false && turn_played == 9)
            {
                MessageBox.Show("Draw!");
            }
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch
            {

            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_played = 0;
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                }
            }
            catch
            {

            }
        }
    }
}
