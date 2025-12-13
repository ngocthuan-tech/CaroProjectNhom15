using System;
using System.Drawing;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using CaroProjectNhom15.Forms.Gameplay;

namespace CaroProjectNhom15.Forms
{
    public partial class GameForm : Form
    {
        private GameBoardManager gameBoard;
        private readonly RoomService _roomService;
        private readonly RoomModel _currentRoom;
        private readonly UserModel _currentUser;

        // Xác định mình là Host (Player 0 - X) hay Guest (Player 1 - O)
        private int _myRole; // 0 hoặc 1

        public GameForm(RoomModel room, UserModel user, RoomService service)
        {
            InitializeComponent();
            _currentRoom = room;
            _currentUser = user;
            _roomService = service;

            // Xác định vai trò
            if (_currentUser.Uid == _currentRoom.Host.Uid) _myRole = 0; // Host đi trước (X)
            else _myRole = 1; // Guest đi sau (O)
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            // Setup tên người chơi...
            Lbl_PlayerX_Name.Text = _currentRoom.Host.UserName;
            Lbl_PlayerO_Name.Text = _currentRoom.Guest.UserName;

            // Khởi tạo bàn cờ và đăng ký sự kiện...
            gameBoard = new GameBoardManager(Pnl_BoardContainer);
            gameBoard.PlayerMarked += GameBoard_PlayerMarked;
            gameBoard.EndedGame += GameBoard_EndedGame;

            // 1. GỌI DRAWBOARD TẠI ĐÂY
            gameBoard.DrawBoard();

            // 2. ÉP BUỘC REDRAW
            Pnl_BoardContainer.Refresh();

            // 3. Bắt đầu lắng nghe game
            _roomService.ListenToGame(_currentRoom.ID, OnGameUpdate);

            // Đăng ký các sự kiện để GameForm có thể phản hồi lại game logic
            gameBoard.PlayerMarked += GameBoard_PlayerMarked;
            gameBoard.EndedGame += GameBoard_EndedGame;

            // Khởi tạo các UI liên quan đến Timer/ProgressBar
            Prcb_CoolDown.Step = GameCons.COOL_DOWN_STEP; // Giả định bạn có Prcb_CoolDown
            Prcb_CoolDown.Maximum = GameCons.COOL_DOWN_TIME;
            Tmr_CoolDown.Interval = GameCons.COOL_DOWN_INTERVAL; // Giả định bạn có Tmr_CoolDown

            // Bàn cờ lúc này đã được vẽ xong, nhưng cần cập nhật trạng thái game hiện tại
            if (_currentRoom.Game != null)
            {
                // Xử lý luôn trạng thái game đầu tiên
                OnGameUpdate(_currentRoom.Game);
            }

            // Start listening to the game
            _roomService.ListenToGame(_currentRoom.ID, OnGameUpdate);
        }

        // --- NHẬN DỮ LIỆU TỪ FIREBASE (REALTIME) ---
        private void OnGameUpdate(GameInfo gameInfo)
        {
            // Invoke để thao tác trên UI Thread
            this.Invoke((MethodInvoker)delegate
            {
                // 1. Kiểm tra xem tin này có phải do chính mình gửi không?
                if (gameInfo.SenderID == _currentUser.Uid)
                {
                    // Nếu là mình gửi, thì mình đã xử lý UI ở sự kiện Click rồi.
                    // Chỉ cần đảm bảo Timer hoạt động đúng.
                    return;
                }

                // 2. Xử lý logic dựa trên Command
                switch (gameInfo.Command)
                {
                    case GameInfo.CMD_SEND_POINT:
                        HandleOpponentMove(gameInfo);
                        break;
                    case GameInfo.CMD_END_GAME:
                        HandleEndGame(gameInfo.WinnerID);
                        break;
                    case GameInfo.CMD_NEW_GAME:
                        // Reset bàn cờ nếu cần
                        break;
                    case GameInfo.CMD_EXIT:
                        MessageBox.Show("Đối thủ đã thoát!");
                        this.Close();
                        break;
                }
            });
        }

        // Xử lý khi đối thủ đi
        private void HandleOpponentMove(GameInfo info)
        {
            Point point = new Point(info.X, info.Y);

            // Vẽ nước đi của đối thủ lên bàn cờ mình
            gameBoard.OtherPlayerMark(point);

            // Reset Timer
            ResetCoolDownTimer();

            // Cập nhật lượt: Bây giờ đến lượt mình
            Lbl_CurrentTurn.Text = "Đến lượt bạn!";
            Pnl_BoardContainer.Enabled = true; // Mở khóa bàn cờ

            // Đồng bộ CurrentPlayer trong Manager cho đúng logic vẽ
            gameBoard.CurrentPlayer = _myRole;
            UpdateCurrentMarkUI();
        }

        private void HandleEndGame(string winnerId)
        {
            Tmr_CoolDown.Stop();
            gameBoard.EndGame(); // Khóa bàn cờ

            string msg = (winnerId == _currentUser.Uid) ? "Bạn đã chiến thắng!" : "Bạn đã thua!";
            MessageBox.Show(msg, "Kết thúc", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // --- GỬI DỮ LIỆU LÊN FIREBASE (ACTION) ---

        // Khi mình click vào bàn cờ
        private async void GameBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            Tmr_CoolDown.Stop(); // Dừng tạm thời
            Prcb_CoolDown.Value = 0;

            // Chuẩn bị dữ liệu gửi lên
            var gameInfo = new GameInfo
            {
                Command = GameInfo.CMD_SEND_POINT,
                X = e.ClickedPoint.X,
                Y = e.ClickedPoint.Y,
                SenderID = _currentUser.Uid,
                // Lượt tiếp theo là của người kia
                CurrentTurnID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid
            };

            // Kiểm tra xem nước đi này có thắng không (GameBoardManager đã check và gọi EndGame -> Board disabled)
            if (gameBoard.IsEndGame(e.ClickedPoint))
            {
                gameInfo.Command = GameInfo.CMD_END_GAME;
                gameInfo.WinnerID = _currentUser.Uid;
            }

            // Gửi lên Firebase
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);

            if (gameInfo.Command != GameInfo.CMD_END_GAME)
            {
                // Nếu chưa thắng, update UI chờ đối thủ
                Lbl_CurrentTurn.Text = "Đợi đối thủ...";
                // Start Timer chờ đối thủ (nếu muốn tính giờ cả 2 bên)
                Tmr_CoolDown.Start();
                UpdateCurrentMarkUI();
            }
        }

        // Hết giờ
        private async void GameBoard_EndedGame(object sender, EventArgs e)
        {
            // Sự kiện này kích hoạt khi Hết Giờ (gọi từ Timer Tick)
            // Gửi thông báo thua cuộc lên server
            var gameInfo = new GameInfo
            {
                Command = GameInfo.CMD_END_GAME,
                SenderID = _currentUser.Uid,
                WinnerID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid // Người kia thắng
            };
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);
        }

        // Timer Logic (Giữ nguyên logic của bạn)
        private void InitCoolDownTimer()
        {
            Prcb_CoolDown.Step = GameCons.COOL_DOWN_STEP;
            Prcb_CoolDown.Maximum = GameCons.COOL_DOWN_TIME;
            Prcb_CoolDown.Value = 0;
            Tmr_CoolDown.Interval = GameCons.COOL_DOWN_INTERVAL;
            Tmr_CoolDown.Tick += Tmr_CoolDown_Tick;

            // Nếu mình là Host (đi trước), bắt đầu Timer ngay
            if (_myRole == 0)
            {
                Tmr_CoolDown.Start();
                Pnl_BoardContainer.Enabled = true;
                Lbl_CurrentTurn.Text = "Đến lượt bạn!";
            }
            else
            {
                Pnl_BoardContainer.Enabled = false; // Guest đợi
                Lbl_CurrentTurn.Text = "Đợi Host đi trước...";
            }
            UpdateCurrentMarkUI();
        }

        private void Tmr_CoolDown_Tick(object sender, EventArgs e)
        {
            Prcb_CoolDown.PerformStep();
            if (Prcb_CoolDown.Value >= Prcb_CoolDown.Maximum)
            {
                Tmr_CoolDown.Stop();
                // Hết giờ -> Tự xử thua
                gameBoard.EndGame();
            }
        }

        private void ResetCoolDownTimer()
        {
            Prcb_CoolDown.Value = 0;
            Tmr_CoolDown.Start();
        }

        private void UpdateCurrentMarkUI()
        {
            // Logic hiển thị ảnh X/O ai đang đánh ở góc màn hình
            // Host (0) luôn là X, Guest (1) luôn là O
            // Nếu CurrentTurn là mình -> Hiện hình của mình
            // Nếu CurrentTurn là địch -> Hiện hình địch

            // Đơn giản hóa: Dựa vào gameBoard.CurrentPlayer
            Player currentPlayer = gameBoard.Player[gameBoard.CurrentPlayer];
            Pctb_CurrentMark.Image = currentPlayer.Mark;
        }

        private async void Btn_Exit_Click(object sender, EventArgs e)
        {
            // Gửi lệnh thoát
            var gameInfo = new GameInfo
            {
                Command = GameInfo.CMD_EXIT,
                SenderID = _currentUser.Uid
            };
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);
            _roomService.StopListenGame();
            this.Close();
        }
    }
}