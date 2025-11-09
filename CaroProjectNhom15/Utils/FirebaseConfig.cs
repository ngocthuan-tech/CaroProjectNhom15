using System;

namespace CaroProjectNhom15.Utils
{
    internal static class FirebaseConfig
    {
        // Thử đọc từ Process, rồi User, rồi Machine
        private static readonly string? _apiKey =
            Environment.GetEnvironmentVariable("FIREBASE_API_KEY")
            ?? Environment.GetEnvironmentVariable("FIREBASE_API_KEY", EnvironmentVariableTarget.User)
            ?? Environment.GetEnvironmentVariable("FIREBASE_API_KEY", EnvironmentVariableTarget.Machine);

        // Trả về API key hoặc ném lỗi nếu không có — an toàn cho production
        public static string ApiKey
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_apiKey))
                    return _apiKey;

                // Nếu muốn dùng fallback (chỉ cho phát triển), bạn có thể uncomment dòng bên dưới.
                // WARNING: không để giá trị cứng trong Git cho production.
                // const string devFallback = "YOUR_DEV_API_KEY_HERE";
                // return devFallback;

                throw new InvalidOperationException(
                    "Environment variable 'FIREBASE_API_KEY' chưa được thiết lập. " +
                    "Thiết lập biến môi trường hoặc thêm vào __Project Properties__ > __Debug__ > __Environment variables__ hoặc vào Properties\\launchSettings.json."
                );
            }
        }

        public const string DbURL = "https://project-nhom15-nt106-default-rtdb.asia-southeast1.firebasedatabase.app";
        public const string AuthDomain = "project-nhom15-nt106.firebaseapp.com";
    }
}



