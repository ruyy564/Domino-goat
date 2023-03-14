
namespace WindowsFormsView
{
  partial class InputRecordForm
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
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textBox1
      // 
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textBox1.Location = new System.Drawing.Point(120, 152);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(304, 34);
      this.textBox1.TabIndex = 0;
      this.textBox1.Visible = false;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.ForeColor = System.Drawing.Color.Purple;
      this.label1.Location = new System.Drawing.Point(72, 48);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(396, 51);
      this.label1.TabIndex = 1;
      this.label1.Text = "Введите ваше имя";
      // 
      // btnBack
      // 
      this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnBack.ForeColor = System.Drawing.Color.Purple;
      this.btnBack.Location = new System.Drawing.Point(168, 240);
      this.btnBack.Margin = new System.Windows.Forms.Padding(4);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(176, 41);
      this.btnBack.TabIndex = 18;
      this.btnBack.Text = "Подтвердить";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.ForeColor = System.Drawing.Color.Purple;
      this.label2.Location = new System.Drawing.Point(128, 48);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(302, 51);
      this.label2.TabIndex = 19;
      this.label2.Text = "Вы проиграли";
      // 
      // InputRecordForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.ClientSize = new System.Drawing.Size(544, 326);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Name = "InputRecordForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Домино";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    public System.Windows.Forms.Button btnBack;
    public System.Windows.Forms.TextBox textBox1;
    public System.Windows.Forms.Label label1;
    public System.Windows.Forms.Label label2;
  }
}