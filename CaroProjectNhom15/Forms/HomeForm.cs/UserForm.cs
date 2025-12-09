using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;
using AuthTest01.Services;
using CaroProjectNhom15.Forms.Auth;
using CaroProjectNhom15.Utils;

namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    public partial class UserForm : Form
    {
        private readonly UserService _userService = new UserService();
        private UserModel _currentUser;
        private readonly string _uid;
        private string? _idToken; // thay đổi từ readonly sang mutable (có thể thay đổi)

        // Mới: cờ báo hiệu người dùng yêu cầu đăng xuất
        public bool SignedOut { get; private set; } = false;

        // Mới: hiển thị các thay đổi để người gọi (Home) có thể cập nhật UI
        public string? NewAvatarUrl { get; private set; }
        public string? NewUserName { get; private set; }

        // Constructor hiện chấp nhận uid (và idToken tùy chọn)
        public UserForm(string uid, string? idToken = null)
        {
            InitializeComponent();

            _uid = uid ?? throw new ArgumentNullException(nameof(uid));
            _idToken = idToken;

            // Gắn sự kiện
            Load += UserForm_LoadAsync;
            Btn_DoiTen.Click += Btn_DoiTen_ClickAsync;
            btn_DoiAnh.Click += Btn_DoiAnh_ClickAsync;
            Btn_DangXuat.Click += Btn_DangXuat_Click;

            // Đảm bảo nút Thoát đóng form khi được nhấp
            Btn_ExitUser.Click += (_, __) => Close();
        }

        private async void UserForm_LoadAsync(object? sender, EventArgs e)
        {
            try
            {
                // Đảm bảo Database được khởi tạo. Nếu ta không có idToken hợp lệ, hãy thử làm mới nó
                // từ refresh token đã lưu (Properties.Settings.Default.RefreshToken).
                if (FirebaseProvider.Instance.Database == null)
                {
                    if (!string.IsNullOrEmpty(_idToken))
                    {
                        try
                        {
                            FirebaseProvider.Instance.InitDatabase(_idToken);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[UserForm] Khởi tạo Database bằng idToken được cung cấp thất bại: {ex.Message}");
                        }
                    }

                    // Nếu vẫn chưa được khởi tạo, thử quy trình refresh-token
                    if (FirebaseProvider.Instance.Database == null)
                    {
                        try
                        {
                            var saved = string.Empty;
                            try
                            {
                                saved = global::CaroProjectNhom15.Properties.Settings.Default?.RefreshToken ?? string.Empty;
                            }
                            catch { saved = string.Empty; }

                            if (!string.IsNullOrEmpty(saved))
                            {
                                var auth = new AuthService();
                                try
                                {
                                    var tuple = await auth.LoginWithRefreshTokenAsync(saved);
                                    _idToken = tuple.idToken;
                                    FirebaseProvider.Instance.InitDatabase(_idToken);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine($"[UserForm] Đăng nhập bằng refresh token thất bại: {ex.Message}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[UserForm] Lỗi khi cố gắng refresh token: {ex.Message}");
                        }
                    }
                }

                await LoadUserAsync();

                // Hiển thị các nút hành động sau khi tải người dùng để UI có thể sử dụng được
                try
                {
                    Btn_ExitUser.Visible = true;
                    Btn_DoiTen.Visible = true;
                    btn_DoiAnh.Visible = true;
                    Btn_DangXuat.Visible = true;
                }
                catch { /* bỏ qua nếu tên control của designer khác */ }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải thông tin user: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadUserAsync()
        {
            // Tắt controls trong khi tải
            SetControlsEnabled(false);
            try
            {
                var user = await _userService.GetUserAsync(_uid);
                if (user == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin người dùng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _currentUser = user;

                // Đặt tên hiển thị: ưu tiên UserName, nếu không có thì dùng FullName hoặc Email
                Tb_nameUser.Text = !string.IsNullOrWhiteSpace(user.UserName)
                    ? user.UserName
                    : !string.IsNullOrWhiteSpace(user.FullName)
                        ? user.FullName
                        : user.Email;

                // Tải avatar nếu có
                if (!string.IsNullOrWhiteSpace(user.AvatarUrl))
                {
                    try
                    {
                        if (user.AvatarUrl.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                        {
                            // Data URI -> giải mã
                            var comma = user.AvatarUrl.IndexOf(',');
                            if (comma >= 0)
                            {
                                var base64 = user.AvatarUrl.Substring(comma + 1);
                                var bytes = Convert.FromBase64String(base64);
                                using var ms = new MemoryStream(bytes);
                                var img = Image.FromStream(ms);
                                pb_avatar.Image = new Bitmap(img);
                            }
                        }
                        else if (user.AvatarUrl.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            // URL từ xa: LoadAsync (có thể ném lỗi nếu không truy cập được)
                            try
                            {
                                pb_avatar.LoadAsync(user.AvatarUrl);
                            }
                            catch
                            {
                                // bỏ qua, giữ hình ảnh mặc định từ resources
                            }
                        }
                        else
                        {
                            // coi là đường dẫn tệp nếu tồn tại
                            if (File.Exists(user.AvatarUrl))
                            {
                                pb_avatar.Image = Image.FromFile(user.AvatarUrl);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[UserForm] Lỗi khi tải avatar: {ex.Message}");
                        // giữ avatar hiện có/mặc định
                    }
                }
            }
            finally
            {
                SetControlsEnabled(true);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            Tb_nameUser.Enabled = enabled;
            Btn_DoiTen.Enabled = enabled;
            btn_DoiAnh.Enabled = enabled;
            Btn_DangXuat.Enabled = enabled;
        }

        private async void Btn_DoiTen_ClickAsync(object? sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("User chưa được tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newName = Tb_nameUser.Text?.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                MessageBox.Show("Tên không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newName == _currentUser.UserName)
            {
                MessageBox.Show("Tên không thay đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Btn_DoiTen.Enabled = false;
            try
            {
                _currentUser.UserName = newName;
                await _userService.UpdateUserAsync(_currentUser.Uid, _currentUser);
                NewUserName = _currentUser.UserName;
                MessageBox.Show("Đổi tên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi tên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Btn_DoiTen.Enabled = true;
            }
        }

        private async void Btn_DoiAnh_ClickAsync(object? sender, EventArgs e)
        {
            if (_currentUser == null)
            {
                MessageBox.Show("User chưa được tải.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var ofd = new OpenFileDialog
            {
                Title = "Chọn ảnh đại diện",
                Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
                Multiselect = false
            };

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            var file = ofd.FileName;
            try
            {
                // Tải ảnh và hiển thị ngay lập tức
                using var src = Image.FromFile(file);
                // tạo một bản sao để tránh khóa tệp
                var bmp = new Bitmap(src);
                pb_avatar.Image = new Bitmap(bmp);

                // Chuyển sang Data URI PNG base64 và lưu trữ trong user model
                byte[] imageBytes;
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    imageBytes = ms.ToArray();
                }

                var base64 = Convert.ToBase64String(imageBytes);
                _currentUser.AvatarUrl = $"data:image/png;base64,{base64}";

                // Đảm bảo DB được khởi tạo trước khi cố gắng cập nhật
                if (FirebaseProvider.Instance.Database == null)
                {
                    // Thử khởi tạo với idToken trong bộ nhớ nếu có sẵn
                    if (!string.IsNullOrEmpty(_idToken))
                    {
                        try { FirebaseProvider.Instance.InitDatabase(_idToken); }
                        catch { }
                    }

                    // dự phòng sang refresh token đã lưu
                    if (FirebaseProvider.Instance.Database == null)
                    {
                        try
                        {
                            var saved = string.Empty;
                            try { saved = global::CaroProjectNhom15.Properties.Settings.Default?.RefreshToken ?? string.Empty; } catch { saved = string.Empty; }
                            if (!string.IsNullOrEmpty(saved))
                            {
                                var auth = new AuthService();
                                var tuple = await auth.LoginWithRefreshTokenAsync(saved);
                                _idToken = tuple.idToken;
                                FirebaseProvider.Instance.InitDatabase(_idToken);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"[UserForm] Không thể khởi tạo DB trước khi lưu avatar: {ex.Message}");
                        }
                    }
                }

                // Lưu thay đổi
                btn_DoiAnh.Enabled = false;
                try
                {
                    await _userService.UpdateUserAsync(_currentUser.Uid, _currentUser);
                    // hiển thị avatar mới để người gọi (Home) có thể cập nhật UI sau khi đóng dialog
                    NewAvatarUrl = _currentUser.AvatarUrl;
                    MessageBox.Show("Đổi ảnh thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi lưu ảnh lên server: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn_DoiAnh.Enabled = true;
            }
        }

        private void Btn_DangXuat_Click(object? sender, EventArgs e)
        {
            // Đánh dấu rằng người dùng yêu cầu đăng xuất để người gọi (Home) có thể phản ứng
            SignedOut = true;

            // Xóa tham chiếu DB
            try
            {
                FirebaseProvider.Instance.ClearDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserForm] Lỗi khi xóa DB: {ex.Message}");
            }

            // Tùy chọn xóa refresh token đã lưu khỏi settings (cố gắng hết sức)
            try
            {
                var settings = Properties.Settings.Default;
                if (settings != null)
                {
                    try
                    {
                        var prop = settings.Properties["RefreshToken"];
                        if (prop != null)
                        {
                            settings["RefreshToken"] = string.Empty;
                            settings.Save();
                        }
                    }
                    catch { }
                }
            }
            catch { }

            // Đóng form này. Home (người gọi) sẽ kiểm tra SignedOut và thực hiện điều hướng (đóng + mở Login).
            Close();
        }
    }
}