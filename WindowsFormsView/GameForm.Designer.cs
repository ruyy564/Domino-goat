namespace WindowsFormsView
{
  partial class GameForm
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
      this.pictureBoxBoard = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBoxBoard
      // 
      this.pictureBoxBoard.Location = new System.Drawing.Point(0, 0);
      this.pictureBoxBoard.Margin = new System.Windows.Forms.Padding(4);
      this.pictureBoxBoard.Name = "pictureBoxBoard";
      this.pictureBoxBoard.Size = new System.Drawing.Size(100, 50);
      this.pictureBoxBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.pictureBoxBoard.TabIndex = 5;
      this.pictureBoxBoard.TabStop = false;
      // 
      // GameForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(1348, 729);
      this.Controls.Add(this.pictureBoxBoard);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "GameForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Домино";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameForm_FormClosing);
      ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.PictureBox pictureBoxBoard;
  }
}

