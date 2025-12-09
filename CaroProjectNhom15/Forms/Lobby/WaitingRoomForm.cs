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

            this.Invoke((MethodInvoker)async delegate // Chuyển thành async để gọi hàm await
            {
                if (updatedRoom == null)
                {
                    MessageBox.Show("Phòng đã bị giải tán.");
                    CloseSafely();
                    return;
                }

                _currentRoom = updatedRoom;

                // --- LOGIC TỰ ỨNG CỬ (SELF-PROMOTION) ---
                // Nếu phát hiện Host bị null (trống) MÀ mình đang là Guest
                // -> Tự động chiếm quyền Host ngay lập tức
                if (_currentRoom.Host == null && _currentRoom.Guest?.Uid == _currentUser.Uid)
                {
                    try
                    {
                        // Gọi Service để update lại: Host = Mình, Guest = Null
                        // (Bạn cần đảm bảo logic này có trong JoinRoom hoặc viết hàm Update mới, 
                        // nhưng cách nhanh nhất là dùng lại JoinRoomAsync vì nó có logic chiếm phòng trống)
                        var newRoom = await _roomService.JoinRoomAsync(_currentRoom, _currentUser);

                        // Sau khi Join xong, nó sẽ trả về room mới, cập nhật lại luôn
                        _currentRoom = newRoom;
                    }
                    catch { /* Lỗi mạng thì bỏ qua, đợi lần update sau */ }
                }
                // ----------------------------------------

                // Tính toán lại quyền Host
                _isHost = (_currentRoom.Host?.Uid == _currentUser.Uid);

                UpdateUI(_currentRoom);

                if (_currentRoom.Status == "Playing")
                    StartGame();
            });
        }

        private void UpdateUI(RoomModel room)
        {
            Tb_RoomId.Text = room.ID;
            // 1. Hiển thị Host (Player 1 - Bên Trái)
            if (room.Host != null)
            {
                Lbl_Player01.Text = room.Host.UserName;
            }
            else
            {
                Lbl_Player01.Text = "(Đang chờ Host...)";
            }

            // 2. Hiển thị Guest (Player 2 - Bên Phải)
            if (room.Guest != null)
            {
                Lbl_Player02.Text = room.Guest.UserName;
                Lbl_Player02.ForeColor = Color.Yellow;
            }
            else
            {
                // QUAN TRỌNG: Phải reset về text mặc định khi không có Guest
                Lbl_Player02.Text = "Waiting...";
                Lbl_Player02.ForeColor = Color.WhiteSmoke;
            }

            // 3. Xử lý nút Start
            if (_isHost)
            {
                Btn_Start.Text = "Start Game";
                // Chỉ cho phép Start khi đã có đối thủ (Guest khác null)
                Btn_Start.Enabled = (room.Guest != null);
                Btn_Start.Visible = true;
            }
            else
            {
                Btn_Start.Text = "Ready";
                Btn_Start.Enabled = false;
                // Hoặc ẩn luôn nút nếu là Guest cho đỡ rối
                // Btn_Start.Visible = false; 
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
