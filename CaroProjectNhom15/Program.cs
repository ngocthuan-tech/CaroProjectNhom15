using System;
using System.Windows.Forms;
using CaroProjectNhom15.Forms.Forms.Gameplay;

namespace CaroProjectNhom15
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // CÁC THIẾT LẬP CHUNG
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Xử lý lỗi (Giữ nguyên)
            Application.ThreadException += (s, e) => HandleUiException(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => HandleDomainException(e.ExceptionObject as Exception);

            try
            {
                // [PHẦN ĐÃ SỬA LỖI PARSING]
                string playerRole = "X"; // Mặc định là X

                string[] args = Environment.GetCommandLineArgs();

                // Kiểm tra nếu có đối số thứ hai (args[0] là tên file .exe)
                if (args.Length >= 2)
                {
                    string argument = args[1].ToUpper();
                    if (argument == "O")
                    {
                        playerRole = "O";
                    }
                    // Nếu là bất kỳ đối số nào khác, nó vẫn sẽ là X (giữ mặc định)
                }

                Console.WriteLine($"[Program] Đã gán vai trò: {playerRole}");
                // KHI CHẠY .EXE MÀ KHÔNG CÓ VS, HÃY KIỂM TRA CỬA SỔ CMD ĐỂ XEM DÒNG NÀY.

                // Khởi tạo Form và truyền vai trò (X hoặc O)
                Application.Run(new TicTacToe(playerRole));
            }
            catch (Exception ex)
            {
                // ... (Logic xử lý lỗi khởi động)
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
            // ...
        }

        private static void HandleDomainException(Exception? ex)
        {
            // ...
        }
    }
}