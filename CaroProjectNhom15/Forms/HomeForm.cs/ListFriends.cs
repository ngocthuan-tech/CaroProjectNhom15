using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class ListFriends : UserControl
    {
        public ListFriends()
        {
            InitializeComponent();
            // Cấu hình listView cho chế độ xem chi tiết đơn giản
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Tên người dùng", 140); // Username
            listView1.Columns.Add("Họ và tên", 180); // Full name
            listView1.FullRowSelect = true;
        }

        /// <summary>
        /// Điền vào control danh sách bằng một tập hợp người dùng.
        /// </summary>
        public void SetUsers(List<UserModel> users)
        {
            listView1.Items.Clear();

            if (users == null || users.Count == 0)
                return;

            foreach (var u in users)
            {
                var li = new ListViewItem(u.UserName ?? "");
                li.SubItems.Add(u.FullName ?? "");
                li.Tag = u; // giữ model để sử dụng khi chọn
                listView1.Items.Add(li);
            }
        }

        /// <summary>
        /// Trả về model người dùng đã chọn hoặc null nếu không có gì được chọn.
        /// </summary>
        public UserModel? GetSelectedUser()
        {
            if (listView1.SelectedItems.Count == 0) return null;
            return listView1.SelectedItems[0].Tag as UserModel;
        }

        /// <summary>
        /// Hiển thị ListView bên dưới nếu người gọi muốn gắn các sự kiện.
        /// </summary>
        public ListView InnerListView => listView1;
    }
}