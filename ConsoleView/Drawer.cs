using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ConsoleView
{
  /// <summary>
  /// Отрисовщик 
  /// </summary>
  public class Drawer
  {
    /// <summary>
    /// Ширина экрана
    /// </summary>
    public const int WIDTH_WINDOW = 170;

    /// <summary>
    /// Ширина буфера экрана
    /// </summary>
    public const int WIDTH_BUFFER_WINDOW = 250;
    
    /// <summary>
    /// Высота экрана
    /// </summary>
    public const int HEIGHT_WINDOW = 40;

    /// <summary>
    /// Поток для отрисовки игрового поля
    /// </summary>
    private Thread _drawerThread;

    /// <summary>
    /// Блокировочный объект для потоков
    /// </summary>
    private static Player _locker ;

    /// <summary>
    /// Игра
    /// </summary>
    private Game _game;

    /// <summary>
    /// Блокировочный объект для потоков
    /// </summary>
    protected static Player Locker { get => _locker; set => _locker = value; }

    /// <summary>
    /// Getter и Setter для поля _drawerThread
    /// </summary>
    public Thread DrawerThread { get => _drawerThread; set => _drawerThread = value; }


    /// <summary>
    /// Getter и Setter для поля _drawerThread
    /// </summary>
    public Game Game { get => _game; set => _game = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Drawer()
    {
      FastOutput.Clear();
      Console.SetWindowSize(WIDTH_WINDOW, HEIGHT_WINDOW);
      Console.Title = "Домино козел";
      Console.SetBufferSize(WIDTH_BUFFER_WINDOW, HEIGHT_WINDOW);
    }
    
    /// <summary>
    /// Запуск потока отрисовки игрового поля
    /// </summary>
    /// <param name="parGame"></param>
    public void Draw(Game parGame)
    {
      Locker = parGame.Players.ElementAt(0);
      DrawerThread = new Thread(Render);
      DrawerThread.Start();
      Game = parGame;
    }

    /// <summary>
    /// Метод для отрисовки состояния игры
    /// </summary>
    public void Render()
    {
      while (!Game.GameIsEnd)
      {
        lock (Locker) {
          DrawPanel(Game.Players, Game);
          DrawBazzar(Game.Bazaar);
          DrawSkip(Game.Bazaar);
          DrawGameTable(Game.GameTable);
        }
      }
    }

    /// <summary> 
    /// Внутренний метод класса, позволяющий рисовать пункты меню
    /// </summary>
    /// <param name="selectedPosition">Позиция меню</param>
    ///  /// <param name="parMatchItem">Пункт меню</param>
    private void DrawMenuItem(int parMatchItemPosition, MenuItem parMatchItem)
    {
      FastOutput.Write(parMatchItem.TextMenuItem, 80, parMatchItemPosition + 15, ConsoleColor.White);
    }

    /// <summary>
    /// Отрисовка пропуска хода
    /// </summary>
    public void DrawSkip(Bazzar parBazzar)
    {
      ConsoleColor color = ConsoleColor.DarkGray;
      int x = 5;
      int y = 25;

      if (parBazzar.SelectSkip)
      {
        color = ConsoleColor.Magenta;
      }
      FastOutput.Write("Пропуск хода", x, y - 2, color);
      FastOutput.Write("╔═══╗", x, y - 1, color);
      FastOutput.Write("║   ║", x, y++, color);
      FastOutput.Write("╚═══╝", x, y++, color);
    }
    /// <summary>
    /// Отрисовка меню
    /// </summary>
    /// <param name="parMatchMainMenu">Меню</param>
    public void DrawMenu(Menu parMatchMainMenu)
    {
      int x = 67;
      int y = 10;
      ConsoleColor color = ConsoleColor.DarkMagenta;
      FastOutput.Write(" ╔══╗   ╔══╗  ╔═╗ ╔═╗ ║  ║║ ║  ║ ╔══╗ ", x, y++, color);
      FastOutput.Write(" ║  ║   ║  ║  ║ ║ ║ ║ ║ ║ ║ ║══║ ║  ║", x, y++, color);
      FastOutput.Write("╔════╗  ╚══╝  ║ ╚═╝ ║ ║╝  ║ ║  ║ ╚══╝ ", x, y++, color);

      for (int i = 0; i < parMatchMainMenu.Items.Count; i++)
      {
        DrawMenuItem(i * 2, parMatchMainMenu.Items[i]);
      }
      FastOutput.PrintOnConsole();
    }

    /// <summary>
    /// Отрисовка базара
    /// </summary>
    /// <param name="parBazzar">Базар</param>
    public void DrawBazzar(Bazzar parBazzar)
    {
      ConsoleColor color = ConsoleColor.DarkGray;
      int x = 5;
      int y = 30;

      if (parBazzar.SelectBazzar)
      {
        color = ConsoleColor.Magenta;
      }
      FastOutput.Write("Базар", x, y - 2, color);
      FastOutput.Write("╔═══╗", x, y - 1, color);
      String space = (parBazzar.Data.Count >= 10) ? " " : "  ";
      FastOutput.Write("║" + parBazzar.Data.Count + space + "║", x, y++, color);
      FastOutput.Write("╚═══╝", x, y++, color);

    }

    /// <summary>
    /// Отрисовка домино
    /// </summary>
    /// <param name="parDomino">Домино</param>
    /// <param name="parX">Координата х</param>
    /// <param name="parY">Координата у</param>
    public void DrawDomino(Domino parDomino, int parX, int parY)
    {
      ConsoleColor color = ConsoleColor.DarkGray;

      if (parDomino.View != Domino.ViewDomino.Close)
      {

        if (parDomino.Status == Domino.StatusDomino.Available)
        {
          color = ConsoleColor.White;
        }

        if (parDomino.Status == Domino.StatusDomino.Selected)
        {
          color = ConsoleColor.Magenta;
          parY -= 1;
        }
        FastOutput.Write("╔══╗", parX, parY - 1, color);
        FastOutput.Write("║" + parDomino.Value[0] + " ║", parX, parY++, color);
        FastOutput.Write("╠══╣", parX, parY++, color);
        FastOutput.Write("║" + parDomino.Value[1] + " ║", parX, parY++, color);
        FastOutput.Write("╚══╝", parX, parY++, color);
      }
      else
      {
        color = ConsoleColor.White;
        FastOutput.Write("╔══╗", parX, parY - 1, color);
        FastOutput.Write("║  ║", parX, parY++, color);
        FastOutput.Write("╠══╣", parX, parY++, color);
        FastOutput.Write("║  ║", parX, parY++, color);
        FastOutput.Write("╚══╝", parX, parY++, color);
      }
    }

    /// <summary>
    /// Отрисовка домино в руках у игроков
    /// </summary>
    /// <param name="parPlayers">Список игроков</param>
    /// <param name="parGame">Игра</param>
    public void DrawPanel(List<Player> parPlayers, Game parGame)
    {
      FastOutput.Clear();
      int x = 0;
      lock (Player.Locker)
      {

      if (parGame.Message != null)
      {
        FastOutput.Write(parGame.Message, 5, 7, ConsoleColor.Magenta);
      }

      for (int j = 0; j < parPlayers.Count; j++)
      {
        int y = 0;
        x = j > 1 ? x + 10 : 10;
        int count = parPlayers.ElementAt(j).Hand.Count;

        for (int i = 0; i < count; i++)
        {
          y = j == 0 ? 30 : 1;
          x += 4;
          Domino domino = parPlayers.ElementAt(j).GetDominoInHand(i);
          DrawDomino(domino, x, y);
        }
        }
      }
    }

    /// <summary>
    /// Отрисовка игрового поля
    /// </summary>
    /// <param name="parGameTable">Игровое поле</param>
    public void DrawGameTable(GameTable parGameTable)
    {
      int x = 10;
      ConsoleColor color = ConsoleColor.White;

      for (int i = 0; i < parGameTable.Data.Count; i++)
      {
        int y = 10;
        int[] valueDomino = parGameTable.Data.ElementAt(i).Value;

        if (valueDomino[0] == valueDomino[1])
        {
          y -= 1;
          FastOutput.Write("╔══╗", x, y - 1, color);
          FastOutput.Write("║" + valueDomino[0] + " ║", x, y++, color);
          FastOutput.Write("╠══╣", x, y++, color);
          FastOutput.Write("║" + valueDomino[1] + " ║", x, y++, color);
          FastOutput.Write("╚══╝", x, y++, color);
          x += 4;
        }
        else
        {
          FastOutput.Write("╔══╦══╗", x, y - 1, color);
          FastOutput.Write("║" + valueDomino[0] + " ║" + valueDomino[1] + " ║", x, y++, color);
          FastOutput.Write("╚══╩══╝", x, y++, color);
          x += 7;
        }
      }
      FastOutput.PrintOnConsole();
    }

    /// <summary>
    /// Активирует выбранную позицию меню
    /// </summary>
    /// <param name="parMatchSelectedPosition">Выбранная позиция меню</param>
    public void ActivateMenuItem(int parMatchSelectedPosition, int parY, Menu parMatchMainMenu)
    {
      MenuItem currentItem = parMatchMainMenu.Items[parMatchSelectedPosition];
      FastOutput.Write(currentItem.TextMenuItem, 80, 15 + parY, ConsoleColor.Magenta);
      FastOutput.PrintOnConsole();
    }


    /// <summary>
    /// Отрисовка надписей меню About
    /// </summary>
    public void DrawAbout()
    {
      FastOutput.Clear();
      FastOutput.Write("Управление: ", 0, 2, ConsoleColor.Magenta);
      FastOutput.Write("Стрелочка влево - переместить курсор влево", 0, 3, ConsoleColor.White);
      FastOutput.Write("Стрелочка вправо - переместить курсор вправо", 0, 4, ConsoleColor.White);
      FastOutput.Write("Space или Enter - походить", 0, 5, ConsoleColor.White);
      FastOutput.Write("Переместить курсор на базар можно используя стрелки влево/вправо", 0, 6, ConsoleColor.White);
      FastOutput.Write("Правила:", 0, 7, ConsoleColor.Magenta);
      FastOutput.Write("Домино должно быть 28 штук", 0, 8, ConsoleColor.White);
      FastOutput.Write("Перемешиваем кости.Берем домино.", 0, 9, ConsoleColor.White);
      FastOutput.Write("2 игрока — берут по 7 костей каждый;", 0, 10, ConsoleColor.White);
      FastOutput.Write("3–4 игрока — берут по 5 костей каждый;", 0, 11, ConsoleColor.White);
      FastOutput.Write("Первый камень на центр стола кладет игрок, у которого в руке есть дубль с меньшим количеством очков, 0–0, 1–1, 2–2 и так далее.", 0, 12, ConsoleColor.White);
      FastOutput.Write("Если ни у кого нет дублей, ставится простая кость с меньшим количество очков, например, 0–1, 0–2 и так далее.", 0, 13, ConsoleColor.White);
      FastOutput.Write("Игра заканчивается когда у одного из игроков закончатся кости или когда получается «Рыба». ", 0, 14, ConsoleColor.White);
      FastOutput.Write("Все остальные игроки с костями на руках начинают подсчитывать очки. ", 0, 15, ConsoleColor.White);
      FastOutput.PrintOnConsole();
    }

    
    /// <summary>
    /// Ввод имени по окончании игры
    /// </summary>
    public void DrawInputName(int parMatchPlayerScore)
    {
      FastOutput.Write("Вы выиграли! Ваши очки:" + parMatchPlayerScore, 0, 2, ConsoleColor.White);
      FastOutput.Write("Введите ваше имя: ", 0, 1, ConsoleColor.White);
      FastOutput.PrintOnConsole();
      Console.SetCursorPosition(0, 5);
    }
    /// <summary>
    /// Ввод имени по окончании игры
    /// </summary>
    public void DrawLossMessage()
    {
      FastOutput.Write("Вы проиграли!", 0, 1, ConsoleColor.White);
      FastOutput.PrintOnConsole();
    }
    /// <summary>
    /// Выход из ввода имени по окончании игры
    /// </summary>
    public void DrawInputNameEnd()
    {
      FastOutput.Write("Нажмите Enter, чтобы вернуться в основное меню...", 0, 6, ConsoleColor.Magenta);
      FastOutput.PrintOnConsole();
    }

    /// <summary>
    /// Отрисовка рекордов
    /// </summary>
    /// <param name="parRecords">Рекорды</param>
    public void DrawRecords(List<string> parRecords)
    {
      FastOutput.Write("Топ 30:", 0, 2, ConsoleColor.Magenta);
      for (int i = 0; i < 30; i++)
      {
        FastOutput.Write(i+1+") "+parRecords[i], 0, i + 3, ConsoleColor.White);
      }
      FastOutput.PrintOnConsole();
    }

  }
}
