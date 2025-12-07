using CaroProjectNhom15.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CaroProjectNhom15.UserControls
{
    public partial class UC_RoomItem : UserControl
    {
        public RoomModel RoomData { get; private set; }

        public event EventHandler<RoomModel> OnJoinClicked;

        public UC_RoomItem(RoomModel room)
        {
            InitializeComponent();
            UpdateData(room);
        }

        public void UpdateData(RoomModel room)
        {
            RoomData = room;
            RefreshUI();
        }

        private void RefreshUI()
        {
            if (RoomData == null) return;

            Lbl_RoomId.Text = $"ID: {RoomData.ID}";

            // 1. Hiển thị Tên Phòng (Xử lý chuỗi rỗng)
            // Nếu Name null thì hiện "Phòng Trống", ngược lại hiện Name
            string displayName = string.IsNullOrEmpty(RoomData.Name) ? "Phòng Trống" : RoomData.Name;
            Lbl_RoomName.Text = $"Name: {displayName}";

            // 2. Hiển thị Tên Host
            if (RoomData.Host != null)
            {
                Lbl_HostName.Text = $"Host: {RoomData.Host.UserName}";
            }
            else
            {
                // Nếu Host null -> Báo là Trống để người dùng biết có thể vào chiếm phòng
                Lbl_HostName.Text = "Host: (Trống)";
            }

            // 3. Hiển thị Status gốc
            Lbl_Status.Text = $"Status: {RoomData.Status ?? "Unknown"}";

            // 4. Xử lý Logic nút Join và Màu sắc
            // ƯU TIÊN CAO NHẤT: Nếu Host null HOẶC Status là Waiting -> Cho phép Join (màu xanh)
            if (RoomData.Host == null || RoomData.Status == "Waiting")
            {
                Lbl_Status.ForeColor = Color.Green;

                // Nếu Host null, sửa hiển thị Status chút cho dễ hiểu
                if (RoomData.Host == null) Lbl_Status.Text = "Status: Waiting (Free)";

                Btn_Join.Enabled = true;
                Btn_Join.Text = "Join";
                return; // Return luôn để không chạy vào switch bên dưới
            }

            // Các trường hợp còn lại
            switch (RoomData.Status)
            {
                case "Ready":
                    // Ready nghĩa là Host đang chờ Guest -> Cho phép Join
                    Lbl_Status.ForeColor = Color.Orange;
                    Btn_Join.Enabled = true;
                    Btn_Join.Text = "Join";
                    break;

                case "Playing":
                case "Full":
                    // Đã đầy hoặc đang chơi -> Cấm Join
                    Lbl_Status.ForeColor = Color.Red;
                    Btn_Join.Enabled = false;
                    Btn_Join.Text = "Full";
                    break;

                default:
                    // Trạng thái lạ
                    Lbl_Status.ForeColor = Color.Gray;
                    Btn_Join.Enabled = false;
                    Btn_Join.Text = "N/A";
                    break;
            }
        }

        private void Btn_Join_Click(object sender, EventArgs e)
        {
            OnJoinClicked?.Invoke(this, RoomData);
        }
    }
}
