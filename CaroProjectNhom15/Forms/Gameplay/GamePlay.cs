// GamePlay.cs
using Auth.Models;
using CaroProjectNhom15.Models; // Cần thêm để sử dụng GameRoomModel, UserModel, v.v.
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; // Cần thiết cho async/await
using System.Windows.Forms;

namespace CaroProjectNhom15.Forms.Gameplay
{
    public partial class GamePlay : Form
    {
        private const string FirebaseURL = "https://project-nhom15-nt106-default-rtdb.asia-southeast1.firebasedatabase.app/";
        private const string RoomId = "DemoRoom_Nhom15";

        private FirebaseClient firebaseClient;
        private GameRoomModel currentGameState;
        private UserModel currentUser; // 👈 Đối tượng người chơi hiện tại

        public GamePlay()
        {
            InitializeComponent();

            // 1. Khởi tạo Firebase Client
            firebaseClient = new FirebaseClient(FirebaseURL);

            // 2. Khởi tạo người dùng hiện tại (Giả định là Host)
            currentUser = new UserModel
            {
                Uid = "user123_Host",
                UserName = "PlayerA_Host"
            };

            // 3. Khởi tạo Game Room trên Firebase khi Form được tạo
            InitializeGameRoomAsync();
        }

        private async Task InitializeGameRoomAsync() // Hàm khởi tạo phòng
        {
            try
            {
                var newRoom = new GameRoomModel
                {
                    ID = RoomId,
                    Name = "Phòng của " + currentUser.UserName,
                    Host = currentUser,
                    Guest = null,
                    Status = "Waiting",

                    // --- THUỘC TÍNH GAMEPLAY MỚI ---
                    Turn = currentUser.Uid, // 👈 Host (PlayerA) đi trước
                    Winner = "",
                    Moves = new Dictionary<string, MoveModel>(), // Bàn cờ trống
                    LastMove = null,
                };

                // Đẩy dữ liệu lên Firebase
                await firebaseClient
                    .Child(RoomId)
                    .PutAsync(newRoom);

                MessageBox.Show($"Phòng game '{RoomId}' đã được tạo thành công.", "Khởi tạo thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật trạng thái cục bộ
                currentGameState = newRoom;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo phòng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Btn_Click(object sender, EventArgs e)
        {
            // Logic xử lý nước đi sẽ được đặt ở đây
        }
    }
}