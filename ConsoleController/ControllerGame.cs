using ConsoleView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Linq;

namespace ConsoleController
{
  /// <summary>
  /// Класс контроллера "ControllerGame"
  /// </summary>
  public class ControllerGame : Controller.ControllerBase
  {
    /// <summary>
    /// Отрисовщик в консоли
    /// </summary>
    private ConsoleView.Drawer _drawer;

    /// <summary>
    /// Экземпляр игры
    /// </summary>
    private Game _game;

    /// <summary>
    /// Поток для отрисовки игрового поля
    /// </summary>
    private Thread _controllerThread;

    /// <summary>
    /// Getter и Setter для поля _drawer
    /// </summary>
    public Drawer Drawer { get => _drawer; set => _drawer = value; }

    /// <summary>
    /// Getter и Setter для поля _game
    /// </summary>
    public Game Game { get => _game; set => _game = value; }

    /// <summary>
    /// Getter и Setter для поля _drawerThread
    /// </summary>
    public Thread ControllerThread { get => _controllerThread; set => _controllerThread = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCountPlayers">Количество игроков</param>
    public ControllerGame(int parCountPlayers)
    {
      View = new Viewer();
      Game = new Game(parCountPlayers);
      Drawer = new Drawer();
      Drawer.Draw(Game);
      ControllerThread = new Thread(Render);
      ControllerThread.Start();
      View.KeyDown += KeyDown;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      
      ((Viewer)View).RunApp();
    }

    /// <summary>
    /// Метод проверки на окончание игры
    /// </summary>
    public void Render()
    {
      while (true)
      {
        if (Game.GameIsEnd)
        {
          break;
        }
      }
      
      EndGame();
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKey">Код клавиши</param>
    public override void KeyDown(int parMatchKey)
    {
      //Влево
      if (parMatchKey == 37)
      {
        Game.MoveLeft();
      }

      //Вправо
      if (parMatchKey == 39)
      {
        Game.MoveRight();
      }

      //Enter или Space
      if ((parMatchKey == 13) || (parMatchKey == 32))
      {
        Game.MoveAccept();
      }

    }

    /// <summary>
    /// Метод для запуска конца игры
    /// </summary>
    private void EndGame()
    {
      Game.FindWinner();
      if (Game.Winner != Game.Players.First())
      {
        Game.ScoreWinner = 0;
      }
      View.KeyDown -= KeyDown;
      ControllerInputRecordMenu controllerInputRecordMenu = (ControllerInputRecordMenu)FactoryController.GetController(ControllerType.ControllerInputRecordMenu, Game.ScoreWinner);
      controllerInputRecordMenu.RunApp();
    }

  }
}
