using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using System.Reactive.Linq;
using MethodInvoker = System.Windows.Forms.MethodInvoker;


namespace CaroProjectNhom15.Forms.Forms.Gameplay
{
    public partial class TicTacToe : Form
    {
        // -------------------------------------------------------------
        // BIẾN FIREBASE VÀ TRẠNG THÁI
        // -------------------------------------------------------------
        // LƯU Ý: THAY THẾ URL NÀY BẰNG URL FIREBASE CỦA BẠN
        private const string FirebaseURL = "https://caro-gameplaydb-default-rtdb.asia-southeast1.firebasedatabase.app/";
        private const string RoomId = "DemoRoom_Nhom15";

        private FirebaseClient firebaseClient;
        private GameRoomModel currentGameState;

        // mySymbol được gán giá trị từ Program.cs
        private string mySymbol = "";
        private Dictionary<string, (int row, int col)> buttonMap;

        // -------------------------------------------------------------
        // CONSTRUCTOR VÀ KHỞI TẠO
        // -------------------------------------------------------------
        public TicTacToe(string role)
        {
            InitializeComponent();
            InitializeButtonMap();

            // mySymbol ĐƯỢC GÁN TỪ PROGRAM.CS, KHÔNG CÓ GIÁ TRỊ MẶC ĐỊNH
            mySymbol = role;
            SetupGameAndFirebaseAsync();
        }

        private void InitializeButtonMap()
        {
            // Ánh xạ tên nút với tọa độ (row, col)
            buttonMap = new Dictionary<string, (int row, int col)>();
            buttonMap.Add("Btn_A1", (0, 0)); buttonMap.Add("Btn_A2", (0, 1)); buttonMap.Add("Btn_A3", (0, 2));
            buttonMap.Add("Btn_B1", (1, 0)); buttonMap.Add("Btn_B2", (1, 1)); buttonMap.Add("Btn_B3", (1, 2));
            buttonMap.Add("Btn_C1", (2, 0)); buttonMap.Add("Btn_C2", (2, 1)); buttonMap.Add("Btn_C3", (2, 2));
            // Cần đảm bảo các nút của bạn có tên này trong Designer
        }

        // --- HÀM CỐT LÕI: KHỞI TẠO FIREBASE VÀ GAME (ĐÃ SỬA LỖI LOGIC) ---
        private async void SetupGameAndFirebaseAsync()
        {
            // Dòng gán mySymbol ở đây đã được XÓA BỎ.
            // mySymbol đã có giá trị từ Constructor.
            this.Text = $"Tic Tac Toe (Demo Sync) - ROLE: {mySymbol}";

            try
            {
                firebaseClient = new FirebaseClient(FirebaseURL);

                // CHỈ Player X mới được phép reset game
                if (mySymbol == "X")
                {
                    await ResetBoardOnFirebaseAsync();
                    MessageBox.Show("Bạn là Player X. Game đã được khởi tạo/reset.", "Vai trò X");
                }
                else
                {
                    MessageBox.Show("Bạn là Player O. Đang chờ đồng bộ từ Firebase.", "Vai trò O");
                }

                StartListeningToGame(); // Bắt đầu nghe thay đổi
                EnableAllButtons(); // Bật nút để chuẩn bị chơi
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Firebase Error] Setup failed: {ex}");
                MessageBox.Show($"Lỗi Khởi tạo Firebase: {ex.Message}\nKiểm tra lại URL và Quy tắc Bảo mật!", "Lỗi Kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
                disableButtons();
            }
        }

        // --- ĐẶT LẠI BOARD TRÊN FIREBASE (Dùng cho Khởi tạo/New Game) ---
        private async Task ResetBoardOnFirebaseAsync()
        {
            if (firebaseClient == null) return;

            var initialGameState = new GameRoomModel();

            await firebaseClient
                .Child("GameRooms")
                .Child(RoomId)
                .PutAsync(initialGameState); // Ghi đè toàn bộ Room bằng trạng thái mới
        }

        // -------------------------------------------------------------
        // LẮNG NGHE REAL-TIME (ĐỒNG BỘ)
        // -------------------------------------------------------------
        private void StartListeningToGame()
        {
            firebaseClient
                .Child("GameRooms")
                .Child(RoomId)
                .AsObservable<GameRoomModel>()
                .Subscribe(d =>
                {
                    if (d.Object != null)
                    {
                        // Đảm bảo cập nhật UI trên luồng chính
                        this.Invoke((MethodInvoker)delegate
                        {
                            currentGameState = d.Object; // Lưu trạng thái mới
                            UpdateUIFromFirebase(currentGameState); // Cập nhật Form
                        });
                    }
                },
                ex =>
                {
                    // Xử lý lỗi khi lắng nghe (ví dụ: mất mạng)
                    Console.WriteLine($"[Firebase Realtime Error]: {ex}");
                    MessageBox.Show($"Lỗi kết nối Real-time: {ex.Message}", "Lỗi Đồng bộ");
                });
        }

        // --- CẬP NHẬT GIAO DIỆN TỪ FIREBASE ---
        private void UpdateUIFromFirebase(GameRoomModel state)
        {
            if (state?.Board == null) return;
            var board = state.Board;

            // Duyệt qua tất cả các nút để đồng bộ trạng thái
            foreach (var control in Controls)
            {
                if (control is Button b && buttonMap.ContainsKey(b.Name))
                {
                    var (r, c) = buttonMap[b.Name];

                    string symbol = board[r][c];

                    b.Text = symbol;

                    // Vô hiệu hóa nút nếu đã được đánh HOẶC nếu đã có người thắng
                    b.Enabled = string.IsNullOrEmpty(symbol) && state.Winner == null;

                    // Thêm logic để chỉ cho phép đánh khi đến lượt mình
                    if (b.Enabled)
                    {
                        b.Enabled = (state.CurrentTurn == mySymbol);
                    }
                }
            }

            this.Text = $"Tic Tac Toe (Demo Sync) - Lượt: {state.CurrentTurn} - ROLE: {mySymbol}";

            // Sau khi đồng bộ, kiểm tra xem có người thắng/thua chưa
            checkWinner();
        }


        // -------------------------------------------------------------
        // XỬ LÝ NƯỚC ĐI (GHI LÊN FIREBASE)
        // -------------------------------------------------------------
        private async void button_CLick(object sender, EventArgs e)
        {
            Console.WriteLine("[DEBUG] --- Bắt đầu xử lý click ---");

            if (firebaseClient == null || currentGameState == null)
            {
                Console.WriteLine("[DEBUG] Lỗi: firebaseClient hoặc currentGameState chưa khởi tạo.");
                return;
            }

            Button b = (Button)sender;
            if (!b.Enabled) return;

            // Kiểm tra lượt: chỉ cho phép đánh khi đến lượt mình
            if (currentGameState.CurrentTurn != mySymbol)
            {
                Console.WriteLine($"[DEBUG] Đánh bị chặn: Chưa đến lượt của {mySymbol}. Lượt hiện tại: {currentGameState.CurrentTurn}");
                MessageBox.Show($"Chưa đến lượt của bạn ({mySymbol}). Lượt của {currentGameState.CurrentTurn}.", "Chờ đợi");
                return;
            }

            if (!buttonMap.TryGetValue(b.Name, out var coords))
            {
                Console.WriteLine($"[DEBUG] Lỗi: Không tìm thấy button {b.Name} trong buttonMap.");
                return;
            }

            int row = coords.row;
            int col = coords.col;
            string playerSymbol = mySymbol;

            Console.WriteLine($"[DEBUG] Đã xác nhận: {playerSymbol} đánh vào ({row}, {col}). Chuẩn bị ghi Firebase...");

            try
            {
                // Lấy bản sao của Board hiện tại
                var newBoard = currentGameState.Board.Select(list => new List<string>(list)).ToList();
                newBoard[row][col] = playerSymbol;

                // Chuyển Lượt
                string nextPlayer = (playerSymbol == "X") ? "O" : "X";

                // Cập nhật cả Board và CurrentTurn cùng lúc
                var updates = new Dictionary<string, object>
        {
            { "Board", newBoard },
            { "CurrentTurn", nextPlayer }
        };

                // Sử dụng PatchAsync để cập nhật nhiều trường
                await firebaseClient
                    .Child("GameRooms")
                    .Child(RoomId)
                    .PatchAsync(updates);

                Console.WriteLine("[DEBUG] Ghi Firebase thành công. Đang chờ đồng bộ...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Firebase WRITE ERROR] Lỗi khi gửi nước đi: {ex}");
                MessageBox.Show($"Lỗi khi gửi nước đi: {ex.Message}", "Lỗi Ghi dữ liệu");
            }
        }

        // -------------------------------------------------------------
        // HÀM OFFLINE GỐC CỦA BẠN (ĐÃ TÍCH HỢP VÀO LOGIC REAL-TIME)
        // -------------------------------------------------------------

        private void checkWinner()
        {
            // Logic check thắng thua
            bool thereIsWinner = false;
            string winnerSymbol = "";

            // Kiểm tra 8 trường hợp thắng...
            // horizontal
            if (Btn_A1.Text == Btn_A2.Text && Btn_A2.Text == Btn_A3.Text && (!string.IsNullOrEmpty(Btn_A1.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A1.Text; }
            else if (Btn_B1.Text == Btn_B2.Text && Btn_B2.Text == Btn_B3.Text && (!string.IsNullOrEmpty(Btn_B1.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_B1.Text; }
            else if (Btn_C1.Text == Btn_C2.Text && Btn_C2.Text == Btn_C3.Text && (!string.IsNullOrEmpty(Btn_C1.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_C1.Text; }
            // vertical
            else if (Btn_A1.Text == Btn_B1.Text && Btn_B1.Text == Btn_C1.Text && (!string.IsNullOrEmpty(Btn_A1.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A1.Text; }
            else if (Btn_A2.Text == Btn_B2.Text && Btn_B2.Text == Btn_C2.Text && (!string.IsNullOrEmpty(Btn_A2.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A2.Text; }
            else if (Btn_A3.Text == Btn_B3.Text && Btn_B3.Text == Btn_C3.Text && (!string.IsNullOrEmpty(Btn_A3.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A3.Text; }
            // diagonal
            else if (Btn_A1.Text == Btn_B2.Text && Btn_B2.Text == Btn_C3.Text && (!string.IsNullOrEmpty(Btn_A1.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A1.Text; }
            else if (Btn_A3.Text == Btn_B2.Text && Btn_B2.Text == Btn_C1.Text && (!string.IsNullOrEmpty(Btn_A3.Text)))
            { thereIsWinner = true; winnerSymbol = Btn_A3.Text; }


            if (thereIsWinner && currentGameState.Winner == null)
            {
                // Cập nhật trạng thái Winner lên Firebase
                firebaseClient.Child("GameRooms").Child(RoomId).Child("Winner").PutAsync(winnerSymbol);
                MessageBox.Show("Winner is " + winnerSymbol + "!");
                disableButtons();
            }
            else if (AllButtonsPlayed() && currentGameState.Winner == null)
            {
                // Cập nhật trạng thái Draw lên Firebase
                firebaseClient.Child("GameRooms").Child(RoomId).Child("Winner").PutAsync("Draw");
                MessageBox.Show("Draw!");
                disableButtons();
            }
            else if (currentGameState.Winner != null && currentGameState.Winner != "Draw")
            {
                // Nếu game đã kết thúc (thắng/thua) và đã được đồng bộ
                MessageBox.Show("Game Over! Winner is " + currentGameState.Winner);
                disableButtons();
            }
            else if (currentGameState.Winner == "Draw")
            {
                // Nếu game đã kết thúc (hòa) và đã được đồng bộ
                MessageBox.Show("Game Over! It's a Draw.");
                disableButtons();
            }
        }

        private bool AllButtonsPlayed()
        {
            // Đảm bảo count chỉ các nút trong map
            return Controls.OfType<Button>().Count(b => buttonMap.ContainsKey(b.Name) && !string.IsNullOrEmpty(b.Text)) == 9;
        }

        private void disableButtons()
        {
            try
            {
                foreach (Control c in Controls)
                {
                    if (c is Button b && buttonMap.ContainsKey(b.Name))
                    {
                        b.Enabled = false;
                    }
                }
            }
            catch { }
        }

        // Cần đảm bảo hàm này đặt text về rỗng để hiển thị đúng khi game reset
        private void EnableAllButtons()
        {
            foreach (Control c in Controls)
            {
                if (c is Button b && buttonMap.ContainsKey(b.Name))
                {
                    b.Enabled = true;
                    b.Text = ""; // Rất quan trọng để xóa ký hiệu cũ
                }
            }
        }

        private async void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Chỉ Player X (người khởi tạo) mới cần reset
            if (mySymbol == "X")
            {
                await ResetBoardOnFirebaseAsync();
                MessageBox.Show("Game mới đã được bắt đầu!", "New Game");
            }
            else
            {
                MessageBox.Show("Chỉ Player X mới có thể khởi tạo game mới.", "Lỗi quyền");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game made by Tri", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    // CÁC DATA MODEL CẦN THIẾT CHO FIREBASE
    public class GameRoomModel
    {
        public List<List<string>> Board { get; set; } = new List<List<string>>
        {
            new List<string> { "", "", "" },
            new List<string> { "", "", "" },
            new List<string> { "", "", "" }
        };
        public string CurrentTurn { get; set; } = "X";
        public string Winner { get; set; } = null;
    }
}