using Auth.Models;
using CaroProjectNhom15.Models; 
using CaroProjectNhom15.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Database.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroProjectNhom15.Services
{
    public class RoomService
    {
        private FirebaseClient Database => FirebaseProvider.Instance.Database;

        // Biến lưu subscription để quản lý
        private IDisposable roomListener, roomsListener;

        // ================= GET DATA (SNAPSHOT) =================

        public async Task<RoomModel> GetRoomByIdAsync(string roomId)
        {
            return await TryHelper.TryAsync<RoomModel>(async () =>
            {
                var newRoom = await Database.Child("rooms").Child(roomId).
                OnceSingleAsync<RoomModel>().ConfigureAwait(false);

                return newRoom;
            }, "tìm phòng qua id");
        }

        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            return await TryHelper.TryAsync<List<RoomModel>>(async () =>
            {
                var roomList = await Database.Child("rooms").
                OnceAsync<RoomModel>().ConfigureAwait(false);

                return roomList.Select(r =>
                {
                    var room = r.Object;
                    room.ID = r.Key; // Quan trọng: Gán ID từ Key
                    return room;
                }).ToList();
            }, "lấy toàn bộ phòng");
        }

        // ================= ACTIONS (CREATE / JOIN / LEAVE) =================

        //hỗ trợ random room id
        private readonly Random _random = new Random();

        public async Task<RoomModel> CreateRoomAsync(UserModel currentUser)
        {
            return await TryHelper.TryAsync<RoomModel>(async () =>
            {
                string roomId = "";
                bool isUnique = false;

                // VÒNG LẶP ĐẢM BẢO DUY NHẤT
                // Random liên tục đến khi nào tìm được ID chưa ai dùng
                while (!isUnique)
                {
                    roomId = GenerateRandomId(6); // Tạo ID 6 số (VD: 839210)

                    // Kiểm tra xem ID này có trên Firebase chưa
                    var existingRoom = await Database.Child("rooms").Child(roomId)
                                             .OnceSingleAsync<object>();

                    if (existingRoom == null)
                    {
                        isUnique = true; // Chưa có -> Dùng được
                    }
                }

                // Tạo object phòng với ID ngắn gọn vừa tìm được
                var newRoom = new RoomModel()
                {
                    ID = roomId, // Gán ID số vào đây
                    Name = "Phòng của " + currentUser.UserName,
                    Host = currentUser,
                    Guest = null,
                    Status = "Waiting",
                };

                // QUAN TRỌNG: Dùng PutAsync (Ghi đè theo Key chỉ định) thay vì PostAsync (Tự sinh Key)
                await Database.Child("rooms").Child(roomId)
                              .PutAsync(newRoom).ConfigureAwait(false);

                return newRoom;
            }, "tạo phòng");
        }

        // Hàm hỗ trợ random số
        private string GenerateRandomId(int length)
        {
            const string chars = "0123456789";
            char[] stringChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                stringChars[i] = chars[_random.Next(chars.Length)];
            }
            return new string(stringChars);
        }

        public async Task<RoomModel> JoinRoomAsync(RoomModel room, UserModel currentUser)
        {
            return await TryHelper.TryAsync(async () =>
            {
                if (room == null) throw new Exception("Phòng không tồn tại");

                // TRƯỜNG HỢP 1: Phòng bị lỗi mất Host (hoặc Host cũ đã thoát) -> Chiếm quyền làm Host
                if (room.Host == null)
                {
                    room.Host = currentUser;
                    room.Status = "Waiting";
                    room.Guest = null; // Đảm bảo guest trống

                    // Cập nhật tên phòng theo Host mới luôn cho đẹp
                    room.Name = "Phòng của " + currentUser.UserName;

                    // Update Firebase
                    await Database.Child("rooms").Child(room.ID)
                        .PatchAsync(new
                        {
                            Host = currentUser,
                            Guest = (UserModel)null, // Xóa Guest cũ nếu có
                            Status = "Waiting",
                            Name = room.Name
                        });

                    return room;
                }

                // TRƯỜNG HỢP 2: Đã có Host, mình vào làm Guest
                if (room.Guest == null)
                {
                    // Kiểm tra: Không cho phép tự mình vào phòng mình tạo (tránh lỗi logic)
                    if (room.Host.Uid == currentUser.Uid)
                        throw new Exception("Bạn đang ở trong phòng này rồi!");

                    room.Guest = currentUser;
                    room.Status = "Ready";

                    // Update Firebase
                    await Database.Child("rooms").Child(room.ID).
                    PatchAsync(new
                    {
                        Guest = currentUser,
                        Status = "Ready"
                    });

                    return room;
                }

                throw new Exception("Phòng này đã đủ 2 người chơi");
            }, "join phòng");
        }

        public async Task KickGuestAsync(string roomId)
        {
            await TryHelper.TryAsync(async () =>
            {
                await Database.Child("rooms").Child(roomId)
                .PatchAsync(new
                {
                    Guest = (UserModel)null,
                    Status = "Waiting"
                }).ConfigureAwait(false);
            }, "đuổi người chơi");
        }

        // Trong Services/RoomService.cs

        public async Task ExitRoomAsync(RoomModel room, UserModel currentUser)
        {
            await TryHelper.TryAsync(async () =>
            {
                // Lấy dữ liệu mới nhất để đảm bảo không xử lý sai nếu mạng lag
                var currentData = await GetRoomByIdAsync(room.ID);
                if (currentData == null) return; // Phòng đã bị xóa trước đó

                // TRƯỜNG HỢP 1: BẠN LÀ GUEST
                if (currentData.Guest != null && currentData.Guest.Uid == currentUser.Uid)
                {
                    // Guest thoát -> Chỉ cần set Guest về null
                    await Database.Child("rooms").Child(room.ID).PatchAsync(new
                    {
                        Guest = (UserModel)null,
                        Status = "Waiting"
                    });
                    return;
                }

                // TRƯỜNG HỢP 2: BẠN LÀ HOST
                if (currentData.Host.Uid == currentUser.Uid)
                {
                    // 2a. Nếu còn Guest trong phòng -> Thăng chức Guest lên làm Host
                    if (currentData.Guest != null)
                    {
                        var newHost = currentData.Guest;

                        // Cập nhật Firebase: Guest thành Host, xóa vị trí Guest cũ
                        await Database.Child("rooms").Child(room.ID).PatchAsync(new
                        {
                            Host = newHost,
                            Guest = (UserModel)null,
                            Name = "Phòng của " + newHost.UserName, // Đổi tên phòng theo chủ mới
                            Status = "Waiting"
                        });
                    }
                    // 2b. Nếu chỉ có một mình Host -> Xóa phòng luôn
                    else
                    {
                        await Database.Child("rooms").Child(room.ID).DeleteAsync();
                    }
                }
            }, "thoát phòng");
        }

        public async Task StartGameAsync(RoomModel myRoom)
        {
            await TryHelper.TryAsync(async () =>
            {
                await Database.Child("rooms").Child(myRoom.ID).
                PatchAsync(new
                {
                    Status = "Playing"
                }).ConfigureAwait(false);
            }, "bắt đầu game");
        }

        // ================= LISTENERS (REALTIME) =================

        // Lắng nghe 1 phòng cụ thể (Dùng trong Waiting Room)
        // Trong Services/RoomService.cs

        public void ListenToRoom(string roomID, Action<RoomModel> onRoomChanged)
        {
            // Hủy đăng ký cũ
            StopListenRoom();

            try
            {
                // Thay vì AsObservable<RoomModel>, ta dùng AsObservable<object> 
                // để bắt mọi thay đổi (kể cả thay đổi nhỏ nhất)
                roomListener = Database
                    .Child("rooms")
                    .Child(roomID)
                    .AsObservable<object>()
                    .Subscribe(d =>
                    {
                        // MỖI KHI CÓ THAY ĐỔI: Gọi hàm lấy lại toàn bộ thông tin phòng
                        try
                        {
                            // Gọi GetRoomByIdAsync để lấy dữ liệu tươi mới nhất
                            GetRoomByIdAsync(roomID).ContinueWith(task =>
                            {
                                if (task.IsFaulted)
                                {
                                    Console.WriteLine("Lỗi tải lại phòng: " + task.Exception?.Message);
                                }
                                else if (task.IsCompleted && task.Result != null)
                                {
                                    // Lấy được dữ liệu mới -> Trả về cho UI cập nhật
                                    var freshRoom = task.Result;
                                    freshRoom.ID = roomID; // Đảm bảo ID luôn đúng

                                    onRoomChanged(freshRoom);
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Lỗi trong Subscribe: " + ex.Message);
                        }
                    },
                    error =>
                    {
                        Console.WriteLine("Stream error (Room): " + error.Message);
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ListenToRoom: " + ex.Message);
            }
        }

        public void StopListenRoom()
        {
            roomListener?.Dispose();
            roomListener = null;
        }

        // Lắng nghe danh sách phòng (Dùng trong Lobby)
        // Lưu ý: Cách dùng GetAllRoomsAsync trong này hơi tốn tài nguyên nhưng code đơn giản, chấp nhận được.
        // Trong Services/RoomService.cs

        public void ListenToRoomsList(Action<List<RoomModel>> onRoomsChanged)
        {
            // Hủy cái cũ nếu có
            if (roomsListener != null)
            {
                roomsListener.Dispose();
                roomsListener = null;
            }

            try
            {
                roomsListener = Database
                    .Child("rooms")
                    .AsObservable<RoomModel>()
                    .Subscribe(d =>
                    {
                        // SỬA: Đưa logic gọi lại GetAllRoomsAsync vào khối try-catch để không crash app
                        try
                        {
                            // Gọi GetAllRoomsAsync nhưng không await để tránh block UI, dùng ContinueWith để xử lý kết quả
                            GetAllRoomsAsync().ContinueWith(task =>
                            {
                                if (task.IsFaulted)
                                {
                                    // Log lỗi nhẹ nhàng vào Console thay vì crash
                                    Console.WriteLine("Lỗi tải danh sách phòng: " + task.Exception?.InnerException?.Message);
                                }
                                else if (task.IsCompleted)
                                {
                                    // Trả về UI
                                    onRoomsChanged(task.Result);
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Lỗi xử lý luồng rooms: " + ex.Message);
                        }
                    },
                    error =>
                    {
                        // SỬA: Xử lý lỗi kết nối của Stream (ví dụ: mất mạng, sai quyền)
                        // Không MessageBox ở đây để tránh spam popup liên tục
                        Console.WriteLine("Stream error: " + error.Message);
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể khởi tạo trình lắng nghe phòng: " + ex.Message);
            }
        }

        // ================= CHAT SYSTEM =================

        // SỬA QUAN TRỌNG: Đổi void -> Task
        public async Task SendMessageAsync(RoomModel currentRoom, UserModel sender, string content)
        {
            await TryHelper.TryAsync(async () =>
            {
                // Dùng MessageModel nếu bạn đã tạo class này, hoặc Anonymous Object như dưới cũng được
                var messageData = new
                {
                    SenderName = sender.UserName,
                    Content = content,
                    Time = DateTime.Now.ToString("HH:mm")
                };

                await Database
                    .Child("rooms")
                    .Child(currentRoom.ID)
                    .Child("Messages")
                    .PostAsync(messageData)
                    .ConfigureAwait(false);

            }, "gửi tin nhắn");
        }

        // SỬA QUAN TRỌNG: Bỏ Try-Catch return null để tránh Crash ở Form
        public IObservable<FirebaseEvent<MessageModel>> ListenToMessages(string roomId)
        {
            // Không cần try-catch ở đây, nếu lỗi kết nối thì .Subscribe bên Form sẽ hứng lỗi
            return Database
                .Child("rooms")
                .Child(roomId)
                .Child("Messages")
                .AsObservable<MessageModel>();
        }
    }
}