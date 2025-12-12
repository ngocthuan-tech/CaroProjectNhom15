// GameCons.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroProjectNhom15.Forms.Gameplay
{
    public class GameCons
    {
        // THÔNG SỐ KÍCH THƯỚC BÀN CỜ (Giữ theo chuẩn 35x35, 20x20)
        public static int CHESS_WIDTH = 35;
        public static int CHESS_HEIGHT = 35;
        public static int CHESS_BOARD_SIZE = 20;

        // THÔNG SỐ LOGIC GAME VÀ TIMER
        public static int COOL_DOWN_STEP = 100;
        public static int COOL_DOWN_TIME = 10000; // 10000 ms = 10 giây
        public static int COOL_DOWN_INTERVAL = 100; // Cập nhật mỗi 100ms
        public static int WINNING_COUNT = 5; // Số quân cờ liên tiếp để thắng
    }
}