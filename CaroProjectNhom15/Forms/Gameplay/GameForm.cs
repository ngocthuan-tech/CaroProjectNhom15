using System;
using System.Drawing;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using CaroProjectNhom15.Forms.Gameplay; // Giả sử GameBoardManager nằm ở đây

namespace CaroProjectNhom15.Forms
{
    public partial class GameForm : Form
    {
        private GameBoardManager _gameBoardManager;
        private readonly RoomService _roomService;
        private RoomModel _currentRoom;
        private readonly UserModel _currentUser;
        private bool _gameEndHandled = false;

        // Xác định mình là Host (Player 0 - X) hay Guest (Player 1 - O)
        private int _myRole; // 0 hoặc 1

        public GameForm(RoomModel room, UserModel user, RoomService service)
        {
            InitializeComponent();
            _currentRoom = room;
            _currentUser = user;
            _roomService = service;

            // Xác định vai trò
            if (_currentUser.Uid == _currentRoom.Host.Uid) _myRole = 0; // Host (X)
            else _myRole = 1; // Guest (O)

            this.Load += Frm_GameForm_Load;
            this.FormClosing += Frm_GameForm_FormClosing;
        }

        private void Frm_GameForm_Load(object sender, EventArgs e)
        {
            // Cập nhật UI ban đầu
            Lbl_PlayerX_Name.Text = _currentRoom.Host.UserName;
            Lbl_PlayerO_Name.Text = _currentRoom.Guest.UserName;

            // Khởi tạo bàn cờ và đăng ký sự kiện...
            _gameBoardManager = new GameBoardManager(Pnl_BoardContainer);
            _gameBoardManager.PlayerMarked += GameBoard_PlayerMarked;
            _gameBoardManager.EndedGame += GameBoard_EndedGame;

            // Vẽ bàn cờ
            _gameBoardManager.DrawBoard();
            Pnl_BoardContainer.Refresh();

            // BẮT ĐẦU LẮNG NGHE GAME: Y HỆT WaitingRoomForm
            _roomService.ListenToGame(_currentRoom.ID, OnGameUpdate);

            // Cập nhật UI theo trạng thái game ban đầu
            UpdateTurnUI(_currentRoom.Game.CurrentTurnID);
        }

        // --- HÀM LẮNG NGHE FIREBASE (OnGameUpdate) ---
        // NHẬN DATA VÀ XỬ LÝ TRÊN UI THREAD (GIỐNG OnRoomUpdate trong WaitingRoomForm)
        private async void OnGameUpdate(GameInfo gameInfo)
        {
            if (this.IsDisposed || _gameEndHandled) return;

            // Sử dụng BeginInvoke hoặc Invoke để đảm bảo chạy trên UI Thread
            this.BeginInvoke((MethodInvoker)async delegate
            {
                // 1. CẬP NHẬT NƯỚC ĐI TRƯỚC (Dù thắng hay thua đều phải thấy nước đi này)
                // Kiểm tra nếu là nước đi của ĐỐI THỦ (SenderID khác mình) và có tọa độ hợp lệ
                if (gameInfo.SenderID != _currentUser.Uid && (gameInfo.X > 0 || gameInfo.Y > 0))
                {
                    HandleOpponentMove(gameInfo);

                    // Ép bàn cờ vẽ lại ngay lập tức để người chơi thấy quân cờ
                    Pnl_BoardContainer.Refresh();

                    // Cho người chơi 0.5 giây để nhìn thấy quân cờ "kết liễu" trước khi hiện Form
                    if (gameInfo.WinnerID != null) await Task.Delay(500);
                }

                // 2. KIỂM TRA KẾT THÚC GAME SAU KHI ĐÃ VẼ
                if (gameInfo.WinnerID != null)
                {
                    HandleEndGame(gameInfo.WinnerID);
                    return;
                }

                // 3. NẾU CHƯA KẾT THÚC THÌ CẬP NHẬT LƯỢT NHƯ BÌNH THƯỜNG
                UpdateTurnUI(gameInfo.CurrentTurnID);
            });
        }

        // Xử lý khi đối thủ đi
        private void HandleOpponentMove(GameInfo info)
        {
            Point point = new Point(info.X, info.Y);

            // Vẽ nước đi của đối thủ lên bàn cờ mình
            _gameBoardManager.OtherPlayerMark(point);
        }

        private void HandleEndGame(string winnerId)
        {
            if (_gameEndHandled) return;
            _gameEndHandled = true;

            // Dừng listener ngay để tránh nhận thêm dữ liệu thừa
            _roomService.StopListenGame();

            // Khóa bàn cờ
            _gameBoardManager.EndGame();
            Pnl_BoardContainer.Enabled = false;

            bool isWinner = (winnerId == _currentUser.Uid);

            // Tìm tên người thắng một cách chính xác
            string winnerName = "";
            if (winnerId == _currentRoom.Host.Uid) winnerName = _currentRoom.Host.UserName;
            else if (_currentRoom.Guest != null && winnerId == _currentRoom.Guest.Uid) winnerName = _currentRoom.Guest.UserName;
            else winnerName = "Đối thủ";

            // Hiển thị Form kết thúc
            using (var endGameForm = new EndGameForm(winnerName, isWinner))
            {
                endGameForm.ShowDialog();
            }

            this.Close();
        }

        // Cập nhật giao diện lượt chơi
        private void UpdateTurnUI(string currentTurnId)
        {
            if (currentTurnId == _currentUser.Uid)
            {
                Lbl_CurrentTurn.Text = "Đến lượt bạn!";
                Pnl_BoardContainer.Enabled = true; // Mở khóa bàn cờ

                // Đồng bộ CurrentPlayer trong Manager để vẽ quân cờ đúng
                // Host (X) là Player 0, Guest (O) là Player 1
                _gameBoardManager.CurrentPlayer = _myRole;
                // UpdateCurrentMarkUI(); // Cập nhật hình ảnh X/O lớn
            }
            else if (currentTurnId != null)
            {
                Lbl_CurrentTurn.Text = "Đợi đối thủ...";
                Pnl_BoardContainer.Enabled = false; // Khóa bàn cờ

                // Đồng bộ CurrentPlayer sang đối thủ 
                _gameBoardManager.CurrentPlayer = (_myRole == 0) ? 1 : 0;
                // UpdateCurrentMarkUI(); // Cập nhật hình ảnh X/O lớn
            }
            else // currentTurnId == null (Game kết thúc)
            {
                Lbl_CurrentTurn.Text = "Game đã kết thúc";
                Pnl_BoardContainer.Enabled = false;
            }
        }


        // --- GỬI DỮ LIỆU LÊN FIREBASE (ACTION) ---

        // Khi mình click vào bàn cờ (sự kiện của GameBoardManager)
        private async void GameBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            // 1. Chỉ mình đánh mới chạy vào đây. Khóa bàn cờ tạm thời để tránh click liên tục
            Pnl_BoardContainer.Enabled = false;

            string winnerID = null;
            string nextTurnID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid;

            // 2. KIỂM TRA THẮNG THUA CỤC BỘ (Chỉ máy người vừa đánh mới kiểm tra)
            if (_gameBoardManager.IsEndGame(e.ClickedPoint))
            {
                winnerID = _currentUser.Uid; // Mình thắng
                nextTurnID = null;           // Không còn lượt tiếp theo
            }

            var gameInfo = new GameInfo
            {
                X = e.ClickedPoint.X,
                Y = e.ClickedPoint.Y,
                SenderID = _currentUser.Uid,
                CurrentTurnID = nextTurnID,
                WinnerID = winnerID
            };

            // 3. Gửi lên Firebase
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);
        }

        // Hết giờ hoặc sự kiện kết thúc khác
        private async void GameBoard_EndedGame(object sender, EventArgs e)
        {
            // Gửi thông báo thua cuộc lên server do hết giờ
            string loserID = _currentUser.Uid;
            string winnerID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid; // Người kia thắng

            var gameInfo = new GameInfo
            {
                X = 0, // Không phải nước đi
                Y = 0, // Không phải nước đi
                SenderID = loserID,
                CurrentTurnID = null, // Game kết thúc
                WinnerID = winnerID
            };
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);
        }

        private async void Btn_Exit_Click(object sender, EventArgs e)
        {
            // Cập nhật trạng thái phòng.
            await _roomService.ExitRoomAsync(_currentRoom, _currentUser);

            _roomService.StopListenGame();
            this.Close();
        }

        private void Frm_GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đảm bảo tắt Listener khi form đóng (phòng trường hợp thoát bằng nút X)
            _roomService.StopListenGame();
        }
    }
}