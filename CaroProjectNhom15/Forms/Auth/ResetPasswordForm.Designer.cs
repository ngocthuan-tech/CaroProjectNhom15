namespace CaroProjectNhom15.Forms.Auth
{
    partial class ResetPasswordForm
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
            Lbl_ConfirmPassword = new Label();
            Tb_ConfirmPassword = new TextBox();
            Lbl_Password = new Label();
            Tb_Password = new TextBox();
            Lbl_Register = new Label();
            Btn_ResetPassword = new Button();
            LnkL_BackToLogIn = new LinkLabel();
            SuspendLayout();
            // 
            // Lbl_ConfirmPassword
            // 
            Lbl_ConfirmPassword.AutoSize = true;
            Lbl_ConfirmPassword.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_ConfirmPassword.Location = new Point(186, 224);
            Lbl_ConfirmPassword.Name = "Lbl_ConfirmPassword";
            Lbl_ConfirmPassword.Size = new Size(207, 25);
            Lbl_ConfirmPassword.TabIndex = 25;
            Lbl_ConfirmPassword.Text = "Confirm your Password";
            // 
            // Tb_ConfirmPassword
            // 
            Tb_ConfirmPassword.BorderStyle = BorderStyle.FixedSingle;
            Tb_ConfirmPassword.Font = new Font("Segoe UI", 15F);
            Tb_ConfirmPassword.Location = new Point(186, 268);
            Tb_ConfirmPassword.Name = "Tb_ConfirmPassword";
            Tb_ConfirmPassword.PlaceholderText = "Password";
            Tb_ConfirmPassword.Size = new Size(424, 34);
            Tb_ConfirmPassword.TabIndex = 24;
            Tb_ConfirmPassword.UseSystemPasswordChar = true;
            // 
            // Lbl_Password
            // 
            Lbl_Password.AutoSize = true;
            Lbl_Password.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_Password.Location = new Point(186, 116);
            Lbl_Password.Name = "Lbl_Password";
            Lbl_Password.Size = new Size(183, 25);
            Lbl_Password.TabIndex = 23;
            Lbl_Password.Text = "Enter your Password";
            // 
            // Tb_Password
            // 
            Tb_Password.BorderStyle = BorderStyle.FixedSingle;
            Tb_Password.Font = new Font("Segoe UI", 15F);
            Tb_Password.Location = new Point(186, 160);
            Tb_Password.Name = "Tb_Password";
            Tb_Password.PlaceholderText = "Password";
            Tb_Password.Size = new Size(424, 34);
            Tb_Password.TabIndex = 22;
            Tb_Password.UseSystemPasswordChar = true;
            // 
            // Lbl_Register
            // 
            Lbl_Register.AutoSize = true;
            Lbl_Register.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_Register.ForeColor = Color.DimGray;
            Lbl_Register.Location = new Point(197, 28);
            Lbl_Register.Name = "Lbl_Register";
            Lbl_Register.Size = new Size(394, 45);
            Lbl_Register.TabIndex = 21;
            Lbl_Register.Text = "RESET YOUR PASSWORD";
            // 
            // Btn_ResetPassword
            // 
            Btn_ResetPassword.BackColor = Color.Blue;
            Btn_ResetPassword.FlatAppearance.BorderColor = Color.Black;
            Btn_ResetPassword.FlatStyle = FlatStyle.Flat;
            Btn_ResetPassword.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_ResetPassword.ForeColor = Color.White;
            Btn_ResetPassword.Location = new Point(186, 347);
            Btn_ResetPassword.Name = "Btn_ResetPassword";
            Btn_ResetPassword.Size = new Size(424, 63);
            Btn_ResetPassword.TabIndex = 26;
            Btn_ResetPassword.Text = "Reset Password";
            Btn_ResetPassword.UseVisualStyleBackColor = false;
            // 
            // LnkL_BackToLogIn
            // 
            LnkL_BackToLogIn.AutoSize = true;
            LnkL_BackToLogIn.Font = new Font("Segoe UI", 13.8F);
            LnkL_BackToLogIn.Location = new Point(311, 435);
            LnkL_BackToLogIn.Name = "LnkL_BackToLogIn";
            LnkL_BackToLogIn.Size = new Size(139, 25);
            LnkL_BackToLogIn.TabIndex = 27;
            LnkL_BackToLogIn.TabStop = true;
            LnkL_BackToLogIn.Text = "Back to Log in?";
            // 
            // ResetPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 519);
            Controls.Add(LnkL_BackToLogIn);
            Controls.Add(Btn_ResetPassword);
            Controls.Add(Lbl_ConfirmPassword);
            Controls.Add(Tb_ConfirmPassword);
            Controls.Add(Lbl_Password);
            Controls.Add(Tb_Password);
            Controls.Add(Lbl_Register);
            Name = "ResetPasswordForm";
            Text = "ResetPasswordForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Lbl_ConfirmPassword;
        private TextBox Tb_ConfirmPassword;
        private Label Lbl_Password;
        private TextBox Tb_Password;
        private Label Lbl_Register;
        private Button Btn_ResetPassword;
        private LinkLabel LnkL_BackToLogIn;
    }
}