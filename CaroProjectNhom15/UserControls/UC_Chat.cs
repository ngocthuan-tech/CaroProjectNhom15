using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using Firebase.Database.Streaming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.UserControls
{
    public partial class UC_Chat : UserControl
    {
        private RoomService _roomService;
        private string _roomId;
        private UserModel _currentUser;

        private IDisposable _chatSubscription;

        public UC_Chat()
        {
            InitializeComponent();
            Tb_Msg.KeyDown += Tb_Msg_KeyDown;
        }

        // Khởi tạo phải được gọi từ Form
        public void Init(RoomService service, string roomId, UserModel currentUser)
        {
            _roomService = service;
            _roomId = roomId;
            _currentUser = currentUser;

            Rtb_Log.Clear();
            StartListening();
        }

        // Bắt đầu nghe tin nhắn realtime
        private void StartListening()
        {
            StopListening(); // hủy cái cũ trước khi tạo cái mới

            _chatSubscription = _roomService
                .ListenToMessages(_roomId)
                .Subscribe(ev =>
                {
                    if (ev.EventType != FirebaseEventType.InsertOrUpdate)
                        return;

                    if (!this.IsHandleCreated) return;

                    this.Invoke((MethodInvoker)delegate
                    {
                        AppendMessage(ev.Object);
                    });
                });
        }

        // Hiển thị tin nhắn lên UI
        private void AppendMessage(MessageModel msg)
        {
            if (msg == null || string.IsNullOrEmpty(msg.Content))
                return;

            // ====== In Timestamp (xám) ======
            Rtb_Log.SelectionColor = Color.Gray;
            Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Regular);
            Rtb_Log.AppendText($"[{msg.Time}] ");

            // ====== In tên người gửi ======
            if (msg.SenderName == _currentUser.UserName)
            {
                Rtb_Log.SelectionColor = Color.Blue;
            }
            else
            {
                Rtb_Log.SelectionColor = Color.Red;
            }
            Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Bold);
            Rtb_Log.AppendText(msg.SenderName + ": ");

            // ====== In nội dung ======
            Rtb_Log.SelectionColor = Color.Black;
            Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Regular);
            Rtb_Log.AppendText(msg.Content + "\n");

            // Auto scroll
            Rtb_Log.ScrollToCaret();
        }

        // Người dùng nhấn Enter để gửi tin
        private async void Tb_Msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                string content = Tb_Msg.Text.Trim();
                if (string.IsNullOrEmpty(content)) return;

                Tb_Msg.Clear();

                try
                {
                    await _roomService.SendMessageAsync(
                        new RoomModel { ID = _roomId },
                        _currentUser,
                        content
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gửi tin lỗi: " + ex.Message);
                }
            }
        }

        // Hủy lắng nghe
        public void StopListening()
        {
            _chatSubscription?.Dispose();
            _chatSubscription = null;
        }
    }
}
