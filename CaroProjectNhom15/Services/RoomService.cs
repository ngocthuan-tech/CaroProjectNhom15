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

        public async Task<RoomModel> CreateRoomAsync(UserModel currentUser)
        {
            return await TryHelper.TryAsync<RoomModel>(async () =>
            {
                var newRoom = new RoomModel()
                {
                    Name = "Phòng của " + currentUser.UserName,
                    Host = currentUser,
                    Guest = null,
                    Status = "Waiting",
                };

                // 1. Post lên để lấy Key
                var result = await Database.Child("rooms").
                PostAsync(newRoom).ConfigureAwait(false);

                string roomID = result.Key;
                newRoom.ID = roomID;

                // 2. Update lại ID vào trong object trên Firebase
                await Database.Child("rooms").Child(roomID).Child("ID").
                PutAsync(roomID).ConfigureAwait(false);

                return newRoom;
            }, "tạo phòng");
        }

        public async Task<RoomModel> JoinRoomAsync(RoomModel room, UserModel currentUser)
        {
            return await TryHelper.TryAsync(async () =>
            {
                if (room == null) throw new Exception("Phòng không tồn tại");
                if (room.Guest != null) throw new Exception("Phòng này đã đủ 2 người chơi");

                // Update Local object
                room.Guest = currentUser;
                room.Status = "Ready";

                // Update Firebase
                await Database.Child("rooms").Child(room.ID).
                PatchAsync(new
                {
                    Guest = currentUser,
                    Status = "Ready"
                }).ConfigureAwait(false);

                return room;
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

        public async Task LeaveRoomAsync(RoomModel currentRoom)
        {
            await TryHelper.TryAsync(async () =>
            {
                // Update Local (cho chắc)
                currentRoom.Guest = null;
                currentRoom.Status = "Waiting";

                // Update Firebase
                await Database.Child("rooms").Child(currentRoom.ID).
                PatchAsync(new
                {
                    Guest = (UserModel)null,
                    Status = "Waiting"
                }).ConfigureAwait(false);
            }, "rời phòng");
        }

        public async Task DeleteRoomAsync(RoomModel myRoom)
        {
            await TryHelper.TryAsync(async () =>
            {
                await Database.Child("rooms").Child(myRoom.ID).
                DeleteAsync().ConfigureAwait(false);
            }, "xóa phòng");
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
        public void ListenToRoom(string roomID, Action<RoomModel> onRoomChanged)
        {
            // Hủy đăng ký cũ nếu có để tránh nghe 2 lần
            StopListenRoom();

            try
            {
                roomListener = Database
                    .Child("rooms")
                    .Child(roomID)
                    .AsObservable<RoomModel>()
                    .Subscribe(d =>
                    {
                        if (d.Object != null)
                        {
                            // Gán lại Key cho chắc ăn
                            d.Object.ID = d.Key;
                            onRoomChanged(d.Object);
                        }
                    },
                    error =>
                    {
                        // Xử lý lỗi khi mất kết nối
                        MessageBox.Show("Mất kết nối phòng: " + error.Message);
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
        public void ListenToRoomsList(Action<List<RoomModel>> onRoomsChanged)
        {
            // Hủy cái cũ nếu có
            if (roomsListener != null) roomsListener.Dispose();

            try
            {
                roomsListener = Database
                .Child("rooms")
                .AsObservable<RoomModel>()
                .Subscribe(_ =>
                {
                    // Mỗi lần có thay đổi bất kỳ, tải lại toàn bộ danh sách
                    // Dùng ContinueWith để không chặn luồng
                    GetAllRoomsAsync().ContinueWith(task =>
                    {
                        if (task.Exception == null)
                            onRoomsChanged(task.Result);
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ListenToRoomsList: " + ex.Message);
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