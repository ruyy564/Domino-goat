using ConsoleView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleController
{
  /// <summary>
  /// Класс контроллера "ControllerInputRecordMenu"
  /// </summary>
  public class ControllerInputRecordMenu : Controller.ControllerBase
  {
    /// <summary>
    /// Имя игрока
    /// </summary>
    private string _playerName;

    /// <summary>
    /// Счёт игрока
    /// </summary>
    private int _playerScore;

    /// <summary>
    /// Отрисовщик в консоли
    /// </summary>
    private Drawer _drawer;

    /// <summary>
    /// Getter и Setter для поля _playerScore
    /// </summary>
    public int PlayerScore { get => _playerScore; set => _playerScore = value; }

    /// <summary>
    /// Getter и Setter для поля _drawer
    /// </summary>
    public Drawer Drawer { get => _drawer; set => _drawer = value; }

    /// <summary>
    /// Инициализирует экземпляр класса
    /// </summary>
    /// <param name="parMatchPoints">Очки игры</param>
    public ControllerInputRecordMenu(int parMatchPoints)
    { 
      View = new Viewer();
      Drawer = new Drawer();
      PlayerScore = parMatchPoints;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {

      if (PlayerScore != 0)
      {
        Console.CursorVisible = true;
        Drawer.DrawInputName(PlayerScore);
        _playerName = Console.ReadLine();
      }
      else
      {
        Drawer.DrawLossMessage();
      }
      Drawer.DrawInputNameEnd();

      View.KeyDown += KeyDown;
      ((Viewer)View).RunApp();
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKeyCode">Код клавиши</param>
    public override void KeyDown(int parMatchKeyCode)
    {
      //Клавиша Enter 
      if (parMatchKeyCode == 13)
      {
        View.KeyDown -= KeyDown;

        if (PlayerScore != 0)
        {
          Recorder.AddRecord(PlayerScore, _playerName.Trim(' '));
        }
        ControllerMenu controllerMenu = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
        controllerMenu.RunApp();
      }
    }
  }
}
