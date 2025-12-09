using System;

namespace CaroProjectNhom15.Utils
{
    internal static class FirebaseConfig
    {
        // Thay vì đọc biến môi trường rắc rối, bạn return luôn chuỗi Key của bạn ở đây
        // Bạn lấy Key này trong Project Settings trên web Firebase hoặc trong launchSettings.json cũ của bạn
        public static string ApiKey => "AIzaSyBfW24uSxwCyHeWUOeaqL51XjqbNTJo-IA";

        public const string DbURL = "https://project-nhom15-nt106-default-rtdb.asia-southeast1.firebasedatabase.app";
        public const string AuthDomain = "project-nhom15-nt106.firebaseapp.com";
    }
}



