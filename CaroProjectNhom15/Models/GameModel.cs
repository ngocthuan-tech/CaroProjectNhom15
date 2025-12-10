using System.Collections.Generic;

namespace CaroProjectNhom15.Models
{
    public class GameModel
    {
        // 1. LƯỢT ĐI HIỆN TẠI
        // Chứa Uid của người chơi tiếp theo.
        public string Turn { get; set; }

        // 2. NGƯỜI THẮNG CUỘC
        // Chứa Uid của người chiến thắng, hoặc "" nếu đang chơi/hòa.
        public string Winner { get; set; }

        // 3. TRẠNG THÁI NƯỚC ĐI (Bàn cờ)
        // Chứa tất cả các nước đi đã đánh, được key bằng tọa độ (ví dụ: "1_1", "1_2").
        public Dictionary<string, MoveModel> Moves { get; set; }

        // 4. NƯỚC ĐI CUỐI CÙNG
        // Chứa tọa độ (X, Y) của nước đi gần nhất.
        public LastMoveModel LastMove { get; set; }

        public GameModel()
        {
            Turn = ""; // Sẽ được set bằng Host.Uid khi bắt đầu game
            Winner = "";
            Moves = new Dictionary<string, MoveModel>(); // Bàn cờ trống
            LastMove = null;
        }
    }
}