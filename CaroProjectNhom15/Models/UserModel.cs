using Firebase.Database; // dùng Realtime Database
namespace Auth.Models
{
    public class UserModel
    {
        public string Uid { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public UserModel() { }

        public UserModel(string uid, string email, string userName, string fullName)
        {
            Uid = uid;
            Email = email;
            UserName = userName;
            FullName = fullName;
        }
    }
}



