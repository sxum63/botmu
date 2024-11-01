namespace AutoResetTool
{
  partial class ServiceItem
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
      this.lblServiceName = new System.Windows.Forms.Label();
      this.btnRunAuto = new System.Windows.Forms.Button();
      this.btnDelete = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.ckbCtrlF = new System.Windows.Forms.CheckBox();
      this.ckbAntiLag = new System.Windows.Forms.CheckBox();
      this.txtCharName = new System.Windows.Forms.TextBox();
      this.lblCharName = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblServiceName
      // 
      this.lblServiceName.AutoSize = true;
      this.lblServiceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lblServiceName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
      this.lblServiceName.Location = new System.Drawing.Point(3, 11);
      this.lblServiceName.MaximumSize = new System.Drawing.Size(248, 0);
      this.lblServiceName.Name = "lblServiceName";
      this.lblServiceName.Size = new System.Drawing.Size(166, 13);
      this.lblServiceName.TabIndex = 0;
      this.lblServiceName.Text = "Character - [Level] - [Reset]";
      // 
      // btnRunAuto
      // 
      this.btnRunAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnRunAuto.Location = new System.Drawing.Point(294, 8);
      this.btnRunAuto.Name = "btnRunAuto";
      this.btnRunAuto.Size = new System.Drawing.Size(70, 24);
      this.btnRunAuto.TabIndex = 4;
      this.btnRunAuto.Text = "Auto Reset";
      this.btnRunAuto.UseVisualStyleBackColor = true;
      this.btnRunAuto.Click += new System.EventHandler(this.btnRunAuto_Click);
      // 
      // btnDelete
      // 
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnDelete.Location = new System.Drawing.Point(370, 8);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(38, 24);
      this.btnDelete.TabIndex = 5;
      this.btnDelete.Text = "Xóa";
      this.btnDelete.UseVisualStyleBackColor = true;
      this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
      // 
      // btnStart
      // 
      this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnStart.Location = new System.Drawing.Point(216, 8);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(72, 24);
      this.btnStart.TabIndex = 3;
      this.btnStart.Text = "Chạy Client";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Visible = false;
      this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
      // 
      // ckbCtrlF
      // 
      this.ckbCtrlF.AutoSize = true;
      this.ckbCtrlF.Location = new System.Drawing.Point(265, 38);
      this.ckbCtrlF.Name = "ckbCtrlF";
      this.ckbCtrlF.Size = new System.Drawing.Size(59, 17);
      this.ckbCtrlF.TabIndex = 6;
      this.ckbCtrlF.Text = "Ctrl + F";
      this.ckbCtrlF.UseVisualStyleBackColor = true;
      this.ckbCtrlF.CheckedChanged += new System.EventHandler(this.ckbCtrlF_CheckedChanged);
      // 
      // ckbAntiLag
      // 
      this.ckbAntiLag.AutoSize = true;
      this.ckbAntiLag.Location = new System.Drawing.Point(343, 38);
      this.ckbAntiLag.Name = "ckbAntiLag";
      this.ckbAntiLag.Size = new System.Drawing.Size(65, 17);
      this.ckbAntiLag.TabIndex = 7;
      this.ckbAntiLag.Text = "Anti Lag";
      this.ckbAntiLag.UseVisualStyleBackColor = true;
      this.ckbAntiLag.CheckedChanged += new System.EventHandler(this.ckbAntiLag_CheckedChanged);
      // 
      // txtCharName
      // 
      this.txtCharName.Location = new System.Drawing.Point(69, 35);
      this.txtCharName.Name = "txtCharName";
      this.txtCharName.Size = new System.Drawing.Size(100, 20);
      this.txtCharName.TabIndex = 2;
      this.txtCharName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCharName_KeyUp);
      // 
      // lblCharName
      // 
      this.lblCharName.AutoSize = true;
      this.lblCharName.Location = new System.Drawing.Point(3, 39);
      this.lblCharName.Name = "lblCharName";
      this.lblCharName.Size = new System.Drawing.Size(53, 13);
      this.lblCharName.TabIndex = 1;
      this.lblCharName.Text = "Character";
      // 
      // ServiceItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoSize = true;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.lblCharName);
      this.Controls.Add(this.txtCharName);
      this.Controls.Add(this.ckbAntiLag);
      this.Controls.Add(this.ckbCtrlF);
      this.Controls.Add(this.btnDelete);
      this.Controls.Add(this.btnStart);
      this.Controls.Add(this.btnRunAuto);
      this.Controls.Add(this.lblServiceName);
      this.Name = "ServiceItem";
      this.Size = new System.Drawing.Size(417, 58);
      this.Click += new System.EventHandler(this.ServiceItem_Click);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblServiceName;
    private System.Windows.Forms.Button btnRunAuto;
    private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox ckbCtrlF;
        private System.Windows.Forms.CheckBox ckbAntiLag;
    private System.Windows.Forms.TextBox txtCharName;
    private System.Windows.Forms.Label lblCharName;
  }
}
