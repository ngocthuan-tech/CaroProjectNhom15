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

        //create room, join room, exit room, start game, leave game, get all room
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
                PutAsync(newRoom).ConfigureAwait(false);

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

        public async Task ExitRoomAsync()
        {

        }
    }
}
