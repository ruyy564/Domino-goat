using ConsoleView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleController
{
  /// <summary>
  /// Класс контроллера "ControllerMenu"
  /// </summary>
  public class ControllerMenu : Controller.ControllerBase
  {
    /// <summary>
    /// Основное меню
    /// </summary>
    private Menu _mainMenu;

    /// <summary>
    /// Отрисовщик в консоли
    /// </summary>
    private Drawer _drawer;

    /// <summary>
    /// Номер выбранного пункта меню
    /// </summary>
    private int _selectedMenuItem = 0;

    /// <summary>
    /// Getter и Setter для поля _mainMenu
    /// </summary>
    public Menu MainMenu { get => _mainMenu; set => _mainMenu = value; }

    /// <summary>
    /// Getter и Setter для поля _drawer
    /// </summary>
    public Drawer Drawer { get => _drawer; set => _drawer = value; }

    /// <summary>
    /// Инициализирует экземпляр класса
    /// </summary>
    public ControllerMenu()
    {
      View = new Viewer();
      Drawer = new Drawer();
      View.KeyDown += KeyDown;
      ((Viewer)View).RunApp();
      List<MenuItem> mainMenuItems = new List<MenuItem>
            {
                new MenuItem("Новая игра"),
                new MenuItem("Рекорды"),
                new MenuItem("Об игре"),
                new MenuItem("Выйти")
            };
      MainMenu = new Menu(mainMenuItems);
      Console.CursorVisible = false;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      Drawer.DrawMenu(MainMenu);
      Drawer.ActivateMenuItem(_selectedMenuItem, _selectedMenuItem * 2, MainMenu);
      
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKeyCode">Код клавиши</param>
    public override void KeyDown(int parMatchKeyCode)
    {
      switch (parMatchKeyCode)
      {
        case 13:
          CallOperation();
          break;
        case 38:
          if (_selectedMenuItem > 0)
          {
            _selectedMenuItem--;
          }
          Drawer.DrawMenu(MainMenu);
          Drawer.ActivateMenuItem(_selectedMenuItem, _selectedMenuItem * 2, MainMenu);
          break;
        case 40:
          if (_selectedMenuItem < MainMenu.Items.Count - 1)
          {
            _selectedMenuItem++;
          }
          Drawer.DrawMenu(MainMenu);
          Drawer.ActivateMenuItem(_selectedMenuItem, _selectedMenuItem * 2, MainMenu);
          break;
      }
    }

    /// <summary>
    /// Вызывает методы при выборе соответствующего пункта меню
    /// </summary>
    private void CallOperation()
    {
      View.KeyDown -= KeyDown;
      switch (_selectedMenuItem)
      {
        case 0:
          ControllerGame controllerGame = (ControllerGame)FactoryController.GetController(ControllerType.ControllerGame, 2);
          controllerGame.RunApp();
          break;
        case 1:
          ControllerRecordMenu controllerRecordMenu = (ControllerRecordMenu)FactoryController.GetController(ControllerType.ControllerRecordMenu, 0);
          controllerRecordMenu.RunApp();
          break;
        case 2:
          ControllerAbout controllerAbout = (ControllerAbout)FactoryController.GetController(ControllerType.ControllerAbout, 0);
          controllerAbout.RunApp();
          break;
        case 3:
          View.StopApp();
          break;
      }
    }
  }
}
