using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;
using CaroProjectNhom15.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;

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

            // Normalize oobCode: trim, try to extract if a full URL was passed, and only decode when needed.
            var normalized = ExtractOobCode(oobCode);
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
                // Debug: print raw token to Output (do not expose in production)
                Debug.WriteLine($"Applying oobCode raw='{_oobCode}' length={_oobCode.Length}");

                // 1) Pre-validate the oobCode using accounts:resetPassword so we can show Firebase errors early.
                var verifyUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:resetPassword?key={FirebaseConfig.ApiKey}";
                var verifyRes = await _client.PostAsJsonAsync(verifyUrl, new { oobCode = _oobCode });
                var verifyTxt = await verifyRes.Content.ReadAsStringAsync();

                if (!verifyRes.IsSuccessStatusCode)
                {
                    try
                    {
                        using var doc = JsonDocument.Parse(verifyTxt);
                        if (doc.RootElement.TryGetProperty("error", out var err) && err.TryGetProperty("message", out var msg))
                        {
                            MessageBox.Show(msg.GetString(), "Lỗi từ Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch { }

                    MessageBox.Show(verifyTxt, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Optionally read email returned by resetPassword response for debugging
                try
                {
                    using var doc2 = JsonDocument.Parse(verifyTxt);
                    if (doc2.RootElement.TryGetProperty("email", out var em))
                        Debug.WriteLine($"oobCode is valid for email: {em.GetString()}");
                }
                catch { }

                // 2) Apply the new password using accounts:update (this consumes the oobCode)
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

        // Helper: try extract oobCode from a pasted URL or raw token
        private static string ExtractOobCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            input = input.Trim().Trim('<', '>', '"', '\'', '(', ')');

            if (Uri.TryCreate(input, UriKind.Absolute, out var uri))
            {
                var q = uri.Query.TrimStart('?');
                var m = Regex.Match(q, @"(?:^|&)oobCode=([^&]+)", RegexOptions.IgnoreCase);
                if (m.Success) { try { return Uri.UnescapeDataString(m.Groups[1].Value); } catch { return m.Groups[1].Value; } }

                var m2 = Regex.Match(input, @"oobCode=([^&\s]+)", RegexOptions.IgnoreCase);
                if (m2.Success) { try { return Uri.UnescapeDataString(m2.Groups[1].Value); } catch { return m2.Groups[1].Value; } }
            }

            var m3 = Regex.Match(input, @"oobCode=([^&\s]+)", RegexOptions.IgnoreCase);
            if (m3.Success) { try { return Uri.UnescapeDataString(m3.Groups[1].Value); } catch { return m3.Groups[1].Value; } }

            if (input.Contains("%"))
            {
                try
                {
                    var un = Uri.UnescapeDataString(input);
                    if (Regex.IsMatch(un, @"^[A-Za-z0-9\-_]+$")) return un;
                    var mm = Regex.Match(un, @"oobCode=([^&\s]+)", RegexOptions.IgnoreCase);
                    if (mm.Success) return mm.Groups[1].Value;
                }
                catch { }
            }

            var candidate = input.Replace(" ", "").Replace("\r", "").Replace("\n", "");
            if (candidate.Length >= 10 && Regex.IsMatch(candidate, @"^[A-Za-z0-9\-_]+$"))
                return candidate;

            return input;
        }
    }
}
