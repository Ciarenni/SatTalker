namespace SatTalker
{
    partial class SatTalker
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
            this.azLabel = new System.Windows.Forms.Label();
            this.elLabel = new System.Windows.Forms.Label();
            this.dopListenLabel = new System.Windows.Forms.Label();
            this.dopTalkLabel = new System.Windows.Forms.Label();
            this.azParamLabel = new System.Windows.Forms.Label();
            this.elParamLabel = new System.Windows.Forms.Label();
            this.dopLisParamLabel = new System.Windows.Forms.Label();
            this.dopTalkParamLabel = new System.Windows.Forms.Label();
            this.downlinkBtn = new System.Windows.Forms.Button();
            this.uplinkBtn = new System.Windows.Forms.Button();
            this.offsetValueLabel = new System.Windows.Forms.Label();
            this.offsetLabel = new System.Windows.Forms.Label();
            this.offsetIncBtn = new System.Windows.Forms.Button();
            this.offsetDecBtn = new System.Windows.Forms.Button();
            this.offsetReset = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // azLabel
            // 
            this.azLabel.AutoSize = true;
            this.azLabel.Location = new System.Drawing.Point(72, 12);
            this.azLabel.Name = "azLabel";
            this.azLabel.Size = new System.Drawing.Size(47, 13);
            this.azLabel.TabIndex = 0;
            this.azLabel.Text = "Azimuth:";
            // 
            // elLabel
            // 
            this.elLabel.AutoSize = true;
            this.elLabel.Location = new System.Drawing.Point(65, 41);
            this.elLabel.Name = "elLabel";
            this.elLabel.Size = new System.Drawing.Size(54, 13);
            this.elLabel.TabIndex = 1;
            this.elLabel.Text = "Elevation:";
            // 
            // dopListenLabel
            // 
            this.dopListenLabel.AutoSize = true;
            this.dopListenLabel.Location = new System.Drawing.Point(12, 72);
            this.dopListenLabel.Name = "dopListenLabel";
            this.dopListenLabel.Size = new System.Drawing.Size(107, 13);
            this.dopListenLabel.TabIndex = 2;
            this.dopListenLabel.Text = "Downlink Frequency:";
            // 
            // dopTalkLabel
            // 
            this.dopTalkLabel.AutoSize = true;
            this.dopTalkLabel.Location = new System.Drawing.Point(26, 105);
            this.dopTalkLabel.Name = "dopTalkLabel";
            this.dopTalkLabel.Size = new System.Drawing.Size(93, 13);
            this.dopTalkLabel.TabIndex = 3;
            this.dopTalkLabel.Text = "Uplink Frequency:";
            // 
            // azParamLabel
            // 
            this.azParamLabel.AutoSize = true;
            this.azParamLabel.Location = new System.Drawing.Point(126, 12);
            this.azParamLabel.Name = "azParamLabel";
            this.azParamLabel.Size = new System.Drawing.Size(0, 13);
            this.azParamLabel.TabIndex = 4;
            // 
            // elParamLabel
            // 
            this.elParamLabel.AutoSize = true;
            this.elParamLabel.Location = new System.Drawing.Point(126, 41);
            this.elParamLabel.Name = "elParamLabel";
            this.elParamLabel.Size = new System.Drawing.Size(0, 13);
            this.elParamLabel.TabIndex = 5;
            // 
            // dopLisParamLabel
            // 
            this.dopLisParamLabel.AutoSize = true;
            this.dopLisParamLabel.Location = new System.Drawing.Point(126, 72);
            this.dopLisParamLabel.Name = "dopLisParamLabel";
            this.dopLisParamLabel.Size = new System.Drawing.Size(0, 13);
            this.dopLisParamLabel.TabIndex = 6;
            // 
            // dopTalkParamLabel
            // 
            this.dopTalkParamLabel.AutoSize = true;
            this.dopTalkParamLabel.Location = new System.Drawing.Point(126, 105);
            this.dopTalkParamLabel.Name = "dopTalkParamLabel";
            this.dopTalkParamLabel.Size = new System.Drawing.Size(0, 13);
            this.dopTalkParamLabel.TabIndex = 7;
            // 
            // downlinkBtn
            // 
            this.downlinkBtn.Location = new System.Drawing.Point(224, 67);
            this.downlinkBtn.Name = "downlinkBtn";
            this.downlinkBtn.Size = new System.Drawing.Size(82, 23);
            this.downlinkBtn.TabIndex = 8;
            this.downlinkBtn.Text = "Start Listening";
            this.downlinkBtn.UseVisualStyleBackColor = true;
            this.downlinkBtn.Click += new System.EventHandler(this.downlinkBtn_Click);
            // 
            // uplinkBtn
            // 
            this.uplinkBtn.Location = new System.Drawing.Point(224, 100);
            this.uplinkBtn.Name = "uplinkBtn";
            this.uplinkBtn.Size = new System.Drawing.Size(82, 23);
            this.uplinkBtn.TabIndex = 9;
            this.uplinkBtn.Text = "Start Talking";
            this.uplinkBtn.UseVisualStyleBackColor = true;
            this.uplinkBtn.Click += new System.EventHandler(this.uplinkBtn_Click);
            // 
            // offsetValueLabel
            // 
            this.offsetValueLabel.AutoSize = true;
            this.offsetValueLabel.Location = new System.Drawing.Point(126, 145);
            this.offsetValueLabel.Name = "offsetValueLabel";
            this.offsetValueLabel.Size = new System.Drawing.Size(0, 13);
            this.offsetValueLabel.TabIndex = 11;
            // 
            // offsetLabel
            // 
            this.offsetLabel.AutoSize = true;
            this.offsetLabel.Location = new System.Drawing.Point(81, 145);
            this.offsetLabel.Name = "offsetLabel";
            this.offsetLabel.Size = new System.Drawing.Size(38, 13);
            this.offsetLabel.TabIndex = 10;
            this.offsetLabel.Text = "Offset:";
            // 
            // offsetIncBtn
            // 
            this.offsetIncBtn.Location = new System.Drawing.Point(225, 132);
            this.offsetIncBtn.Name = "offsetIncBtn";
            this.offsetIncBtn.Size = new System.Drawing.Size(18, 22);
            this.offsetIncBtn.TabIndex = 12;
            this.offsetIncBtn.Text = "+";
            this.offsetIncBtn.UseVisualStyleBackColor = true;
            this.offsetIncBtn.Click += new System.EventHandler(this.offsetIncBtn_Click);
            // 
            // offsetDecBtn
            // 
            this.offsetDecBtn.Location = new System.Drawing.Point(225, 153);
            this.offsetDecBtn.Name = "offsetDecBtn";
            this.offsetDecBtn.Size = new System.Drawing.Size(18, 23);
            this.offsetDecBtn.TabIndex = 13;
            this.offsetDecBtn.Text = "-";
            this.offsetDecBtn.UseVisualStyleBackColor = true;
            this.offsetDecBtn.Click += new System.EventHandler(this.offsetDecBtn_Click);
            // 
            // offsetReset
            // 
            this.offsetReset.Location = new System.Drawing.Point(249, 140);
            this.offsetReset.Name = "offsetReset";
            this.offsetReset.Size = new System.Drawing.Size(57, 23);
            this.offsetReset.TabIndex = 14;
            this.offsetReset.Text = "Reset";
            this.offsetReset.UseVisualStyleBackColor = true;
            this.offsetReset.Click += new System.EventHandler(this.offsetReset_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SatTalker.Properties.Resources.SHC3;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(15, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(498, 126);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::SatTalker.Properties.Resources.ARISSSidebarLogo;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(369, 30);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(142, 55);
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::SatTalker.Properties.Resources.icom;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(369, 108);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(142, 63);
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // SatTalker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 338);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.offsetReset);
            this.Controls.Add(this.offsetDecBtn);
            this.Controls.Add(this.offsetIncBtn);
            this.Controls.Add(this.offsetValueLabel);
            this.Controls.Add(this.offsetLabel);
            this.Controls.Add(this.uplinkBtn);
            this.Controls.Add(this.downlinkBtn);
            this.Controls.Add(this.dopTalkParamLabel);
            this.Controls.Add(this.dopLisParamLabel);
            this.Controls.Add(this.elParamLabel);
            this.Controls.Add(this.azParamLabel);
            this.Controls.Add(this.dopTalkLabel);
            this.Controls.Add(this.dopListenLabel);
            this.Controls.Add(this.elLabel);
            this.Controls.Add(this.azLabel);
            this.MinimumSize = new System.Drawing.Size(335, 220);
            this.Name = "SatTalker";
            this.Text = "SatTalker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label azLabel;
        private System.Windows.Forms.Label elLabel;
        private System.Windows.Forms.Label dopListenLabel;
        private System.Windows.Forms.Label dopTalkLabel;
        private System.Windows.Forms.Label azParamLabel;
        private System.Windows.Forms.Label elParamLabel;
        private System.Windows.Forms.Label dopLisParamLabel;
        private System.Windows.Forms.Label dopTalkParamLabel;
        private System.Windows.Forms.Button downlinkBtn;
        private System.Windows.Forms.Button uplinkBtn;
        private System.Windows.Forms.Label offsetValueLabel;
        private System.Windows.Forms.Label offsetLabel;
        private System.Windows.Forms.Button offsetIncBtn;
        private System.Windows.Forms.Button offsetDecBtn;
        private System.Windows.Forms.Button offsetReset;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;

    }
}

