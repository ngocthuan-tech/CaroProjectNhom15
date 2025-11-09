using CaroProjectNhom15.Utils;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthTest01.Services
{
    public class AuthService
    {
        private readonly FirebaseAuthClient _authClient = FirebaseProvider.Instance.AuthClient;

        // DTO for REST sign-in response
        public record RestSignInResponse(string IdToken, string RefreshToken, string LocalId, string Email, string ExpiresIn);

        // Đăng nhập (SDK)
        public async Task<UserCredential> LoginAsync(string email, string password)
        {
            return await TryHelper.TryAsync(async () =>
            {
                return await _authClient.SignInWithEmailAndPasswordAsync(email, password);
            }, "đăng nhập");
        }

        // Đăng ký (SDK)
        public async Task<UserCredential> RegisterAsync(string email, string password)
        {
            return await TryHelper.TryAsync(async () =>
            {
                return await _authClient.CreateUserWithEmailAndPasswordAsync(email, password);
            }, "đăng ký");
        }

        // Sign-in bằng REST (dùng khi SDK không trả refresh token)
        public async Task<RestSignInResponse> SignInWithPasswordRestAsync(string email, string password)
        {
            using var client = new HttpClient();
            var payload = new
            {
                email = email,
                password = password,
                returnSecureToken = true
            };

            var url = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={FirebaseConfig.ApiKey}";
            var response = await client.PostAsJsonAsync(url, payload);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                // Try extract friendly message from Firebase error
                try
                {
                    using var doc = JsonDocument.Parse(json);
                    if (doc.RootElement.TryGetProperty("error", out var err) &&
                        err.TryGetProperty("message", out var msg))
                    {
                        throw new Exception(msg.GetString());
                    }
                }
                catch { }

                throw new Exception($"SignInWithPassword (REST) failed: {json}");
            }

            var result = JsonSerializer.Deserialize<RestSignInResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (result == null)
                throw new Exception("Không parse được phản hồi từ Firebase signInWithPassword.");

            return result;
        }

        //làm mới token bằng refreshtoken
        public async Task<(string idToken, string userId)> LoginWithRefreshTokenAsync(string refreshToken)
        {
            using var client = new HttpClient();

            var content = new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["refresh_token"] = refreshToken
            };

            var response = await client.PostAsync(
                $"https://securetoken.googleapis.com/v1/token?key={FirebaseConfig.ApiKey}",
                new FormUrlEncodedContent(content)
            );

            if (!response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new System.Exception($"Làm mới token thất bại: {msg}");
            }

            var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            string idToken = result["id_token"];
            string userId = result["user_id"];

            FirebaseProvider.Instance.InitDatabase(idToken);

            return (idToken, userId);
        }

        // Gửi email xác thực
        public async Task SendEmailVerificationAsync(string idToken)
        {
            using var client = new HttpClient();
            var request = new
            {
                requestType = "VERIFY_EMAIL",
                idToken = idToken
            };
            var response = await client.PostAsJsonAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={FirebaseConfig.ApiKey}",
                request
            );

            if (!response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gửi email xác thực thất bại: {msg}");
            }
        }

        // Gửi email reset mật khẩu
        public async Task<bool> TrySendPasswordResetEmailAsync(string email)
        {
            using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

            var request = new
            {
                requestType = "PASSWORD_RESET",
                email = email
            };

            var response = await client.PostAsJsonAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:sendOobCode?key={FirebaseConfig.ApiKey}",
                request
            );

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[TrySendPasswordResetEmailAsync] Response: {result}");

            if (response.IsSuccessStatusCode)
                return true;

            // Parse Firebase error
            try
            {
                using var doc = JsonDocument.Parse(result);
                if (doc.RootElement.TryGetProperty("error", out var errRoot))
                {
                    string message = errRoot.GetProperty("message").GetString();

                    switch (message)
                    {
                        case "EMAIL_NOT_FOUND":
                        case "USER_NOT_FOUND":
                            throw new Exception("Email này chưa được đăng ký tài khoản nào!");
                        case "INVALID_EMAIL":
                            throw new Exception("Địa chỉ email không hợp lệ. Vui lòng nhập đúng định dạng!");
                        default:
                            throw new Exception($"Firebase trả về lỗi không xác định: {message}");
                    }
                }

                throw new Exception("Phản hồi lỗi không hợp lệ từ Firebase.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể gửi email đặt lại mật khẩu. Chi tiết: {ex.Message}");
            }
        }

        // Đăng xuất
        public async Task LogoutAsync()
        {
            await TryHelper.TryAsync(async () =>
            {
                try
                {
                    _authClient?.SignOut();
                }
                catch { }

                FirebaseProvider.Instance.ClearDatabase();

                // Safely clear stored refresh token and persist settings.
                try
                {
                    global::CaroProjectNhom15.Properties.Settings.Default.RefreshToken = string.Empty;
                    global::CaroProjectNhom15.Properties.Settings.Default.Save();
                }
                catch (Exception)
                {
                    // Ignore settings save failures to avoid crashing logout flow.
                }

                await Task.Delay(50);
                GC.Collect();
            }, "đăng xuất");
        }

        public async Task ChangePasswordAsync(string idToken, string newPassword)
        {
            using var client = new HttpClient();
            var payload = new
            {
                idToken = idToken,
                password = newPassword,
                returnSecureToken = true
            };

            var response = await client.PostAsJsonAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:update?key={FirebaseConfig.ApiKey}",
                payload
            );

            if (!response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Đổi mật khẩu thất bại: {msg}");
            }
        }

        public async Task DeleteAccountAsync(string idToken)
        {
            using var client = new HttpClient();
            var request = new { idToken = idToken };

            var response = await client.PostAsJsonAsync(
                $"https://identitytoolkit.googleapis.com/v1/accounts:delete?key={FirebaseConfig.ApiKey}",
                request
            );

            if (!response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new Exception($"Xóa tài khoản thất bại: {msg}");
            }
        }
    }
}



