using System;

namespace WindowsFormsController
{
  /// <summary>
  /// Статический класс-фабрика для производства контроллеров
  /// </summary>
  public static class FactoryController
  {
    /// <summary>
    /// Статический метод, который возвращает нужный контроллер
    /// </summary>
    /// <param name="parControllerType">Тип возвращаемого контроллера</param>
    /// <param name="parMatchArgController"> Целочисленный аргумент для некоторых контроллеров (Контроллер игры и Контроллер ввода рекордов) </param>
    /// <returns></returns>
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
