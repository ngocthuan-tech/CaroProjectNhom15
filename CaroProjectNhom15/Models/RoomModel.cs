using System;
using System.Drawing; // Cần reference System.Drawing
using Auth.Models;

namespace CaroProjectNhom15.Models
{
    public class RoomModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public UserModel Host { get; set; }
        public UserModel Guest { get; set; }
        public string Status { get; set; } // "Waiting", "Playing"

        // --- THÊM PHẦN GAME INFO ---
        public GameInfo Game { get; set; }

        public RoomModel()
        {
            Status = "Waiting";
            Game = new GameInfo();
        }
    }

    public class GameInfo
    {
        // Tọa độ nước đi (X, Y)
        public int X { get; set; }
        public int Y { get; set; }

        // UID người vừa thực hiện hành động (để Client lọc: nếu là mình thì bỏ qua)
        public string SenderID { get; set; }

        // UID người đến lượt đánh tiếp theo
        public string CurrentTurnID { get; set; }

        // UID người thắng (nếu có)
        public string WinnerID { get; set; }
    }
}