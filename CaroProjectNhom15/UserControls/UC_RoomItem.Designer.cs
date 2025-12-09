namespace CaroProjectNhom15.UserControls
{
    partial class UC_RoomItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Lbl_RoomName = new Label();
            Lbl_Status = new Label();
            Lbl_HostName = new Label();
            Btn_Join = new Button();
            Lbl_RoomId = new Label();
            SuspendLayout();
            // 
            // Lbl_RoomName
            // 
            Lbl_RoomName.AutoSize = true;
            Lbl_RoomName.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            Lbl_RoomName.Location = new Point(34, 19);
            Lbl_RoomName.Name = "Lbl_RoomName";
            Lbl_RoomName.Size = new Size(111, 38);
            Lbl_RoomName.TabIndex = 0;
            Lbl_RoomName.Text = "Name: ";
            // 
            // Lbl_Status
            // 
            Lbl_Status.AutoSize = true;
            Lbl_Status.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            Lbl_Status.Location = new Point(34, 82);
            Lbl_Status.Name = "Lbl_Status";
            Lbl_Status.Size = new Size(114, 38);
            Lbl_Status.TabIndex = 1;
            Lbl_Status.Text = "Status: ";
            // 
            // Lbl_HostName
            // 
            Lbl_HostName.AutoSize = true;
            Lbl_HostName.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            Lbl_HostName.Location = new Point(34, 147);
            Lbl_HostName.Name = "Lbl_HostName";
            Lbl_HostName.Size = new Size(94, 38);
            Lbl_HostName.TabIndex = 4;
            Lbl_HostName.Text = "Host: ";
            // 
            // Btn_Join
            // 
            Btn_Join.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Join.Location = new Point(303, 277);
            Btn_Join.Name = "Btn_Join";
            Btn_Join.Size = new Size(123, 51);
            Btn_Join.TabIndex = 6;
            Btn_Join.Text = "Join";
            Btn_Join.UseVisualStyleBackColor = true;
            Btn_Join.Click += Btn_Join_Click;
            // 
            // Lbl_RoomId
            // 
            Lbl_RoomId.AutoSize = true;
            Lbl_RoomId.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold);
            Lbl_RoomId.Location = new Point(34, 212);
            Lbl_RoomId.Name = "Lbl_RoomId";
            Lbl_RoomId.Size = new Size(55, 38);
            Lbl_RoomId.TabIndex = 7;
            Lbl_RoomId.Text = "ID:";
            // 
            // UC_RoomItem
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Lbl_RoomId);
            Controls.Add(Btn_Join);
            Controls.Add(Lbl_HostName);
            Controls.Add(Lbl_Status);
            Controls.Add(Lbl_RoomName);
            Name = "UC_RoomItem";
            Size = new Size(750, 353);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Lbl_RoomName;
        private Label Lbl_Status;
        private Label Lbl_HostName;
        private Button Btn_Join;
        private Label Lbl_RoomId;
    }
}
