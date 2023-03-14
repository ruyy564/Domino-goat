using ConsoleController;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
  /// <summary>
  /// Класс для запуска консольной версии
  /// </summary>
  class Program
  {
    /// <summary>
    /// Точка входа в программу
    /// </summary>
    /// <param name="parArgs"></param>
    static void Main(string[] parArgs)
    {
      ControllerMenu controller = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
      controller.RunApp();
    }
  }
}
