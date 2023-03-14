using Model;
using System;
using System.Threading;
using WindowsFormsView;

namespace WindowsFormsController
{
  /// <summary>
  /// Контроллер для меню с рекордами
  /// </summary>
  public class ControllerRecordMenu : Controller.ControllerBase
  {
    /// <summary>
    /// Форма основного меню с рекордами
    /// </summary>
    private MenuForm _menuForm;

    /// <summary>
    /// Поток, который проверяет нажатые кнопки на форме
    /// </summary>
    private Thread _inspectorButtonPressedThread;

    /// <summary>
    /// Getter и Setter для поля _menuForm
    /// </summary>
    public MenuForm MenuForm { get => _menuForm; set => _menuForm = value; }

    /// <summary>
    /// Getter и Setter для поля _inspectorButtonPressedThread
    /// </summary>
    public Thread InspectorButtonPressedThread { get => _inspectorButtonPressedThread; set => _inspectorButtonPressedThread = value; }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      ((Viewer)View).RunApp();
      MenuForm.btnStart.Visible = false;
      MenuForm.btnRecords.Visible = false;
      MenuForm.btnExit.Visible = false;
      MenuForm.listBoxAbout.Visible = false;
      MenuForm.btnAbout.Visible = false;
      MenuForm.btnBack.Visible = true;
      MenuForm.RecordsBox.Visible = true;
      InspectorButtonPressedThread.Start();
    }

    /// <summary>
    /// Инициализирует экземпляр класса ControllerRecordMenu
    /// </summary>
    public ControllerRecordMenu()
    {
      View = new Viewer(new MenuForm());
      MenuForm = (MenuForm)Viewer.ViewForm;
      MenuForm.RecordsBox.DataSource = Recorder.GetListRecords();
      InspectorButtonPressedThread = new Thread(CheckPressedButton);
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parKeyCode">Код клавиши</param>
    public override void KeyDown(int parKeyCode) { }

    /// <summary>
    /// Метод для потока, который проверяет нажатые на кнопке формы
    /// </summary>
    private void CheckPressedButton()
    {
      while (true)
      {
        if (MenuForm._senderButton == MenuForm.btnBack)
        {
          MenuForm.Invoke(new Action(() => MenuForm.Close()));
          ControllerMenu controllerMenu = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
          controllerMenu.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if (MenuForm._senderButton == MenuForm)
        {
          ((Viewer)View).StopApp();
          InspectorButtonPressedThread.Abort();
        }
      }
    }
  }
}
