namespace WindowsFormsView
{
  partial class MenuForm
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
      this.btnBack = new System.Windows.Forms.Button();
      this.RecordsBox = new System.Windows.Forms.ListBox();
      this.btnExit = new System.Windows.Forms.Button();
      this.btnRecords = new System.Windows.Forms.Button();
      this.btnStart = new System.Windows.Forms.Button();
      this.listBoxAbout = new System.Windows.Forms.ListBox();
      this.btnAbout = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // btnBack
      // 
      this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnBack.ForeColor = System.Drawing.Color.Purple;
      this.btnBack.Location = new System.Drawing.Point(272, 304);
      this.btnBack.Margin = new System.Windows.Forms.Padding(4);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(136, 41);
      this.btnBack.TabIndex = 17;
      this.btnBack.Text = "Назад";
      this.btnBack.UseVisualStyleBackColor = true;
      this.btnBack.Visible = false;
      this.btnBack.Click += new System.EventHandler(this.BtnBack_Click);
      // 
      // RecordsBox
      // 
      this.RecordsBox.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.RecordsBox.FormattingEnabled = true;
      this.RecordsBox.ItemHeight = 33;
      this.RecordsBox.Location = new System.Drawing.Point(16, 16);
      this.RecordsBox.Margin = new System.Windows.Forms.Padding(4);
      this.RecordsBox.Name = "RecordsBox";
      this.RecordsBox.Size = new System.Drawing.Size(641, 268);
      this.RecordsBox.TabIndex = 16;
      this.RecordsBox.Visible = false;
      // 
      // btnExit
      // 
      this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnExit.ForeColor = System.Drawing.Color.Purple;
      this.btnExit.Location = new System.Drawing.Point(272, 208);
      this.btnExit.Margin = new System.Windows.Forms.Padding(4);
      this.btnExit.Name = "btnExit";
      this.btnExit.Size = new System.Drawing.Size(136, 41);
      this.btnExit.TabIndex = 12;
      this.btnExit.Text = "Выход";
      this.btnExit.UseVisualStyleBackColor = true;
      this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
      // 
      // btnRecords
      // 
      this.btnRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnRecords.ForeColor = System.Drawing.Color.Purple;
      this.btnRecords.Location = new System.Drawing.Point(272, 112);
      this.btnRecords.Margin = new System.Windows.Forms.Padding(4);
      this.btnRecords.Name = "btnRecords";
      this.btnRecords.Size = new System.Drawing.Size(136, 41);
      this.btnRecords.TabIndex = 11;
      this.btnRecords.Text = "Рекорды";
      this.btnRecords.UseVisualStyleBackColor = true;
      this.btnRecords.Click += new System.EventHandler(this.BtnRecords_Click);
      // 
      // btnStart
      // 
      this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnStart.ForeColor = System.Drawing.Color.Purple;
      this.btnStart.Location = new System.Drawing.Point(272, 64);
      this.btnStart.Margin = new System.Windows.Forms.Padding(4);
      this.btnStart.Name = "btnStart";
      this.btnStart.Size = new System.Drawing.Size(136, 41);
      this.btnStart.TabIndex = 10;
      this.btnStart.Text = "Начать";
      this.btnStart.UseVisualStyleBackColor = true;
      this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
      // 
      // listBoxAbout
      // 
      this.listBoxAbout.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.listBoxAbout.ForeColor = System.Drawing.Color.Purple;
      this.listBoxAbout.FormattingEnabled = true;
      this.listBoxAbout.ItemHeight = 18;
      this.listBoxAbout.Items.AddRange(new object[] {
            "Управление:",
            "Стрелочка влево - переместить курсор влево",
            "Стрелочка вправо - переместить курсор вправо",
            "Space или Enter - походить",
            "Переместить курсор на базар можно используя стрелки влево/вправо",
            "Правила:",
            "Домино должно быть 28 штук",
            "Перемешиваем кости.Берем домино.",
            "2 игрока — берут по 7 костей каждый;",
            "3–4 игрока — берут по 5 костей каждый;",
            "Первый камень на центр стола кладет игрок, у которого в руке есть дубль с меньшим" +
                " ",
            "количеством очков, 0–0, 1–1, 2–2 и так далее.",
            "Если ни у кого нет дублей, ставится простая кость с меньшим количество ",
            "очков, например, 0–1, 0–2 и так далее.",
            "Игра заканчивается когда у одного из игроков закончатся кости или когда получаетс" +
                "я «Рыба».",
            "Все остальные игроки с костями на руках начинают подсчитывать очки. "});
      this.listBoxAbout.Location = new System.Drawing.Point(16, 24);
      this.listBoxAbout.Margin = new System.Windows.Forms.Padding(4);
      this.listBoxAbout.Name = "listBoxAbout";
      this.listBoxAbout.Size = new System.Drawing.Size(641, 256);
      this.listBoxAbout.TabIndex = 25;
      this.listBoxAbout.Visible = false;
      // 
      // btnAbout
      // 
      this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.btnAbout.ForeColor = System.Drawing.Color.Purple;
      this.btnAbout.Location = new System.Drawing.Point(272, 160);
      this.btnAbout.Margin = new System.Windows.Forms.Padding(4);
      this.btnAbout.Name = "btnAbout";
      this.btnAbout.Size = new System.Drawing.Size(136, 41);
      this.btnAbout.TabIndex = 12;
      this.btnAbout.Text = "Справка";
      this.btnAbout.UseVisualStyleBackColor = true;
      this.btnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
      // 
      // MenuForm
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
      this.ClientSize = new System.Drawing.Size(675, 363);
      this.Controls.Add(this.btnAbout);
      this.Controls.Add(this.listBoxAbout);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.RecordsBox);
      this.Controls.Add(this.btnExit);
      this.Controls.Add(this.btnRecords);
      this.Controls.Add(this.btnStart);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "MenuForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Домино";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuForm_FormClosing);
      this.ResumeLayout(false);

    }

    #endregion
    public System.Windows.Forms.Button btnBack;
    public System.Windows.Forms.ListBox RecordsBox;
    public System.Windows.Forms.Button btnExit;
    public System.Windows.Forms.Button btnRecords;
    public System.Windows.Forms.Button btnStart;
    public System.Windows.Forms.ListBox listBoxAbout;
    public System.Windows.Forms.Button btnAbout;
  }
}