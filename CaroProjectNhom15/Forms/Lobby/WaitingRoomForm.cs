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
            Lb_UserName1.Text = room.Host?.UserName ?? "(Host rời phòng)";
            Lb_UserName2.Text = room.Guest?.UserName ?? "Đang chờ...";
            Lb_UserName2.ForeColor = room.Guest == null ? Color.Gray : Color.Black;

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
            _isClosing = true;

            try
            {
                if (_isHost)
                    await _roomService.DeleteRoomAsync(_currentRoom);
                else
                    await _roomService.LeaveRoomAsync(_currentRoom);
            }
            catch { }

            this.Close();
        }

        private async void Frm_WaitingRoomForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isClosing == false)
            {
                // user bấm dấu X
                try
                {
                    if (_isHost)
                        await _roomService.DeleteRoomAsync(_currentRoom);
                    else
                        await _roomService.LeaveRoomAsync(_currentRoom);
                }
                catch { }
            }

            _roomService.StopListenRoom();
            uC_Chat1.StopListening();
        }

        private void CloseSafely()
        {
            _isClosing = true;
            this.Close();
        }
    }
}
