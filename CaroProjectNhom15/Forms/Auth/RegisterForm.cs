using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Auth.Models;
using AuthTest01.Services;
using CaroProjectNhom15.Utils; // cần để gọi FirebaseProvider

namespace CaroProjectNhom15.Forms.Auth
{
    public partial class RegisterForm : Form
    {
        private readonly AuthService _authService = new();
        private readonly UserService _userService = new();

        public RegisterForm()
        {
            InitializeComponent();

            Btn_Continue.Click += Btn_Continue_Click;
            Btn_BackToLogIn.Click += Btn_BackToLogIn_Click;
        }

        private async void Btn_Continue_Click(object? sender, EventArgs e)
        {
            var email = Tb_Email.Text.Trim();
            var password = Tb_Password.Text;
            var confirm = Tb_ConfirmPassword.Text;
            var name = Tb_Name.Text.Trim();
            var username = Tb_Username.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Nhập email và mật khẩu.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Btn_Continue.Enabled = false;
            try
            {
                // 1) Tạo account (SDK)
                var createdCred = await _authService.RegisterAsync(email, password);

                // debug: dump returned object để biết SDK trả gì (Output -> Debug)
                DebugWriteObjectProperties(createdCred, "createdCred");

                // 2) Thử lấy refreshToken từ createdCred
                string? refreshToken = ExtractRefreshToken(createdCred);

                string idToken = null;
                string userId = null;

                // 3) Nếu không có refreshToken, fallback gọi REST signInWithPassword để lấy idToken/refreshToken/localId
                if (string.IsNullOrEmpty(refreshToken))
                {
                    var rest = await _authService.SignInWithPasswordRestAsync(email, password);
                    if (rest == null)
                        throw new Exception("Không nhận được phản hồi hợp lệ từ Firebase (REST).");

                    idToken = rest.IdToken;
                    userId = rest.LocalId;

                    // init DB client
                    FirebaseProvider.Instance.InitDatabase(idToken);
                }
                else
                {
                    // nếu có refreshToken, dùng method hiện có để lấy idToken + userId và init DB
                    var tuple = await _authService.LoginWithRefreshTokenAsync(refreshToken);
                    idToken = tuple.idToken;
                    userId = tuple.userId;
                }

                if (string.IsNullOrEmpty(idToken) || string.IsNullOrEmpty(userId))
                    throw new Exception("Không lấy được idToken hoặc userId sau khi đăng ký.");

                // 4) tạo user model và lưu DB
                var user = new UserModel(userId, email, username, name);
                await _userService.CreateUserAsync(user);

                // 5) gửi email xác thực (dùng idToken)
                await _authService.SendEmailVerificationAsync(idToken);

                MessageBox.Show("Đăng ký thành công. Email xác thực đã được gửi.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                var msg = ex.Message ?? string.Empty;
                if (msg.Contains("Email này đã được đăng ký") || msg.Contains("EMAIL_EXISTS", StringComparison.OrdinalIgnoreCase))
                {
                    var r = MessageBox.Show(
                        "Email này đã được đăng ký. Bạn muốn gửi email đặt lại mật khẩu (Yes) hoặc mở trang đăng nhập (No)?",
                        "Email đã tồn tại",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        try
                        {
                            await _authService.TrySendPasswordResetEmailAsync(email);
                            MessageBox.Show("Email đặt lại mật khẩu đã được gửi.", "Đã gửi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception sendEx)
                        {
                            MessageBox.Show($"Không thể gửi email đặt lại: {sendEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Close();
                        using var lf = new LoginForm();
                        lf.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show($"Lỗi đăng ký: {msg}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                Btn_Continue.Enabled = true;
            }
        }

        private void Btn_BackToLogIn_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // Giữ helper lấy refresh token (tham khảo nhiều tên property)
        private static string? ExtractRefreshToken(object credential)
        {
            if (credential == null) return null;
            try
            {
                dynamic d = credential;
                try { string rt = d.RefreshToken; if (!string.IsNullOrEmpty(rt)) return rt; } catch { }

                var t = credential.GetType();
                foreach (var name in new[] { "RefreshToken", "RefreshTokenToken", "Refresh_Token", "Refresh" })
                {
                    var p = t.GetProperty(name);
                    if (p != null)
                    {
                        var v = p.GetValue(credential) as string;
                        if (!string.IsNullOrEmpty(v)) return v;
                    }
                }

                var userProp = t.GetProperty("User");
                if (userProp != null)
                {
                    var userObj = userProp.GetValue(credential);
                    if (userObj != null)
                    {
                        var ut = userObj.GetType();
                        var p2 = ut.GetProperty("RefreshToken") ?? ut.GetProperty("RefreshTokenToken");
                        if (p2 != null)
                        {
                            var v = p2.GetValue(userObj) as string;
                            if (!string.IsNullOrEmpty(v)) return v;
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        // Debug helper: in ra Output -> Debug để bạn biết SDK trả gì
        private static void DebugWriteObjectProperties(object obj, string name)
        {
            try
            {
                if (obj == null)
                {
                    Debug.WriteLine($"{name} = null");
                    return;
                }

                var t = obj.GetType();
                var props = t.GetProperties().Select(p =>
                {
                    try
                    {
                        var val = p.GetValue(obj);
                        var s = val == null ? "<null>" : val.ToString();
                        if (s != null && s.Length > 200) s = s.Substring(0, 200) + "...";
                        return $"{p.Name}={s}";
                    }
                    catch { return $"{p.Name}=<err>"; }
                });

                Debug.WriteLine($"{name} props: {string.Join(", ", props)}");
            }
            catch { }
        }
    }
}
