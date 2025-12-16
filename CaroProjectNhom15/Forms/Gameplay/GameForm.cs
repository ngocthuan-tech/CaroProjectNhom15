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
        private void OnGameUpdate(GameInfo gameInfo)
        {
            if (this.IsDisposed) return;

            // BẮT BUỘC dùng Invoke để chạy code trên UI Thread
            this.Invoke((MethodInvoker)delegate
            {
                // Cập nhật _currentRoom.Game để Form luôn có trạng thái mới nhất
                _currentRoom.Game = gameInfo;

                // 1. Kiểm tra xem tin này có phải do chính mình gửi không?
                if (gameInfo.SenderID == _currentUser.Uid)
                {
                    // Nếu là mình gửi, đã xử lý UI cục bộ, chỉ cần cập nhật lượt.
                    UpdateTurnUI(gameInfo.CurrentTurnID);
                    return;
                }

                // 2. KIỂM TRA KẾT THÚC GAME
                if (gameInfo.WinnerID != null)
                {
                    HandleEndGame(gameInfo.WinnerID);
                }
                // 3. XỬ LÝ NƯỚC ĐI CỦA ĐỐI THỦ
                // Chỉ xử lý nếu có tọa độ hợp lệ (X, Y > 0)
                else if (gameInfo.X > 0 && gameInfo.Y > 0)
                {
                    HandleOpponentMove(gameInfo);
                }

                // 4. Cập nhật lượt
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
            _gameBoardManager.EndGame(); // Khóa bàn cờ
            Pnl_BoardContainer.Enabled = false;

            string msg = (winnerId == _currentUser.Uid) ? "Bạn đã chiến thắng!" : "Bạn đã thua!";
            MessageBox.Show(msg, "Kết thúc Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // 1. Kiểm tra lượt (Phòng trường hợp người chơi click quá nhanh trước khi UI kịp khóa)
            if (_currentRoom.Game.CurrentTurnID != _currentUser.Uid)
            {
                MessageBox.Show("Chưa đến lượt bạn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Xác định trạng thái tiếp theo
            string nextTurnID = (_myRole == 0) ? _currentRoom.Guest.Uid : _currentRoom.Host.Uid;
            string winnerID = null;

            // Kiểm tra xem nước đi này có thắng không
            if (_gameBoardManager.IsEndGame(e.ClickedPoint))
            {
                winnerID = _currentUser.Uid;
                nextTurnID = null; // Game kết thúc
            }

            // 3. Chuẩn bị dữ liệu gửi lên
            var gameInfo = new GameInfo
            {
                X = e.ClickedPoint.X,
                Y = e.ClickedPoint.Y,
                SenderID = _currentUser.Uid,
                CurrentTurnID = nextTurnID,
                WinnerID = winnerID
            };

            // 4. Gửi lên Firebase
            await _roomService.UpdateGameAsync(_currentRoom.ID, gameInfo);

            // 5. Cập nhật UI cục bộ (tạm thời)
            UpdateTurnUI(nextTurnID);
            Pnl_BoardContainer.Enabled = false; // Khóa bàn cờ lại ngay
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
            // Xử lý thoát phòng: xóa Game khỏi Firebase và cập nhật lại RoomModel
            // Cần có logic để thông báo cho đối thủ bằng cách cập nhật RoomStatus (đã được xử lý trong ExitRoomAsync của RoomService)
            await _roomService.ExitRoomAsync(_currentRoom, _currentUser);

            _roomService.StopListenGame();
            this.Close();
        }

        private void Frm_GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đảm bảo tắt Listener khi form đóng
            _roomService.StopListenGame();
        }
    }
}