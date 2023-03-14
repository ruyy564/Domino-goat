using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace View
{
  /// <summary>
  /// Абстрактный класс для представления игры
  /// </summary>
  public abstract class ViewBase
  {
    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parKey">Код клавиши</param>
    public delegate void dDelegateKeyDown(int parKey);

    /// <summary>
    /// Событие нажатия на клавишу
    /// </summary>
    public event dDelegateKeyDown KeyDown;

    /// <summary>
    /// Вызывает событие нажатия на клавишу
    /// </summary>
    /// <param name="parKey">Клавиша</param>
    protected void KeyDowning(int parKey)
    {
      KeyDown?.Invoke(parKey);
    }

    /// <summary>
    /// Запускает приложение
    /// </summary>
    public void RunApp()
    {
      Thread thread = new Thread(Run);
      thread.Start();
    }

    /// <summary>
    /// Останавливает приложение
    /// </summary>
    public abstract void StopApp();

    /// <summary>
    /// Основной метод приложения
    /// </summary>
    protected abstract void Run();
  }
}
