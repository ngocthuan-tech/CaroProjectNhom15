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
            this.Lbl_Email = new Label();
            Tb_Password = new TextBox();
            this.Btn_Continue = new Button();
            this.Btn_BackToLogIn = new Button();
            Tb_Email = new TextBox();
            this.Lbl_Register = new Label();
            this.Lbl_Name = new Label();
            this.Tb_Name = new TextBox();
            this.Tb_Username = new TextBox();
            this.Lbl_Username = new Label();
            this.Lbl_Password = new Label();
            this.Lbl_ConfirmPassword = new Label();
            this.Tb_ConfirmPassword = new TextBox();
            Lbl_AlreadyHaveAnAccount = new Label(); 
            SuspendLayout();
            // 
            // Lbl_Email
            // 
            this.Lbl_Email.AutoSize = true;
            this.Lbl_Email.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Lbl_Email.Location = new Point(51, 109);
            this.Lbl_Email.Name = "Lbl_Email";
            this.Lbl_Email.Size = new Size(180, 31);
            this.Lbl_Email.TabIndex = 13;
            this.Lbl_Email.Text = "Enter your Email";
            // 
            // Tb_Password
            // 
            Tb_Password.BorderStyle = BorderStyle.FixedSingle;
            Tb_Password.Font = new Font("Segoe UI", 15F);
            Tb_Password.Location = new Point(562, 153);
            Tb_Password.Name = "Tb_Password";
            Tb_Password.PlaceholderText = "Password";
            Tb_Password.Size = new Size(424, 41);
            Tb_Password.TabIndex = 12;
            Tb_Password.UseSystemPasswordChar = true;
            // 
            // Btn_Continue
            // 
            this.Btn_Continue.BackColor = Color.YellowGreen;
            this.Btn_Continue.FlatAppearance.BorderColor = Color.Black;
            this.Btn_Continue.FlatStyle = FlatStyle.Flat;
            this.Btn_Continue.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_Continue.ForeColor = Color.White;
            this.Btn_Continue.Location = new Point(297, 448);
            this.Btn_Continue.Name = "Btn_Continue";
            this.Btn_Continue.Size = new Size(424, 63);
            this.Btn_Continue.TabIndex = 11;
            this.Btn_Continue.Text = "Continue";
            this.Btn_Continue.UseVisualStyleBackColor = false;
            // 
            // Btn_BackToLogIn
            // 
            this.Btn_BackToLogIn.BackColor = Color.FromArgb(128, 128, 255);
            this.Btn_BackToLogIn.FlatAppearance.BorderColor = Color.Black;
            this.Btn_BackToLogIn.FlatStyle = FlatStyle.Flat;
            this.Btn_BackToLogIn.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Btn_BackToLogIn.ForeColor = Color.White;
            this.Btn_BackToLogIn.Location = new Point(297, 606);
            this.Btn_BackToLogIn.Name = "Btn_BackToLogIn";
            this.Btn_BackToLogIn.Size = new Size(424, 63);
            this.Btn_BackToLogIn.TabIndex = 10;
            this.Btn_BackToLogIn.Text = "Back to Log in";
            this.Btn_BackToLogIn.UseVisualStyleBackColor = false;
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(51, 153);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email addresss";
            Tb_Email.Size = new Size(424, 41);
            Tb_Email.TabIndex = 9;
            // 
            // Lbl_Register
            // 
            this.Lbl_Register.AutoSize = true;
            this.Lbl_Register.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.Lbl_Register.ForeColor = Color.DimGray;
            this.Lbl_Register.Location = new Point(259, 24);
            this.Lbl_Register.Name = "Lbl_Register";
            this.Lbl_Register.Size = new Size(523, 54);
            this.Lbl_Register.TabIndex = 7;
            this.Lbl_Register.Text = "REGISTER YOUR ACCOUNT";
            // 
            // Lbl_Name
            // 
            this.Lbl_Name.AutoSize = true;
            this.Lbl_Name.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Lbl_Name.Location = new Point(51, 320);
            this.Lbl_Name.Name = "Lbl_Name";
            this.Lbl_Name.Size = new Size(185, 31);
            this.Lbl_Name.TabIndex = 14;
            this.Lbl_Name.Text = "Enter your Name";
            // 
            // Tb_Name
            // 
            this.Tb_Name.BorderStyle = BorderStyle.FixedSingle;
            this.Tb_Name.Font = new Font("Segoe UI", 15F);
            this.Tb_Name.Location = new Point(51, 363);
            this.Tb_Name.Name = "Tb_Name";
            this.Tb_Name.PlaceholderText = "Name";
            this.Tb_Name.Size = new Size(424, 41);
            this.Tb_Name.TabIndex = 15;
            // 
            // Tb_Username
            // 
            this.Tb_Username.BorderStyle = BorderStyle.FixedSingle;
            this.Tb_Username.Font = new Font("Segoe UI", 15F);
            this.Tb_Username.Location = new Point(51, 261);
            this.Tb_Username.Name = "Tb_Username";
            this.Tb_Username.PlaceholderText = "Username";
            this.Tb_Username.Size = new Size(424, 41);
            this.Tb_Username.TabIndex = 17;
            // 
            // Lbl_Username
            // 
            this.Lbl_Username.AutoSize = true;
            this.Lbl_Username.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Lbl_Username.Location = new Point(51, 214);
            this.Lbl_Username.Name = "Lbl_Username";
            this.Lbl_Username.Size = new Size(227, 31);
            this.Lbl_Username.TabIndex = 16;
            this.Lbl_Username.Text = "Enter your Username";
            // 
            // Lbl_Password
            // 
            this.Lbl_Password.AutoSize = true;
            this.Lbl_Password.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Lbl_Password.Location = new Point(562, 109);
            this.Lbl_Password.Name = "Lbl_Password";
            this.Lbl_Password.Size = new Size(220, 31);
            this.Lbl_Password.TabIndex = 18;
            this.Lbl_Password.Text = "Enter your Password";
            // 
            // Lbl_ConfirmPassword
            // 
            this.Lbl_ConfirmPassword.AutoSize = true;
            this.Lbl_ConfirmPassword.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.Lbl_ConfirmPassword.Location = new Point(562, 217);
            this.Lbl_ConfirmPassword.Name = "Lbl_ConfirmPassword";
            this.Lbl_ConfirmPassword.Size = new Size(248, 31);
            this.Lbl_ConfirmPassword.TabIndex = 20;
            this.Lbl_ConfirmPassword.Text = "Confirm your Password";
            // 
            // Tb_ConfirmPassword
            // 
            this.Tb_ConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            this.Tb_ConfirmPassword.Font = new Font("Segoe UI", 15F);
            this.Tb_ConfirmPassword.Location = new Point(562, 261);
            this.Tb_ConfirmPassword.Name = "Tb_ConfirmPassword";
            this.Tb_ConfirmPassword.PlaceholderText = "Password";
            this.Tb_ConfirmPassword.Size = new Size(424, 41);
            this.Tb_ConfirmPassword.TabIndex = 19;
            this.Tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // Lbl_AlreadyHaveAnAccount
            // 
            Lbl_AlreadyHaveAnAccount.AutoSize = true;
            Lbl_AlreadyHaveAnAccount.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_AlreadyHaveAnAccount.Location = new Point(347, 555);
            Lbl_AlreadyHaveAnAccount.Name = "Lbl_AlreadyHaveAnAccount";
            Lbl_AlreadyHaveAnAccount.Size = new Size(314, 31);
            Lbl_AlreadyHaveAnAccount.TabIndex = 21;
            Lbl_AlreadyHaveAnAccount.Text = "You already have an account?";
            // 
            // RegisterForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 732);
            Controls.Add(Lbl_AlreadyHaveAnAccount);
            Controls.Add(this.Lbl_ConfirmPassword);
            Controls.Add(this.Tb_ConfirmPassword);
            Controls.Add(this.Lbl_Password);
            Controls.Add(this.Tb_Username);
            Controls.Add(this.Lbl_Username);
            Controls.Add(this.Tb_Name);
            Controls.Add(this.Lbl_Name);
            Controls.Add(this.Lbl_Email);
            Controls.Add(Tb_Password);
            Controls.Add(this.Btn_Continue);
            Controls.Add(this.Btn_BackToLogIn);
            Controls.Add(Tb_Email);
            Controls.Add(this.Lbl_Register); 
            Name = "RegisterForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "RegisterForm";
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
    }
}