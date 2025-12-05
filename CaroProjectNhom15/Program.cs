using System;
using System.Windows.Forms;
using Auth.Models;
using CaroProjectNhom15.Forms;
using CaroProjectNhom15.Utils; 

namespace CaroProjectNhom15
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += (s, e) => ShowError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                ShowError(e.ExceptionObject as Exception);

            try
            {
                // ==============================================================
                // BƯỚC 1: BẬT CẦU DAO DATABASE (QUAN TRỌNG NHẤT)
                // ==============================================================
                // Nếu không có dòng này, biến 'Database' trong Service sẽ bị NULL -> Lỗi Crash
                // Truyền null vì đang test chế độ không cần đăng nhập
                FirebaseProvider.Instance.InitDatabase(null);

                // ==============================================================
                // BƯỚC 2: TẠO USER GIẢ ĐỂ TEST
                // ==============================================================
                var mockUser = new UserModel
                {
                    // Random ID để mỗi lần chạy là 1 người khác nhau, đỡ trùng
                    Uid = "test_user_" + new Random().Next(1000, 9999),
                    UserName = "TesterPro",
                    Email = "test@gmail.com",
                    FullName = "Người Kiểm Thử"
                };

                // ==============================================================
                // BƯỚC 3: MỞ LOBBY
                // ==============================================================
                Application.Run(new LobbyForm(mockUser));
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private static void ShowError(Exception ex)
        {
            if (ex == null) return;
            MessageBox.Show(ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}