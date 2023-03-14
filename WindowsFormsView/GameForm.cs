using Model;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsView
{
  /// <summary>
  /// Класс формы игры
  /// </summary>
  public partial class GameForm : Form
  {
    /// <summary>
    /// Нажатая на форме кнопка
    /// </summary>
    public Object _senderButton = new Object();

    /// <summary>
    /// Экземпляр игры
    /// </summary>
    private Game _game;

    /// <summary>
    /// Поток для отрисовки игрового поля
    /// </summary>
    private Thread _drawerThread;

    /// <summary>
    /// Шрифт для отрисовки
    /// </summary>
    private Font _drawFont;

    /// <summary>
    /// Формат отрисовки
    /// </summary>
    private StringFormat _drawFormat;

    /// <summary>
    /// Блокировочный объект для потоков
    /// </summary>
    private static Player _locker;

    /// <summary>
    /// Блокировочный объект для потоков
    /// </summary>
    protected static Player Locker { get => _locker; set => _locker = value; }

    /// <summary>
    /// Getter и Setter для поля _game
    /// </summary>
    public Game Game { get => _game; set => _game = value; }

    /// <summary>
    /// Getter и Setter для поля _drawFont
    /// </summary>
    public Font DrawFont { get => _drawFont; set => _drawFont = value; }

    /// <summary>
    /// Getter и Setter для поля _drawFormat
    /// </summary>
    public StringFormat DrawFormat { get => _drawFormat; set => _drawFormat = value; }

    /// <summary>
    /// Getter и Setter для поля _drawFormat
    /// </summary>
    public Thread DrawerThread { get => _drawerThread; set => _drawerThread = value; }

    /// <summary>
    ///  Начальные настройки игры
    /// </summary>
    public GameForm(Game parGame)
    {
      this.WindowState = FormWindowState.Maximized;
      InitializeComponent();
      Game = parGame;
      DrawFont = new Font("Arial", 8);
      DrawFormat = new StringFormat();
      Locker = parGame.Players.ElementAt(0);
      DrawerThread = new Thread(DrawGameState)
      {
        IsBackground = true
      };
      DrawerThread.Start();
    }

    /// <summary>
    /// Отобразить игровое поле
    /// </summary>
    /// <param name="parGameScreen">Изображение игрового поля</param>
    public void SetGameScreen(Bitmap parGameScreen)
    {
      if (InvokeRequired)
      {
        pictureBoxBoard.Invoke((MethodInvoker)delegate
        {
          pictureBoxBoard.Image = parGameScreen;
        });
      }
    }

    /// <summary>
    /// Потоковое отображение текущего состояние игрового поля и информации об игроках
    /// </summary>
    /// <param name="parGame"></param>
    public void DrawGameState()
    {
      while (!Game.GameIsEnd)
      {
        lock (Locker)
        {
          Bitmap gameScreen = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
          ClearImage(gameScreen);
          DrawPanel(gameScreen);
          DrawBazzar(gameScreen);
          DrawSkip(gameScreen);
          DrawGameTable(gameScreen);
          SetGameScreen(gameScreen);
          Thread.Sleep(60);
        }
      }
    }

    /// <summary>
    /// Отрисовка домино в руках у игроков
    /// </summary>
    /// <param name="parPlayers">Список игроков</param>
    /// <param name="parGame">Игра</param>
    public void DrawPanel(Bitmap parGameScreen)
    {

      SolidBrush drawBrush = new SolidBrush(Color.Magenta);
      Graphics g = Graphics.FromImage(parGameScreen);

      int x = 0;
      lock (Player.Locker)
      {
        if (Game.Message != null)
        {
          g.DrawString(Game.Message, DrawFont, drawBrush, 5, 7, DrawFormat);
        }

        for (int j = 0; j < Game.Players.Count; j++)
        {
          int y = 0;
          x = j > 1 ? x + 10 : 10;
          int count = Game.Players.ElementAt(j).Hand.Count;

          for (int i = 0; i < count; i++)
          {
            y = j == 0 ? 600 : 50;
            x += 40;
            Domino domino = Game.Players.ElementAt(j).GetDominoInHand(i);
            DrawDomino(domino, x, y, parGameScreen);
          }
        }
      }
    }

    /// <summary>
    /// Отрисовка домино
    /// </summary>
    /// <param name="parDomino">Домино</param>
    /// <param name="parX">Координата х</param>
    /// <param name="parY">Координата у</param>
    public void DrawDomino(Domino parDomino, int parX, int parY, Bitmap parGameScreen)
    {
      SolidBrush drawBrush = new SolidBrush(Color.DarkGray);
      Graphics g = Graphics.FromImage(parGameScreen);
      if (parDomino.View != Domino.ViewDomino.Close)
      {

        if (parDomino.Status == Domino.StatusDomino.Available)
        {
          drawBrush = new SolidBrush(Color.White);
        }

        if (parDomino.Status == Domino.StatusDomino.Selected)
        {
          drawBrush = new SolidBrush(Color.Magenta);
          parY -= 1;
        }
        g.DrawString("╔══╗", DrawFont, drawBrush, parX, parY - 1, DrawFormat);
        g.DrawString("║  " + parDomino.Value[0] + " ║", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("╠══╣", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("║  " + parDomino.Value[1] + " ║", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("╚══╝", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
      }
      else
      {
        drawBrush = new SolidBrush(Color.White);
        g.DrawString("╔══╗", DrawFont, drawBrush, parX, parY - 1, DrawFormat);
        g.DrawString("║     ║", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("╠══╣", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("║     ║", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
        g.DrawString("╚══╝", DrawFont, drawBrush, parX, parY += 15, DrawFormat);
      }
    }
    /// <summary>
    /// Отрисовка игрового поля
    /// </summary>
    /// <param name="parGameTable">Игровое поле</param>
    public void DrawGameTable(Bitmap parGameScreen)
    {
      SolidBrush drawBrush = new SolidBrush(Color.White);
      Graphics g = Graphics.FromImage(parGameScreen);

      int x = 10;

      for (int i = 0; i < Game.GameTable.Data.Count; i++)
      {
        int y = 300;
        int[] valueDomino = Game.GameTable.Data.ElementAt(i).Value;

        if (valueDomino[0] == valueDomino[1])
        {
          y -= 1;
          g.DrawString("╔══╗", DrawFont, drawBrush, x, y - 1, DrawFormat);
          g.DrawString("║  " + valueDomino[0] + " ║", DrawFont, drawBrush, x, y += 15, DrawFormat);
          g.DrawString("╠══╣", DrawFont, drawBrush, x, y += 15, DrawFormat);
          g.DrawString("║  " + valueDomino[1] + " ║", DrawFont, drawBrush, x, y += 15, DrawFormat);
          g.DrawString("╚══╝", DrawFont, drawBrush, x, y += 15, DrawFormat);
          x += 35;
        }
        else
        {
          g.DrawString("╔══╦══╗", DrawFont, drawBrush, x, y - 1, DrawFormat);
          g.DrawString("║  " + valueDomino[0] + " ║ " + valueDomino[1] + "  ║", DrawFont, drawBrush, x, y += 15, DrawFormat);
          g.DrawString("╚══╩══╝", DrawFont, drawBrush, x, y += 15, DrawFormat);
          x += 55;
        }
      }
    }
    /// <summary>
    /// Отрисовка базара
    /// </summary>
    /// <param name="parBazzar">Базар</param>
    public void DrawBazzar(Bitmap parGameScreen)
    {
      SolidBrush drawBrush = new SolidBrush(Color.DarkGray);
      Graphics g = Graphics.FromImage(parGameScreen);
      int x = 5;
      int y = 500;

      if (Game.Bazaar.SelectBazzar)
      {
        drawBrush = new SolidBrush(Color.Magenta);
      }
      g.DrawString("Базар", DrawFont, drawBrush, x, y - 1, DrawFormat);
      g.DrawString("╔═══╗", DrawFont, drawBrush, x, y += 15, DrawFormat);
      String space = (Game.Bazaar.Data.Count >= 10) ? "    " : "    ";
      g.DrawString("║" + Game.Bazaar.Data.Count + space + "║", DrawFont, drawBrush, x, y += 15, DrawFormat);
      g.DrawString("╚═══╝", DrawFont, drawBrush, x, y += 15, DrawFormat);

    }
    /// <summary>
    /// Отрисовка пропуска хода
    /// </summary>
    public void DrawSkip(Bitmap parGameScreen)
    {
      SolidBrush drawBrush = new SolidBrush(Color.DarkGray);
      Graphics g = Graphics.FromImage(parGameScreen);
      int x = 50;
      int y = 500;

      if (Game.Bazaar.SelectSkip)
      {
        drawBrush = new SolidBrush(Color.Magenta);
      }
      g.DrawString("Пропуск хода", DrawFont, drawBrush, x, y - 1, DrawFormat);
      g.DrawString("╔═══╗", DrawFont, drawBrush, x, y += 15, DrawFormat);
      g.DrawString("║       ║", DrawFont, drawBrush, x, y += 15, DrawFormat);
      g.DrawString("╚═══╝", DrawFont, drawBrush, x, y += 15, DrawFormat);
    }
    /// <summary>
    /// Очистить игровое поле
    /// </summary>
    public void ClearImage(Bitmap parGameScreen)
    {
      Graphics g = Graphics.FromImage(parGameScreen);
      g.Clear(Color.Black);
    }


    /// <summary>
    /// Закрытие формы
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void GameForm_FormClosing(object parSender, FormClosingEventArgs parEvent)
    {
      DrawerThread.Abort();
      _senderButton = (GameForm)parSender;
    }

    /// <summary>
    /// Нажатие на кнопку окончания игры
    /// </summary>
    /// <param name="parSender"></param>
    /// <param name="parEvent"></param>
    private void ButtonEndGame_Click(object parSender, EventArgs parEvent)
    {
      _senderButton = (Button)parSender;
    }

    
  }
}