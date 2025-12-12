// GameForm.cs
using System;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using System.Drawing;
using CaroProjectNhom15.Forms.Gameplay;

namespace CaroProjectNhom15.Forms
{
    // ĐÃ ĐỔI TÊN THÀNH GameForm
    public partial class GameForm : Form
    {
        #region Properties
        GameBoardManager gameBoard;
        #endregion
        private readonly RoomService _roomService;
        private readonly RoomModel _currentRoom;
        private readonly UserModel _currentUser;

        // Biến lưu trạng thái game cục bộ (Realtime)
        private GameModel _currentGameData;

        // Listener cho game
        private IDisposable _gameListener;

        // CÁC THÀNH PHẦN MỚI CẦN CÓ TRONG DESIGNER:
        // 1. Timer: Tmr_CoolDown
        // 2. ProgressBar: Prcb_CoolDown
        // 3. PictureBox: Pctb_CurrentMark


        // Constructor Tương thích với WaitingRoomForm.cs
        public GameForm(RoomModel room, UserModel user, RoomService service)
        {
            // Designer sẽ tạo ra các control và gán sự kiện Btn_Exit_Click ở đây.
            InitializeComponent();

            _currentRoom = room;
            _currentUser = user;
            _roomService = service;

            this.Text = $"Game Caro — ID: {_currentRoom.ID} | {user.UserName}";

            // Chỉ cần gán sự kiện Load
            this.Load += GameForm_Load;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            // Tải thông tin người chơi lên UI
            LoadPlayerInfo();

            // Khởi tạo bàn cờ (InitGameBoard(); - cần bạn tự viết)
            InitGameBoard();

            // Thiết lập Timer
            InitCoolDownTimer();

            // Bắt đầu lắng nghe Game data (sẽ viết sau)
            // ListenToGameChanges();
        }

        // --- KHỞI TẠO BÀN CỜ VÀ GẮN SỰ KIỆN ---
        private void InitGameBoard()
        {
            // 1. Khởi tạo GameBoardManager
            gameBoard = new GameBoardManager(Pnl_BoardContainer);

            // 2. Gán sự kiện khi người chơi đánh dấu (dùng để reset Timer)
            gameBoard.PlayerMarked += GameBoard_PlayerMarked;

            // 3. Gán sự kiện khi game kết thúc (dùng để dừng Timer)
            gameBoard.EndedGame += GameBoard_EndedGame;

            // Bàn cờ được vẽ trong Constructor của GameBoardManager
            // gameBoard.DrawBoard();

            // Thiết lập UI ban đầu
            UpdateCurrentMark();
        }


        // --- LOGIC TIMER VÀ PROGRESS BAR ---
        private void InitCoolDownTimer()
        {
            // Thiết lập thông số cho ProgressBar
            Prcb_CoolDown.Maximum = GameCons.COOL_DOWN_TIME;
            Prcb_CoolDown.Value = GameCons.COOL_DOWN_TIME;

            // Thiết lập thông số cho Timer
            Tmr_CoolDown.Interval = GameCons.COOL_DOWN_INTERVAL;
            Tmr_CoolDown.Tick += Tmr_CoolDown_Tick;
            Tmr_CoolDown.Start();
        }

        // Xử lý sự kiện Tick của Timer (cập nhật Progress Bar và kiểm tra hết giờ)
        private void Tmr_CoolDown_Tick(object sender, EventArgs e)
        {
            // Giảm giá trị ProgressBar
            Prcb_CoolDown.PerformStep();

            // Progress Bar giảm dần
            Prcb_CoolDown.Value -= GameCons.COOL_DOWN_INTERVAL;

            // Kiểm tra xem thời gian đã hết chưa
            if (Prcb_CoolDown.Value <= 0)
            {
                // Dừng Timer
                Tmr_CoolDown.Stop();

                // Kết thúc game vì hết thời gian
                gameBoard.EndGame();
            }
        }

        // Reset Timer khi có nước đi mới
        private void ResetCoolDownTimer()
        {
            Tmr_CoolDown.Stop();
            Prcb_CoolDown.Value = GameCons.COOL_DOWN_TIME;
            Tmr_CoolDown.Start();
        }


        // --- XỬ LÝ SỰ KIỆN GAMEBOARD ---

        // Xử lý khi có người đánh dấu (cả local và other)
        private void GameBoard_PlayerMarked(object sender, ButtonClickEvent e)
        {
            // 1. Reset lại Timer
            ResetCoolDownTimer();

            // 2. Cập nhật hình ảnh quân cờ hiện tại và tên người chơi
            UpdateCurrentMark();

            // --- Ở đây cần gửi nước đi (e.ClickedPoint) lên Firebase (sẽ viết sau) ---
            // SendMoveToFirebase(e.ClickedPoint);
        }

        // Xử lý khi Game kết thúc (Thắng hoặc Hết giờ)
        private void GameBoard_EndedGame(object sender, EventArgs e)
        {
            // Dừng Timer
            Tmr_CoolDown.Stop();

            // Vô hiệu hóa Progress Bar (tùy chọn)
            Prcb_CoolDown.Value = 0;
        }


        // --- CẬP NHẬT UI ---

        private void LoadPlayerInfo()
        {
            // Host là X, Guest là O
            Lbl_PlayerX_Name.Text = _currentRoom.Host?.UserName ?? "Host (X)";
            Lbl_PlayerO_Name.Text = _currentRoom.Guest?.UserName ?? "Guest (O)";

            // Tên người chơi ban đầu
            // Cập nhật lượt hiện tại
            Lbl_CurrentTurn.Text = "Đang chờ...";
        }

        // Cập nhật hình ảnh quân cờ và tên người chơi
        private void UpdateCurrentMark()
        {
            if (gameBoard == null) return;

            // Lấy Player hiện tại
            Player currentPlayer = gameBoard.Player[gameBoard.CurrentPlayer];

            // Cập nhật PictureBox
            Pctb_CurrentMark.Image = currentPlayer.Mark;

            // Cập nhật tên người chơi (dựa trên CurrentPlayer index 0=X, 1=O)
            if (gameBoard.CurrentPlayer == 0)
            {
                Lbl_CurrentTurn.Text = _currentRoom.Host?.UserName ?? "Host (X)";
            }
            else
            {
                Lbl_CurrentTurn.Text = _currentRoom.Guest?.UserName ?? "Guest (O)";
            }
        }


        // HÀM XỬ LÝ SỰ KIỆN CLICK CHO NÚT EXIT (DESIGNER TỰ GỌI)
        private async void Btn_Exit_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc muốn thoát ván game này không?", "Xác nhận",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                // Dừng Timer
                Tmr_CoolDown.Stop();

                // Dừng lắng nghe
                // StopListenToGameChanges();

                // Gọi hàm thoát (sẽ tạo sau trong RoomService)
                // await _roomService.ExitGameAsync(_currentRoom, _currentUser); 

                this.Close();
            }
        }

        // --- LOGIC GAME CỐT LÕI (SẼ VIẾT Ở BƯỚC SAU) ---
    }
}