namespace PacmanGame_WinForms_
{
    partial class Game
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PacmanTimer = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            //this.TimerGhostHitPacman = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.LevelTimer = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.StopWatchTimer = new System.Windows.Forms.Timer(this.components);
            this.BonusTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.PacmanTimer.Interval = 20;
            this.PacmanTimer.Tick += new System.EventHandler(this.PacmanMoving);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Berlin Sans FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label2.Location = new System.Drawing.Point(1120, 194);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "F5 - restart\r\n";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Berlin Sans FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1120, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(169, 35);
            this.label3.TabIndex = 2;
            this.label3.Text = "Esc - quit\r\n\r\n";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Aqua;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Berlin Sans FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1120, 264);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 95);
            this.label4.TabIndex = 3;
            this.label4.Text = "Home - return to the \r\nmain menu\r\n\r\n";
            // 
            // timer6
            // 
            //this.TimerGhostHitPacman.Interval = 70;
            //this.TimerGhostHitPacman.Tick += new System.EventHandler(this.GhostHitPacman);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Berlin Sans FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1120, 359);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 64);
            this.label6.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Magenta;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Berlin Sans FB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(930, 620);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(311, 38);
            this.label5.TabIndex = 5;
            this.label5.Text = "Last key pressed:\r\n\r\n\r\n";
            // 
            // timer8
            // 
            this.StopWatchTimer.Interval = 1000;
            this.StopWatchTimer.Tick += new System.EventHandler(this.StopWatch);
            // 
            // timer9
            // 
            this.BonusTimer.Interval = 1000;
            this.BonusTimer.Tick += new System.EventHandler(this.BonusMoving);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1270, 957);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Enabled = false;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game";
            this.Load += new System.EventHandler(this.GameLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameKeyDown);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Timer PacmanTimer;
        public static System.Windows.Forms.Label InfoBlock = new System.Windows.Forms.Label();
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        //private System.Windows.Forms.Timer TimerGhostHitPacman;
        //private static System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Timer LevelTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer StopWatchTimer;
        private System.Windows.Forms.Timer BonusTimer;
        //private System.Windows.Forms.Panel panel1;
        //private System.Windows.Forms.Label label5;

        #endregion

        //public static System.Windows.Forms.Timer timer2;
        //public static System.Windows.Forms.Timer timer3;
        //public static System.Windows.Forms.Timer timer4;
        //public static System.Windows.Forms.Timer timer5;
    }
}

