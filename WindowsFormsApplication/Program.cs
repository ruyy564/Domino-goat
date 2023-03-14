using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsController;

namespace WindowsFormsApplication
{
  /// <summary>
  /// Класс для запуска приложения
  /// </summary>
  static class Program
  {
    /// <summary>
    /// Главная точка входа для приложения
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      ControllerMenu controller = (ControllerMenu)FactoryController.GetController(ControllerType.ControllerMenu, 0);
      controller.RunApp();
    }
  }
}
