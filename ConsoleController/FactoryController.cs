using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleController
{
  /// <summary>
  /// Класс фабрики контроллеров
  /// </summary>
  public class FactoryController
  {
    /// <summary>
    /// Статический метод, который возвращает нужный контроллер
    /// </summary>
    /// <param name="parMatchControllerType"> Тип возвращаемого контроллера </param>
    /// <param name="parMatchArgController"> Целочисленный аргумент для некоторых контроллеров (Контроллер игры и Контроллер ввода рекордов) </param>
    /// <returns>Нужный контроллер</returns>
    public static Controller.ControllerBase GetController(ControllerType parMatchControllerType, int parMatchArgController)
    {
      switch (parMatchControllerType)
      {
        case ControllerType.ControllerMenu: return new ControllerMenu();
        case ControllerType.ControllerRecordMenu: return new ControllerRecordMenu();
        case ControllerType.ControllerAbout: return new ControllerAbout();
        case ControllerType.ControllerGame: return new ControllerGame(parMatchArgController);
        case ControllerType.ControllerInputRecordMenu: return new ControllerInputRecordMenu(parMatchArgController);
      }

      return null;
    }
  }
}
