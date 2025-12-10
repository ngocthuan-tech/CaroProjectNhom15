// GameForm.cs
using System;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using System.Drawing;

namespace CaroProjectNhom15.Forms
{
    // ĐÃ ĐỔI TÊN THÀNH GameForm
    public partial class GameForm : Form
    {
        private readonly RoomService _roomService;
        private readonly RoomModel _currentRoom;
        private readonly UserModel _currentUser;

        // Biến lưu trạng thái game cục bộ (Realtime)
        private GameModel _currentGameData;

        // Listener cho game
        private IDisposable _gameListener;


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
            // Bắt đầu lắng nghe Game data (sẽ viết sau)
            // ListenToGameChanges();

            // Tải thông tin người chơi lên UI
            LoadPlayerInfo();

            // Khởi tạo bàn cờ (InitGameBoard(); - cần bạn tự viết)
        }

        private void LoadPlayerInfo()
        {
            // Host là X, Guest là O
            Lbl_PlayerX_Name.Text = _currentRoom.Host?.UserName ?? "Host (X)";
            Lbl_PlayerO_Name.Text = _currentRoom.Guest?.UserName ?? "Guest (O)";

            // Cập nhật lượt hiện tại
            Lbl_CurrentTurn.Text = "Đang chờ...";
        }

        // HÀM XỬ LÝ SỰ KIỆN CLICK CHO NÚT EXIT (DESIGNER TỰ GỌI)
        private async void Btn_Exit_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc muốn thoát ván game này không?", "Xác nhận",
                                          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
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