// GameBoardManager.cs

using System;
using System.Collections.Generic;
using System.Drawing; // Cần thiết
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // Cần thiết

namespace CaroProjectNhom15.Forms.Gameplay
{
    public class GameBoardManager
    {
        #region Properties
        private Panel board;
        // Panel chứa bàn cờ (Pnl_BoardContainer)
        public Panel Board { get => board; set => board = value; }

        private List<Player> player;
        // Danh sách người chơi (Player X và Player O)
        public List<Player> Player { get => player; set => player = value; }

        // 0 là Player 1 (X), 1 là Player 2 (O)
        private int currentPlayer;
        public int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

        // Ma trận lưu trữ các Button trên bàn cờ
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }

        // Đã loại bỏ Stack<Point> playTimeline (chức năng Undo)
        // private Stack<Point> playTimeline;
        // public Stack<Point> PlayTimeline { get => playTimeline; set => playTimeline = value; }

        // ----------------------- EVENTS -----------------------------
        // Sự kiện báo cho Form biết khi người chơi đánh dấu (để Form reset Timer)
        public event EventHandler<ButtonClickEvent> PlayerMarked;
        // Sự kiện báo cho Form biết khi kết thúc game
        public event EventHandler EndedGame;
        #endregion

        #region Initialize
        public GameBoardManager(Panel board)
        {
            this.Board = board;

            // Khởi tạo các danh sách cần thiết
            this.Matrix = new List<List<Button>>();
            // Đã loại bỏ PlayTimeline = new Stack<Point>();

            // Khởi tạo Player. Cần đảm bảo bạn đã thêm hình ảnh X/O vào Resources (Ví dụ: Properties.Resources.x_mark)
            this.Player = new List<Player>()
            {
                // Player 0 (X) - Cần hình ảnh quân X (Đã bỏ Name)
                new Player(Properties.Resources.x_mark), 
                // Player 1 (O) - Cần hình ảnh quân O (Đã bỏ Name)
                new Player(Properties.Resources.o_mark)
            };

            // Bắt đầu với Player 0 (X) và vẽ bàn cờ ban đầu
            CurrentPlayer = 0;
            DrawBoard();
            Board.Enabled = true; // Đảm bảo bàn cờ được kích hoạt ngay từ đầu
        }
        #endregion

        #region Methods

        // Đã loại bỏ phương thức NewGame()

        // Vẽ bàn cờ
        public void DrawBoard()
        {
            // Xóa hết các Button cũ và ma trận
            Board.Controls.Clear();
            Matrix.Clear();
            // Đã loại bỏ PlayTimeline.Clear();

            // Reset oldButton cho dòng đầu tiên
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };

            for (int i = 0; i < GameCons.CHESS_BOARD_SIZE; i++) // i là dòng (Y)
            {
                Matrix.Add(new List<Button>()); // Thêm hàng mới vào Ma trận
                oldButton.Location = new Point(0, oldButton.Location.Y + GameCons.CHESS_HEIGHT); // Bắt đầu dòng mới
                oldButton.Width = 0; // Đặt lại chiều rộng của oldButton

                for (int j = 0; j < GameCons.CHESS_BOARD_SIZE; j++) // j là cột (X)
                {
                    Button Btn = new Button()
                    {
                        Width = GameCons.CHESS_WIDTH,
                        Height = GameCons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + GameCons.CHESS_WIDTH, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        BackColor = Color.LightGray, // Màu nền bàn cờ
                        Tag = new Point(j, i) // Lưu tọa độ (X, Y) vào Tag
                    };

                    // Gán sự kiện click
                    Btn.Click += Btn_Click;

                    // Thêm Button vào Panel và Matrix
                    Board.Controls.Add(Btn);
                    Matrix[i].Add(Btn);

                    oldButton = Btn; // Cập nhật oldButton cho lần lặp tiếp theo
                }
            }
        }

        // Xử lý sự kiện khi người chơi click vào ô cờ
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            // Chỉ cho phép đánh khi ô đó chưa có quân cờ
            if (btn.BackgroundImage != null)
                return;

            // 1. Đánh dấu quân cờ
            Mark(btn);

            // Lấy tọa độ nước đi (Point(X, Y))
            Point point = (Point)btn.Tag;

            // 2. Đã loại bỏ việc lưu lịch sử nước đi

            // 3. Thông báo đã đánh dấu (cho Form biết để reset Timer)
            PlayerMarked?.Invoke(this, new ButtonClickEvent(point));

            // 4. Kiểm tra kết thúc game
            if (IsEndGame(point))
            {
                EndGame();
            }
            else
            {
                // 5. Đổi lượt chơi
                CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
            }
        }

        // Đánh dấu quân cờ của đối thủ (dùng cho logic mạng)
        public void OtherPlayerMark(Point point)
        {
            // Kiểm tra tính hợp lệ của tọa độ
            if (point.Y < 0 || point.Y >= GameCons.CHESS_BOARD_SIZE ||
                point.X < 0 || point.X >= GameCons.CHESS_BOARD_SIZE)
                return;

            Button btn = Matrix[point.Y][point.X];

            // Đảm bảo ô đó chưa có quân cờ
            if (btn.BackgroundImage != null)
                return;

            // Đổi sang lượt đối thủ (người chơi còn lại)
            CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;

            // Đánh dấu cho đối thủ
            Mark(btn);

            // Đã loại bỏ việc lưu lịch sử nước đi

            // Kiểm tra kết thúc game
            if (IsEndGame(point))
            {
                EndGame();
            }
            else
            {
                // Đổi lượt chơi ngược lại (quay về lượt của mình)
                CurrentPlayer = CurrentPlayer == 0 ? 1 : 0;
            }
        }

        // Đánh dấu quân cờ lên Button
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
        }

        // Phương thức EndGame đã cập nhật
        public void EndGame()
        {
            // Vô hiệu hóa bàn cờ
            Board.Enabled = false;

            // Hiển thị thông báo kết thúc game
            MessageBox.Show("Game đã kết thúc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Kích hoạt sự kiện kết thúc game (cho Form biết để xử lý Timer/Giao diện)
            EndedGame?.Invoke(this, new EventArgs());
        }

        // Đã loại bỏ phương thức Undo()

        #region Check Win Logic
        // Hàm kiểm tra chung
        private bool IsEndGame(Point point)
        {
            Button btn = Matrix[point.Y][point.X];

            return IsEndGameHorizontal(point, btn) ||
                   IsEndGameVertical(point, btn) ||
                   IsEndGamePrimary(point, btn) || // Đường chéo chính \
                   IsEndGameSub(point, btn);       // Đường chéo phụ /
        }

        // Kiểm tra chiều ngang
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
        #endregion

        #endregion
    }

    // Class Event Arguments (theo ChessBoardManager.txt) - Giữ nguyên
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint
        {
            get { return clickedPoint; }
            set { clickedPoint = value; }
        }

        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}