using Auth.Models;
using CaroProjectNhom15.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Threading.Tasks;

namespace AuthTest01.Services
{
    public class UserService
    {
        private FirebaseClient Database => FirebaseProvider.Instance.Database;
        private const string _collectionName = "users";

        // Create
        public async Task CreateUserAsync(UserModel user)
        {
            await TryHelper.TryAsync(async () =>
            {
                if (Database == null)
                    throw new Exception("Database chưa được khởi tạo.");

                await Database
                    .Child(_collectionName)
                    .Child(user.Uid)
                    .PutAsync(user);

                Console.WriteLine($"[UserService] Tạo user {user.Uid} thành công.");
            }, "tạo user mới");
        }

        // Read
        public async Task<UserModel> GetUserAsync(string uid)
        {
            return await TryHelper.TryAsync(async () =>
            {
                if (Database == null)
                    throw new Exception("Database chưa được khởi tạo.");

                var user = await Database
                    .Child(_collectionName)
                    .Child(uid)
                    .OnceSingleAsync<UserModel>();

                Console.WriteLine($"[UserService] Lấy user {uid}: {(user != null ? "OK" : "null")}");
                return user;
            }, "lấy dữ liệu user");
        }

        // Update
        public async Task UpdateUserAsync(string uid, UserModel updatedUser)
        {
            await TryHelper.TryAsync(async () =>
            {
                if (Database == null)
                    throw new Exception("Database chưa được khởi tạo.");

                await Database
                    .Child(_collectionName)
                    .Child(uid)
                    .PutAsync(updatedUser);

                Console.WriteLine($"[UserService] Cập nhật user {uid} thành công.");
            }, "cập nhật user");
        }

        // Delete
        public async Task DeleteUserAsync(string uid)
        {
            await TryHelper.TryAsync(async () =>
            {
                if (Database == null)
                    throw new Exception("Database chưa được khởi tạo.");

                Console.WriteLine($"[UserService] Đang xóa user {uid}...");

                try
                {
                    // Kiểm tra kết nối nhẹ
                    await Database.Child("ping_test").OnceAsync<object>();
                }
                catch
                {
                    throw new Exception("Không thể kết nối tới Firebase Database. Token có thể đã hết hạn.");
                }

                await Database
                    .Child(_collectionName)
                    .Child(uid)
                    .DeleteAsync();

                Console.WriteLine($"[UserService] Đã xóa user {uid} thành công.");
            }, "xóa user");
        }
    }
}



