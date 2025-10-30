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
            Lbl_LogIn = new Label();
            LnkL_ForgotPassword = new LinkLabel();
            Tb_Email = new TextBox();
            Btn_LogIn = new Button();
            Btn_CreateNewAccount = new Button();
            Tb_Password = new TextBox();
            Lbl_CreateNewAccount = new Label();
            SuspendLayout();
            // 
            // Lbl_LogIn
            // 
            Lbl_LogIn.AutoSize = true;
            Lbl_LogIn.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_LogIn.ForeColor = Color.DimGray;
            Lbl_LogIn.Location = new Point(205, 21);
            Lbl_LogIn.Name = "Lbl_LogIn";
            Lbl_LogIn.Size = new Size(145, 54);
            Lbl_LogIn.TabIndex = 0;
            Lbl_LogIn.Text = "LOGIN";
            // 
            // LnkL_ForgotPassword
            // 
            LnkL_ForgotPassword.AutoSize = true;
            LnkL_ForgotPassword.Font = new Font("Segoe UI", 13.8F);
            LnkL_ForgotPassword.Location = new Point(172, 298);
            LnkL_ForgotPassword.Name = "LnkL_ForgotPassword";
            LnkL_ForgotPassword.Size = new Size(228, 31);
            LnkL_ForgotPassword.TabIndex = 1;
            LnkL_ForgotPassword.TabStop = true;
            LnkL_ForgotPassword.Text = "Forgotten password?";
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(62, 90);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email addresss";
            Tb_Email.Size = new Size(424, 41);
            Tb_Email.TabIndex = 2;
            // 
            // Btn_LogIn
            // 
            Btn_LogIn.BackColor = Color.FromArgb(128, 128, 255);
            Btn_LogIn.FlatAppearance.BorderColor = Color.Black;
            Btn_LogIn.FlatStyle = FlatStyle.Flat;
            Btn_LogIn.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_LogIn.ForeColor = Color.White;
            Btn_LogIn.Location = new Point(62, 215);
            Btn_LogIn.Name = "Btn_LogIn";
            Btn_LogIn.Size = new Size(424, 63);
            Btn_LogIn.TabIndex = 3;
            Btn_LogIn.Text = "Log in";
            Btn_LogIn.UseVisualStyleBackColor = false;
            // 
            // Btn_CreateNewAccount
            // 
            Btn_CreateNewAccount.BackColor = Color.YellowGreen;
            Btn_CreateNewAccount.FlatAppearance.BorderColor = Color.Black;
            Btn_CreateNewAccount.FlatStyle = FlatStyle.Flat;
            Btn_CreateNewAccount.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_CreateNewAccount.ForeColor = Color.White;
            Btn_CreateNewAccount.Location = new Point(62, 420);
            Btn_CreateNewAccount.Name = "Btn_CreateNewAccount";
            Btn_CreateNewAccount.Size = new Size(424, 63);
            Btn_CreateNewAccount.TabIndex = 4;
            Btn_CreateNewAccount.Text = "Create new account";
            Btn_CreateNewAccount.UseVisualStyleBackColor = false;
            // 
            // Tb_Password
            // 
            Tb_Password.BorderStyle = BorderStyle.FixedSingle;
            Tb_Password.Font = new Font("Segoe UI", 15F);
            Tb_Password.Location = new Point(62, 149);
            Tb_Password.Name = "Tb_Password";
            Tb_Password.PlaceholderText = "Password";
            Tb_Password.Size = new Size(424, 41);
            Tb_Password.TabIndex = 5;
            Tb_Password.UseSystemPasswordChar = true;
            // 
            // Lbl_CreateNewAccount
            // 
            Lbl_CreateNewAccount.AutoSize = true;
            Lbl_CreateNewAccount.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_CreateNewAccount.Location = new Point(62, 375);
            Lbl_CreateNewAccount.Name = "Lbl_CreateNewAccount";
            Lbl_CreateNewAccount.Size = new Size(288, 31);
            Lbl_CreateNewAccount.TabIndex = 6;
            Lbl_CreateNewAccount.Text = "Don't have an account yet?";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 568);
            Controls.Add(Lbl_CreateNewAccount);
            Controls.Add(Tb_Password);
            Controls.Add(Btn_CreateNewAccount);
            Controls.Add(Btn_LogIn);
            Controls.Add(Tb_Email);
            Controls.Add(LnkL_ForgotPassword);
            Controls.Add(Lbl_LogIn);
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LOGIN";
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
    }
}
