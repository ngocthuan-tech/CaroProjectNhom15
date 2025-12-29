namespace CaroProjectNhom15.Forms
{
    partial class EndGameForm
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
            Lbl_ResultTitle = new Label();
            Btn_Exit = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Lbl_ResultTitle
            // 
            Lbl_ResultTitle.AutoSize = true;
            Lbl_ResultTitle.Font = new Font("Stencil", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_ResultTitle.ForeColor = Color.Goldenrod;
            Lbl_ResultTitle.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Lbl_ResultTitle.Location = new Point(231, 22);
            Lbl_ResultTitle.Name = "Lbl_ResultTitle";
            Lbl_ResultTitle.Size = new Size(229, 56);
            Lbl_ResultTitle.TabIndex = 0;
            Lbl_ResultTitle.Text = "KẾT QUẢ";
            Lbl_ResultTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Btn_Exit
            // 
            Btn_Exit.Font = new Font("Stencil", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Exit.ForeColor = Color.Gold;
            Btn_Exit.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_Exit.Location = new Point(231, 193);
            Btn_Exit.Margin = new Padding(3, 4, 3, 4);
            Btn_Exit.Name = "Btn_Exit";
            Btn_Exit.Size = new Size(229, 53);
            Btn_Exit.TabIndex = 2;
            Btn_Exit.Text = "CLOSE";
            Btn_Exit.UseVisualStyleBackColor = true;
            Btn_Exit.Click += Btn_Exit_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.tạo_cho_tôi_một_cái_nền_trò_chơi__kiểu_cổ_điển_gạch_rêu_này_kia_á__nhìn_nó_đồ_hoạ_2d_thôi_hoạt_tình_tí;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(695, 300);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // EndGameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 300);
            Controls.Add(Btn_Exit);
            Controls.Add(Lbl_ResultTitle);
            Controls.Add(pictureBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EndGameForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kết thúc Game";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Lbl_ResultTitle;
        private System.Windows.Forms.Button Btn_Exit;
        private PictureBox pictureBox1;
    }
}