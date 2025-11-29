using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Utils;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroProjectNhom15.Services
{
    public class RoomService
    {
        private FirebaseClient Database => FirebaseProvider.Instance.Database;
        private IDisposable roomListener;
        private IDisposable roomsListener;

        public async Task<RoomModel> GetRoomByIdAsync(string roomId)
        {
            return await TryHelper.TryAsync<RoomModel>(async Task<RoomModel> () =>
            {
                var newRoom = await Database.Child("rooms").Child(roomId).
                OnceSingleAsync<RoomModel>().ConfigureAwait(false);

                return newRoom;

            }, "tìm phòng qua id");

        }

        public async Task<List<RoomModel>> GetAllRoomsAsync()
        {
            return await TryHelper.TryAsync<List<RoomModel>>(async Task<List<RoomModel>> () =>
            {
                var roomList = await Database.Child("rooms").
                OnceAsync<RoomModel>().ConfigureAwait(false);

                return roomList.Select(r =>
                {
                    var room = r.Object;
                    room.ID = r.Key;
                    return room;
                }).ToList();

            }, "lấy toàn bộ phòng");

        }

        public async Task<RoomModel> CreateRoomAsync(UserModel currentUser)
        {
            return await TryHelper.TryAsync<RoomModel>(async Task<RoomModel> () =>
            {
                var newRoom = new RoomModel()
                {                   
                    Name = "Phòng của " + currentUser.UserName,
                    Host = currentUser,
                    Guest = null,
                    Status = "Waiting",
                };

                
                var result = await Database.Child("rooms").
                PostAsync(newRoom).ConfigureAwait(false);

                string roomID = result.Key;
                newRoom.ID = roomID;

                await Database.Child("rooms").Child(roomID).Child("ID").
                PutAsync(roomID).ConfigureAwait(false);

                return newRoom;

            }, "tạo phòng");
        }

        //vào phòng qua id hoặc qua chọn danh sách phòng
        public async Task<RoomModel> JoinRoomAsync(RoomModel room, UserModel currentUser)
        {
            return await TryHelper.TryAsync(async Task<RoomModel> () =>
            {
                if (room == null)
                {
                    throw new Exception("Phòng không tồn tại");
                }

                if (room.Guest != null)
                {
                    throw new Exception("Phòng này đã đủ 2 người chơi");
                }

                room.Guest = currentUser;
                room.Status = "Ready";

                await Database.Child("rooms").Child(room.ID).
                PatchAsync(new 
                {
                    Guest = currentUser,
                    Status = "Ready"
                }).ConfigureAwait(false);

                return room;

            }, "join phòng");
        }

        public async Task LeaveRoomAsync(RoomModel currentRoom)
        {
            await TryHelper.TryAsync(async () =>
            {
                currentRoom.Guest = null;
                currentRoom.Status = "Waiting";

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
                })
                .ConfigureAwait(false);

            }, "bắt đầu game");
        }
                
        public void ListenToRoom(string roomID, Action<RoomModel> onRoomChanged)
        {
            roomListener = Database
                .Child("rooms")
                .Child(roomID)
                .AsObservable<RoomModel>()
                .Subscribe(d =>
                {
                    if (d.Object != null)
                    {
                        onRoomChanged(d.Object);
                    }
                });
        }

        public void StopListenRoom()
        {
            roomListener?.Dispose();
        }

        public void ListenToRoomsList(Action<List<RoomModel>> onRoomsChanged)
        {
            roomsListener = Database
                .Child("rooms")
                .AsObservable<RoomModel>()
                .Subscribe(_ =>
                {
                    // mỗi lần rooms thay đổi thì lấy lại danh sách
                    GetAllRoomsAsync().ContinueWith(task =>
                    {
                        if (task.Exception == null)
                            onRoomsChanged(task.Result);
                    });
                });
        }

        public async void SendMessageAsync(RoomModel currentRoom, UserModel sender, string content)
        {
            await TryHelper.TryAsync(async () =>
            {
                // 1. Đóng gói nội dung tin nhắn
                var messageData = new
                {
                    SenderName = sender.UserName, // Lưu tên người gửi để hiện lên
                    Content = content,            // Nội dung chat
                    Time = DateTime.Now.ToString("HH:mm") // Lưu giờ để biết chat lúc nào
                };

                // 2. Dán lên bảng tin "Messages" trong phòng
                // Dùng PostAsync để nó tự sinh ID cho tin nhắn này (mess1, mess2...)
                await Database
                    .Child("rooms")
                    .Child(currentRoom.ID)
                    .Child("Messages") // Tạo thêm một mục Messages trong phòng
                    .PostAsync(messageData)
                    .ConfigureAwait(false);

            }, "gửi tin nhắn");
        }

        public async Task KickGuestAsync(string roomId)
        {
            await TryHelper.TryAsync(async () =>
            {
                // Dùng PatchAsync để sửa 2 dòng cùng lúc cho gọn
                await Database
                    .Child("rooms")
                    .Child(roomId)
                    .PatchAsync(new
                    {
                        Guest = (UserModel)null, // 1. Xóa người ngồi ghế khách
                        Status = "Waiting"       // 2. Bật lại đèn xanh chờ khách mới
                    })
                    .ConfigureAwait(false);

            }, "đuổi người chơi");
        }
    }
}
