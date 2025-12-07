using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using CaroProjectNhom15.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms
{
    public partial class LobbyForm : Form
    {
        private readonly RoomService _roomService = new RoomService();
        private UserModel _currentUser;
        private bool _isListening = false;

        public LobbyForm(UserModel user)
        {
            InitializeComponent();
            _currentUser = user;
            Btn_FindRoom.Click += Btn_FindRoom_Click;
        }

        private void LobbyForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Lobby — Xin chào: {_currentUser.UserName}";
            StartRoomsListener();
        }

        private void StartRoomsListener()
        {
            if (_isListening) return;
            _isListening = true;

            _roomService.ListenToRoomsList(OnRoomListChanged);
        }

        private void OnRoomListChanged(List<RoomModel> rooms)
        {
            if (this.IsDisposed) return;

            this.Invoke((MethodInvoker)delegate
            {
                Flp_RoomList.SuspendLayout();
                Flp_RoomList.Controls.Clear();

                foreach (var room in rooms)
                {
                    var item = new UC_RoomItem(room);
                    item.OnJoinClicked += Item_OnJoinClicked;
                    Flp_RoomList.Controls.Add(item);
                }

                Flp_RoomList.ResumeLayout();
            });
        }

        private async void Item_OnJoinClicked(object sender, RoomModel room)
        {
            try
            {
                var joinedRoom = await _roomService.JoinRoomAsync(room, _currentUser);
                GoToWaitingRoom(joinedRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể vào phòng: " + ex.Message);
            }
        }

        private async void Btn_CreateRoom_Click(object sender, EventArgs e)
        {
            Btn_CreateRoom.Enabled = false;

            try
            {
                // Gọi Service tạo phòng
                var newRoom = await _roomService.CreateRoomAsync(_currentUser);

                // --- ĐOẠN QUAN TRỌNG CẦN THÊM ---
                // Nếu newRoom bị null (do lỗi Firebase chặn, hoặc mạng), thì DỪNG LẠI NGAY
                if (newRoom == null)
                {
                    MessageBox.Show("Tạo phòng thất bại! (Kiểm tra lại Rules trên Firebase hoặc Mạng)");
                    return;
                }
                // --------------------------------

                // Nếu có phòng thật thì mới đi tiếp
                GoToWaitingRoom(newRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Code: " + ex.Message);
            }
            finally
            {
                Btn_CreateRoom.Enabled = true;
            }
        }

        private void GoToWaitingRoom(RoomModel room)
        {
            this.Hide();

            using (var waitForm = new Frm_WaitingRoomForm(room, _currentUser, _roomService))
                waitForm.ShowDialog();

            this.Show();
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            // chỉ cần gọi lại listener, listener sẽ tự cập nhật
            StartRoomsListener();
        }

        private async void Btn_FindRoom_Click(object sender, EventArgs e)
        {
            string roomId = Tb_RoomId.Text.Trim();

            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Vui lòng nhập ID phòng cần tìm.");
                return;
            }

            Btn_FindRoom.Enabled = false; // Khóa nút để tránh bấm liên tục

            try
            {
                // 1. Tìm thông tin phòng trên Firebase trước
                var roomFound = await _roomService.GetRoomByIdAsync(roomId);

                if (roomFound == null)
                {
                    MessageBox.Show("Không tìm thấy phòng có ID này!");
                    return;
                }

                // 2. Nếu tìm thấy, gọi hàm JoinRoomAsync
                // Hàm này (bạn đã sửa ở bước trước) sẽ tự động lo liệu việc:
                // - Nếu phòng mất Host -> Bạn thành Host
                // - Nếu phòng có Host -> Bạn thành Guest
                var joinedRoom = await _roomService.JoinRoomAsync(roomFound, _currentUser);

                // 3. Vào phòng chờ
                GoToWaitingRoom(joinedRoom);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm phòng: " + ex.Message);
            }
            finally
            {
                Btn_FindRoom.Enabled = true; // Mở lại nút
            }
        }

        private void Btn_Back_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
