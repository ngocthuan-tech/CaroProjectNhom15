using System.Windows.Forms;
using System.Drawing;
using CaroProjectNhom15.Forms.Auth;

namespace CaroProjectNhom15.Forms.Auth
{
    partial class RegisterForm : System.Windows.Forms.Form // <-- Inherit from Form to enable override
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegisterForm));
            Lbl_Email = new Label();
            Tb_Password = new TextBox();
            Btn_Continue = new Button();
            Btn_BackToLogIn = new Button();
            Tb_Email = new TextBox();
            Lbl_Register = new Label();
            Lbl_Name = new Label();
            Tb_Name = new TextBox();
            Tb_Username = new TextBox();
            Lbl_Username = new Label();
            Lbl_Password = new Label();
            Lbl_ConfirmPassword = new Label();
            Tb_ConfirmPassword = new TextBox();
            Lbl_AlreadyHaveAnAccount = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Lbl_Email
            // 
            Lbl_Email.AutoSize = true;
            Lbl_Email.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Email.ForeColor = SystemColors.ControlLightLight;
            Lbl_Email.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_Email.Location = new Point(37, 132);
            Lbl_Email.Margin = new Padding(4, 0, 4, 0);
            Lbl_Email.Name = "Lbl_Email";
            Lbl_Email.Size = new Size(219, 38);
            Lbl_Email.TabIndex = 13;
            Lbl_Email.Text = "Enter your Email";
            // 
            // Tb_Password
            // 
            Tb_Password.BorderStyle = BorderStyle.FixedSingle;
            Tb_Password.Font = new Font("Segoe UI", 15F);
            Tb_Password.Location = new Point(616, 174);
            Tb_Password.Margin = new Padding(4);
            Tb_Password.Name = "Tb_Password";
            Tb_Password.PlaceholderText = "Password";
            Tb_Password.Size = new Size(530, 47);
            Tb_Password.TabIndex = 12;
            Tb_Password.UseSystemPasswordChar = true;
            // 
            // Btn_Continue
            // 
            Btn_Continue.BackColor = Color.YellowGreen;
            Btn_Continue.FlatAppearance.BorderColor = Color.Black;
            Btn_Continue.FlatStyle = FlatStyle.Flat;
            Btn_Continue.Font = new Font("Snap ITC", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Continue.ForeColor = Color.Gold;
            Btn_Continue.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_Continue.Location = new Point(313, 492);
            Btn_Continue.Margin = new Padding(4);
            Btn_Continue.Name = "Btn_Continue";
            Btn_Continue.Size = new Size(530, 79);
            Btn_Continue.TabIndex = 11;
            Btn_Continue.Text = "Continue";
            Btn_Continue.UseVisualStyleBackColor = false;
            // 
            // Btn_BackToLogIn
            // 
            Btn_BackToLogIn.BackColor = Color.FromArgb(128, 128, 255);
            Btn_BackToLogIn.FlatAppearance.BorderColor = Color.Black;
            Btn_BackToLogIn.FlatStyle = FlatStyle.Flat;
            Btn_BackToLogIn.Font = new Font("Snap ITC", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_BackToLogIn.ForeColor = Color.Gold;
            Btn_BackToLogIn.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_BackToLogIn.Location = new Point(313, 635);
            Btn_BackToLogIn.Margin = new Padding(4);
            Btn_BackToLogIn.Name = "Btn_BackToLogIn";
            Btn_BackToLogIn.Size = new Size(530, 79);
            Btn_BackToLogIn.TabIndex = 10;
            Btn_BackToLogIn.Text = "Back to Log in";
            Btn_BackToLogIn.UseVisualStyleBackColor = false;
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(37, 174);
            Tb_Email.Margin = new Padding(4);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email addresss";
            Tb_Email.Size = new Size(530, 47);
            Tb_Email.TabIndex = 9;
            // 
            // Lbl_Register
            // 
            Lbl_Register.AutoSize = true;
            Lbl_Register.Font = new Font("Showcard Gothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_Register.ForeColor = Color.Gold;
            Lbl_Register.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_Register.Location = new Point(291, 18);
            Lbl_Register.Margin = new Padding(4, 0, 4, 0);
            Lbl_Register.Name = "Lbl_Register";
            Lbl_Register.Size = new Size(642, 60);
            Lbl_Register.TabIndex = 7;
            Lbl_Register.Text = "REGISTER YOUR ACCOUNT";
            // 
            // Lbl_Name
            // 
            Lbl_Name.AutoSize = true;
            Lbl_Name.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Name.ForeColor = SystemColors.ControlLightLight;
            Lbl_Name.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_Name.Location = new Point(37, 386);
            Lbl_Name.Margin = new Padding(4, 0, 4, 0);
            Lbl_Name.Name = "Lbl_Name";
            Lbl_Name.Size = new Size(227, 38);
            Lbl_Name.TabIndex = 14;
            Lbl_Name.Text = "Enter your Name";
            // 
            // Tb_Name
            // 
            Tb_Name.BorderStyle = BorderStyle.FixedSingle;
            Tb_Name.Font = new Font("Segoe UI", 15F);
            Tb_Name.Location = new Point(37, 428);
            Tb_Name.Margin = new Padding(4);
            Tb_Name.Name = "Tb_Name";
            Tb_Name.PlaceholderText = "Name";
            Tb_Name.Size = new Size(530, 47);
            Tb_Name.TabIndex = 15;
            // 
            // Tb_Username
            // 
            Tb_Username.BorderStyle = BorderStyle.FixedSingle;
            Tb_Username.Font = new Font("Segoe UI", 15F);
            Tb_Username.Location = new Point(37, 309);
            Tb_Username.Margin = new Padding(4);
            Tb_Username.Name = "Tb_Username";
            Tb_Username.PlaceholderText = "Username";
            Tb_Username.Size = new Size(530, 47);
            Tb_Username.TabIndex = 17;
            // 
            // Lbl_Username
            // 
            Lbl_Username.AutoSize = true;
            Lbl_Username.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Username.ForeColor = SystemColors.ControlLightLight;
            Lbl_Username.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_Username.Location = new Point(37, 267);
            Lbl_Username.Margin = new Padding(4, 0, 4, 0);
            Lbl_Username.Name = "Lbl_Username";
            Lbl_Username.Size = new Size(278, 38);
            Lbl_Username.TabIndex = 16;
            Lbl_Username.Text = "Enter your Username";
            // 
            // Lbl_Password
            // 
            Lbl_Password.AutoSize = true;
            Lbl_Password.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Password.ForeColor = SystemColors.ControlLightLight;
            Lbl_Password.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_Password.Location = new Point(616, 132);
            Lbl_Password.Margin = new Padding(4, 0, 4, 0);
            Lbl_Password.Name = "Lbl_Password";
            Lbl_Password.Size = new Size(268, 38);
            Lbl_Password.TabIndex = 18;
            Lbl_Password.Text = "Enter your Password";
            // 
            // Lbl_ConfirmPassword
            // 
            Lbl_ConfirmPassword.AutoSize = true;
            Lbl_ConfirmPassword.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_ConfirmPassword.ForeColor = SystemColors.ControlLightLight;
            Lbl_ConfirmPassword.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_ConfirmPassword.Location = new Point(616, 267);
            Lbl_ConfirmPassword.Margin = new Padding(4, 0, 4, 0);
            Lbl_ConfirmPassword.Name = "Lbl_ConfirmPassword";
            Lbl_ConfirmPassword.Size = new Size(303, 38);
            Lbl_ConfirmPassword.TabIndex = 20;
            Lbl_ConfirmPassword.Text = "Confirm your Password";
            // 
            // Tb_ConfirmPassword
            // 
            Tb_ConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            Tb_ConfirmPassword.Font = new Font("Segoe UI", 15F);
            Tb_ConfirmPassword.Location = new Point(616, 309);
            Tb_ConfirmPassword.Margin = new Padding(4);
            Tb_ConfirmPassword.Name = "Tb_ConfirmPassword";
            Tb_ConfirmPassword.PlaceholderText = "Password";
            Tb_ConfirmPassword.Size = new Size(530, 47);
            Tb_ConfirmPassword.TabIndex = 19;
            Tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // Lbl_AlreadyHaveAnAccount
            // 
            Lbl_AlreadyHaveAnAccount.AutoSize = true;
            Lbl_AlreadyHaveAnAccount.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_AlreadyHaveAnAccount.ForeColor = SystemColors.ControlLightLight;
            Lbl_AlreadyHaveAnAccount.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_AlreadyHaveAnAccount.Location = new Point(382, 593);
            Lbl_AlreadyHaveAnAccount.Margin = new Padding(4, 0, 4, 0);
            Lbl_AlreadyHaveAnAccount.Name = "Lbl_AlreadyHaveAnAccount";
            Lbl_AlreadyHaveAnAccount.Size = new Size(382, 38);
            Lbl_AlreadyHaveAnAccount.TabIndex = 21;
            Lbl_AlreadyHaveAnAccount.Text = "You already have an account?";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-3, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1190, 764);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1189, 761);
            Controls.Add(Lbl_AlreadyHaveAnAccount);
            Controls.Add(Lbl_ConfirmPassword);
            Controls.Add(Tb_ConfirmPassword);
            Controls.Add(Lbl_Password);
            Controls.Add(Tb_Username);
            Controls.Add(Lbl_Username);
            Controls.Add(Tb_Name);
            Controls.Add(Lbl_Name);
            Controls.Add(Lbl_Email);
            Controls.Add(Tb_Password);
            Controls.Add(Btn_Continue);
            Controls.Add(Btn_BackToLogIn);
            Controls.Add(Tb_Email);
            Controls.Add(Lbl_Register);
            Controls.Add(pictureBox1);
            Margin = new Padding(4);
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RegisterForm";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label Lbl_Email;
        private TextBox Tb_Password;
        private Button Btn_Continue;
        private Button Btn_BackToLogIn;
        private TextBox Tb_Email;
        private Label Lbl_Register;
        private Label Lbl_Name;
        private TextBox Tb_Name;
        private TextBox Tb_Username;
        private Label Lbl_Username;
        private Label Lbl_Password;
        private Label Lbl_ConfirmPassword;
        private TextBox Tb_ConfirmPassword;
        private Label Lbl_AlreadyHaveAnAccount;
        private PictureBox pictureBox1;
    }
}