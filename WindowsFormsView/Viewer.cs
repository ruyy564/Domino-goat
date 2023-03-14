using Model;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsView
{
  /// <summary>
  /// Конкретная реализация Viewer для WinForms
  /// </summary>
  public class Viewer : View.ViewBase
  {
    /// <summary>
    /// Метод для отображения формы
    /// </summary>
    private static Form _viewForm;

    public static Form ViewForm { get => _viewForm; set => _viewForm = value; }

    /// <summary>
    /// Конструктор, который задает класс Viewer
    /// </summary>
    /// <param name="parInitForm"> Форма для инициализации </param>
    public Viewer(Form parInitForm)
    {
      ViewForm = parInitForm;
    }

    /// <summary>
    /// Отбработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void KeyDowning(object parSender, KeyEventArgs parEvent)
    
    {
      KeyDowning(parEvent.KeyValue);
    }

    /// <summary>
    /// Останавливает приложения
    /// </summary>
    public override void StopApp()
    {
      Application.Exit();
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    protected override void Run()
    {
      ViewForm.KeyDown += KeyDowning;
      Application.Run(ViewForm);
    }
  }
}

