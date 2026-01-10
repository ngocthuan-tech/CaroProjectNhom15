using Firebase.Auth;
using Grpc.Core; // để bắt lỗi từ Firestore (RpcException)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Utils
{
    internal static class TryHelper
    {
        /// <summary>
        /// Dùng cho các hàm async KHÔNG trả về dữ liệu (Task)
        /// </summary>
        public static async Task TryAsync(Func<Task> func, string action)
        {
            try
            {
                await func();
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"[Firestore] Lỗi khi {action}: {ex.Status.Detail}");
                throw new Exception($"Lỗi Firestore khi {action}: {ex.Status.Detail}", ex);
            }
            catch (FirebaseAuthException ex)
            {
                string friendlyMessage;

                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        friendlyMessage = "Email này đã được đăng ký. Vui lòng dùng email khác.";
                        break;

                    case AuthErrorReason.WeakPassword:
                        friendlyMessage = "Mật khẩu quá yếu. Vui lòng chọn mật khẩu mạnh hơn (ít nhất 6 ký tự).";
                        break;

                    case AuthErrorReason.InvalidEmailAddress:
                        friendlyMessage = "Địa chỉ email không hợp lệ. Vui lòng nhập đúng định dạng email.";
                        break;

                    default:
                        friendlyMessage = $"Lỗi Firebase Auth khi {action}: {ex.Reason}";
                        break;
                }

                MessageBox.Show(friendlyMessage, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(friendlyMessage, ex);
            }


            catch (Exception ex) // lỗi chung
            {
                Console.WriteLine($"[Lỗi chung] Khi {action}: {ex.Message}");
                return; // ném lại lỗi để caller xử lý tiếp nếu cần
            }
        }


        /// <summary>
        /// Dùng cho các hàm async CÓ trả về dữ liệu (Task<T>)
        /// </summary>
        public static async Task<T> TryAsync<T>(Func<Task<T>> func, string action)
        {
            try
            {
                return await func();
            }

            catch (RpcException ex)
            {
                Console.WriteLine($"[Firestore] Lỗi khi {action}: {ex.Status.Detail}");
                throw new Exception($"Lỗi Firestore khi {action}: {ex.Status.Detail}", ex);
            }

            catch (FirebaseAuthException ex)
            {
                string friendlyMessage;

                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        friendlyMessage = "Email này đã được đăng ký. Vui lòng dùng email khác.";
                        break;

                    case AuthErrorReason.WeakPassword:
                        friendlyMessage = "Mật khẩu quá yếu. Vui lòng chọn mật khẩu mạnh hơn (ít nhất 6 ký tự).";
                        break;

                    case AuthErrorReason.InvalidEmailAddress:
                        friendlyMessage = "Địa chỉ email không hợp lệ. Vui lòng nhập đúng định dạng email.";
                        break;

                    default:
                        friendlyMessage = $"Lỗi Firebase Auth khi {action}: {ex.Reason}";
                        break;
                }

                MessageBox.Show(friendlyMessage, "Lỗi đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception(friendlyMessage, ex);
            }

            catch (Exception ex) // lỗi chung
            {
                Console.WriteLine($"[Lỗi chung] Khi {action}: {ex.Message}");
                return default; // ném lại lỗi để caller xử lý tiếp nếu cần
            }
        }
    }
}



