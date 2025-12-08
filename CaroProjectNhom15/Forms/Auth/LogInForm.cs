using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaroProjectNhom15.Utils;
using AuthTest01.Services;
using Auth.Models;

namespace CaroProjectNhom15.Forms.Auth
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _authService = new();
        private readonly UserService _userService = new();

        public LoginForm()
        {
            InitializeComponent();

            // Đảm bảo event được gắn — an toàn dù Designer có gắn hay không
            Btn_LogIn.Click += Btn_LogIn_Click;
            Btn_CreateNewAccount.Click += Btn_CreateNewAccount_Click;
            LnkL_ForgotPassword.LinkClicked += LnkL_ForgotPassword_LinkClicked;
        }

        private async void Btn_LogIn_Click(object? sender, EventArgs e)
        {
            var email = Tb_Email.Text.Trim();
            var password = Tb_Password.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập email và mật khẩu.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Btn_LogIn.Enabled = false;
            try
            {
                // 1) Thử đăng nhập bằng SDK (nếu SDK trả đủ token, dùng đường này)
                var cred = await _authService.LoginAsync(email, password);

                // 2) Cố gắng lấy refreshToken từ credential SDK
                string? refreshToken = ExtractRefreshToken(cred);
                string idToken = null;
                string userId = null;

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    // Nếu có refresh token, đổi lấy idToken + userId và init DB
                    var tuple = await _authService.LoginWithRefreshTokenAsync(refreshToken);
                    idToken = tuple.idToken;
                    userId = tuple.userId;
                }
                else
                {
                    // SDK không trả refresh token — fallback: gọi REST signInWithPassword để lấy idToken & refreshToken
                    var rest = await _authService.SignInWithPasswordRestAsync(email, password);
                    if (rest == null)
                        throw new Exception("Không nhận được phản hồi hợp lệ từ Firebase (REST).");

                    idToken = rest.IdToken;
                    userId = rest.LocalId;
                    refreshToken = rest.RefreshToken;

                    // init DB client với idToken
                    FirebaseProvider.Instance.InitDatabase(idToken);
                }

                if (string.IsNullOrEmpty(idToken))
                    throw new Exception("Không nhận được idToken sau khi đăng nhập.");

                // 3) Kiểm tra emailVerified bằng accounts:lookup
                bool emailVerified = await CheckEmailVerifiedAsync(idToken);
                if (!emailVerified)
                {
                    var r = MessageBox.Show("Email của bạn chưa được xác thực. Gửi lại email xác thực?", "Email chưa xác thực", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (r == DialogResult.Yes)
                    {
                        await _authService.SendEmailVerificationAsync(idToken);
                        MessageBox.Show("Đã gửi email xác thực. Vui lòng kiểm tra hộp thư.", "Đã gửi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    return;
                }

                // 4) Lưu refreshToken (nếu bạn đã có setting RefreshToken) — thực hiện an toàn trong try/catch
                try
                {
                    var settings = Properties.Settings.Default;
                    if (settings != null)
                    {
                        // kiểm tra tồn tại property trước khi gán (tránh lỗi nếu chưa tạo Settings)
                        try
                        {
                            var prop = settings.Properties["RefreshToken"];
                            if (prop != null)
                            {
                                settings["RefreshToken"] = refreshToken ?? string.Empty;
                                settings.Save();
                            }
                        }
                        catch { /* bỏ qua nếu property không tồn tại hoặc save lỗi */ } // ignore if property does not exist or save fails
                    }
                }
                catch { /* Bỏ qua lỗi */ } // ignore

                // 5) Thành công
                MessageBox.Show($"Đăng nhập thành công: {email}", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // TODO: mở MainForm, truyền idToken/userId nếu cần
                try
                {
                    FirebaseProvider.Instance.InitDatabase(idToken);
                }
                catch { /* Bỏ qua lỗi */ } // ignore
                UserModel user = null;
                try
                {
                    if (!string.IsNullOrEmpty(userId))
                    {
                        user = await _userService.GetUserAsync(userId);
                    }
                }
                catch { user = null; }

                if (user == null)
                {
                    var username = string.IsNullOrEmpty(email) ? "unknown" : email.Split('@')[0];
                    user = new UserModel(userId ?? string.Empty, email, username, username, null);
                    try
                    {
                        if (!string.IsNullOrEmpty(user.Uid))
                            await _userService.CreateUserAsync(user);
                    }
                    catch { /* Bỏ qua lỗi */ } // ignore
                }

                // Luôn mở Home sau khi có UserModel
                // truyền idToken để Home có thể chuyển tiếp nó đến UserForm / FriendsForm
                var home = new global::CaroProjectNhom15.Forms.HomeForm.cs.Home(user, idToken);

                // QUAN TRỌNG: hiển thị lại LoginForm ban đầu khi Home đóng
                home.FormClosed += (s, args) => this.Show();

                home.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đăng nhập thất bại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Btn_LogIn.Enabled = true;
            }
        }

        private void Btn_CreateNewAccount_Click(object? sender, EventArgs e)
        {
            using var rg = new RegisterForm();
            rg.ShowDialog(this);
        }

        private void LnkL_ForgotPassword_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            using var frm = new ForgotPasswordForm();
            frm.ShowDialog(this);
        }

        // Các hàm trợ giúp
        private static string? ExtractRefreshToken(object credential)
        {
            if (credential == null) return null;
            try
            {
                dynamic d = credential;
                try { string rt = d.RefreshToken; if (!string.IsNullOrEmpty(rt)) return rt; } catch { } // try dynamic access

                var t = credential.GetType();
                foreach (var name in new[] { "RefreshToken", "RefreshTokenToken" })
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
            catch { } // ignore
            return null;
        }

        private static async Task<bool> CheckEmailVerifiedAsync(string idToken)
        {
            try
            {
                using var client = new HttpClient();
                var payload = new { idToken = idToken };
                var url = $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={FirebaseConfig.ApiKey}";
                var res = await client.PostAsJsonAsync(url, payload);
                if (!res.IsSuccessStatusCode) return false;
                var json = await res.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("users", out var users) && users.GetArrayLength() > 0)
                {
                    var first = users[0];
                    if (first.TryGetProperty("emailVerified", out var ev))
                        return ev.GetBoolean();
                }
            }
            catch { } // ignore
            return false;
        }
    }
}