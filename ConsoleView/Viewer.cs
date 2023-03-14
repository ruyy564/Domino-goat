using Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace ConsoleView
{
  /// <summary>
  /// Конкретная реализация Viewer для консоли
  /// </summary>
  public class Viewer : View.ViewBase
  {
    /// <summary>
    /// Слушатель клавиш
    /// </summary>
    public ListenerKey Listener { get; private set; }

    /// <summary>
    /// Конструктор класса Viewer
    /// </summary>
    public Viewer()
    {
      if (Listener != null)
      {
        Listener.KeyDown -= KeyDowning;
      }
      Listener = ListenerKey.GetListener();
      Listener.KeyDown += KeyDowning;
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    protected override void Run()
    {
      Listener.Run();
    }

    /// <summary>
    /// Останавливает приложение
    /// </summary>
    public override void StopApp()
    {
      Listener.Stop();
    }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parMatchKey"></param>
    private void KeyDowning(ConsoleKey parMatchKey)
    {
      KeyDowning((int)parMatchKey);
    }

  }
}
