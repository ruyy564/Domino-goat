using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using WindowsFormsView;

namespace WindowsFormsController
{
  /// <summary>
  /// Контроллер игры
  /// </summary>
  public class ControllerGame : Controller.ControllerBase
  {
    /// <summary>
    /// Форма игры
    /// </summary>
    private GameForm _gameForm;

    /// <summary>
    /// Экземпляр игры
    /// </summary>
    private Game _game;

    /// <summary>
    /// Поток для отрисовки игрового поля
    /// </summary>
    private Thread _controllerThread;

    /// <summary>
    /// Поток, который проверяет нажатые кнопки на форме
    /// </summary>
    private Thread _inspectorButtonPressedThread;

    /// <summary>
    /// Getter и Setter для поля _drawerThread
    /// </summary>
    public Thread ControllerThread { get => _controllerThread; set => _controllerThread = value; }

    /// <summary>
    /// Getter и Setter для поля _game
    /// </summary>
    public Game Game { get => _game; set => _game = value; }

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
      _gameForm = (GameForm)Viewer.ViewForm;

      InspectorButtonPressedThread.Start();
    }

    /// <summary>
    /// Инициализирует экземпляр класса ControllerGame
    /// </summary>
    public ControllerGame(int parCountPlayers)
    {
      Game = new Game(parCountPlayers);
      View = new Viewer(new GameForm(Game));
      ControllerThread = new Thread(Render);
      ControllerThread.Start();
      View.KeyDown += KeyDown;

      InspectorButtonPressedThread = new Thread(CheckPressedButton);
    }
    /// <summary>
    /// Метод для потока, который проверяет нажатые на кнопке формы
    /// </summary>
    private void CheckPressedButton()
    {
      while (true)
      {
        if (_gameForm._senderButton == _gameForm)
        {
          
          if (ControllerThread.IsAlive)
          {
            ((Viewer)View).StopApp();
            ControllerThread.Abort();
          }

          InspectorButtonPressedThread.Abort();
        }
      }
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

      InspectorButtonPressedThread.Abort();
      if (Game.Winner != Game.Players.First())
      {
        Game.ScoreWinner = 0;
      }
      View.KeyDown -= KeyDown;
      _gameForm.Invoke(new Action(() => _gameForm.Close()));
      ControllerInputRecordMenu controllerInputRecordMenu = (ControllerInputRecordMenu)FactoryController.GetController(ControllerType.ControllerInputRecordMenu, Game.ScoreWinner);
      controllerInputRecordMenu.RunApp();

    }
  }
}

