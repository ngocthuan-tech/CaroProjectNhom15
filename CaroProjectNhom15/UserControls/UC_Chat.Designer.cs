namespace CaroProjectNhom15.UserControls
{
    partial class UC_Chat
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
            Rtb_Log = new RichTextBox();
            Tb_Msg = new TextBox();
            SuspendLayout();
            // 
            // Rtb_Log
            // 
            Rtb_Log.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Rtb_Log.BackColor = SystemColors.ControlLightLight;
            Rtb_Log.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Rtb_Log.Location = new Point(21, 22);
            Rtb_Log.Name = "Rtb_Log";
            Rtb_Log.ReadOnly = true;
            Rtb_Log.Size = new Size(480, 173);
            Rtb_Log.TabIndex = 0;
            Rtb_Log.Text = "";
            // 
            // Tb_Msg
            // 
            Tb_Msg.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Tb_Msg.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Tb_Msg.Location = new Point(21, 229);
            Tb_Msg.Name = "Tb_Msg";
            Tb_Msg.Size = new Size(480, 38);
            Tb_Msg.TabIndex = 1;
            // 
            // UC_Chat
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Tb_Msg);
            Controls.Add(Rtb_Log);
            Name = "UC_Chat";
            Size = new Size(520, 298);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox Rtb_Log;
        private TextBox Tb_Msg;
    }
}
