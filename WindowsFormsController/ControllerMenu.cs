using System;
using System.Threading;
using WindowsFormsView;

namespace WindowsFormsController
{
  /// <summary>
  /// Контроллер для основного меню
  /// </summary>
  public class ControllerMenu : Controller.ControllerBase
  {
    /// <summary>
    /// Форма основного меню
    /// </summary>
    private static MenuForm _menuForm = new MenuForm();

    /// <summary>
    /// Поток, который проверяет нажатые кнопки на форме
    /// </summary>
    private Thread _inspectorButtonPressedThread;

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

      _menuForm = (MenuForm)Viewer.ViewForm;

      _menuForm.btnBack.Visible = false;
      _menuForm.RecordsBox.Visible = false;
      _menuForm.btnAbout.Visible = true;
      _menuForm.btnStart.Visible = true;
      _menuForm.btnRecords.Visible = true;
      _menuForm.btnExit.Visible = true;

      InspectorButtonPressedThread.Start();
    }

    /// <summary>
    /// Инициализирует экземпляр класса ControllerMenu
    /// </summary>
    public ControllerMenu()
    {
      View = new Viewer(new MenuForm());
      InspectorButtonPressedThread = new Thread(CheckPressedButton);
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parKeyCode">Код клавиши</param>
    public override void KeyDown(int parMatchKey)
    {
    }

    /// <summary>
    /// Метод для потока, который проверяет нажатые на кнопке формы
    /// </summary>
    private void CheckPressedButton()
    {
      while (true)
      {
        if (_menuForm._senderButton == _menuForm.btnStart)
        {
          _menuForm.Invoke(new Action(() => _menuForm.Close()));
          ControllerGame controllerDifficultyMenu = (ControllerGame)FactoryController.GetController(ControllerType.ControllerGame, 2);
          controllerDifficultyMenu.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if (_menuForm._senderButton == _menuForm.btnRecords)
        {
          _menuForm.Invoke(new Action(() => _menuForm.Close()));
          ControllerRecordMenu controllerRecordMenu = (ControllerRecordMenu)FactoryController.GetController(ControllerType.ControllerRecordMenu, 0);
          controllerRecordMenu.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if (_menuForm._senderButton == _menuForm.btnAbout)
        {
          _menuForm.Invoke(new Action(() => _menuForm.Close()));
          ControllerAbout controllerAbout = (ControllerAbout)FactoryController.GetController(ControllerType.ControllerAbout, 0);
          controllerAbout.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if ((_menuForm._senderButton == _menuForm.btnExit) || (_menuForm._senderButton == _menuForm))
        {
          ((Viewer)View).StopApp();
          InspectorButtonPressedThread.Abort();
        }
      }
    }

  }
}
