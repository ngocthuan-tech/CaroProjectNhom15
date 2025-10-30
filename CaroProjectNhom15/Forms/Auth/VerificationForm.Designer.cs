namespace CaroProjectNhom15.Forms.Auth
{
    partial class VerificationForm
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
            Lbl_Verification = new Label();
            Lbl_EnterOTPCode = new Label();
            Tb_OTPCode = new TextBox();
            Btn_Verify = new Button();
            SuspendLayout();
            // 
            // Lbl_Verification
            // 
            Lbl_Verification.AutoSize = true;
            Lbl_Verification.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_Verification.ForeColor = Color.DimGray;
            Lbl_Verification.Location = new Point(220, 41);
            Lbl_Verification.Name = "Lbl_Verification";
            Lbl_Verification.Size = new Size(292, 54);
            Lbl_Verification.TabIndex = 8;
            Lbl_Verification.Text = "VERIFICATION";
            // 
            // Lbl_EnterOTPCode
            // 
            Lbl_EnterOTPCode.AutoSize = true;
            Lbl_EnterOTPCode.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_EnterOTPCode.Location = new Point(69, 141);
            Lbl_EnterOTPCode.Name = "Lbl_EnterOTPCode";
            Lbl_EnterOTPCode.Size = new Size(420, 31);
            Lbl_EnterOTPCode.TabIndex = 20;
            Lbl_EnterOTPCode.Text = "Enter The OTP Code Sent To Your Email: ";
            // 
            // Tb_OTPCode
            // 
            Tb_OTPCode.BorderStyle = BorderStyle.FixedSingle;
            Tb_OTPCode.Font = new Font("Segoe UI", 15F);
            Tb_OTPCode.Location = new Point(69, 195);
            Tb_OTPCode.Name = "Tb_OTPCode";
            Tb_OTPCode.PlaceholderText = "OTP";
            Tb_OTPCode.Size = new Size(585, 41);
            Tb_OTPCode.TabIndex = 19;
            // 
            // Btn_Verify
            // 
            Btn_Verify.BackColor = Color.FromArgb(0, 192, 0);
            Btn_Verify.FlatAppearance.BorderColor = Color.Black;
            Btn_Verify.FlatStyle = FlatStyle.Flat;
            Btn_Verify.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Verify.ForeColor = Color.White;
            Btn_Verify.Location = new Point(151, 259);
            Btn_Verify.Name = "Btn_Verify";
            Btn_Verify.Size = new Size(424, 63);
            Btn_Verify.TabIndex = 21;
            Btn_Verify.Text = "Verify OTP Code";
            Btn_Verify.UseVisualStyleBackColor = false;
            // 
            // VerificationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(734, 448);
            Controls.Add(Btn_Verify);
            Controls.Add(Lbl_EnterOTPCode);
            Controls.Add(Tb_OTPCode);
            Controls.Add(Lbl_Verification);
            Name = "VerificationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VerificationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Lbl_Verification;
        private Label Lbl_EnterOTPCode;
        private TextBox Tb_OTPCode;
        private Button Btn_Verify;
    }
}