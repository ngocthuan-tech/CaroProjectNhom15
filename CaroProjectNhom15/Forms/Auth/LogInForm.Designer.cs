using System.Windows.Forms;
using System.Drawing;

namespace CaroProjectNhom15.Forms.Auth
{
    partial class LoginForm: System.Windows.Forms.Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            Lbl_LogIn = new Label();
            LnkL_ForgotPassword = new LinkLabel();
            Tb_Email = new TextBox();
            Btn_LogIn = new Button();
            Btn_CreateNewAccount = new Button();
            Tb_Password = new TextBox();
            Lbl_CreateNewAccount = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Lbl_LogIn
            // 
            Lbl_LogIn.AutoSize = true;
            Lbl_LogIn.Font = new Font("Showcard Gothic", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_LogIn.ForeColor = Color.Gold;
            Lbl_LogIn.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_LogIn.Location = new Point(495, 37);
            Lbl_LogIn.Margin = new Padding(4, 0, 4, 0);
            Lbl_LogIn.Name = "Lbl_LogIn";
            Lbl_LogIn.Size = new Size(166, 60);
            Lbl_LogIn.TabIndex = 0;
            Lbl_LogIn.Text = "LOGIN";
            // 
            // LnkL_ForgotPassword
            // 
            LnkL_ForgotPassword.AutoSize = true;
            LnkL_ForgotPassword.BackColor = SystemColors.Control;
            LnkL_ForgotPassword.Font = new Font("Segoe UI", 13.8F);
            LnkL_ForgotPassword.ForeColor = Color.ForestGreen;
            LnkL_ForgotPassword.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            LnkL_ForgotPassword.LinkColor = Color.Goldenrod;
            LnkL_ForgotPassword.Location = new Point(447, 385);
            LnkL_ForgotPassword.Margin = new Padding(4, 0, 4, 0);
            LnkL_ForgotPassword.Name = "LnkL_ForgotPassword";
            LnkL_ForgotPassword.Size = new Size(275, 38);
            LnkL_ForgotPassword.TabIndex = 1;
            LnkL_ForgotPassword.TabStop = true;
            LnkL_ForgotPassword.Text = "Forgotten password?";
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(317, 123);
            Tb_Email.Margin = new Padding(4, 4, 4, 4);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email addresss";
            Tb_Email.Size = new Size(530, 47);
            Tb_Email.TabIndex = 2;
            // 
            // Btn_LogIn
            // 
            Btn_LogIn.BackColor = Color.FromArgb(128, 128, 255);
            Btn_LogIn.FlatAppearance.BorderColor = Color.Black;
            Btn_LogIn.FlatStyle = FlatStyle.Flat;
            Btn_LogIn.Font = new Font("Snap ITC", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_LogIn.ForeColor = Color.Gold;
            Btn_LogIn.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_LogIn.Location = new Point(317, 284);
            Btn_LogIn.Margin = new Padding(4, 4, 4, 4);
            Btn_LogIn.Name = "Btn_LogIn";
            Btn_LogIn.Size = new Size(530, 79);
            Btn_LogIn.TabIndex = 3;
            Btn_LogIn.Text = "Log in";
            Btn_LogIn.UseVisualStyleBackColor = false;
            // 
            // Btn_CreateNewAccount
            // 
            Btn_CreateNewAccount.BackColor = Color.YellowGreen;
            Btn_CreateNewAccount.FlatAppearance.BorderColor = Color.Black;
            Btn_CreateNewAccount.FlatStyle = FlatStyle.Flat;
            Btn_CreateNewAccount.Font = new Font("Snap ITC", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_CreateNewAccount.ForeColor = Color.Gold;
            Btn_CreateNewAccount.Image = Properties.Resources.giờ_hãy_tạo_một_nền_màu_nâu_đất__đồ_hoạ_2đ;
            Btn_CreateNewAccount.Location = new Point(317, 536);
            Btn_CreateNewAccount.Margin = new Padding(4, 4, 4, 4);
            Btn_CreateNewAccount.Name = "Btn_CreateNewAccount";
            Btn_CreateNewAccount.Size = new Size(530, 79);
            Btn_CreateNewAccount.TabIndex = 4;
            Btn_CreateNewAccount.Text = "Create new account";
            Btn_CreateNewAccount.UseVisualStyleBackColor = false;
            Btn_CreateNewAccount.Click += Btn_CreateNewAccount_Click;
            // 
            // Tb_Password
            // 
            Tb_Password.BorderStyle = BorderStyle.FixedSingle;
            Tb_Password.Font = new Font("Segoe UI", 15F);
            Tb_Password.Location = new Point(317, 197);
            Tb_Password.Margin = new Padding(4, 4, 4, 4);
            Tb_Password.Name = "Tb_Password";
            Tb_Password.PlaceholderText = "Password";
            Tb_Password.Size = new Size(530, 47);
            Tb_Password.TabIndex = 5;
            Tb_Password.UseSystemPasswordChar = true;
            // 
            // Lbl_CreateNewAccount
            // 
            Lbl_CreateNewAccount.AutoSize = true;
            Lbl_CreateNewAccount.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_CreateNewAccount.ForeColor = Color.FloralWhite;
            Lbl_CreateNewAccount.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Lbl_CreateNewAccount.Location = new Point(317, 482);
            Lbl_CreateNewAccount.Margin = new Padding(4, 0, 4, 0);
            Lbl_CreateNewAccount.Name = "Lbl_CreateNewAccount";
            Lbl_CreateNewAccount.Size = new Size(352, 38);
            Lbl_CreateNewAccount.TabIndex = 6;
            Lbl_CreateNewAccount.Text = "Don't have an account yet?";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-2, -5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1153, 724);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1147, 710);
            Controls.Add(Lbl_CreateNewAccount);
            Controls.Add(Tb_Password);
            Controls.Add(Btn_CreateNewAccount);
            Controls.Add(Btn_LogIn);
            Controls.Add(Tb_Email);
            Controls.Add(LnkL_ForgotPassword);
            Controls.Add(Lbl_LogIn);
            Controls.Add(pictureBox1);
            Margin = new Padding(4, 4, 4, 4);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LOGIN";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Lbl_LogIn;
        private LinkLabel LnkL_ForgotPassword;
        private TextBox Tb_Email;
        private Button Btn_LogIn;
        private Button Btn_CreateNewAccount;
        private TextBox Tb_Password;
        private Label Lbl_CreateNewAccount;
        private PictureBox pictureBox1;
    }
}
