using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleView
{
  /// <summary>
  /// Слушатель клавиш
  /// </summary>
  public class ListenerKey
  {
    /// <summary>
    /// Обработчик нажатия клавиши
    /// </summary>
    /// <param name="parMatchKey">Код клавиши</param>
    public delegate void dKeyDown(ConsoleKey parMatchKey);

    /// <summary>
    /// Событие нажатия на клавишу
    /// </summary>
    public event dKeyDown KeyDown;

    /// <summary>
    /// Запуск потока
    /// </summary>
    private Boolean _run;

    /// <summary>
    /// Образец ListenerKey (для Singleton)
    /// </summary>
    private static ListenerKey _listenerKey;

    /// <summary>
    /// Поток для чтения клавиш
    /// </summary>
    public Thread ReadThread { get; private set; }

    /// <summary>
    /// Getter и Setter для поля _run
    /// </summary>
    public bool RunTread { get => _run; set => _run = value; }

    /// <summary>
    /// Инициализирует экземпляр класса ListenerKey
    /// </summary>
    private ListenerKey()
    {
      ReadThread = new Thread(Read);
    }

    /// <summary>
    /// Метод для получения образца ListenerKey 
    /// </summary>
    public static ListenerKey GetListener()
    {
      if (_listenerKey == null)
      {
        _listenerKey = new ListenerKey();
      }
      return _listenerKey;
    }

    /// <summary>
    /// Запускает слушатель
    /// </summary>
    public void Run()
    {
      RunTread = true;
      if (!ReadThread.IsAlive)
      {
        ReadThread.Start();
      }
    }

    /// <summary>
    /// Останавливает слушатель
    /// </summary>
    public void Stop()
    {
      ReadThread.IsBackground = true;
    }

    /// <summary>
    /// Метод для потока, который позволяет слушать клавиши
    /// </summary>
    private void Read()
    {
      while (true)
      {
        ConsoleKey key = Console.ReadKey(true).Key;
        KeyDown.Invoke(key);
      }
    }
  }
}
