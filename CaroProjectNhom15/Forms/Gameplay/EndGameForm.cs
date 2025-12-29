using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms
{
    public partial class EndGameForm : Form
    {
        public EndGameForm(bool isWinner)
        {
            InitializeComponent();

            // Thiết lập màu sắc và tiêu đề
            this.BackColor = isWinner ? Color.Green : Color.Red;
            this.Text = "Kết thúc Game";

            Lbl_ResultTitle.Text = isWinner ? "YOU WIN" : "YOU LOSE";

        }

        private void Btn_Exit_Click(object sender, EventArgs e)
        {
            // Kết quả trả về cho GameForm biết là người chơi muốn đóng game.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Bạn có thể thêm nút "Chơi lại" ở đây nếu có logic New Game.
    }
}