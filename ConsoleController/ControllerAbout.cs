using ConsoleView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleController
{
  /// <summary>
  /// Класс контроллера "Об игре"
  /// </summary>
  public class ControllerAbout : Controller.ControllerBase
  {
    /// <summary>
    /// Основное меню
    /// </summary>
    private Menu _aboutMenu;

    /// <summary>
    /// Отрисовщик основых панелей
    /// </summary>
    private Drawer _drawer;

    /// <summary>
    /// Выбранный пункт меню
    /// </summary>
    private int _selectedMenuItem;

    /// <summary>
    /// Getter и Setter для поля _aboutMenu
    /// </summary>
    public Menu AboutMenu { get => _aboutMenu; set => _aboutMenu = value; }

    /// <summary>
    /// Getter и Setter для поля _selectedMenuItem
    /// </summary>
    public int SelectedMenuItem { get => _selectedMenuItem; set => _selectedMenuItem = value; }

    /// <summary>
    /// Getter и Setter для поля _drawer
    /// </summary>
    public Drawer Drawer { get => _drawer; set => _drawer = value; }

    /// <summary>
    /// Инициализирует экземпляр класса
    /// </summary>
    public ControllerAbout()
    {
      View = new Viewer();
      Drawer = new Drawer();
      List<MenuItem> recordMenuItems = new List<MenuItem>
            {
                new MenuItem("Чтобы вернуться в меню нажмите Enter...")
            };
      AboutMenu = new Menu(recordMenuItems);
      Console.CursorVisible = false;
      View.KeyDown += KeyDown;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      Drawer.DrawAbout();
      Drawer.ActivateMenuItem(SelectedMenuItem, SelectedMenuItem - 15, AboutMenu);
      
      ((Viewer)View).RunApp();
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKeyCode">Код клавиши</param>
    public override void KeyDown(int parMatchKeyCode)
    {
      if (parMatchKeyCode == 13)
      {
        View.KeyDown -= KeyDown;
        ControllerMenu controllerMenu = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
        controllerMenu.RunApp();
      }
    }
  }
}
