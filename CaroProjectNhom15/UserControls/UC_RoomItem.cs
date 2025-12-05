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

            Lbl_RoomName.Text = $"Name: {RoomData.Name ?? "Unknown"}";
            Lbl_HostName.Text = $"Host: {RoomData.Host?.UserName ?? "(None)"}";
            Lbl_Status.Text = $"Status: {RoomData.Status ?? "Unknown"}";

            // Xử lý màu + trạng thái nút Join
            switch (RoomData.Status)
            {
                case "Waiting":
                    Lbl_Status.ForeColor = Color.Green;
                    Btn_Join.Enabled = true;
                    Btn_Join.Text = "Join";
                    break;

                case "Ready":
                    Lbl_Status.ForeColor = Color.Orange;
                    Btn_Join.Enabled = true;
                    Btn_Join.Text = "Join";
                    break;

                case "Playing":
                case "Full":
                    Lbl_Status.ForeColor = Color.Red;
                    Btn_Join.Enabled = false;
                    Btn_Join.Text = "Full";
                    break;

                default:
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
