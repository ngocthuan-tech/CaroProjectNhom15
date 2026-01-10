using Firebase.Database; // dùng Realtime Database
namespace Auth.Models
{
    public class UserModel
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        // New: store avatar as URL or data URI (data:image/png;base64,...)
        public string AvatarUrl { get; set; }



        public UserModel() { }

        public UserModel(string uid, string email, string userName, string fullName, string avatarUrl = null)
        {
            Uid = uid;
            Email = email;
            UserName = userName;
            FullName = fullName;
            AvatarUrl = avatarUrl;
        }
    }
}



