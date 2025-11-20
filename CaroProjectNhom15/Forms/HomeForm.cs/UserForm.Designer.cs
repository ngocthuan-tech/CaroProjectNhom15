namespace CaroProjectNhom15.Forms.HomeForm.cs
{
    partial class UserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserForm));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            Tb_nameUser = new TextBox();
            Btn_ExitUser = new Button();
            Btn_DoiTen = new Button();
            Btn_DangXuat = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 491);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(195, 136);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(182, 168);
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // Tb_nameUser
            // 
            Tb_nameUser.BackColor = Color.DarkOliveGreen;
            Tb_nameUser.BorderStyle = BorderStyle.FixedSingle;
            Tb_nameUser.Font = new Font("Showcard Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Tb_nameUser.ForeColor = Color.FromArgb(192, 192, 0);
            Tb_nameUser.Location = new Point(383, 221);
            Tb_nameUser.Multiline = true;
            Tb_nameUser.Name = "Tb_nameUser";
            Tb_nameUser.Size = new Size(243, 42);
            Tb_nameUser.TabIndex = 5;
            // 
            // Btn_ExitUser
            // 
            Btn_ExitUser.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_ExitUser.ForeColor = Color.Goldenrod;
            Btn_ExitUser.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_ExitUser.Location = new Point(12, 36);
            Btn_ExitUser.Name = "Btn_ExitUser";
            Btn_ExitUser.Size = new Size(85, 43);
            Btn_ExitUser.TabIndex = 9;
            Btn_ExitUser.Text = "Exit";
            Btn_ExitUser.UseVisualStyleBackColor = true;
            Btn_ExitUser.Visible = false;
            // 
            // Btn_DoiTen
            // 
            Btn_DoiTen.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_DoiTen.ForeColor = Color.Goldenrod;
            Btn_DoiTen.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_DoiTen.Location = new Point(383, 269);
            Btn_DoiTen.Name = "Btn_DoiTen";
            Btn_DoiTen.Size = new Size(119, 35);
            Btn_DoiTen.TabIndex = 10;
            Btn_DoiTen.Text = "Đổi tên";
            Btn_DoiTen.UseVisualStyleBackColor = true;
            Btn_DoiTen.Visible = false;
            // 
            // Btn_DangXuat
            // 
            Btn_DangXuat.Font = new Font("Showcard Gothic", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_DangXuat.ForeColor = Color.Goldenrod;
            Btn_DangXuat.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            Btn_DangXuat.Location = new Point(611, 408);
            Btn_DangXuat.Name = "Btn_DangXuat";
            Btn_DangXuat.Size = new Size(177, 43);
            Btn_DangXuat.TabIndex = 11;
            Btn_DangXuat.Text = "Đăng xuất";
            Btn_DangXuat.UseVisualStyleBackColor = true;
            Btn_DangXuat.Visible = false;
            // 
            // UserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 491);
            Controls.Add(Btn_DangXuat);
            Controls.Add(Btn_DoiTen);
            Controls.Add(Btn_ExitUser);
            Controls.Add(Tb_nameUser);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "UserForm";
            Text = "UserForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private TextBox Tb_nameUser;
        private Button Btn_ExitUser;
        private Button Btn_DoiTen;
        private Button Btn_DangXuat;
    }
}