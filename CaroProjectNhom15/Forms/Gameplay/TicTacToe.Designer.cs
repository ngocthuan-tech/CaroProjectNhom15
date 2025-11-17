namespace CaroProjectNhom15.Forms.Forms.Gameplay
{
    partial class TicTacToe
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newGameToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            Btn_A1 = new Button();
            Btn_A2 = new Button();
            Btn_A3 = new Button();
            Btn_B1 = new Button();
            Btn_B2 = new Button();
            Btn_B3 = new Button();
            Btn_C1 = new Button();
            Btn_C2 = new Button();
            Btn_C3 = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(335, 28);
            menuStrip1.TabIndex = 0;
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
            newGameToolStripMenuItem.Size = new Size(224, 26);
            newGameToolStripMenuItem.Text = "New game";
            newGameToolStripMenuItem.Click += newGameToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(224, 26);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(64, 24);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // Btn_A1
            // 
            Btn_A1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A1.Location = new Point(12, 31);
            Btn_A1.Name = "Btn_A1";
            Btn_A1.Size = new Size(100, 100);
            Btn_A1.TabIndex = 1;
            Btn_A1.UseVisualStyleBackColor = true;
            Btn_A1.Click += button_CLick;
            // 
            // Btn_A2
            // 
            Btn_A2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A2.Location = new Point(118, 31);
            Btn_A2.Name = "Btn_A2";
            Btn_A2.Size = new Size(100, 100);
            Btn_A2.TabIndex = 2;
            Btn_A2.UseVisualStyleBackColor = true;
            Btn_A2.Click += button_CLick;
            // 
            // Btn_A3
            // 
            Btn_A3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_A3.Location = new Point(224, 31);
            Btn_A3.Name = "Btn_A3";
            Btn_A3.Size = new Size(100, 100);
            Btn_A3.TabIndex = 3;
            Btn_A3.UseVisualStyleBackColor = true;
            Btn_A3.Click += button_CLick;
            // 
            // Btn_B1
            // 
            Btn_B1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B1.Location = new Point(12, 137);
            Btn_B1.Name = "Btn_B1";
            Btn_B1.Size = new Size(100, 100);
            Btn_B1.TabIndex = 4;
            Btn_B1.UseVisualStyleBackColor = true;
            Btn_B1.Click += button_CLick;
            // 
            // Btn_B2
            // 
            Btn_B2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B2.Location = new Point(118, 137);
            Btn_B2.Name = "Btn_B2";
            Btn_B2.Size = new Size(100, 100);
            Btn_B2.TabIndex = 5;
            Btn_B2.UseVisualStyleBackColor = true;
            Btn_B2.Click += button_CLick;
            // 
            // Btn_B3
            // 
            Btn_B3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_B3.Location = new Point(224, 137);
            Btn_B3.Name = "Btn_B3";
            Btn_B3.Size = new Size(100, 100);
            Btn_B3.TabIndex = 6;
            Btn_B3.UseVisualStyleBackColor = true;
            Btn_B3.Click += button_CLick;
            // 
            // Btn_C1
            // 
            Btn_C1.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C1.Location = new Point(12, 243);
            Btn_C1.Name = "Btn_C1";
            Btn_C1.Size = new Size(100, 100);
            Btn_C1.TabIndex = 7;
            Btn_C1.UseVisualStyleBackColor = true;
            Btn_C1.Click += button_CLick;
            // 
            // Btn_C2
            // 
            Btn_C2.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C2.Location = new Point(118, 243);
            Btn_C2.Name = "Btn_C2";
            Btn_C2.Size = new Size(100, 100);
            Btn_C2.TabIndex = 8;
            Btn_C2.UseVisualStyleBackColor = true;
            Btn_C2.Click += button_CLick;
            // 
            // Btn_C3
            // 
            Btn_C3.Font = new Font("SimSun", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Btn_C3.Location = new Point(223, 243);
            Btn_C3.Name = "Btn_C3";
            Btn_C3.Size = new Size(100, 100);
            Btn_C3.TabIndex = 9;
            Btn_C3.UseVisualStyleBackColor = true;
            Btn_C3.Click += button_CLick;
            // 
            // TicTacToe
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(335, 355);
            Controls.Add(Btn_C3);
            Controls.Add(Btn_C2);
            Controls.Add(Btn_C1);
            Controls.Add(Btn_B3);
            Controls.Add(Btn_B2);
            Controls.Add(Btn_B1);
            Controls.Add(Btn_A3);
            Controls.Add(Btn_A2);
            Controls.Add(Btn_A1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "TicTacToe";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tic Tac Toe";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private Button Btn_A1;
        private Button Btn_A2;
        private Button Btn_A3;
        private Button Btn_B1;
        private Button Btn_B2;
        private Button Btn_B3;
        private Button Btn_C1;
        private Button Btn_C2;
        private Button Btn_C3;
    }
}