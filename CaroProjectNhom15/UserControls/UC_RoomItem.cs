using CaroProjectNhom15.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroProjectNhom15.UserControls
{
    public partial class UC_RoomItem : UserControl
    {
        // Biến này để lưu dữ liệu phòng, sau này nút Join sẽ cần dùng
        public RoomModel RoomData { get; private set; }

        // Sự kiện để báo ra ngoài LobbyForm
        public event EventHandler<RoomModel> OnJoinClicked;

        public UC_RoomItem(RoomModel room)
        {
            InitializeComponent();
            this.RoomData = room;

            // --- GÁN DỮ LIỆU VÀO LABEL (APPEND STRING) ---

            // 1. Tên phòng
            Lbl_RoomName.Text = "Name: " + room.Name;

            // 2. Trạng thái
            Lbl_Status.Text = "Status: " + room.Status;

            // Đổi màu trạng thái cho dễ nhìn
            if (room.Status == "Playing")
            {
                Lbl_Status.ForeColor = Color.Red;
                Btn_Join.Enabled = false; // Đang chơi thì khóa nút Join
                Btn_Join.Text = "Full";
            }
            else
            {
                Lbl_Status.ForeColor = Color.Green;
                Btn_Join.Enabled = true;
                Btn_Join.Text = "Join";
            }

            // 3. Chủ phòng (Cần check null để không bị lỗi)
            if (room.Host != null)
            {
                Lbl_HostName.Text = "Host: " + room.Host.UserName;
            }
            else
            {
                Lbl_HostName.Text = "Host: (Trống)";
            }
        }

        // Sự kiện khi bấm nút Join
        private void Btn_Join_Click(object sender, EventArgs e)
        {
            // Bắn tín hiệu ra ngoài LobbyForm kèm theo dữ liệu phòng
            OnJoinClicked?.Invoke(this, RoomData);
        }
    }
}
