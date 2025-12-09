namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    partial class Home
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            pictureBox1 = new PictureBox();
            Btn_EnterLobby = new Button();
            Btn_Friends = new Button();
            Pb_anhUser = new PictureBox();
            Btn_user = new Button();
            Tb_tenUser = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Pb_anhUser).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.Center;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(998, 611);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Btn_EnterLobby
            // 
            Btn_EnterLobby.BackColor = SystemColors.ActiveCaption;
            Btn_EnterLobby.BackgroundImageLayout = ImageLayout.Center;
            Btn_EnterLobby.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold | FontStyle.Italic);
            Btn_EnterLobby.ForeColor = Color.Goldenrod;
            Btn_EnterLobby.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_EnterLobby.Location = new Point(457, 410);
            Btn_EnterLobby.Margin = new Padding(4);
            Btn_EnterLobby.Name = "Btn_EnterLobby";
            Btn_EnterLobby.Size = new Size(278, 51);
            Btn_EnterLobby.TabIndex = 2;
            Btn_EnterLobby.Text = "Vào sảnh";
            Btn_EnterLobby.UseVisualStyleBackColor = false;
            Btn_EnterLobby.Click += Btn_EnterLobby_Click;
            // 
            // Btn_Friends
            // 
            Btn_Friends.BackColor = SystemColors.ActiveCaption;
            Btn_Friends.BackgroundImageLayout = ImageLayout.Center;
            Btn_Friends.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold | FontStyle.Italic);
            Btn_Friends.ForeColor = Color.Goldenrod;
            Btn_Friends.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_Friends.Location = new Point(267, 410);
            Btn_Friends.Margin = new Padding(4);
            Btn_Friends.Name = "Btn_Friends";
            Btn_Friends.Size = new Size(182, 51);
            Btn_Friends.TabIndex = 3;
            Btn_Friends.Text = "Bạn bè";
            Btn_Friends.UseVisualStyleBackColor = false;
            // 
            // Pb_anhUser
            // 
            Pb_anhUser.Image = Properties.Resources.avatar1;
            Pb_anhUser.Location = new Point(15, 15);
            Pb_anhUser.Margin = new Padding(4);
            Pb_anhUser.Name = "Pb_anhUser";
            Pb_anhUser.Size = new Size(60, 56);
            Pb_anhUser.SizeMode = PictureBoxSizeMode.StretchImage;
            Pb_anhUser.TabIndex = 5;
            Pb_anhUser.TabStop = false;
            // 
            // Btn_user
            // 
            Btn_user.BackColor = SystemColors.ActiveCaption;
            Btn_user.BackgroundImageLayout = ImageLayout.Center;
            Btn_user.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold | FontStyle.Italic);
            Btn_user.ForeColor = Color.Goldenrod;
            Btn_user.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_user.Location = new Point(16, 79);
            Btn_user.Margin = new Padding(4);
            Btn_user.Name = "Btn_user";
            Btn_user.Size = new Size(93, 44);
            Btn_user.TabIndex = 6;
            Btn_user.Text = "User";
            Btn_user.UseVisualStyleBackColor = false;
            // 
            // Tb_tenUser
            // 
            Tb_tenUser.BackColor = Color.DarkOliveGreen;
            Tb_tenUser.BorderStyle = BorderStyle.FixedSingle;
            Tb_tenUser.Font = new Font("Showcard Gothic", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Tb_tenUser.ForeColor = Color.FromArgb(192, 192, 0);
            Tb_tenUser.Location = new Point(83, 43);
            Tb_tenUser.Margin = new Padding(4);
            Tb_tenUser.Multiline = true;
            Tb_tenUser.Name = "Tb_tenUser";
            Tb_tenUser.Size = new Size(200, 28);
            Tb_tenUser.TabIndex = 7;
            // 
            // Home
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(998, 611);
            Controls.Add(Tb_tenUser);
            Controls.Add(Btn_user);
            Controls.Add(Pb_anhUser);
            Controls.Add(Btn_Friends);
            Controls.Add(Btn_EnterLobby);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "Home";
            Text = "Home";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)Pb_anhUser).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button Btn_TaoPhong;
        private Button Btn_EnterLobby;
        private Button Btn_Friends;
        private PictureBox Pb_anhUser;
        private Button Btn_user;
        private TextBox Tb_tenUser;
    }
}