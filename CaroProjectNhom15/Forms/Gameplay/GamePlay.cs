using CaroProjectNhom15.Forms.Forms.Gameplay;
using Firebase.Database; // Thư viện cốt lõi
using Firebase.Database.Query; // Dùng cho PatchAsync/PutAsync
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq; // Dùng cho Real-time Listener (AsObservable)
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace CaroProjectNhom15.Forms.Gameplay
{
    public partial class GamePlay : Form
    {
        private const string FirebaseURL = "https://project-nhom15-nt106-default-rtdb.asia-southeast1.firebasedatabase.app/";
        private const string RoomId = "DemoRoom_Nhom15";

        private FirebaseClient firebaseClient;
        private GameRoomModel currentGameState;

        public GamePlay()
        {
            InitializeComponent();
        }

        private async void Btn_Click(object sender, EventArgs e)
        {

        }


    }
}
