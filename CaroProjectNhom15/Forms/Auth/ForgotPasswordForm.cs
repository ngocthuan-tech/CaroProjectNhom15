using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using AuthTest01.Services;

namespace CaroProjectNhom15.Forms.Auth
{
    public partial class ForgotPasswordForm : Form
    {
        private readonly AuthService _authService = new();

        public ForgotPasswordForm()
        {
            InitializeComponent();

            Btn_Continue.Click += Btn_Continue_Click;
            LnkL_BackToLogIn.LinkClicked += LnkL_BackToLogIn_LinkClicked;
            LnkL_Register.LinkClicked += LnkL_Register_LinkClicked;
        }

        private async void Btn_Continue_Click(object? sender, EventArgs e)
        {
            var email = Tb_Email.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Btn_Continue.Enabled = false;
            try
            {
                bool ok = await _authService.TrySendPasswordResetEmailAsync(email);
                if (ok)
                {
                    MessageBox.Show("Email đặt lại mật khẩu đã được gửi. Vui lòng kiểm tra hộp thư.", "Đã gửi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở VerificationForm để người dùng paste oobCode và reset mật khẩu tại chỗ nếu muốn
                    var ask = MessageBox.Show("Bạn có muốn nhập mã (oobCode) và mật khẩu mới ngay bây giờ không?", "Reset mật khẩu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (ask == DialogResult.Yes)
                    {
                        using var vf = new VerificationForm();
                        vf.ShowDialog(this);
                        // Nếu reset thành công, VerificationForm sẽ đóng lại
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể gửi email đặt lại. Kiểm tra lại địa chỉ email.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi email đặt lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Btn_Continue.Enabled = true;
            }
        }

        private void LnkL_BackToLogIn_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            Close();
        }

        private void LnkL_Register_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            using var rf = new RegisterForm();
            rf.ShowDialog(this);
        }
    }
}
