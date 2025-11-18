using System;

namespace CaroProjectNhom15.Utils
{
    internal static class FirebaseConfig
    {
        private static readonly string? _apiKey =
            Environment.GetEnvironmentVariable("FIREBASE_API_KEY")
            ?? Environment.GetEnvironmentVariable("FIREBASE_API_KEY", EnvironmentVariableTarget.User)
            ?? Environment.GetEnvironmentVariable("FIREBASE_API_KEY", EnvironmentVariableTarget.Machine);

        public static string ApiKey
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_apiKey))
                    return _apiKey;


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



