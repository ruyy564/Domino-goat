using System;
using System.Windows.Forms;


namespace WindowsFormsView
{
  /// <summary>
  /// Класс, описывающий форму меню
  /// </summary>
  public partial class MenuForm : Form
  {
    /// <summary>
    /// Нажатая на форме кнопка
    /// </summary>
    public Object _senderButton = new Object();

    /// <summary>
    /// Конструктор класса
    /// </summary>
    public MenuForm()
    {
      InitializeComponent();
      Viewer.ViewForm = this;
    }

    /// <summary>
    /// Нажатие на кнопку старта
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnStart_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

    /// <summary>
    /// Нажатие на кнопку выхода
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnExit_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

    /// <summary>
    /// Нажатие на кнопку "Назад"
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnBack_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

    /// <summary>
    /// Нажатие на кнопку "Рекорды"
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnRecords_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

    /// <summary>
    /// Закрытие формы
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void MenuForm_FormClosing(object parSender, FormClosingEventArgs parEvent)
    {
      _senderButton = (MenuForm)parSender;
    }

    /// <summary>
    /// О игре
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnAbout_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

  }
}
