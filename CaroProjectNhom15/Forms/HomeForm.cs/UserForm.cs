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
        private readonly string? _idToken;

        // New: flag to indicate user requested sign-out
        public bool SignedOut { get; private set; } = false;

        // New: expose changes so caller (Home) can update UI
        public string? NewAvatarUrl { get; private set; }
        public string? NewUserName { get; private set; }

        // Constructor now accepts uid (and optional idToken)
        public UserForm(string uid, string? idToken = null)
        {
            InitializeComponent();

            _uid = uid ?? throw new ArgumentNullException(nameof(uid));
            _idToken = idToken;

            // Wire events
            Load += UserForm_LoadAsync;
            Btn_DoiTen.Click += Btn_DoiTen_ClickAsync;
            btn_DoiAnh.Click += Btn_DoiAnh_ClickAsync;
            Btn_DangXuat.Click += Btn_DangXuat_Click;

            // Ensure Exit button closes the form when clicked
            Btn_ExitUser.Click += (_, __) => Close();
        }

        private async void UserForm_LoadAsync(object? sender, EventArgs e)
        {
            try
            {
                // If an idToken is provided, ensure Database is initialized here (safe to call if already init)
                if (!string.IsNullOrEmpty(_idToken))
                {
                    try
                    {
                        FirebaseProvider.Instance.InitDatabase(_idToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[UserForm] InitDatabase failed: {ex.Message}");
                        // continue — GetUserAsync will inform if DB not initialized
                    }
                }

                await LoadUserAsync();

                // Make action buttons visible after loading user so the UI is usable
                try
                {
                    Btn_ExitUser.Visible = true;
                    Btn_DoiTen.Visible = true;
                    btn_DoiAnh.Visible = true;
                    Btn_DangXuat.Visible = true;
                }
                catch { /* ignore if designer control names differ */ }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải thông tin user: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadUserAsync()
        {
            // Disable controls while loading
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

                // Set display name: prefer UserName, fallback FullName or Email
                Tb_nameUser.Text = !string.IsNullOrWhiteSpace(user.UserName)
                    ? user.UserName
                    : !string.IsNullOrWhiteSpace(user.FullName)
                        ? user.FullName
                        : user.Email;

                // Load avatar if available
                if (!string.IsNullOrWhiteSpace(user.AvatarUrl))
                {
                    try
                    {
                        if (user.AvatarUrl.StartsWith("data:", StringComparison.OrdinalIgnoreCase))
                        {
                            // Data URI -> decode
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
                            // Remote URL: LoadAsync (may throw if unreachable)
                            try
                            {
                                pb_avatar.LoadAsync(user.AvatarUrl);
                            }
                            catch
                            {
                                // ignore, keep default image from resources
                            }
                        }
                        else
                        {
                            // treat as file path if exists
                            if (File.Exists(user.AvatarUrl))
                            {
                                pb_avatar.Image = Image.FromFile(user.AvatarUrl);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[UserForm] Error loading avatar: {ex.Message}");
                        // keep existing/default avatar
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
                // Load image and display immediately
                using var src = Image.FromFile(file);
                // create a copy to avoid locking the file
                var bmp = new Bitmap(src);
                pb_avatar.Image = new Bitmap(bmp);

                // Convert to PNG base64 data URI and store in user model
                byte[] imageBytes;
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    imageBytes = ms.ToArray();
                }

                var base64 = Convert.ToBase64String(imageBytes);
                _currentUser.AvatarUrl = $"data:image/png;base64,{base64}";

                // Persist change
                btn_DoiAnh.Enabled = false;
                await _userService.UpdateUserAsync(_currentUser.Uid, _currentUser);

                // expose the new avatar so caller (Home) can update its UI after dialog closes
                NewAvatarUrl = _currentUser.AvatarUrl;

                MessageBox.Show("Đổi ảnh thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // Mark that user requested sign-out so caller (Home) can react
            SignedOut = true;

            // Clear DB reference
            try
            {
                FirebaseProvider.Instance.ClearDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[UserForm] Error clearing DB: {ex.Message}");
            }

            // Optionally clear saved refresh token from settings (best-effort)
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

            // Close this form. Home (caller) will inspect SignedOut and perform navigation (close + open Login).
            Close();
        }
    }
}
