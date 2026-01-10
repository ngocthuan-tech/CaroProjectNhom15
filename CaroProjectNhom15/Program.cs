using System;
using System.Windows.Forms;
using CaroProjectNhom15.Forms.Auth;

namespace CaroProjectNhom15
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // UI initialization (hiện đại, .NET 8)
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Global exception handlers (hiển thị thông báo và log ra console)
            Application.ThreadException += (s, e) => HandleUiException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => HandleDomainException(e.ExceptionObject as Exception);

            try
            {
                // Kiểm tra biến môi trường FIREBASE_API_KEY — cảnh báo trong DEBUG để bạn dễ cấu hình
/*
#if DEBUG
                var apiKey = Environment.GetEnvironmentVariable("FIREBASE_API_KEY");
                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    MessageBox.Show(
                        "Biến môi trường 'FIREBASE_API_KEY' chưa thiết lập.\n" +
                        "Bạn có thể thêm trong __Project Properties__ → __Debug__ → __Environment variables__\n" +
                        "hoặc thiết lập biến hệ thống (setx / PowerShell).",
                        "Cấu hình Firebase",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                }
#endif

                // Khởi chạy form login (không thay đổi Designer)
 */               
                Application.Run(new LoginForm());
            }
            catch (Exception ex)
            {
                // Log + hiển thị lỗi khởi động
                Console.WriteLine($"[App Error] Lỗi khởi tạo: {ex}");
                try
                {
                    MessageBox.Show(
                        $"Lỗi khởi động ứng dụng:\n{ex.Message}\n\nXem console để biết chi tiết.",
                        "Lỗi khởi động",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                catch { }

                Environment.Exit(1);
            }
        }

        private static void HandleUiException(Exception ex)
        {
            Console.WriteLine($"[UI Exception] {ex}");
            try
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }

        private static void HandleDomainException(Exception? ex)
        {
            Console.WriteLine($"[Unhandled Exception] {ex}");
            try
            {
                MessageBox.Show($"Lỗi không xác định: {ex?.Message}", "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
    }
}

