namespace CaroProjectNhom15.Forms.Auth
{
    partial class ForgotPasswordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ForgotPasswordForm));
            Btn_Continue = new Button();
            Lbl_EnterEmail = new Label();
            Tb_Email = new TextBox();
            Lbl_ForgotPassword = new Label();
            LnkL_BackToLogIn = new LinkLabel();
            LnkL_Register = new LinkLabel();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Btn_Continue
            // 
            Btn_Continue.BackColor = Color.Blue;
            Btn_Continue.FlatAppearance.BorderColor = Color.Black;
            Btn_Continue.FlatStyle = FlatStyle.Flat;
            Btn_Continue.Font = new Font("Snap ITC", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Continue.ForeColor = Color.Gold;
            Btn_Continue.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_Continue.Location = new Point(306, 311);
            Btn_Continue.Margin = new Padding(4, 4, 4, 4);
            Btn_Continue.Name = "Btn_Continue";
            Btn_Continue.Size = new Size(530, 79);
            Btn_Continue.TabIndex = 25;
            Btn_Continue.Text = "Continue";
            Btn_Continue.UseVisualStyleBackColor = false;
            // 
            // Lbl_EnterEmail
            // 
            Lbl_EnterEmail.AutoSize = true;
            Lbl_EnterEmail.BackColor = Color.Gold;
            Lbl_EnterEmail.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_EnterEmail.ForeColor = SystemColors.ControlLightLight;
            Lbl_EnterEmail.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_EnterEmail.Location = new Point(201, 189);
            Lbl_EnterEmail.Margin = new Padding(4, 0, 4, 0);
            Lbl_EnterEmail.Name = "Lbl_EnterEmail";
            Lbl_EnterEmail.Size = new Size(232, 38);
            Lbl_EnterEmail.TabIndex = 24;
            Lbl_EnterEmail.Text = "Enter Your Email: ";
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(201, 231);
            Tb_Email.Margin = new Padding(4, 4, 4, 4);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email";
            Tb_Email.Size = new Size(731, 47);
            Tb_Email.TabIndex = 23;
            // 
            // Lbl_ForgotPassword
            // 
            Lbl_ForgotPassword.AutoSize = true;
            Lbl_ForgotPassword.BackColor = Color.Gold;
            Lbl_ForgotPassword.Font = new Font("Showcard Gothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_ForgotPassword.ForeColor = Color.Gold;
            Lbl_ForgotPassword.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_ForgotPassword.Location = new Point(241, 50);
            Lbl_ForgotPassword.Margin = new Padding(4, 0, 4, 0);
            Lbl_ForgotPassword.Name = "Lbl_ForgotPassword";
            Lbl_ForgotPassword.Size = new Size(647, 60);
            Lbl_ForgotPassword.TabIndex = 22;
            Lbl_ForgotPassword.Text = "FORGOT YOUR PASSWORD";
            // 
            // LnkL_BackToLogIn
            // 
            LnkL_BackToLogIn.AutoSize = true;
            LnkL_BackToLogIn.BackColor = Color.Gold;
            LnkL_BackToLogIn.Font = new Font("Segoe UI", 13.8F);
            LnkL_BackToLogIn.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            LnkL_BackToLogIn.LinkColor = Color.Goldenrod;
            LnkL_BackToLogIn.Location = new Point(201, 444);
            LnkL_BackToLogIn.Margin = new Padding(4, 0, 4, 0);
            LnkL_BackToLogIn.Name = "LnkL_BackToLogIn";
            LnkL_BackToLogIn.Size = new Size(204, 38);
            LnkL_BackToLogIn.TabIndex = 26;
            LnkL_BackToLogIn.TabStop = true;
            LnkL_BackToLogIn.Text = "Back to Log in?";
            // 
            // LnkL_Register
            // 
            LnkL_Register.AutoSize = true;
            LnkL_Register.BackColor = Color.Gold;
            LnkL_Register.Font = new Font("Segoe UI", 13.8F);
            LnkL_Register.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            LnkL_Register.LinkColor = Color.Goldenrod;
            LnkL_Register.Location = new Point(572, 444);
            LnkL_Register.Margin = new Padding(4, 0, 4, 0);
            LnkL_Register.Name = "LnkL_Register";
            LnkL_Register.Size = new Size(352, 38);
            LnkL_Register.TabIndex = 27;
            LnkL_Register.TabStop = true;
            LnkL_Register.Text = "Don't have an account yet?";
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1161, 634);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 28;
            pictureBox1.TabStop = false;
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1161, 634);
            Controls.Add(LnkL_Register);
            Controls.Add(LnkL_BackToLogIn);
            Controls.Add(Btn_Continue);
            Controls.Add(Lbl_EnterEmail);
            Controls.Add(Tb_Email);
            Controls.Add(Lbl_ForgotPassword);
            Controls.Add(pictureBox1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "ForgotPasswordForm";
            Text = "ForgotPasswordForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_Continue;
        private Label Lbl_EnterEmail;
        private TextBox Tb_Email;
        private Label Lbl_ForgotPassword;
        private LinkLabel LnkL_BackToLogIn;
        private LinkLabel LnkL_Register;
        private PictureBox pictureBox1;
    }
}