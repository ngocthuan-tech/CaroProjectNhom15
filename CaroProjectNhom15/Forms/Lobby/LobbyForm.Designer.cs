namespace CaroProjectNhom15.Forms
{
    partial class LobbyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Flp_RoomList = new FlowLayoutPanel();
            panel1 = new Panel();
            Btn_FindRoom = new Button();
            Tb_RoomId = new TextBox();
            Btn_Refresh = new Button();
            Btn_Back = new Button();
            Btn_CreateRoom = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // Flp_RoomList
            // 
            Flp_RoomList.AutoScroll = true;
            Flp_RoomList.BackgroundImage = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Flp_RoomList.Location = new Point(1, 139);
            Flp_RoomList.Margin = new Padding(4, 4, 4, 4);
            Flp_RoomList.Name = "Flp_RoomList";
            Flp_RoomList.Size = new Size(1163, 582);
            Flp_RoomList.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackgroundImage = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            panel1.Controls.Add(Btn_FindRoom);
            panel1.Controls.Add(Tb_RoomId);
            panel1.Controls.Add(Btn_Refresh);
            panel1.Controls.Add(Btn_Back);
            panel1.Controls.Add(Btn_CreateRoom);
            panel1.Location = new Point(1, 1);
            panel1.Margin = new Padding(4, 4, 4, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(1163, 144);
            panel1.TabIndex = 1;
            // 
            // Btn_FindRoom
            // 
            Btn_FindRoom.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_FindRoom.ForeColor = Color.Gold;
            Btn_FindRoom.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_FindRoom.Location = new Point(220, 5);
            Btn_FindRoom.Margin = new Padding(4, 4, 4, 4);
            Btn_FindRoom.Name = "Btn_FindRoom";
            Btn_FindRoom.Size = new Size(247, 59);
            Btn_FindRoom.TabIndex = 4;
            Btn_FindRoom.Text = "Find Room";
            Btn_FindRoom.UseVisualStyleBackColor = true;
            // 
            // Tb_RoomId
            // 
            Tb_RoomId.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Tb_RoomId.Location = new Point(475, 12);
            Tb_RoomId.Margin = new Padding(4, 4, 4, 4);
            Tb_RoomId.Name = "Tb_RoomId";
            Tb_RoomId.Size = new Size(679, 44);
            Tb_RoomId.TabIndex = 3;
            // 
            // Btn_Refresh
            // 
            Btn_Refresh.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Refresh.ForeColor = Color.Gold;
            Btn_Refresh.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_Refresh.Location = new Point(4, 71);
            Btn_Refresh.Margin = new Padding(4, 4, 4, 4);
            Btn_Refresh.Name = "Btn_Refresh";
            Btn_Refresh.Size = new Size(208, 59);
            Btn_Refresh.TabIndex = 2;
            Btn_Refresh.Text = "Refresh";
            Btn_Refresh.UseVisualStyleBackColor = true;
            Btn_Refresh.Click += Btn_Refresh_Click;
            // 
            // Btn_Back
            // 
            Btn_Back.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Back.ForeColor = Color.Gold;
            Btn_Back.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_Back.Location = new Point(4, 4);
            Btn_Back.Margin = new Padding(4, 4, 4, 4);
            Btn_Back.Name = "Btn_Back";
            Btn_Back.Size = new Size(208, 59);
            Btn_Back.TabIndex = 1;
            Btn_Back.Text = "Back";
            Btn_Back.UseVisualStyleBackColor = true;
            Btn_Back.Click += Btn_Back_Click;
            // 
            // Btn_CreateRoom
            // 
            Btn_CreateRoom.Font = new Font("Showcard Gothic", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_CreateRoom.ForeColor = Color.Gold;
            Btn_CreateRoom.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_CreateRoom.Location = new Point(220, 71);
            Btn_CreateRoom.Margin = new Padding(4, 4, 4, 4);
            Btn_CreateRoom.Name = "Btn_CreateRoom";
            Btn_CreateRoom.Size = new Size(247, 59);
            Btn_CreateRoom.TabIndex = 0;
            Btn_CreateRoom.Text = "Create Room";
            Btn_CreateRoom.UseVisualStyleBackColor = true;
            Btn_CreateRoom.Click += Btn_CreateRoom_Click;
            // 
            // LobbyForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1168, 718);
            Controls.Add(panel1);
            Controls.Add(Flp_RoomList);
            Margin = new Padding(4, 4, 4, 4);
            Name = "LobbyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LobbyForm";
            Load += LobbyForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel Flp_RoomList;
        private Panel panel1;
        private Button Btn_FindRoom;
        private TextBox Tb_RoomId;
        private Button Btn_Refresh;
        private Button Btn_Back;
        private Button Btn_CreateRoom;
    }
}