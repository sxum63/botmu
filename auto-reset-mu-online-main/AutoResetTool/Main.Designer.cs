
namespace AutoResetTool
{
  partial class Main
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      this.btnBrowse = new System.Windows.Forms.Button();
      this.pnlContainer = new System.Windows.Forms.Panel();
      this.label1 = new System.Windows.Forms.Label();
      this.txtFileMain = new System.Windows.Forms.TextBox();
      this.btnAddClient = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnBrowse
      // 
      this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnBrowse.Location = new System.Drawing.Point(416, 7);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(43, 25);
      this.btnBrowse.TabIndex = 0;
      this.btnBrowse.Text = "Chọn";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // pnlContainer
      // 
      this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.pnlContainer.AutoScroll = true;
      this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
      this.pnlContainer.Location = new System.Drawing.Point(10, 67);
      this.pnlContainer.Name = "pnlContainer";
      this.pnlContainer.Size = new System.Drawing.Size(449, 183);
      this.pnlContainer.TabIndex = 1;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(46, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "File MU:";
      // 
      // txtFileMain
      // 
      this.txtFileMain.Location = new System.Drawing.Point(58, 10);
      this.txtFileMain.Name = "txtFileMain";
      this.txtFileMain.ReadOnly = true;
      this.txtFileMain.Size = new System.Drawing.Size(352, 20);
      this.txtFileMain.TabIndex = 3;
      // 
      // btnAddClient
      // 
      this.btnAddClient.Location = new System.Drawing.Point(10, 36);
      this.btnAddClient.Name = "btnAddClient";
      this.btnAddClient.Size = new System.Drawing.Size(85, 25);
      this.btnAddClient.TabIndex = 0;
      this.btnAddClient.Text = "Thêm client";
      this.btnAddClient.UseVisualStyleBackColor = true;
      this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
      // 
      // Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(470, 260);
      this.Controls.Add(this.txtFileMain);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.pnlContainer);
      this.Controls.Add(this.btnAddClient);
      this.Controls.Add(this.btnBrowse);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size(486, 249);
      this.Name = "Main";
      this.Text = "Auto Reset Tool - MU Online";
      this.Load += new System.EventHandler(this.Main_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFileMain;
        private System.Windows.Forms.Button btnAddClient;
    }
}

