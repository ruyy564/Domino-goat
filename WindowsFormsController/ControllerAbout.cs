using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsView;

namespace WindowsFormsController
{
  /// <summary>
  /// Контроллер для меню справки
  /// </summary>
  public class ControllerAbout : Controller.ControllerBase
  {
    /// <summary>
    /// Форма основного меню справки
    /// </summary>
    private MenuForm _menuForm;

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

      _menuForm.btnStart.Visible = false;
      _menuForm.btnRecords.Visible = false;
      _menuForm.btnExit.Visible = false;
      _menuForm.btnAbout.Visible = false;
      _menuForm.btnBack.Visible = true;
      _menuForm.listBoxAbout.Visible = true;
      InspectorButtonPressedThread.Start();
    }

    /// <summary>
    /// Инициализирует экземпляр класса ControllerAboutMenu
    /// </summary>
    public ControllerAbout()
    {
      View = new Viewer(new MenuForm());
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
        if (_menuForm._senderButton == _menuForm.btnBack)
        {
          _menuForm.Invoke(new Action(() => _menuForm.Close()));
          ControllerMenu controllerMenu = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
          controllerMenu.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if (_menuForm._senderButton == _menuForm)
        {
          ((Viewer)View).StopApp();
          InspectorButtonPressedThread.Abort();
        }
      }
    }
  }
}
