namespace mypylonview
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOneshot = new System.Windows.Forms.Button();
            this.btnContinous = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOneshot
            // 
            this.btnOneshot.Location = new System.Drawing.Point(770, 12);
            this.btnOneshot.Name = "btnOneshot";
            this.btnOneshot.Size = new System.Drawing.Size(130, 33);
            this.btnOneshot.TabIndex = 0;
            this.btnOneshot.Text = "ONE-SHOT";
            this.btnOneshot.UseVisualStyleBackColor = true;
            this.btnOneshot.Click += new System.EventHandler(this.btnOneshot_Click);
            // 
            // btnContinous
            // 
            this.btnContinous.Location = new System.Drawing.Point(770, 51);
            this.btnContinous.Name = "btnContinous";
            this.btnContinous.Size = new System.Drawing.Size(130, 33);
            this.btnContinous.TabIndex = 1;
            this.btnContinous.Text = "CONTINOUS";
            this.btnContinous.UseVisualStyleBackColor = true;
            this.btnContinous.Click += new System.EventHandler(this.btnContinous_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(770, 90);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(130, 33);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(752, 522);
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 546);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnContinous);
            this.Controls.Add(this.btnOneshot);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOneshot;
        private System.Windows.Forms.Button btnContinous;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}

