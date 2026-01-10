using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms.Gameplay
{
    public class GameBoardManager
    {
        public Panel Board { get; set; }
        public List<Player> Player { get; set; }

        // GIỮ NGUYÊN: CurrentPlayer (0: Host/X, 1: Guest/O)
        // QUAN TRỌNG: GameForm sẽ TỰ GÁN giá trị này trước khi gọi Mark.
        public int CurrentPlayer { get; set; }

        public List<List<Button>> Matrix { get; set; }

        public event EventHandler<ButtonClickEvent> PlayerMarked;
        public event EventHandler EndedGame;

        public GameBoardManager(Panel board)
        {
            this.Board = board;
            this.Matrix = new List<List<Button>>();

            // Player 0: X, Player 1: O
            // Lưu ý: Đảm bảo Resource files có hình ảnh 'x_mark' và 'o_mark'
            this.Player = new List<Player>()
            {
                new Player(Properties.Resources.x_mark),
                new Player(Properties.Resources.o_mark)
            };

            CurrentPlayer = 0; // Host (X) đi trước
        }

        public void DrawBoard()
        {
            Board.Enabled = true; // Bật bàn cờ
            Board.Controls.Clear(); // Đảm bảo làm sạch khi Draw lại (cho New Game)
            Matrix.Clear();

            // Dùng vòng lặp tạo nút dựa trên tọa độ toán học
            for (int i = 0; i < GameCons.CHESS_BOARD_SIZE; i++) // Vòng lặp Hàng (Y)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < GameCons.CHESS_BOARD_SIZE; j++) // Vòng lặp Cột (X)
                {
                    Button btn = new Button()
                    {
                        Width = GameCons.CHESS_WIDTH,
                        Height = GameCons.CHESS_HEIGHT,
                        Location = new Point(j * GameCons.CHESS_WIDTH, i * GameCons.CHESS_HEIGHT),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag = new Point(j, i) // Tag: (X, Y)
                    };
                    btn.Click += Btn_Click;

                    Board.Controls.Add(btn);
                    Matrix[i].Add(btn);
                }
            }
        }

        // Sự kiện khi TÔI click vào bàn cờ
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null) return;

            // 1. Vẽ ngay lập tức lên bàn cờ của mình
            // CurrentPlayer đã được GameForm set trước đó (là _myRole)
            Mark(btn);

            // 2. Vô hiệu hóa bàn cờ ngay (ĐÃ XÓA logic Board.Enabled = false; ở đây,
            // để GameForm tự quản lý việc khóa/mở sau khi gửi lệnh lên Firebase)

            Point point = (Point)btn.Tag;

            // 3. Kiểm tra thắng thua CỤC BỘ
            bool isWin = IsEndGame(point);
            if (isWin) EndGame(); // Kích hoạt EndedGame và khóa Board

            // 4. Bắn sự kiện ra Form để gửi lên Firebase
            // GameForm sẽ dùng thông tin Point này để gửi tọa độ (X, Y) và check Win
            PlayerMarked?.Invoke(this, new ButtonClickEvent(point));
        }

        // Xử lý khi ĐỐI PHƯƠNG đánh (nhận từ Firebase)
        public void OtherPlayerMark(Point point)
        {
            // CurrentPlayer lúc này đã được GameForm set là vai trò của ĐỐI THỦ

            Button btn = Matrix[point.Y][point.X];
            if (btn.BackgroundImage != null) return;

            // Đã loại bỏ logic tự đổi lượt: CurrentPlayer đã được GameForm set chính xác

            Mark(btn);

            if (IsEndGame(point))
            {
                EndGame();
            }
            // ĐÃ XÓA logic trả lại lượt cho mình (CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;)
            // -> Việc quản lý lượt, bao gồm cả việc set lại CurrentPlayer sau nước đi,
            //    thuộc về logic của GameForm.
        }

        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
        }

        // Xóa tất cả các quân cờ
        public void ClearBoard()
        {
            foreach (var row in Matrix)
            {
                foreach (var btn in row)
                {
                    btn.BackgroundImage = null;
                }
            }
            Board.Refresh();
        }

        public void EndGame()
        {
            Board.Enabled = false; // Khóa bàn cờ
            // Kích hoạt sự kiện để GameForm xử lý thông báo/lưu trữ (nếu cần)
            EndedGame?.Invoke(this, new EventArgs());
        }

        // --- CÁC HÀM KIỂM TRA THẮNG THUA (Giữ nguyên) ---
        public bool IsEndGame(Point point)
        {
            Button btn = Matrix[point.Y][point.X];
            return IsEndGameHorizontal(point, btn) || IsEndGameVertical(point, btn) ||
                   IsEndGamePrimary(point, btn) || IsEndGameSub(point, btn);
        }

        private bool IsEndGameHorizontal(Point point, Button btn)
        {
            int countLeft = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X - i < 0) break;
                if (Matrix[point.Y][point.X - i].BackgroundImage == btn.BackgroundImage)
                    countLeft++;
                else break;
            }

            int countRight = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X + i >= GameCons.CHESS_BOARD_SIZE) break;
                if (Matrix[point.Y][point.X + i].BackgroundImage == btn.BackgroundImage)
                    countRight++;
                else break;
            }

            return (countLeft + countRight + 1) >= GameCons.WINNING_COUNT;
        }

        // Kiểm tra chiều dọc
        private bool IsEndGameVertical(Point point, Button btn)
        {
            int countTop = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X].BackgroundImage == btn.BackgroundImage)
                    countTop++;
                else break;
            }

            int countBottom = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.Y + i >= GameCons.CHESS_BOARD_SIZE) break;
                if (Matrix[point.Y + i][point.X].BackgroundImage == btn.BackgroundImage)
                    countBottom++;
                else break;
            }

            return (countTop + countBottom + 1) >= GameCons.WINNING_COUNT;
        }

        // Kiểm tra đường chéo chính (\)
        private bool IsEndGamePrimary(Point point, Button btn)
        {
            int countTopLeft = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                    countTopLeft++;
                else break;
            }

            int countBottomRight = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X + i >= GameCons.CHESS_BOARD_SIZE || point.Y + i >= GameCons.CHESS_BOARD_SIZE) break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                    countBottomRight++;
                else break;
            }

            return (countTopLeft + countBottomRight + 1) >= GameCons.WINNING_COUNT;
        }

        // Kiểm tra đường chéo phụ (/)
        private bool IsEndGameSub(Point point, Button btn)
        {
            int countTopRight = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X + i >= GameCons.CHESS_BOARD_SIZE || point.Y - i < 0) break;
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                    countTopRight++;
                else break;
            }

            int countBottomLeft = 0;
            for (int i = 1; i < GameCons.WINNING_COUNT; i++)
            {
                if (point.X - i < 0 || point.Y + i >= GameCons.CHESS_BOARD_SIZE) break;
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                    countBottomLeft++;
                else break;
            }

            return (countTopRight + countBottomLeft + 1) >= GameCons.WINNING_COUNT;
        }
    }

    public class ButtonClickEvent : EventArgs
    {
        public Point ClickedPoint { get; set; }
        public ButtonClickEvent(Point point) { ClickedPoint = point; }
    }
}