using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Services;
using Firebase.Database.Streaming; // Thư viện để hứng sự kiện Realtime
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.UserControls
{
    public partial class UC_Chat : UserControl
    {
        // Các biến lưu trữ thông tin cần thiết để chat
        private RoomService _roomService;
        private string _roomId;
        private UserModel _currentUser;
        private IDisposable _chatSubscription; // Vé để hủy đăng ký khi tắt

        public UC_Chat()
        {
            InitializeComponent();

            // Gắn sự kiện nhấn phím cho ô nhập liệu
            // (Phòng trường hợp Designer chưa gắn)
            this.Tb_Msg.KeyDown += new KeyEventHandler(this.Tb_Msg_KeyDown);
        }

        // ==========================================
        // 1. HÀM KHỞI TẠO (Form cha BẮT BUỘC phải gọi hàm này)
        // ==========================================
        public void Init(RoomService service, string roomId, UserModel currentUser)
        {
            _roomService = service;
            _roomId = roomId;
            _currentUser = currentUser;

            // Xóa sạch log cũ (nếu có)
            Rtb_Log.Clear();

            // Bắt đầu lắng nghe tin nhắn ngay lập tức
            StartListening();
        }

        // ==========================================
        // 2. LẮNG NGHE TIN NHẮN TỪ FIREBASE
        // ==========================================
        private void StartListening()
        {
            if (_roomService == null) return;

            // Hủy cái cũ nếu lỡ có
            StopListening();

            _chatSubscription = _roomService.ListenToMessages(_roomId)
                .Subscribe(msgEvent =>
                {
                    // Chỉ xử lý khi có tin nhắn mới được thêm vào hoặc cập nhật
                    if (msgEvent.EventType == FirebaseEventType.InsertOrUpdate)
                    {
                        // Dữ liệu từ Firebase đang ở luồng phụ (Background Thread)
                        // Phải dùng Invoke để "Về Bờ" vẽ lên giao diện (UI Thread)
                        if (this.IsHandleCreated) // Kiểm tra xem cửa sổ còn sống không
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                AppendMessage(msgEvent.Object);
                            });
                        }
                    }
                });
        }

        // ==========================================
        // 3. HIỂN THỊ TIN NHẮN LÊN MÀN HÌNH (Tô màu)
        // ==========================================
        private void AppendMessage(MessageModel msg)
        {
            if (msg == null) return;

            // Format hiển thị: [12:30] Tên: Nội dung

            // A. In Giờ (Màu Xám)
            Rtb_Log.SelectionColor = Color.Gray;
            Rtb_Log.AppendText($"[{msg.Time}] ");

            // B. In Tên người gửi (Đậm, Có màu)
            if (msg.SenderName == _currentUser.UserName)
            {
                // Tin của mình: Màu Xanh Dương
                Rtb_Log.SelectionColor = Color.Blue;
                Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Bold);
            }
            else
            {
                // Tin của đối thủ: Màu Đỏ
                Rtb_Log.SelectionColor = Color.Red;
                Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Bold);
            }
            Rtb_Log.AppendText(msg.SenderName + ": ");

            // C. In Nội dung (Màu Đen, Chữ thường)
            Rtb_Log.SelectionColor = Color.Black;
            Rtb_Log.SelectionFont = new Font(Rtb_Log.Font, FontStyle.Regular);
            Rtb_Log.AppendText(msg.Content + "\n");

            // D. Tự động cuộn xuống dòng cuối cùng
            Rtb_Log.ScrollToCaret();
        }

        // ==========================================
        // 4. GỬI TIN NHẮN (Sự kiện Enter)
        // ==========================================
        private async void Tb_Msg_KeyDown(object sender, KeyEventArgs e)
        {
            // Kiểm tra nếu phím nhấn là ENTER
            if (e.KeyCode == Keys.Enter)
            {
                // Ngăn tiếng "Bíp" khó chịu của Windows khi nhấn Enter trong TextBox 1 dòng
                e.SuppressKeyPress = true;

                // Lấy nội dung và cắt khoảng trắng thừa
                string content = Tb_Msg.Text.Trim();

                // Nếu rỗng thì không gửi
                if (string.IsNullOrEmpty(content)) return;

                // Xóa ô nhập liệu ngay lập tức cho cảm giác mượt
                Tb_Msg.Clear();
                Tb_Msg.Focus(); // Giữ chuột ở ô nhập

                try
                {
                    // Tạo một RoomModel tạm chỉ chứa ID để truyền vào Service
                    // (Vì Service của bạn yêu cầu RoomModel chứ không phải string ID)
                    RoomModel tempRoom = new RoomModel() { ID = _roomId };

                    // Gọi Service gửi tin (Async Task)
                    await _roomService.SendMessageAsync(tempRoom, _currentUser, content);
                }
                catch (Exception ex)
                {
                    // Nếu lỗi mạng thì báo nhẹ, không làm crash app
                    MessageBox.Show("Gửi tin lỗi: " + ex.Message);
                }
            }
        }

        // ==========================================
        // 5. DỌN DẸP TÀI NGUYÊN (QUAN TRỌNG)
        // ==========================================
        // Hàm này để Form cha gọi khi đóng cửa sổ
        public void StopListening()
        {
            if (_chatSubscription != null)
            {
                _chatSubscription.Dispose();
                _chatSubscription = null;
            }
        }
    }
}