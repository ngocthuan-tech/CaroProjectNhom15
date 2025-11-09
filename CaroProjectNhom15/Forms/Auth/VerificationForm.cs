using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Http.Json; // <-- required for PostAsJsonAsync extension
using CaroProjectNhom15.Utils;

namespace CaroProjectNhom15.Forms.Auth
{
    public partial class VerificationForm : Form
    {
        public VerificationForm()
        {
            InitializeComponent();
            Btn_Verify.Click += Btn_Verify_Click;
        }

        // Made async to use await instead of blocking GetAwaiter().GetResult()
        private async void Btn_Verify_Click(object? sender, EventArgs e)
        {
            var input = Tb_OTPCode.Text.Trim();
            var oobCode = ExtractOobCode(input);
            if (string.IsNullOrEmpty(oobCode))
            {
                MessageBox.Show("Không tìm thấy mã (oobCode). Vui lòng paste toàn bộ link trong email hoặc chỉ mã oobCode.", "Thiếu mã", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var isReset = MessageBox.Show(
                "Bạn muốn đặt mật khẩu mới? Yes = đặt lại mật khẩu, No = chỉ xác thực email",
                "Chọn hành động",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

            if (isReset)
            {
                // Không gọi accounts:resetPassword ở đây (có thể tiêu thụ mã).
                // Chỉ mở form ResetPasswordForm và để form đó gọi accounts:update với oobCode + newPassword.
                using var rf = new ResetPasswordForm(oobCode);
                rf.ShowDialog(this);
                return;
            }

            // Nếu không reset, thực hiện verify email ngay (accounts:update với oobCode)
            try
            {
                using var client = new System.Net.Http.HttpClient();
                var payloadJson = System.Text.Json.JsonSerializer.Serialize(new { oobCode = oobCode });
                using var content = new System.Net.Http.StringContent(payloadJson, System.Text.Encoding.UTF8, "application/json");
                var url = $"https://identitytoolkit.googleapis.com/v1/accounts:update?key={FirebaseConfig.ApiKey}";
                var res = await client.PostAsync(url, content).ConfigureAwait(false);
                var txt = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!res.IsSuccessStatusCode)
                {
                    try
                    {
                        using var doc = System.Text.Json.JsonDocument.Parse(txt);
                        if (doc.RootElement.TryGetProperty("error", out var err) &&
                            err.TryGetProperty("message", out var msg))
                        {
                            // Switch back to UI thread to show MessageBox
                            if (InvokeRequired)
                                Invoke(new Action(() => MessageBox.Show(msg.GetString(), "Lỗi từ Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                            else
                                MessageBox.Show(msg.GetString(), "Lỗi từ Firebase", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    catch { }

                    if (InvokeRequired)
                        Invoke(new Action(() => MessageBox.Show(txt, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)));
                    else
                        MessageBox.Show(txt, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (InvokeRequired)
                    Invoke(new Action(() => MessageBox.Show("Xác thực email thành công.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)));
                else
                    MessageBox.Show("Xác thực email thành công.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng mã: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Extraction helper: same as before
        private static string ExtractOobCode(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
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

            return null;
        }
    }
}
