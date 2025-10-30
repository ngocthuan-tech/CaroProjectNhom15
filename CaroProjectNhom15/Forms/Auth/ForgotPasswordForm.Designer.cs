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
            Btn_Continue = new Button();
            Lbl_EnterEmail = new Label();
            Tb_Email = new TextBox();
            Lbl_ForgotPassword = new Label();
            LnkL_BackToLogIn = new LinkLabel();
            LnkL_Register = new LinkLabel();
            SuspendLayout();
            // 
            // Btn_Continue
            // 
            Btn_Continue.BackColor = Color.Blue;
            Btn_Continue.FlatAppearance.BorderColor = Color.Black;
            Btn_Continue.FlatStyle = FlatStyle.Flat;
            Btn_Continue.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Continue.ForeColor = Color.White;
            Btn_Continue.Location = new Point(245, 249);
            Btn_Continue.Name = "Btn_Continue";
            Btn_Continue.Size = new Size(424, 63);
            Btn_Continue.TabIndex = 25;
            Btn_Continue.Text = "Continue";
            Btn_Continue.UseVisualStyleBackColor = false;
            // 
            // Lbl_EnterEmail
            // 
            Lbl_EnterEmail.AutoSize = true;
            Lbl_EnterEmail.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_EnterEmail.Location = new Point(161, 131);
            Lbl_EnterEmail.Name = "Lbl_EnterEmail";
            Lbl_EnterEmail.Size = new Size(191, 31);
            Lbl_EnterEmail.TabIndex = 24;
            Lbl_EnterEmail.Text = "Enter Your Email: ";
            // 
            // Tb_Email
            // 
            Tb_Email.BorderStyle = BorderStyle.FixedSingle;
            Tb_Email.Font = new Font("Segoe UI", 15F);
            Tb_Email.Location = new Point(161, 185);
            Tb_Email.Name = "Tb_Email";
            Tb_Email.PlaceholderText = "Email";
            Tb_Email.Size = new Size(585, 41);
            Tb_Email.TabIndex = 23;
            // 
            // Lbl_ForgotPassword
            // 
            Lbl_ForgotPassword.AutoSize = true;
            Lbl_ForgotPassword.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_ForgotPassword.ForeColor = Color.DimGray;
            Lbl_ForgotPassword.Location = new Point(193, 40);
            Lbl_ForgotPassword.Name = "Lbl_ForgotPassword";
            Lbl_ForgotPassword.Size = new Size(530, 54);
            Lbl_ForgotPassword.TabIndex = 22;
            Lbl_ForgotPassword.Text = "FORGOT YOUR PASSWORD";
            // 
            // LnkL_BackToLogIn
            // 
            LnkL_BackToLogIn.AutoSize = true;
            LnkL_BackToLogIn.Font = new Font("Segoe UI", 13.8F);
            LnkL_BackToLogIn.Location = new Point(161, 355);
            LnkL_BackToLogIn.Name = "LnkL_BackToLogIn";
            LnkL_BackToLogIn.Size = new Size(167, 31);
            LnkL_BackToLogIn.TabIndex = 26;
            LnkL_BackToLogIn.TabStop = true;
            LnkL_BackToLogIn.Text = "Back to Log in?";
            // 
            // LnkL_Register
            // 
            LnkL_Register.AutoSize = true;
            LnkL_Register.Font = new Font("Segoe UI", 13.8F);
            LnkL_Register.Location = new Point(458, 355);
            LnkL_Register.Name = "LnkL_Register";
            LnkL_Register.Size = new Size(288, 31);
            LnkL_Register.TabIndex = 27;
            LnkL_Register.TabStop = true;
            LnkL_Register.Text = "Don't have an account yet?";
            // 
            // ForgotPasswordForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(915, 475);
            Controls.Add(LnkL_Register);
            Controls.Add(LnkL_BackToLogIn);
            Controls.Add(Btn_Continue);
            Controls.Add(Lbl_EnterEmail);
            Controls.Add(Tb_Email);
            Controls.Add(Lbl_ForgotPassword);
            Name = "ForgotPasswordForm";
            Text = "ForgotPasswordForm";
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
    }
}