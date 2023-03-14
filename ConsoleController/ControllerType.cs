using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleController
{
  /// <summary>
  /// Типы контроллеров
  /// </summary>
  public enum ControllerType
  {
    /// <summary>
    /// Контроллер основного меню
    /// </summary>
    ControllerMenu,

    /// <summary>
    /// Контроллер меню рекордов
    /// </summary>
    ControllerRecordMenu,

    /// <summary>
    /// Контроллер меню справки
    /// </summary>
    ControllerAbout,

    /// <summary>
    /// Контроллер меню ввода рекорда
    /// </summary>
    ControllerInputRecordMenu,

    /// <summary>
    /// Контроллер игры
    /// </summary>
    ControllerGame
  }
}
