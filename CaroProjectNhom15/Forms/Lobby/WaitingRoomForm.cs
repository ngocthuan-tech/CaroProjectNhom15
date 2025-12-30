using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms
{
    public partial class Frm_WaitingRoomForm : Form
    {
        private RoomModel _currentRoom;
        private UserModel _currentUser;
        private RoomService _roomService;
        private bool _isHost;
        private bool _gameStarted = false;
        private bool _isClosing = false;

        public Frm_WaitingRoomForm(RoomModel room, UserModel user, RoomService service)
        {
            InitializeComponent();
            _currentRoom = room; //
            _currentUser = user; //
            _roomService = service; //
            _isHost = (_currentRoom.Host?.Uid == _currentUser.Uid); //
        }

        private async void Frm_WaitingRoomForm_Load(object sender, EventArgs e)
        {
            await UpdateUI(_currentRoom); //
            _roomService.ListenToRoom(_currentRoom.ID, OnRoomUpdate); //
            uC_Chat1.Init(_roomService, _currentRoom.ID, _currentUser); //
        }

        private void OnRoomUpdate(RoomModel updatedRoom)
        {
            if (this.IsDisposed) return; //

            this.Invoke((MethodInvoker)async delegate
            {
                if (updatedRoom == null)
                {
                    MessageBox.Show("Phòng đã bị giải tán.");
                    CloseSafely();
                    return;
                }

                _currentRoom = updatedRoom; //

                // Logic tự ứng cử Host nếu Host thoát
                if (_currentRoom.Host == null && _currentRoom.Guest?.Uid == _currentUser.Uid)
                {
                    try
                    {
                        var newRoom = await _roomService.JoinRoomAsync(_currentRoom, _currentUser);
                        _currentRoom = newRoom;
                    }
                    catch { }
                }

                _isHost = (_currentRoom.Host?.Uid == _currentUser.Uid); //

                // Chỉ gọi cập nhật giao diện 1 lần duy nhất và dùng await
                await UpdateUI(_currentRoom);

                if (updatedRoom.Status == "Playing" && !_gameStarted)
                {
                    _gameStarted = true;
                    StartGame();
                }
            });
        }

        // --- HÀM TẢI AVATAR (Hỗ trợ cả URL và BASE64) ---
        private async Task LoadAvatarAsync(PictureBox pb, string avatarData)
        {
            pb.Image = null; // Reset ảnh

            if (string.IsNullOrWhiteSpace(avatarData))
            {
                pb.BackColor = Color.Gray;
                return;
            }

            try
            {
                // TH1: Nếu là chuỗi Base64 (data:image/...)
                if (avatarData.StartsWith("data:image"))
                {
                    string base64String = avatarData.Substring(avatarData.IndexOf(",") + 1);
                    byte[] imageBytes = Convert.FromBase64String(base64String);
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pb.Image = Image.FromStream(ms);
                    }
                }
                // TH2: Nếu là URL thông thường
                else
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");
                        var bytes = await client.GetByteArrayAsync(avatarData);
                        using (var ms = new MemoryStream(bytes))
                        {
                            pb.Image = Image.FromStream(ms);
                        }
                    }
                }
                pb.BackColor = Color.Transparent; // Thành công
            }
            catch
            {
                pb.BackColor = Color.Maroon; // Thất bại (Lỗi link hoặc lỗi Base64)
            }
        }

        private async Task UpdateUI(RoomModel room)
        {
            Tb_RoomId.Text = room.ID; //

            // 1. Hiển thị Host (Bên Trái)
            if (room.Host != null)
            {
                Lbl_Player01.Text = room.Host.UserName;
                await LoadAvatarAsync(Pb_Player01, room.Host.AvatarUrl); //
            }
            else
            {
                Lbl_Player01.Text = "(Trống)";
                Pb_Player01.Image = null;
            }

            // 2. Hiển thị Guest (Bên Phải)
            if (room.Guest != null)
            {
                Lbl_Player02.Text = room.Guest.UserName;
                Lbl_Player02.ForeColor = Color.Yellow;
                await LoadAvatarAsync(Pb_Player02, room.Guest.AvatarUrl); //
            }
            else
            {
                Lbl_Player02.Text = "Waiting...";
                Lbl_Player02.ForeColor = Color.WhiteSmoke;
                Pb_Player02.Image = null;
            }

            // 3. Nút Start
            if (_isHost)
            {
                Btn_Start.Enabled = (room.Guest != null);
                Btn_Start.Visible = true;
            }
            else
            {
                Btn_Start.Visible = false;
            }
        }

        private void StartGame()
        {
            _roomService.StopListenRoom(); //
            uC_Chat1.StopListening();
            var gameForm = new GameForm(_currentRoom, _currentUser, _roomService); //
            this.Hide();
            gameForm.ShowDialog();
            this.Close();
        }

        private async void Btn_ExitRoom_Click(object sender, EventArgs e)
        {
            _isClosing = true;
            try { await _roomService.ExitRoomAsync(_currentRoom, _currentUser); } //
            catch { }
            finally
            {
                _roomService.StopListenRoom();
                uC_Chat1.StopListening();
                this.Close();
            }
        }

        private void CloseSafely() { _isClosing = true; this.Close(); } //
    }
}