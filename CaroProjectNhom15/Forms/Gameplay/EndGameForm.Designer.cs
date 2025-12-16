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
            Lbl_WinnerName = new Label();
            Btn_Exit = new Button();
            SuspendLayout();
            // 
            // Lbl_ResultTitle
            // 
            Lbl_ResultTitle.AutoSize = true;
            Lbl_ResultTitle.Font = new Font("Arial", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_ResultTitle.Location = new Point(12, 9);
            Lbl_ResultTitle.Name = "Lbl_ResultTitle";
            Lbl_ResultTitle.Size = new Size(235, 55);
            Lbl_ResultTitle.TabIndex = 0;
            Lbl_ResultTitle.Text = "KẾT QUẢ";
            Lbl_ResultTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Lbl_WinnerName
            // 
            Lbl_WinnerName.AutoSize = true;
            Lbl_WinnerName.Font = new Font("Arial", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Lbl_WinnerName.Location = new Point(12, 64);
            Lbl_WinnerName.Name = "Lbl_WinnerName";
            Lbl_WinnerName.Size = new Size(355, 39);
            Lbl_WinnerName.TabIndex = 1;
            Lbl_WinnerName.Text = "Tên người chiến thắng";
            Lbl_WinnerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Btn_Exit
            // 
            Btn_Exit.Location = new Point(221, 146);
            Btn_Exit.Margin = new Padding(3, 4, 3, 4);
            Btn_Exit.Name = "Btn_Exit";
            Btn_Exit.Size = new Size(255, 91);
            Btn_Exit.TabIndex = 2;
            Btn_Exit.Text = "Đóng game";
            Btn_Exit.UseVisualStyleBackColor = true;
            Btn_Exit.Click += Btn_Exit_Click;
            // 
            // EndGameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(695, 300);
            Controls.Add(Btn_Exit);
            Controls.Add(Lbl_WinnerName);
            Controls.Add(Lbl_ResultTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EndGameForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Kết thúc Game";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Lbl_ResultTitle;
        private System.Windows.Forms.Label Lbl_WinnerName;
        private System.Windows.Forms.Button Btn_Exit;
    }
}