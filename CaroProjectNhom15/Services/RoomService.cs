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
using System.Text;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Services
{
    public class RoomService
    {
        private FirebaseClient Database => FirebaseProvider.Instance.Database;
        
        public async Task<RoomModel> GetRoomById (string roomId)
        {
            return await TryHelper.TryAsync<RoomModel>(async Task<RoomModel> () =>
            {
                var newRoom = await Database.Child("rooms").Child(roomId).
                OnceSingleAsync<RoomModel>().ConfigureAwait(false);

                return newRoom;

            }, "tìm phòng qua id");

        }

        public async Task<List<RoomModel>> GetAllRoom(string roomId)
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


    }
}
