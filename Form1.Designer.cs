
namespace Backgammon
{
    partial class Form1
    {
        /// <summary>
        /// Vyžaduje se proměnná návrháře.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Uvolněte všechny používané prostředky.
        /// </summary>
        /// <param name="disposing">hodnota true, když by se měl spravovaný prostředek odstranit; jinak false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kód generovaný Návrhářem Windows Form

        /// <summary>
        /// Metoda vyžadovaná pro podporu Návrháře - neupravovat
        /// obsah této metody v editoru kódu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.WScoreBox = new System.Windows.Forms.PictureBox();
            this.BScoreBox = new System.Windows.Forms.PictureBox();
            this.WBarBox = new System.Windows.Forms.PictureBox();
            this.BBarBox = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.TurnBox = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.WScoreLabel = new System.Windows.Forms.Label();
            this.BBarLabel = new System.Windows.Forms.Label();
            this.WBarLabel = new System.Windows.Forms.Label();
            this.BScoreLabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.WScoreBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BScoreBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBarBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BBarBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnBox)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(166, 478);
            this.splitter1.TabIndex = 0;
            this.splitter1.TabStop = false;
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(619, 0);
            this.splitter2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(182, 478);
            this.splitter2.TabIndex = 1;
            this.splitter2.TabStop = false;
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.Location = new System.Drawing.Point(657, 192);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
            this.button1.TabIndex = 2;
            this.button1.Text = "Roll Dice";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Dice_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(652, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "White Score";
            this.label1.Click += new System.EventHandler(this.WScore_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(646, 408);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Black Score";
            this.label2.Click += new System.EventHandler(this.BScore_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(652, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Black Bar";
            this.label3.Click += new System.EventHandler(this.BBar_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(646, 326);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "White Bar";
            this.label4.Click += new System.EventHandler(this.WBar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(18, 17);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 35);
            this.button2.TabIndex = 7;
            this.button2.Text = "New Game";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.NGame_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(18, 62);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 35);
            this.button3.TabIndex = 8;
            this.button3.Text = "Resign";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Resign_Click);
            // 
            // WScoreBox
            // 
            this.WScoreBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WScoreBox.BackColor = System.Drawing.SystemColors.Control;
            this.WScoreBox.Image = ((System.Drawing.Image)(resources.GetObject("WScoreBox.Image")));
            this.WScoreBox.Location = new System.Drawing.Point(628, 42);
            this.WScoreBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WScoreBox.Name = "WScoreBox";
            this.WScoreBox.Size = new System.Drawing.Size(38, 38);
            this.WScoreBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WScoreBox.TabIndex = 16;
            this.WScoreBox.TabStop = false;
            this.WScoreBox.Click += new System.EventHandler(this.WScore_Click);
            // 
            // BScoreBox
            // 
            this.BScoreBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BScoreBox.Image = ((System.Drawing.Image)(resources.GetObject("BScoreBox.Image")));
            this.BScoreBox.Location = new System.Drawing.Point(628, 431);
            this.BScoreBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BScoreBox.Name = "BScoreBox";
            this.BScoreBox.Size = new System.Drawing.Size(38, 38);
            this.BScoreBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BScoreBox.TabIndex = 19;
            this.BScoreBox.TabStop = false;
            this.BScoreBox.Click += new System.EventHandler(this.BScore_Click);
            // 
            // WBarBox
            // 
            this.WBarBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.WBarBox.Image = ((System.Drawing.Image)(resources.GetObject("WBarBox.Image")));
            this.WBarBox.Location = new System.Drawing.Point(628, 351);
            this.WBarBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.WBarBox.Name = "WBarBox";
            this.WBarBox.Size = new System.Drawing.Size(38, 38);
            this.WBarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.WBarBox.TabIndex = 18;
            this.WBarBox.TabStop = false;
            this.WBarBox.Click += new System.EventHandler(this.WBar_Click);
            // 
            // BBarBox
            // 
            this.BBarBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BBarBox.Image = ((System.Drawing.Image)(resources.GetObject("BBarBox.Image")));
            this.BBarBox.Location = new System.Drawing.Point(628, 109);
            this.BBarBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BBarBox.Name = "BBarBox";
            this.BBarBox.Size = new System.Drawing.Size(38, 38);
            this.BBarBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BBarBox.TabIndex = 17;
            this.BBarBox.TabStop = false;
            this.BBarBox.Click += new System.EventHandler(this.BBar_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.InitialImage = global::Backgammon.Properties.Resources.Alea_1;
            this.pictureBox3.Location = new System.Drawing.Point(729, 237);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(54, 52);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.InitialImage = global::Backgammon.Properties.Resources.Alea_1;
            this.pictureBox2.Location = new System.Drawing.Point(628, 237);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(54, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // TurnBox
            // 
            this.TurnBox.Image = ((System.Drawing.Image)(resources.GetObject("TurnBox.Image")));
            this.TurnBox.Location = new System.Drawing.Point(18, 107);
            this.TurnBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TurnBox.Name = "TurnBox";
            this.TurnBox.Size = new System.Drawing.Size(38, 38);
            this.TurnBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.TurnBox.TabIndex = 20;
            this.TurnBox.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Turn";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(653, 294);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 20);
            this.label6.TabIndex = 22;
            // 
            // WScoreLabel
            // 
            this.WScoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WScoreLabel.AutoSize = true;
            this.WScoreLabel.Location = new System.Drawing.Point(678, 42);
            this.WScoreLabel.Name = "WScoreLabel";
            this.WScoreLabel.Size = new System.Drawing.Size(18, 20);
            this.WScoreLabel.TabIndex = 23;
            this.WScoreLabel.Text = "0";
            this.WScoreLabel.Click += new System.EventHandler(this.WScore_Click);
            // 
            // BBarLabel
            // 
            this.BBarLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BBarLabel.AutoSize = true;
            this.BBarLabel.Location = new System.Drawing.Point(678, 109);
            this.BBarLabel.Name = "BBarLabel";
            this.BBarLabel.Size = new System.Drawing.Size(18, 20);
            this.BBarLabel.TabIndex = 24;
            this.BBarLabel.Text = "0";
            this.BBarLabel.Click += new System.EventHandler(this.BBar_Click);
            // 
            // WBarLabel
            // 
            this.WBarLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.WBarLabel.AutoSize = true;
            this.WBarLabel.Location = new System.Drawing.Point(678, 351);
            this.WBarLabel.Name = "WBarLabel";
            this.WBarLabel.Size = new System.Drawing.Size(18, 20);
            this.WBarLabel.TabIndex = 25;
            this.WBarLabel.Text = "0";
            this.WBarLabel.Click += new System.EventHandler(this.WBar_Click);
            // 
            // BScoreLabel
            // 
            this.BScoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BScoreLabel.AutoSize = true;
            this.BScoreLabel.Location = new System.Drawing.Point(678, 428);
            this.BScoreLabel.Name = "BScoreLabel";
            this.BScoreLabel.Size = new System.Drawing.Size(18, 20);
            this.BScoreLabel.TabIndex = 26;
            this.BScoreLabel.Text = "0";
            this.BScoreLabel.Click += new System.EventHandler(this.BScore_Click);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(653, 294);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 20);
            this.label11.TabIndex = 27;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(18, 153);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(146, 44);
            this.checkBox1.TabIndex = 28;
            this.checkBox1.Text = "Random player \r\nplays as black";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.RPlayerBlackChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(18, 203);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(142, 44);
            this.checkBox2.TabIndex = 29;
            this.checkBox2.Text = "Random plays \r\nmove per move";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.RPlayerMovePerMChanged);
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(18, 253);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 35);
            this.button4.TabIndex = 30;
            this.button4.Text = "Next Move";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.RPlayerNextMove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 478);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.BScoreLabel);
            this.Controls.Add(this.WBarLabel);
            this.Controls.Add(this.BBarLabel);
            this.Controls.Add(this.WScoreLabel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TurnBox);
            this.Controls.Add(this.WScoreBox);
            this.Controls.Add(this.BScoreBox);
            this.Controls.Add(this.WBarBox);
            this.Controls.Add(this.BBarBox);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.splitter1);
            this.MinimumSize = new System.Drawing.Size(814, 508);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.FormShown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormClicked);
            this.Resize += new System.EventHandler(this.FormResize);
            ((System.ComponentModel.ISupportInitialize)(this.WScoreBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BScoreBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WBarBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BBarBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TurnBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Splitter splitter2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox WScoreBox;
        private System.Windows.Forms.PictureBox BBarBox;
        private System.Windows.Forms.PictureBox WBarBox;
        private System.Windows.Forms.PictureBox BScoreBox;
        private System.Windows.Forms.PictureBox TurnBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label WScoreLabel;
        private System.Windows.Forms.Label BBarLabel;
        private System.Windows.Forms.Label WBarLabel;
        private System.Windows.Forms.Label BScoreLabel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button button4;
    }
}

