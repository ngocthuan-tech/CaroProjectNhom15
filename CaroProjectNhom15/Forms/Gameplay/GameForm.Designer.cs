// GameForm.Designer.cs
namespace CaroProjectNhom15.Forms
{
    // ĐÃ ĐỔI TÊN THÀNH GameForm
    partial class GameForm
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
            Pnl_BoardContainer = new Panel();
            Pnl_RightSide = new Panel();
            Grp_GameInfo = new GroupBox();
            Btn_Exit = new Button();
            Lbl_CurrentTurn = new Label();
            Lbl_TurnTitle = new Label();
            Lbl_PlayerO_Name = new Label();
            Lbl_PlayerO_Title = new Label();
            Lbl_PlayerX_Name = new Label();
            Lbl_PlayerX_Title = new Label();
            Pnl_RightSide.SuspendLayout();
            Grp_GameInfo.SuspendLayout();
            SuspendLayout();
            // 
            // Pnl_BoardContainer
            // 
            Pnl_BoardContainer.AutoScroll = true;
            Pnl_BoardContainer.BorderStyle = BorderStyle.FixedSingle;
            Pnl_BoardContainer.Dock = DockStyle.Fill;
            Pnl_BoardContainer.Location = new Point(0, 0);
            Pnl_BoardContainer.Margin = new Padding(3, 4, 3, 4);
            Pnl_BoardContainer.Name = "Pnl_BoardContainer";
            Pnl_BoardContainer.Size = new Size(734, 703);
            Pnl_BoardContainer.TabIndex = 0;
            // 
            // Pnl_RightSide
            // 
            Pnl_RightSide.Controls.Add(Grp_GameInfo);
            Pnl_RightSide.Dock = DockStyle.Right;
            Pnl_RightSide.Location = new Point(734, 0);
            Pnl_RightSide.Margin = new Padding(3, 4, 3, 4);
            Pnl_RightSide.Name = "Pnl_RightSide";
            Pnl_RightSide.Size = new Size(343, 703);
            Pnl_RightSide.TabIndex = 1;
            // 
            // Grp_GameInfo
            // 
            Grp_GameInfo.Controls.Add(Btn_Exit);
            Grp_GameInfo.Controls.Add(Lbl_CurrentTurn);
            Grp_GameInfo.Controls.Add(Lbl_TurnTitle);
            Grp_GameInfo.Controls.Add(Lbl_PlayerO_Name);
            Grp_GameInfo.Controls.Add(Lbl_PlayerO_Title);
            Grp_GameInfo.Controls.Add(Lbl_PlayerX_Name);
            Grp_GameInfo.Controls.Add(Lbl_PlayerX_Title);
            Grp_GameInfo.Dock = DockStyle.Top;
            Grp_GameInfo.Location = new Point(0, 0);
            Grp_GameInfo.Margin = new Padding(3, 4, 3, 4);
            Grp_GameInfo.Name = "Grp_GameInfo";
            Grp_GameInfo.Padding = new Padding(3, 4, 3, 4);
            Grp_GameInfo.Size = new Size(343, 267);
            Grp_GameInfo.TabIndex = 0;
            Grp_GameInfo.TabStop = false;
            Grp_GameInfo.Text = "Thông Tin Ván Đấu";
            // 
            // Btn_Exit
            // 
            Btn_Exit.Location = new Point(97, 200);
            Btn_Exit.Margin = new Padding(3, 4, 3, 4);
            Btn_Exit.Name = "Btn_Exit";
            Btn_Exit.Size = new Size(149, 47);
            Btn_Exit.TabIndex = 6;
            Btn_Exit.Text = "Thoát Phòng";
            Btn_Exit.UseVisualStyleBackColor = true;
            Btn_Exit.Click += Btn_Exit_Click;
            // 
            // Lbl_CurrentTurn
            // 
            Lbl_CurrentTurn.AutoSize = true;
            Lbl_CurrentTurn.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            Lbl_CurrentTurn.ForeColor = Color.Red;
            Lbl_CurrentTurn.Location = new Point(160, 112);
            Lbl_CurrentTurn.Name = "Lbl_CurrentTurn";
            Lbl_CurrentTurn.Size = new Size(100, 28);
            Lbl_CurrentTurn.TabIndex = 5;
            Lbl_CurrentTurn.Text = "(Chưa rõ)";
            // 
            // Lbl_TurnTitle
            // 
            Lbl_TurnTitle.AutoSize = true;
            Lbl_TurnTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            Lbl_TurnTitle.Location = new Point(17, 117);
            Lbl_TurnTitle.Name = "Lbl_TurnTitle";
            Lbl_TurnTitle.Size = new Size(137, 23);
            Lbl_TurnTitle.TabIndex = 4;
            Lbl_TurnTitle.Text = "LƯỢT HIỆN TẠI:";
            // 
            // Lbl_PlayerO_Name
            // 
            Lbl_PlayerO_Name.AutoSize = true;
            Lbl_PlayerO_Name.Location = new Point(160, 67);
            Lbl_PlayerO_Name.Name = "Lbl_PlayerO_Name";
            Lbl_PlayerO_Name.Size = new Size(100, 20);
            Lbl_PlayerO_Name.TabIndex = 3;
            Lbl_PlayerO_Name.Text = "Người Chơi O";
            // 
            // Lbl_PlayerO_Title
            // 
            Lbl_PlayerO_Title.AutoSize = true;
            Lbl_PlayerO_Title.Location = new Point(17, 67);
            Lbl_PlayerO_Title.Name = "Lbl_PlayerO_Title";
            Lbl_PlayerO_Title.Size = new Size(116, 20);
            Lbl_PlayerO_Title.TabIndex = 2;
            Lbl_PlayerO_Title.Text = "Khách (Quân O):";
            // 
            // Lbl_PlayerX_Name
            // 
            Lbl_PlayerX_Name.AutoSize = true;
            Lbl_PlayerX_Name.Location = new Point(160, 36);
            Lbl_PlayerX_Name.Name = "Lbl_PlayerX_Name";
            Lbl_PlayerX_Name.Size = new Size(98, 20);
            Lbl_PlayerX_Name.TabIndex = 3;
            Lbl_PlayerX_Name.Text = "Người Chơi X";
            // 
            // Lbl_PlayerX_Title
            // 
            Lbl_PlayerX_Title.Location = new Point(17, 36);
            Lbl_PlayerX_Title.Name = "Lbl_PlayerX_Title";
            Lbl_PlayerX_Title.Size = new Size(114, 31);
            Lbl_PlayerX_Title.TabIndex = 7;
            Lbl_PlayerX_Title.Text = "Chủ phòng (X):";
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1077, 703);
            Controls.Add(Pnl_BoardContainer);
            Controls.Add(Pnl_RightSide);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "GameForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Game Caro";
            Pnl_RightSide.ResumeLayout(false);
            Grp_GameInfo.ResumeLayout(false);
            Grp_GameInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        // ... (Khu vực private controls) ...
        private System.Windows.Forms.Panel Pnl_BoardContainer;
        private System.Windows.Forms.Panel Pnl_RightSide;
        private System.Windows.Forms.GroupBox Grp_GameInfo;
        private System.Windows.Forms.Label Lbl_PlayerX_Name;
        private System.Windows.Forms.Label Lbl_PlayerX_Title;
        private System.Windows.Forms.Label Lbl_PlayerO_Name;
        private System.Windows.Forms.Label Lbl_PlayerO_Title;
        private System.Windows.Forms.Label Lbl_CurrentTurn;
        private System.Windows.Forms.Label Lbl_TurnTitle;
        private System.Windows.Forms.Button Btn_Exit;
    }
}