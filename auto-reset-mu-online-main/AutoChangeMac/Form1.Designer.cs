namespace AutoChangeMac
{
    partial class Form1
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
      this.btnRun = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.txtFilePath = new System.Windows.Forms.TextBox();
      this.btnBrowse = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnRun
      // 
      this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRun.Location = new System.Drawing.Point(624, 3);
      this.btnRun.Name = "btnRun";
      this.btnRun.Size = new System.Drawing.Size(64, 25);
      this.btnRun.TabIndex = 0;
      this.btnRun.Text = "Chạy";
      this.btnRun.UseVisualStyleBackColor = true;
      this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(51, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "File Path:";
      // 
      // txtFilePath
      // 
      this.txtFilePath.Location = new System.Drawing.Point(69, 6);
      this.txtFilePath.Name = "txtFilePath";
      this.txtFilePath.Size = new System.Drawing.Size(469, 20);
      this.txtFilePath.TabIndex = 2;
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(544, 3);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(64, 25);
      this.btnBrowse.TabIndex = 0;
      this.btnBrowse.Text = "Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(700, 40);
      this.Controls.Add(this.txtFilePath);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btnBrowse);
      this.Controls.Add(this.btnRun);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
    }
}

