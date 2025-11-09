using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;
using CaroProjectNhom15.Utils;
using System.Diagnostics;

namespace CaroProjectNhom15.Forms.Auth
{
    public partial class ResetPasswordForm : Form
    {
        private readonly string _oobCode;
        private readonly HttpClient _client = new();

        public ResetPasswordForm(string oobCode)
        {
            InitializeComponent();

            if (string.IsNullOrWhiteSpace(oobCode))
                throw new ArgumentNullException(nameof(oobCode));

            // Normalize oobCode: trim and only URL-decode when it contains percent-encoding.
            var normalized = oobCode.Trim();
            try
            {
                if (normalized.Contains("%"))
                    normalized = Uri.UnescapeDataString(normalized);
            }
            catch
            {
                // ignore decoding errors and keep raw trimmed value
            }

            _oobCode = normalized;
            Btn_ResetPassword.Click += Btn_ResetPassword_Click;
            LnkL_BackToLogIn.LinkClicked += LnkL_BackToLogIn_LinkClicked;
        }

        private async void Btn_ResetPassword_Click(object? sender, EventArgs e)
        {
            var newPwd = Tb_Password.Text;
            var confirm = Tb_ConfirmPassword.Text;

            if (string.IsNullOrEmpty(newPwd) || newPwd.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPwd != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Btn_ResetPassword.Enabled = false;
            try
            {
                // Debug: in oobCode length (không đưa lên UI production)
                Debug.WriteLine($"Applying oobCode='{_oobCode}' length={_oobCode.Length}");

                var payload = new { oobCode = _oobCode, newPassword = newPwd };
                var url = $"https://identitytoolkit.googleapis.com/v1/accounts:update?key={FirebaseConfig.ApiKey}";
                var res = await _client.PostAsJsonAsync(url, payload);
                var txt = await res.Content.ReadAsStringAsync();

                if (!res.IsSuccessStatusCode)
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(txt);
                        if (doc.RootElement.TryGetProperty("error", out var err) && err.TryGetProperty("message", out var msg))
                        {
                            MessageBox.Show(msg.GetString(), "Lỗi từ Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch { }

                    MessageBox.Show(txt, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show("Đổi mật khẩu thành công. Vui lòng đăng nhập bằng mật khẩu mới.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đổi mật khẩu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Btn_ResetPassword.Enabled = true;
            }
        }

        private void LnkL_BackToLogIn_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }
    }
}
