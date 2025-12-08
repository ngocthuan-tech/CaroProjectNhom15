using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class Home : Form
    {
        private readonly string? _currentUid;
        private readonly string? _idToken;

        public Home()
        {
            InitializeComponent();

            // Gắn sự kiện cho nút Bạn bè để mở FriendsForm
            Btn_Friends.Click += Btn_Friends_Click;

            // Gắn sự kiện cho nút Người dùng để mở UserForm
            Btn_user.Click += Btn_user_Click;
        }

        // Constructor mới chấp nhận UserModel và idToken tùy chọn, và điền vào UI
        public Home(UserModel user, string? idToken = null) : this()
        {
            _currentUid = user?.Uid;
            _idToken = idToken;

            try
            {
                Tb_tenUser.Text = !string.IsNullOrEmpty(user?.UserName) ? user.UserName : user?.Email ?? string.Empty;

                // Cố gắng tải avatar một cách mạnh mẽ (URI dữ liệu, http(s), đường dẫn tệp). Giữ mặc định của designer nếu không có/thất bại.
                TryLoadAvatar(user?.AvatarUrl);
            }
            catch { /* linh hoạt với null hoặc dữ liệu thiếu */ } // be resilient to nulls or missing data
        }

        private void Btn_user_Click(object? sender, EventArgs e)
        {
            // Nếu ta không có uid hiện tại, thông báo cho nhà phát triển/người dùng
            if (string.IsNullOrEmpty(_currentUid))
            {
                MessageBox.Show("Id người dùng hiện tại không có sẵn. Không thể hiển thị hồ sơ người dùng.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information); // Current user id not available. User profile cannot be shown.
                return;
            }

            // Mở UserForm và truyền uid hiện tại và idToken tùy chọn
            var userForm = new UserForm(_currentUid, _idToken);
            userForm.ShowDialog(this);

            // Nếu người dùng đăng xuất khỏi UserForm, chỉ cần đóng Home.
            // LoginForm ban đầu (đã mở Home) hiện có một trình xử lý để Show() chính nó,
            // vì vậy việc đóng Home sẽ đưa người dùng trở lại màn hình Đăng nhập mà không thoát ứng dụng.
            if (userForm.SignedOut)
            {
                this.Close();
                return;
            }

            // Cập nhật UI Home ngay lập tức nếu người dùng thay đổi hồ sơ trong UserForm
            try
            {
                if (!string.IsNullOrEmpty(userForm.NewUserName))
                {
                    Tb_tenUser.Text = userForm.NewUserName;
                }

                if (!string.IsNullOrEmpty(userForm.NewAvatarUrl))
                {
                    TryLoadAvatar(userForm.NewAvatarUrl);
                }
            }
            catch { /* bỏ qua các lỗi cập nhật UI */ } // ignore UI update errors
        }

        private void Btn_Friends_Click(object? sender, EventArgs e)
        {
            // Truyền uid hiện tại vào FriendsForm để nó có thể tải danh sách bạn bè
            var friendsForm = new FriendsForm(_currentUid);
            friendsForm.ShowDialog(this);
        }

        // Bộ tải avatar mạnh mẽ: hỗ trợ URI dữ liệu (base64), URL http(s) và đường dẫn tệp cục bộ.
        private void TryLoadAvatar(string? avatarUrl)
        {
            if (string.IsNullOrWhiteSpace(avatarUrl))
                return; // giữ hình ảnh mặc định từ designer

            try
            {
                // Data URI (data:image/png;base64,...)
                if (avatarUrl.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                {
                    var comma = avatarUrl.IndexOf(',');
                    if (comma >= 0 && comma + 1 < avatarUrl.Length)
                    {
                        var base64 = avatarUrl.Substring(comma + 1);
                        var bytes = Convert.FromBase64String(base64);
                        using var ms = new MemoryStream(bytes);
                        var img = Image.FromStream(ms);
                        Pb_anhUser.Image = new Bitmap(img);
                    }
                    return;
                }

                // URL từ xa HTTP/HTTPS
                if (avatarUrl.StartsWith("http:", StringComparison.OrdinalIgnoreCase) ||
                    avatarUrl.StartsWith("https:", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Pb_anhUser.LoadAsync(avatarUrl);
                    }
                    catch
                    {
                        // bỏ qua — giữ mặc định của designer
                    }
                    return;
                }

                // Đường dẫn tệp cục bộ
                if (File.Exists(avatarUrl))
                {
                    try
                    {
                        // Tải bản sao để tránh khóa tệp
                        using var fs = File.OpenRead(avatarUrl);
                        var img = Image.FromStream(fs);
                        Pb_anhUser.Image = new Bitmap(img);
                    }
                    catch
                    {
                        // bỏ qua
                    }
                    return;
                }

                // Nếu không, không được nhận dạng — giữ avatar mặc định (designer).
            }
            catch
            {
                // nuốt lỗi, giữ avatar mặc định
            } // swallow errors, keep default avatar
        }
    }
}