using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using CaroProjectNhom15.Forms.Gameplay;

namespace CaroProjectNhom15.Forms
{
    public partial class GameForm : Form
    {
        private GameBoardManager _gameBoardManager;
        private readonly RoomService _roomService;
        private RoomModel _currentRoom;
        private readonly UserModel _currentUser;

        private int _myRole; // 0: Host, 1: Guest
        private bool _gameEndHandled = false;
        private bool _isLocalMoving = false; // Chặn click khi đang gửi data
        private long _lastProcessedTimestamp = 0; // Double-check logic

        public GameForm(RoomModel room, UserModel user, RoomService service)
        {
            InitializeComponent();
            _currentRoom = room;
            _currentUser = user;
            _roomService = service;
            _myRole = (_currentUser.Uid == _currentRoom.Host.Uid) ? 0 : 1;

            this.Load += Frm_GameForm_Load;
            this.FormClosing += Frm_GameForm_FormClosing;
        }

        private void Frm_GameForm_Load(object sender, EventArgs e)
        {
            _gameBoardManager = new GameBoardManager(Pnl_BoardContainer);
            _gameBoardManager.PlayerMarked += GameBoard_PlayerMarked;
            _gameBoardManager.DrawBoard();

            // Lắng nghe dữ liệu từ Firebase
            _roomService.ListenToGame(_currentRoom.ID, OnGameUpdate);

            // Khởi tạo trạng thái lượt đánh ban đầu
            UpdateTurnUI(_myRole == 0);
        }

        private void OnGameUpdate(GameInfo gameInfo)
        {
            if (this.IsDisposed || _gameEndHandled) return;

            // --- DOUBLE CHECK TIMESTAMP ---
            // Nếu tin nhắn cũ hơn hoặc bằng tin nhắn đã xử lý -> Bỏ qua
            if (gameInfo.Timestamp <= _lastProcessedTimestamp) return;
            _lastProcessedTimestamp = gameInfo.Timestamp;

            this.BeginInvoke((MethodInvoker)async delegate
            {
                // 1. Nếu là nước đi của đối thủ -> Vẽ lên bàn cờ mình
                if (gameInfo.SenderID != _currentUser.Uid && gameInfo.X >= 0)
                {
                    _gameBoardManager.CurrentPlayer = (1 - _myRole); // Đổi sang quân đối thủ
                    _gameBoardManager.OtherPlayerMark(new Point(gameInfo.X, gameInfo.Y));
                    Pnl_BoardContainer.Refresh(); // Ép vẽ lại ngay
                }

                // 2. Kiểm tra có người thắng chưa
                if (!string.IsNullOrEmpty(gameInfo.WinnerID))
                {
                    await Task.Delay(600); // Đợi 0.6s để người chơi kịp nhìn nước đi cuối
                    HandleEndGame(gameInfo.WinnerID);
                    return;
                }

                // 3. Cập nhật lượt đánh và giải phóng khóa local
                _isLocalMoving = false;
                bool isMyTurn = (gameInfo.CurrentTurnID == _currentUser.Uid);
                UpdateTurnUI(isMyTurn);
            });
        }

        private async void GameBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            // CHẶNG ĐÁNH 2 Ô: Nếu đang gửi dữ liệu hoặc không phải lượt thì không cho nhấn
            if (_isLocalMoving || _gameEndHandled) return;

            _isLocalMoving = true;
            Pnl_BoardContainer.Enabled = false; // Khóa UI ngay lập tức

            string winnerID = null;
            string nextTurnID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid;

            // Tự kiểm tra thắng thua cho nước đi của mình
            if (_gameBoardManager.IsEndGame(e.ClickedPoint))
            {
                winnerID = _currentUser.Uid;
                nextTurnID = null;
            }

            var gameInfo = new GameInfo
            {
                X = e.ClickedPoint.X,
                Y = e.ClickedPoint.Y,
                SenderID = _currentUser.Uid,
                CurrentTurnID = nextTurnID,
                WinnerID = winnerID
            };

            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);
        }

        private void UpdateTurnUI(bool isMyTurn)
        {
            _gameBoardManager.CurrentPlayer = _myRole;
            Pnl_BoardContainer.Enabled = isMyTurn;
            Lbl_CurrentTurn.Text = isMyTurn ? "Lượt của bạn" : "Đợi đối thủ...";
            Lbl_CurrentTurn.ForeColor = isMyTurn ? Color.Green : Color.Red;
        }

        private void HandleEndGame(string winnerId)
        {
            if (_gameEndHandled) return;
            _gameEndHandled = true;

            _roomService.StopListenGame();
            Pnl_BoardContainer.Enabled = false;

            bool isMeWinner = (winnerId == _currentUser.Uid);
            string winnerName = (winnerId == _currentRoom.Host.Uid)
                ? _currentRoom.Host.UserName
                : _currentRoom.Guest.UserName;

            using (var endGameForm = new EndGameForm(winnerName, isMeWinner))
            {
                endGameForm.ShowDialog();
            }
            this.Close();
        }

        private void Frm_GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _roomService.StopListenGame();
        }
        private async void Btn_Exit_Click(object sender, EventArgs e)
        {
            // Cập nhật trạng thái phòng.
            await _roomService.ExitRoomAsync(_currentRoom, _currentUser);

            _roomService.StopListenGame();
            this.Close();
        }
    }
}