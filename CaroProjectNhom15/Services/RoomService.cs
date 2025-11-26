using Auth.Models;
using CaroProjectNhom15.Models;
using CaroProjectNhom15.Utils;
using Firebase.Database;
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

        //create room, join room, exit room, start game, leave game
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

                string roomData = JsonConvert.SerializeObject(newRoom);
                var result = await Database.Child("rooms").PostAsync(roomData);

                string roomID = result.Key;

                newRoom.ID = roomID;

                return newRoom;
            }, "tạo phòng");
        }

        public async Task JoinRoomAsync(string roomID, UserModel user)
        {
            
        }
    }
}
