using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using System;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Utils
{
    public sealed class FirebaseProvider
    {
        private static FirebaseProvider _instance;
        public static FirebaseProvider Instance => _instance ??= new FirebaseProvider();

        public FirebaseAuthClient AuthClient { get; }
        public FirebaseClient Database { get; private set; }

        private FirebaseProvider()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = FirebaseConfig.ApiKey,
                AuthDomain = FirebaseConfig.AuthDomain, // thêm domain, Firebase yêu cầu
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider() // thêm provider đăng nhập bằng email/password
                }
            };

            // Dùng FirebaseAuthClient thay vì FirebaseAuthProvider
            AuthClient = new FirebaseAuthClient(config);
        }

        public void InitDatabase(string idToken)
        {
            if (string.IsNullOrEmpty(idToken))
                throw new ArgumentNullException(nameof(idToken));

            Database = new FirebaseClient(
                FirebaseConfig.DbURL,
                new FirebaseOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(idToken)
                });
        }

        public void ClearDatabase() => Database = null;
    }
}



