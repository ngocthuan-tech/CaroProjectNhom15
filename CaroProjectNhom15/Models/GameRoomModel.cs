// GameRoomModel.cs - Cập nhật
using Auth.Models;
using System.Collections.Generic;

namespace CaroProjectNhom15.Models
{
    public class GameRoomModel
    {
        // ❌ Bỏ List<List<string>> Board đi vì đã dùng Moves

        // --- CÁC THUỘC TÍNH MỚI CHO TRẠNG THÁI VÀ NƯỚC ĐI ---
        public string Turn { get; set; } // Thay thế CurrentTurn bằng Turn (chứa PlayerId)
        public string Winner { get; set; } = ""; // Khởi tạo rỗng

        // Key là tọa độ dưới dạng chuỗi "X_Y" (ví dụ: "10_10")
        public Dictionary<string, MoveModel> Moves { get; set; } = new Dictionary<string, MoveModel>();

        // Nước đi cuối cùng
        public LastMoveModel LastMove { get; set; } = null;

        // --- CÁC THUỘC TÍNH CŨ KHÁC ---
        public string ID { get; set; }
        public string Name { get; set; }
        public UserModel Host { get; set; }
        public UserModel Guest { get; set; } = null;
        public string Status { get; set; } = "Waiting";

        // Thiết lập giá trị mặc định cho Turn
        // Chúng ta cần biết UID của Host để đặt làm lượt đi đầu tiên khi phòng được tạo.
        // Tạm thời để Turn là null, sẽ gán sau khi có Host.
    }
}