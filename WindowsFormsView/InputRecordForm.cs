using System;
using System.Windows.Forms;

namespace WindowsFormsView
{
  public partial class InputRecordForm : Form
  {
    /// <summary>
   /// Нажатая на форме кнопка
   /// </summary>
    public Object _senderButton = new Object();

    /// <summary>
    ///  Начальные настройки
    /// </summary>
    public InputRecordForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// В меню
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void BtnBack_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

  }
}
