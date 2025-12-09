using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms
{
    public partial class Frm_WaitingRoomForm : Form
    {
        private RoomModel _currentRoom;
        private UserModel _currentUser;
        private RoomService _roomService;
        private bool _isHost;

        private bool _isClosing = false;

        public Frm_WaitingRoomForm(RoomModel room, UserModel user, RoomService service)
        {
            InitializeComponent();
            _currentRoom = room;
            _currentUser = user;
            _roomService = service;

            _isHost = (_currentRoom.Host?.Uid == _currentUser.Uid);
        }

        private void Frm_WaitingRoomForm_Load(object sender, EventArgs e)
        {
            UpdateUI(_currentRoom);

            _roomService.ListenToRoom(_currentRoom.ID, OnRoomUpdate);

            uC_Chat1.Init(_roomService, _currentRoom.ID, _currentUser);
        }

        private void OnRoomUpdate(RoomModel updatedRoom)
        {
            if (this.IsDisposed) return;

            this.Invoke((MethodInvoker)delegate
            {
                if (updatedRoom == null)
                {
                    MessageBox.Show("Phòng đã bị xóa.");
                    CloseSafely();
                    return;
                }

                _currentRoom = updatedRoom;
                UpdateUI(_currentRoom);

                if (_currentRoom.Status == "Playing")
                    StartGame();
            });
        }

        private void UpdateUI(RoomModel room)
        {
            
            Tb_RoomId.Text = room.ID;

            // 1. Xử lý Host (Player 1)
            if (room.Host != null)
            {
                Lbl_Player01.Text = room.Host.UserName; // Hiện tên lên label đẹp
            }
            else
            {
                Lbl_Player01.Text = "(Trống)";
            }

            // 2. Xử lý Guest (Player 2)
            if (room.Guest != null)
            {
                Lbl_Player02.Text = room.Guest.UserName;
                Lbl_Player02.ForeColor = Color.Yellow; // Màu chữ khi có người
            }
            else
            {
                Lbl_Player02.Text = "Waiting...";
                Lbl_Player02.ForeColor = Color.WhiteSmoke; // Đổi màu nhạt hơn khi chưa có người
            }

            // 3. Ẩn luôn 2 cái label cũ bị thừa đi (hoặc vào Designer xóa cũng được)
            Lb_UserName1.Visible = false;
            Lb_UserName2.Visible = false;

            // --- Phần logic nút Start giữ nguyên ---
            if (_isHost)
            {
                Btn_Start.Text = "Start Game";
                Btn_Start.Enabled = room.Guest != null;
            }
            else
            {
                Btn_Start.Text = "Ready";
                Btn_Start.Enabled = false;
            }
        }

        private async void Btn_Start_Click(object sender, EventArgs e)
        {
            if (_isHost)
                await _roomService.StartGameAsync(_currentRoom);
        }

        private void StartGame()
        {
            _roomService.StopListenRoom();
            uC_Chat1.StopListening();

            MessageBox.Show("→ Vào game (GameForm sẽ mở ở đây)");
        }

        private async void Btn_ExitRoom_Click(object sender, EventArgs e)
        {
            _isClosing = true; // Đánh dấu để không kích hoạt FormClosing lần nữa

            try
            {
                // Gọi hàm xử lý thông minh mới viết
                await _roomService.ExitRoomAsync(_currentRoom, _currentUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thoát: " + ex.Message);
            }
            finally
            {
                // Dừng lắng nghe và đóng form
                _roomService.StopListenRoom();
                uC_Chat1.StopListening();
                this.Close();
            }
        }

        private void CloseSafely()
        {
            _isClosing = true;
            this.Close();
        }
    }
}
