using System;

namespace CaroProjectNhom15.Utils
{
    internal static class FirebaseConfig
    {
        // Thay vì đọc biến môi trường rắc rối, bạn return luôn chuỗi Key của bạn ở đây
        // Bạn lấy Key này trong Project Settings trên web Firebase hoặc trong launchSettings.json cũ của bạn
        public static string ApiKey => "AIzaSyC8w2KZ_z4JEJ-PnSJe4k4ML9UA6_FhVmc";

        public const string DbURL = "https://carodb-c657f-default-rtdb.asia-southeast1.firebasedatabase.app";
        public const string AuthDomain = "carodb-c657f.firebaseapp.com";
    }
}



