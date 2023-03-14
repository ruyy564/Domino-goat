using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс игрок
  /// </summary>
  public class Player
  {
    /// <summary>
    /// Куда можно походить
    /// </summary>
    public enum TurnOnGameTable
    {
      StartTable, EndTable, Both
    }

    /// <summary>
    /// Очки игрока
    /// </summary>
    private int _score;

    /// <summary>
    /// Пропуск хода
    /// </summary>
    private Boolean _skipTurn;

    /// <summary>
    /// Рука игрока
    /// </summary>
    private List<Domino> _hand;

    /// <summary>
    /// Игровое поля
    /// </summary>
    private GameTable _gameTable;

    /// <summary>
    /// Блокировочный объект для потоков
    /// </summary>
    private static Object _locker=new Object();

    /// <summary>
    /// Базар
    /// </summary>
    private Bazzar _bazzar;

    /// <summary>
    /// Getter и Setter для поля _locker
    /// </summary>
    public static Object Locker { get => _locker; set => _locker = value; }

    /// <summary>
    /// Getter и Setter для поля _hand
    /// </summary>
    public List<Domino> Hand { get => _hand; set => _hand = value; }

    /// <summary>
    /// Getter и Setter для поля _gameTable
    /// </summary>
    public GameTable GameTable { get => _gameTable; set => _gameTable = value; }

    /// <summary>
    /// Getter и Setter для поля _bazzar
    /// </summary>
    public Bazzar Bazzar { get => _bazzar; set => _bazzar = value; }

    /// <summary>
    /// Getter и Setter для поля _score
    /// </summary>
    public int Score { get => _score; set => _score = value; }

    /// <summary>
    /// Getter и Setter для поля _skipTurn
    /// </summary>
    public bool SkipTurn { get => _skipTurn; set => _skipTurn = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Player(GameTable parGameTable, Bazzar parBazzar)
    {
      Hand = new List<Domino>();
      GameTable = parGameTable;
      Bazzar = parBazzar;
      SkipTurn = false;
    }

    /// <summary>
    /// Получить домино из руки
    /// </summary>
    /// <param name="parIndex">Индекс домино</param>
    /// <returns>Домино</returns>
    public Domino GetDominoInHand(int parIndex)
    {
      Domino domino=null;
      lock (Locker)
      {
        domino = Hand.ElementAt(parIndex);
      }
      return domino;
    }
    /// <summary>
    /// Добавление домино из базара в руку
    /// </summary>
    public void TakeDominoFromBazzar()
    {
      if (Bazzar.Data.Count != 0)
      {
        _hand.Add(Bazzar.TakeDominoFromBazzar());
      }
    }

    /// <summary>
    /// Ищет выбранную домино
    /// </summary>
    /// <returns>Позиция выбранного домино</returns>
    public int FindSelectedDomino()
    {
      for (int i = Hand.Count - 1; i >= 0; i--)
      {
        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Selected)
        {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Поиск ближайшего доступного домино слева
    /// </summary>
    /// <param name="parCurrentPosition">Текущая позиция</param>
    /// <returns>Индекс доступного домино</returns>
    public int FindCloseAvailableDominoFromLeft(int parCurrentPosition)
    {
      for (int i = parCurrentPosition; i >= 0; i--)
      {
        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Available)
        {
          return i;
        }
      }

      for (int i = Hand.Count - 1; i > parCurrentPosition; i--)
      {
        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Available)
        {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Блокировка хода игрока
    /// </summary>
    public void BlockTurn()
    {
      for (int i = Hand.Count - 1; i >= 0; i--)
      {
        Hand.ElementAt(i).Status = Domino.StatusDomino.NotAvailable;

      }
      Bazzar.SelectBazzar = false;
      Bazzar.SelectSkip = false;
    }

    /// <summary>
    /// Поиск ближайшего доступного домино слева
    /// </summary>
    /// <param name="parCurrentPosition">Текущая позиция</param>
    /// <returns>Индекс доступного домино</returns>
    public int FindCloseAvailableDominoFromRight(int parCurrentPosition)
    {

      for (int i = parCurrentPosition; i < Hand.Count; i++)
      {
        if (parCurrentPosition == -1)
        {
          break;
        }

        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Available)
        {
          return i;
        }
      }
      for (int i = 0; i < Hand.Count; i++)
      {
        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Available)
        {
          return i;
        }
      }

      return -1;
    }

    /// <summary>
    /// Сделать ход
    /// </summary>
    /// <param name="parTurn">Результат:можно ли ходить домино</param>
    /// <param name="parIndexDominoInHand">Индекс выбранного домино в руке</param>
    public void MakeTurn(TurnOnGameTable parTurn, int parIndexDominoInHand)
    {
      Domino domino = _hand.ElementAt(parIndexDominoInHand);
      this.DeleteDomino(parIndexDominoInHand);
      domino.View = Domino.ViewDomino.Open;

      if (parTurn == TurnOnGameTable.StartTable)
      {
        if (GameTable.Data.Count != 0 && domino.Value[1] != GameTable.Data.First().Value[0])
        {
          domino = ReplaceValueDomino(domino);
        }
        GameTable.Data.AddFirst(domino);
      }

      if (parTurn == TurnOnGameTable.EndTable)
      {
        if (GameTable.Data.Count != 0 && domino.Value[0] != GameTable.Data.Last().Value[1])
        {
          domino = ReplaceValueDomino(domino);
        }
        GameTable.Data.AddLast(domino);
      }
    }

    /// <summary>
    /// Меняет значения домино местами для правильного вывода в игровом поле
    /// </summary>
    /// <param name="parDomino">Домино</param>
    /// <returns>Домино</returns>
    public Domino ReplaceValueDomino(Domino parDomino)
    {
      int tmp = parDomino.Value[1];
      parDomino.Value[1] = parDomino.Value[0];
      parDomino.Value[0] = tmp;

      return parDomino;
    }

    /// <summary>
    /// Удалить домино из руки
    /// </summary>
    /// <param name="parDomino">Индек домино в руке</param>
    public void DeleteDomino(int parDomino)
    {
      Domino domino = GetDominoInHand(parDomino);
      Hand.Remove(domino);
    }

    /// <summary>
    /// Устанавливает доступность домино в руке
    /// </summary>
    public void SetValueAvailableInDominoInHand()
    {
      for (int i = Hand.Count - 1; i >= 0; i--)
      {
        if (this.GameTable.Data.Count == 0 || this.CheckTurnInFirst(Hand.ElementAt(i)) || this.CheckTurnInLast(Hand.ElementAt(i)))
        {
          Hand.ElementAt(i).Status = Domino.StatusDomino.Available;
        }
        else
        {
          Hand.ElementAt(i).Status = Domino.StatusDomino.NotAvailable;
        }
      }
    }

    /// <summary>
    /// Устанавливает выбраное домино в руке
    /// </summary>
    public void SetValueSelectInDominoInHand()
    {
      for (int i = Hand.Count - 1; i >= 0; i--)
      {
        if (Hand.ElementAt(i).Status == Domino.StatusDomino.Available)
        {
          Hand.ElementAt(i).Status = Domino.StatusDomino.Selected;

          return;
        }
      }
      if (Bazzar.Data.Count == 0)
      {
        Bazzar.SelectSkip = true;
      }
      else
      {
        Bazzar.SelectBazzar = true;
      }
    }

    /// <summary>
    /// Проверка хода игрока
    /// <param name="parDomino">Домино, которым ходит игрок</param>
    /// </summary>
    /// <returns>Результат проверки</returns>
    public TurnOnGameTable CheckTurnPlayer(Domino parDomino)
    {
      if (GameTable.Data.Count == 0)
      {
        return TurnOnGameTable.EndTable;
      }

      if (this.CheckTurnInFirst(parDomino) && this.CheckTurnInLast(parDomino))
      {
        return TurnOnGameTable.Both;
      }

      if (this.CheckTurnInFirst(parDomino))
      {
        return TurnOnGameTable.StartTable;
      }
      else
      {
        return TurnOnGameTable.EndTable;
      }
    }

    /// <summary>
    /// Проверка хода игрока:домино можно добавить в начало списка стола?
    /// </summary>
    /// <param name="parDomino">Домино, которым ходит игрок</param>
    /// <returns>Результат проверки</returns>
    public Boolean CheckTurnInFirst(Domino parDomino)
    {
      int[] firstDomino = _gameTable.Data.First().Value;

      if (firstDomino[0] == parDomino.Value[0] || firstDomino[0] == parDomino.Value[1])
      {
        return true;
      }

      return false;
    }

    /// <summary>
    /// Проверка хода игрока:домино можно добавить в конец списка стола?
    /// </summary>
    /// <param name="parDomino">Домино, которым ходит игрок</param>
    /// <returns>Результат проверки</returns>
    public Boolean CheckTurnInLast(Domino parDomino)
    {
      int[] lastDomino = _gameTable.Data.Last().Value;
      if (lastDomino[1] == parDomino.Value[0] || lastDomino[1] == parDomino.Value[1])
      {
        return true;
      }

      return false;
    }
  }
}
