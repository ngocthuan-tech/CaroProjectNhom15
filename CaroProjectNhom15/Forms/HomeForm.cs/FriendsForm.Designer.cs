namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    partial class FriendsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendsForm));
            pictureBox1 = new PictureBox();
            Btn_LoiMoiKB = new Button();
            Btn_DsBanBe = new Button();
            Btn_ExitFriends = new Button();
            panel1 = new Panel();
            Btn_timFriend = new Button();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(795, 487);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // Btn_LoiMoiKB
            // 
            Btn_LoiMoiKB.Font = new Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_LoiMoiKB.ForeColor = Color.Goldenrod;
            Btn_LoiMoiKB.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_LoiMoiKB.Location = new Point(67, 335);
            Btn_LoiMoiKB.Name = "Btn_LoiMoiKB";
            Btn_LoiMoiKB.Size = new Size(226, 43);
            Btn_LoiMoiKB.TabIndex = 10;
            Btn_LoiMoiKB.Text = "Lời mời kết bạn";
            Btn_LoiMoiKB.UseVisualStyleBackColor = true;
            Btn_LoiMoiKB.Visible = false;
            // 
            // Btn_DsBanBe
            // 
            Btn_DsBanBe.Font = new Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_DsBanBe.ForeColor = Color.Goldenrod;
            Btn_DsBanBe.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_DsBanBe.Location = new Point(67, 264);
            Btn_DsBanBe.Name = "Btn_DsBanBe";
            Btn_DsBanBe.Size = new Size(226, 43);
            Btn_DsBanBe.TabIndex = 9;
            Btn_DsBanBe.Text = "Danh sách bạn bè";
            Btn_DsBanBe.UseVisualStyleBackColor = true;
            Btn_DsBanBe.Visible = false;
            // 
            // Btn_ExitFriends
            // 
            Btn_ExitFriends.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_ExitFriends.ForeColor = Color.Goldenrod;
            Btn_ExitFriends.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_ExitFriends.Location = new Point(10, 33);
            Btn_ExitFriends.Name = "Btn_ExitFriends";
            Btn_ExitFriends.Size = new Size(85, 43);
            Btn_ExitFriends.TabIndex = 8;
            Btn_ExitFriends.Text = "Exit";
            Btn_ExitFriends.UseVisualStyleBackColor = true;
            Btn_ExitFriends.Visible = false;
            // 
            // panel1
            // 
            panel1.Location = new Point(312, 85);
            panel1.Name = "panel1";
            panel1.Size = new Size(415, 325);
            panel1.TabIndex = 11;
            // 
            // Btn_timFriend
            // 
            Btn_timFriend.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_timFriend.ForeColor = Color.Goldenrod;
            Btn_timFriend.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_timFriend.Location = new Point(131, 196);
            Btn_timFriend.Name = "Btn_timFriend";
            Btn_timFriend.Size = new Size(85, 37);
            Btn_timFriend.TabIndex = 12;
            Btn_timFriend.Text = "Tìm";
            Btn_timFriend.UseVisualStyleBackColor = true;
            Btn_timFriend.Visible = false;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(89, 163);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(173, 27);
            textBox1.TabIndex = 13;
            // 
            // FriendsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 487);
            Controls.Add(textBox1);
            Controls.Add(Btn_timFriend);
            Controls.Add(panel1);
            Controls.Add(Btn_LoiMoiKB);
            Controls.Add(Btn_DsBanBe);
            Controls.Add(Btn_ExitFriends);
            Controls.Add(pictureBox1);
            Name = "FriendsForm";
            Text = "FriendsForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button Btn_LoiMoiKB;
        private Button Btn_DsBanBe;
        private Button Btn_ExitFriends;
        private Panel panel1;
        private Button Btn_timFriend;
        private TextBox textBox1;
    }
}