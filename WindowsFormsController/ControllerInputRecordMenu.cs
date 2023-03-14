using System;
using System.Threading;
using WindowsFormsView;
using Model;
using System.Collections.Generic;
using System.Text;
namespace WindowsFormsController
{
  /// <summary>
  /// Класс контроллера "ControllerInputRecordMenu"
  /// </summary>
  public class ControllerInputRecordMenu : Controller.ControllerBase
  {

    /// <summary>
    /// Счёт игрока
    /// </summary>
    private int _playerScore;

    /// <summary>
    /// Форма основного меню
    /// </summary>
    private InputRecordForm _inputRecordForm;

    /// <summary>
    /// Поток, который проверяет нажатые кнопки на форме
    /// </summary>
    private Thread _inspectorButtonPressedThread;

    /// <summary>
    /// Getter и Setter для поля _playerScore
    /// </summary>
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }

    /// <summary>
    /// Getter и Setter для поля _inputRecordForm
    /// </summary>
    public InputRecordForm InputRecordForm { get => _inputRecordForm; set => _inputRecordForm = value; }

    /// <summary>
    /// Getter и Setter для поля _inspectorButtonPressedThread
    /// </summary>
    public Thread InspectorButtonPressedThread { get => _inspectorButtonPressedThread; set => _inspectorButtonPressedThread = value; }

    /// <summary>
    /// Инициализирует экземпляр класса
    /// </summary>
    /// <param name="parMatchPoints">Очки игры</param>
    public ControllerInputRecordMenu(int parMatchPoints)
    {
      View = new Viewer(new InputRecordForm());
      PlayerScore = parMatchPoints;

      InspectorButtonPressedThread = new Thread(CheckPressedButton);
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      ((Viewer)View).RunApp();
      InputRecordForm = (InputRecordForm)Viewer.ViewForm;

      if (PlayerScore != 0)
      {
        InputRecordForm.textBox1.Visible = true;
        InputRecordForm.label1.Visible = true;
        InputRecordForm.label2.Visible = false;
      }
      else
      {
        InputRecordForm.label1.Visible = false;
        InputRecordForm.label2.Visible = true;
      }
      InspectorButtonPressedThread.Start();
    }

    /// <summary>
    /// Метод для потока, который проверяет нажатые на кнопке формы
    /// </summary>
    private void CheckPressedButton()
    {
      while (true)
      {
        if (InputRecordForm._senderButton == InputRecordForm.btnBack)
        {

          if (PlayerScore != 0)
          {
            Recorder.AddRecord(PlayerScore, InputRecordForm.textBox1.Text.Trim(' '));
          }
          InputRecordForm.Invoke(new Action(() => InputRecordForm.Close()));
          ControllerMenu controllerMenu = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
          controllerMenu.RunApp();
          InspectorButtonPressedThread.Abort();
        }

        if (InputRecordForm._senderButton == InputRecordForm)
        {
          ((Viewer)View).StopApp();
          InspectorButtonPressedThread.Abort();
        }
      }
    }
    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKeyCode">Код клавиши</param>
    public override void KeyDown(int parMatchKeyCode)
    {

    }
  }
}


