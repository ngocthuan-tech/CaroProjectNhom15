namespace CaroProjectNhom15.Forms
{
    partial class Frm_WaitingRoomForm
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
            Pb_Background = new PictureBox();
            Btn_Start = new Button();
            Lb_UserName1 = new Label();
            Pb_Player01 = new PictureBox();
            Pb_Player02 = new PictureBox();
            Lb_UserName2 = new Label();
            Lbl_VS = new Label();
            Btn_ExitRoom = new Button();
            Lbl_Player01 = new Label();
            Lbl_Player02 = new Label();
            uC_Chat1 = new CaroProjectNhom15.UserControls.UC_Chat();
            Lbl_RoomId = new Label();
            Tb_RoomId = new TextBox();
            ((System.ComponentModel.ISupportInitialize)Pb_Background).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pb_Player01).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pb_Player02).BeginInit();
            SuspendLayout();
            // 
            // Pb_Background
            // 
            Pb_Background.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Pb_Background.Location = new Point(-10, 1);
            Pb_Background.Name = "Pb_Background";
            Pb_Background.Size = new Size(1246, 500);
            Pb_Background.TabIndex = 0;
            Pb_Background.TabStop = false;
            // 
            // Btn_Start
            // 
            Btn_Start.Font = new Font("Snap ITC", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Start.ForeColor = Color.Yellow;
            Btn_Start.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Btn_Start.Location = new Point(335, 402);
            Btn_Start.Name = "Btn_Start";
            Btn_Start.Size = new Size(145, 53);
            Btn_Start.TabIndex = 1;
            Btn_Start.Text = "Start";
            Btn_Start.UseVisualStyleBackColor = true;
            Btn_Start.Click += Btn_Start_Click;
            // 
            // Lb_UserName1
            // 
            Lb_UserName1.AutoSize = true;
            Lb_UserName1.Font = new Font("Snap ITC", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lb_UserName1.Location = new Point(115, 296);
            Lb_UserName1.Name = "Lb_UserName1";
            Lb_UserName1.Size = new Size(0, 31);
            Lb_UserName1.TabIndex = 2;
            // 
            // Pb_Player01
            // 
            Pb_Player01.Location = new Point(71, 106);
            Pb_Player01.Name = "Pb_Player01";
            Pb_Player01.Size = new Size(174, 162);
            Pb_Player01.TabIndex = 3;
            Pb_Player01.TabStop = false;
            // 
            // Pb_Player02
            // 
            Pb_Player02.Location = new Point(570, 106);
            Pb_Player02.Name = "Pb_Player02";
            Pb_Player02.Size = new Size(174, 162);
            Pb_Player02.TabIndex = 5;
            Pb_Player02.TabStop = false;
            // 
            // Lb_UserName2
            // 
            Lb_UserName2.AutoSize = true;
            Lb_UserName2.Font = new Font("Snap ITC", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lb_UserName2.Location = new Point(636, 296);
            Lb_UserName2.Name = "Lb_UserName2";
            Lb_UserName2.Size = new Size(0, 31);
            Lb_UserName2.TabIndex = 6;
            // 
            // Lbl_VS
            // 
            Lbl_VS.AutoSize = true;
            Lbl_VS.BackColor = Color.ForestGreen;
            Lbl_VS.Font = new Font("Snap ITC", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_VS.ForeColor = Color.Yellow;
            Lbl_VS.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Lbl_VS.Location = new Point(350, 167);
            Lbl_VS.Name = "Lbl_VS";
            Lbl_VS.Size = new Size(130, 77);
            Lbl_VS.TabIndex = 7;
            Lbl_VS.Text = "VS";
            // 
            // Btn_ExitRoom
            // 
            Btn_ExitRoom.Font = new Font("Snap ITC", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_ExitRoom.ForeColor = Color.Yellow;
            Btn_ExitRoom.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Btn_ExitRoom.Location = new Point(12, 12);
            Btn_ExitRoom.Name = "Btn_ExitRoom";
            Btn_ExitRoom.Size = new Size(222, 59);
            Btn_ExitRoom.TabIndex = 9;
            Btn_ExitRoom.Text = "Exit Room";
            Btn_ExitRoom.UseVisualStyleBackColor = true;
            Btn_ExitRoom.Click += Btn_ExitRoom_Click;
            // 
            // Lbl_Player01
            // 
            Lbl_Player01.AutoSize = true;
            Lbl_Player01.BackColor = Color.ForestGreen;
            Lbl_Player01.Font = new Font("Snap ITC", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Player01.ForeColor = Color.Yellow;
            Lbl_Player01.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Lbl_Player01.Location = new Point(53, 295);
            Lbl_Player01.Name = "Lbl_Player01";
            Lbl_Player01.Size = new Size(207, 51);
            Lbl_Player01.TabIndex = 10;
            Lbl_Player01.Text = "Player 1";
            // 
            // Lbl_Player02
            // 
            Lbl_Player02.AutoSize = true;
            Lbl_Player02.BackColor = Color.ForestGreen;
            Lbl_Player02.Font = new Font("Snap ITC", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Player02.ForeColor = Color.Yellow;
            Lbl_Player02.Image = Properties.Resources.hãy_tạo_một_ảnh_nền_game_2d_phong_cách_hoạt_hình__phong_cách_cổ_điển__tường_cây_cỏ_lá__dây_leo__rêu;
            Lbl_Player02.Location = new Point(553, 298);
            Lbl_Player02.Name = "Lbl_Player02";
            Lbl_Player02.Size = new Size(217, 51);
            Lbl_Player02.TabIndex = 11;
            Lbl_Player02.Text = "Player 2";
            // 
            // uC_Chat1
            // 
            uC_Chat1.Location = new Point(819, 1);
            uC_Chat1.Name = "uC_Chat1";
            uC_Chat1.Size = new Size(429, 500);
            uC_Chat1.TabIndex = 12;
            // 
            // Lbl_RoomId
            // 
            Lbl_RoomId.AutoSize = true;
            Lbl_RoomId.BackColor = Color.Transparent;
            Lbl_RoomId.Font = new Font("Showcard Gothic", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_RoomId.Location = new Point(295, 21);
            Lbl_RoomId.Name = "Lbl_RoomId";
            Lbl_RoomId.Size = new Size(61, 37);
            Lbl_RoomId.TabIndex = 13;
            Lbl_RoomId.Text = "ID:";
            // 
            // Tb_RoomId
            // 
            Tb_RoomId.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Tb_RoomId.Location = new Point(378, 16);
            Tb_RoomId.Name = "Tb_RoomId";
            Tb_RoomId.ReadOnly = true;
            Tb_RoomId.Size = new Size(348, 47);
            Tb_RoomId.TabIndex = 14;
            // 
            // Frm_WaitingRoomForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1237, 496);
            Controls.Add(Tb_RoomId);
            Controls.Add(Lbl_RoomId);
            Controls.Add(uC_Chat1);
            Controls.Add(Lbl_Player02);
            Controls.Add(Lbl_Player01);
            Controls.Add(Btn_ExitRoom);
            Controls.Add(Lbl_VS);
            Controls.Add(Lb_UserName2);
            Controls.Add(Pb_Player02);
            Controls.Add(Pb_Player01);
            Controls.Add(Lb_UserName1);
            Controls.Add(Btn_Start);
            Controls.Add(Pb_Background);
            Name = "Frm_WaitingRoomForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LobbyForm";
            FormClosing += Btn_ExitRoom_Click;
            Load += Frm_WaitingRoomForm_Load;
            ((System.ComponentModel.ISupportInitialize)Pb_Background).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pb_Player01).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pb_Player02).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        private PictureBox Pb_Background;
        private Button Btn_Start;
        private Label Lb_UserName1;
        private PictureBox Pb_Player01;
        private PictureBox Pb_Player02;
        private Label Lb_UserName2;
        private Label Lbl_VS;
        private Button Btn_ExitRoom;
        private Label Lbl_Player01;
        private Label Lbl_Player02;
        private UserControls.UC_Chat uC_Chat1;
        private Label Lbl_RoomId;
        private TextBox Tb_RoomId;
    }
}