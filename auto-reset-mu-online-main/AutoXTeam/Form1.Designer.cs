namespace AutoXTeam
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
      this.btnReadInfo = new System.Windows.Forms.Button();
      this.btnMoveChar = new System.Windows.Forms.Button();
      this.btnChat = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnReadInfo
      // 
      this.btnReadInfo.Location = new System.Drawing.Point(12, 12);
      this.btnReadInfo.Name = "btnReadInfo";
      this.btnReadInfo.Size = new System.Drawing.Size(75, 23);
      this.btnReadInfo.TabIndex = 0;
      this.btnReadInfo.Text = "Read Info";
      this.btnReadInfo.UseVisualStyleBackColor = true;
      this.btnReadInfo.Click += new System.EventHandler(this.btnReadInfo_Click);
      // 
      // btnMoveChar
      // 
      this.btnMoveChar.Location = new System.Drawing.Point(93, 12);
      this.btnMoveChar.Name = "btnMoveChar";
      this.btnMoveChar.Size = new System.Drawing.Size(75, 23);
      this.btnMoveChar.TabIndex = 0;
      this.btnMoveChar.Text = "Move";
      this.btnMoveChar.UseVisualStyleBackColor = true;
      this.btnMoveChar.Click += new System.EventHandler(this.btnMoveChar_Click);
      // 
      // btnChat
      // 
      this.btnChat.Location = new System.Drawing.Point(174, 12);
      this.btnChat.Name = "btnChat";
      this.btnChat.Size = new System.Drawing.Size(75, 23);
      this.btnChat.TabIndex = 0;
      this.btnChat.Text = "Chat";
      this.btnChat.UseVisualStyleBackColor = true;
      this.btnChat.Click += new System.EventHandler(this.btnChart_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(800, 450);
      this.Controls.Add(this.btnChat);
      this.Controls.Add(this.btnMoveChar);
      this.Controls.Add(this.btnReadInfo);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnReadInfo;
    private System.Windows.Forms.Button btnMoveChar;
    private System.Windows.Forms.Button btnChat;
  }
}

