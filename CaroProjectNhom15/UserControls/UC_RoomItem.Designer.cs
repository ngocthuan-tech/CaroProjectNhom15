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
            Lbl_RoomName.Font = new Font("Stencil", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_RoomName.ForeColor = Color.Goldenrod;
            Lbl_RoomName.Location = new Point(23, 22);
            Lbl_RoomName.Margin = new Padding(4, 0, 4, 0);
            Lbl_RoomName.Name = "Lbl_RoomName";
            Lbl_RoomName.Size = new Size(111, 33);
            Lbl_RoomName.TabIndex = 0;
            Lbl_RoomName.Text = "Name: ";
            // 
            // Lbl_Status
            // 
            Lbl_Status.AutoSize = true;
            Lbl_Status.Font = new Font("Stencil", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_Status.ForeColor = Color.Goldenrod;
            Lbl_Status.Location = new Point(23, 80);
            Lbl_Status.Margin = new Padding(4, 0, 4, 0);
            Lbl_Status.Name = "Lbl_Status";
            Lbl_Status.Size = new Size(141, 33);
            Lbl_Status.TabIndex = 1;
            Lbl_Status.Text = "Status: ";
            // 
            // Lbl_HostName
            // 
            Lbl_HostName.AutoSize = true;
            Lbl_HostName.Font = new Font("Stencil", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_HostName.ForeColor = Color.Goldenrod;
            Lbl_HostName.Location = new Point(23, 144);
            Lbl_HostName.Margin = new Padding(4, 0, 4, 0);
            Lbl_HostName.Name = "Lbl_HostName";
            Lbl_HostName.Size = new Size(109, 33);
            Lbl_HostName.TabIndex = 4;
            Lbl_HostName.Text = "Host: ";
            // 
            // Btn_Join
            // 
            Btn_Join.Font = new Font("Stencil", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_Join.ForeColor = Color.Goldenrod;
            Btn_Join.Image = Properties.Resources.Gemini_Generated_Image_swcx3cswcx3cswcx;
            Btn_Join.Location = new Point(1015, 217);
            Btn_Join.Margin = new Padding(4);
            Btn_Join.Name = "Btn_Join";
            Btn_Join.Size = new Size(154, 64);
            Btn_Join.TabIndex = 6;
            Btn_Join.Text = "Join";
            Btn_Join.UseVisualStyleBackColor = true;
            Btn_Join.Click += Btn_Join_Click;
            // 
            // Lbl_RoomId
            // 
            Lbl_RoomId.AutoSize = true;
            Lbl_RoomId.Font = new Font("Stencil", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Lbl_RoomId.ForeColor = Color.Goldenrod;
            Lbl_RoomId.Location = new Point(23, 201);
            Lbl_RoomId.Margin = new Padding(4, 0, 4, 0);
            Lbl_RoomId.Name = "Lbl_RoomId";
            Lbl_RoomId.Size = new Size(56, 33);
            Lbl_RoomId.TabIndex = 7;
            Lbl_RoomId.Text = "ID:";
            // 
            // UC_RoomItem
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOliveGreen;
            Controls.Add(Lbl_RoomId);
            Controls.Add(Btn_Join);
            Controls.Add(Lbl_HostName);
            Controls.Add(Lbl_Status);
            Controls.Add(Lbl_RoomName);
            Margin = new Padding(4);
            Name = "UC_RoomItem";
            Size = new Size(1173, 285);
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
