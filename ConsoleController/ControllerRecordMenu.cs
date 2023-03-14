using ConsoleView;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleController
{
  /// <summary>
  /// Класс контроллера "ControllerRecordMenu"
  /// </summary>
  class ControllerRecordMenu : Controller.ControllerBase
  {
    /// <summary>
    /// Основное меню
    /// </summary>
    private Menu _recordMenu;

    /// <summary>
    /// Отрисовщик в консоли
    /// </summary>
    private Drawer _drawer;

    /// <summary>
    /// Номер выбранного пункта меню
    /// </summary>
    private int _selectedMenuItem = 0;

    /// <summary>
    /// Getter и Setter для поля _recordMenu
    /// </summary>
    public Menu RecordMenu { get => _recordMenu; set => _recordMenu = value; }

    /// <summary>
    /// Getter и Setter для поля _selectedMenuItem
    /// </summary>
    public int SelectedMenuItem { get => _selectedMenuItem; set => _selectedMenuItem = value; }

    /// <summary>
    /// Инициализирует экземпляр класса ControllerRecordMenu
    /// </summary>
    public ControllerRecordMenu()
    {
      View = new Viewer();
      List<MenuItem> recordMenuItems = new List<MenuItem>
            {
                new MenuItem("Чтобы вернуться в меню нажмите Enter...")
            };
      RecordMenu = new Menu(recordMenuItems);
      Console.CursorVisible = false;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      _drawer = new Drawer();
      List<string> records = Recorder.GetListRecords();
      _drawer.DrawRecords(records);
      _drawer.ActivateMenuItem(SelectedMenuItem, SelectedMenuItem - 15, RecordMenu);
      View.KeyDown += KeyDown;
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
