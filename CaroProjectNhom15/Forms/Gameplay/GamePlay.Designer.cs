namespace CaroProjectNhom15.Forms.Gameplay
{
    partial class GamePlay
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
            Btn_A1 = new Button();
            Btn_A2 = new Button();
            Btn_B2 = new Button();
            Btn_B1 = new Button();
            Btn_A3 = new Button();
            Btn_B3 = new Button();
            Btn_C1 = new Button();
            Btn_C2 = new Button();
            Btn_C3 = new Button();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            Lbl_Turn = new Label();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Btn_A1
            // 
            Btn_A1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A1.Location = new Point(12, 44);
            Btn_A1.Name = "Btn_A1";
            Btn_A1.Size = new Size(113, 116);
            Btn_A1.TabIndex = 4;
            Btn_A1.UseVisualStyleBackColor = true;
            Btn_A1.Click += this.Btn_Click;
            // 
            // Btn_A2
            // 
            Btn_A2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A2.Location = new Point(148, 44);
            Btn_A2.Name = "Btn_A2";
            Btn_A2.Size = new Size(113, 116);
            Btn_A2.TabIndex = 5;
            Btn_A2.UseVisualStyleBackColor = true;
            Btn_A2.Click += this.Btn_Click;
            // 
            // Btn_B2
            // 
            Btn_B2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B2.Location = new Point(148, 168);
            Btn_B2.Name = "Btn_B2";
            Btn_B2.Size = new Size(113, 116);
            Btn_B2.TabIndex = 6;
            Btn_B2.UseVisualStyleBackColor = true;
            Btn_B2.Click += this.Btn_Click;
            // 
            // Btn_B1
            // 
            Btn_B1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B1.Location = new Point(12, 168);
            Btn_B1.Name = "Btn_B1";
            Btn_B1.Size = new Size(113, 116);
            Btn_B1.TabIndex = 7;
            Btn_B1.UseVisualStyleBackColor = true;
            Btn_B1.Click += this.Btn_Click;
            // 
            // Btn_A3
            // 
            Btn_A3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A3.Location = new Point(284, 44);
            Btn_A3.Name = "Btn_A3";
            Btn_A3.Size = new Size(113, 116);
            Btn_A3.TabIndex = 8;
            Btn_A3.UseVisualStyleBackColor = true;
            Btn_A3.Click += this.Btn_Click;
            // 
            // Btn_B3
            // 
            Btn_B3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B3.Location = new Point(284, 168);
            Btn_B3.Name = "Btn_B3";
            Btn_B3.Size = new Size(113, 116);
            Btn_B3.TabIndex = 9;
            Btn_B3.UseVisualStyleBackColor = true;
            Btn_B3.Click += this.Btn_Click;
            // 
            // Btn_C1
            // 
            Btn_C1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C1.Location = new Point(12, 294);
            Btn_C1.Name = "Btn_C1";
            Btn_C1.Size = new Size(113, 116);
            Btn_C1.TabIndex = 10;
            Btn_C1.UseVisualStyleBackColor = true;
            Btn_C1.Click += this.Btn_Click;
            // 
            // Btn_C2
            // 
            Btn_C2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C2.Location = new Point(148, 294);
            Btn_C2.Name = "Btn_C2";
            Btn_C2.Size = new Size(113, 116);
            Btn_C2.TabIndex = 11;
            Btn_C2.UseVisualStyleBackColor = true;
            Btn_C2.Click += this.Btn_Click;
            // 
            // Btn_C3
            // 
            Btn_C3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C3.Location = new Point(284, 294);
            Btn_C3.Name = "Btn_C3";
            Btn_C3.Size = new Size(113, 116);
            Btn_C3.TabIndex = 12;
            Btn_C3.UseVisualStyleBackColor = true;
            Btn_C3.Click += this.Btn_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 28);
            menuStrip1.TabIndex = 13;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newGameToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            newGameToolStripMenuItem.Size = new Size(164, 26);
            newGameToolStripMenuItem.Text = "New game";
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(164, 26);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(64, 24);
            aboutToolStripMenuItem.Text = "About";
            // 
            // Lbl_Turn
            // 
            Lbl_Turn.AutoSize = true;
            Lbl_Turn.Location = new Point(448, 44);
            Lbl_Turn.Name = "Lbl_Turn";
            Lbl_Turn.Size = new Size(121, 20);
            Lbl_Turn.TabIndex = 14;
            Lbl_Turn.Text = "Lượt người chơi: ";
            // 
            // GamePlay
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 432);
            Controls.Add(Lbl_Turn);
            Controls.Add(menuStrip1);
            Controls.Add(Btn_C3);
            Controls.Add(Btn_C2);
            Controls.Add(Btn_C1);
            Controls.Add(Btn_B3);
            Controls.Add(Btn_A3);
            Controls.Add(Btn_B1);
            Controls.Add(Btn_B2);
            Controls.Add(Btn_A2);
            Controls.Add(Btn_A1);
            Name = "GamePlay";
            Text = "GamePlay";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Btn_A1;
        private Button Btn_A2;
        private Button Btn_B2;
        private Button Btn_B1;
        private Button Btn_A3;
        private Button Btn_B3;
        private Button Btn_C1;
        private Button Btn_C2;
        private Button Btn_C3;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Label Lbl_Turn;
    }
}