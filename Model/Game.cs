using System;
using System.Collections.Generic;
using System.Linq;
namespace Model
{
  /// <summary>
  /// Класс логики игры
  /// </summary>
  public class Game
  {
    /// <summary>
    /// Тип окончания партии
    /// </summary>
    public enum TypeWin { Fish, Standart }

    /// <summary>
    /// Тип окончания партии
    /// </summary>
    private TypeWin _typeGameWin;

    /// <summary>
    /// Константа с сообщением куда ходить
    /// </summary>
    const String WARNING = "Нажмите <-, чтобы походить в начало;Нажмите ->, чтобы походить в конец";

    /// <summary>
    /// Имя победителя
    /// </summary>
    private Player _winner;

    /// <summary>
    /// Выбран пропуск хода
    /// </summary>
    private Boolean _selectSkip;

    /// <summary>
    /// Сумма очков победителя
    /// </summary>
    private int _scoreWinner;

    /// <summary>
    /// Список пользователей
    /// </summary>
    private List<Player> _players;

    /// <summary>
    /// Сообщение,что игрок может пойти и в начало и в конец
    /// </summary>
    private String _message;

    /// <summary>
    /// Игрок, который ходит в данный момент
    /// </summary>
    private Player _currentTurn;

    /// <summary>
    /// Куда игрок ходит,когда домино можно походить и в начало и в конец
    /// </summary>
    private Player.TurnOnGameTable _answer;

    /// <summary>
    /// Игра окончена?
    /// </summary>
    private Boolean _gameIsEnd;

    /// <summary>
    /// Игровое поле
    /// </summary>
    private GameTable _gameTable;

    /// <summary>
    /// Базар
    /// </summary>
    private Bazzar _bazaar;

    /// <summary>
    /// Getter и Setter для поля _currentTurn
    /// </summary>
    public Player CurrentTurn { get => _currentTurn; set => _currentTurn = value; }

    /// <summary>
    /// Getter и Setter для поля _gameTable
    /// </summary>
    public GameTable GameTable { get => _gameTable; set => _gameTable = value; }

    /// <summary>
    /// Getter и Setter для поля _bazaar
    /// </summary>
    public Bazzar Bazaar { get => _bazaar; set => _bazaar = value; }

    /// <summary>
    /// Getter и Setter для поля _player
    /// </summary>
    public List<Player> Players { get => _players; set => _players = value; }

    /// <summary>
    /// Getter и Setter для поля _gameIsEnd
    /// </summary>
    public bool GameIsEnd { get => _gameIsEnd; set => _gameIsEnd = value; }

    /// <summary>
    /// Getter и Setter для поля _answer
    /// </summary>
    public Player.TurnOnGameTable Answer { get => _answer; set => _answer = value; }

    /// <summary>
    /// Getter и Setter для поля _message
    /// </summary>
    public string Message { get => _message; set => _message = value; }

    /// <summary>
    /// Getter и Setter для поля _nameWinner
    /// </summary>
    public Player Winner { get => _winner; set => _winner = value; }

    /// <summary>
    /// Getter и Setter для поля _scoreWinner
    /// </summary>
    public int ScoreWinner { get => _scoreWinner; set => _scoreWinner = value; }

    /// <summary>
    /// Getter и Setter для поля _typeGameWin
    /// </summary>
    public TypeWin TypeGameWin { get => _typeGameWin; set => _typeGameWin = value; }

    /// <summary>
    /// Getter и Setter для поля _selectSkip
    /// </summary>
    public bool SelectSkip { get => _selectSkip; set => _selectSkip = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parCountPlayers">Количество игроков</param>
    public Game(int parCountPlayers)
    {
      GameIsEnd = false;
      Players = new List<Player>();
      Bazaar = new Bazzar();
      GameTable = new GameTable();
      Players.Add(new Player(GameTable, Bazaar));
      SelectSkip = false;

      for (int i = 1; i < parCountPlayers; i++)
      {
        Players.Add((Computer)new Computer(GameTable, Bazaar));
      }
      CreateDomino();
      AddDominoToBazzar();
      Shuffle();
      DealCardToPlayer();
      SetCurrentTurn();
      Answer = Player.TurnOnGameTable.Both;
      Message = null;

      if (CurrentTurn == Players.First())
      {
        CurrentTurn.SetValueAvailableInDominoInHand();
        CurrentTurn.SetValueSelectInDominoInHand();

        if (CurrentTurn.FindSelectedDomino() == -1)
        {
          Bazaar.SelectBazzar = true;
        }
      }
      else
      {
        Turn();
      }

    }

    /// <summary>
    /// Проверка все ли пропустили ход
    /// </summary>
    /// <returns></returns>
    public Boolean CheckSkipTurn()
    {
      int count=0;
      for (int j = 0; j < Players.Count; j++)
      {
        if (Players[j].SkipTurn)
        {
          count += 1;
        }
      }
      if (count== Players.Count)
      {
        return true;
      }
      return false;
    }

    /// <summary>
    /// Проверка закончилась ли игра
    /// </summary>
    /// <returns></returns>
    public void CheckGameEnd()
    {
      if (Bazaar.Data.Count == 0)
      {
        if (CurrentTurn.FindCloseAvailableDominoFromRight(-1) == -1)
        {
          if (CurrentTurn!=Players[0])
          {
            CurrentTurn.SkipTurn = true;
          }
          if (CheckSkipTurn())
          {
            GameIsEnd = true;
            TypeGameWin = TypeWin.Fish;
          }
          else
          {
            if (CurrentTurn != Players[0])
            {
              CurrentTurn.SkipTurn = true;
              CurrentTurn.BlockTurn();
              ChangeCurrentTurn();
            }
          }
        }

        for (int j = 0; j < Players.Count; j++)
        {
          if (Players.ElementAt(j).Hand.Count == 0)
          {
            TypeGameWin = TypeWin.Standart;
            Winner = Players.ElementAt(j);
            GameIsEnd = true;
          }
        }
      }
      else
      {
        for (int j = 0; j < Players.Count; j++)
        {
          if (Players.ElementAt(j).Hand.Count == 0)
          {
            TypeGameWin = TypeWin.Standart;
            Winner = Players.ElementAt(j);
            GameIsEnd = true;
          }
        }

      }
    }

    /// <summary>
    /// Поиск победителя
    /// </summary>
    public void FindWinner()
    {
      CalcScore();

      if (TypeGameWin == TypeWin.Fish)
      {
        Player player = FindMinScorePlayer();
        Winner = player;
      }
      ScoreWinner = CalcSumScore() - Winner.Score;
    }
    /// <summary>
    /// Поиск игрока с минимальным score
    /// </summary>
    /// <returns>Игрок</returns>
    private Player FindMinScorePlayer()
    {
      int min = Players.ElementAt(0).Score;
      Player player = Players.ElementAt(0);

      for (int j = 0; j < Players.Count; j++)
      {
        if (min > Players.ElementAt(j).Score)
        {
          min = Players.ElementAt(j).Score;
          player = Players.ElementAt(j);
        }
      }

      return player;
    }

    /// <summary>
    /// Подсчет всей суммы очков игроков вместе
    /// </summary>
    /// <returns>Сумма очков</returns>
    private int CalcSumScore()
    {
      int sum = 0;

      for (int j = 0; j < Players.Count; j++)
      {
        sum += Players.ElementAt(j).Score;
      }

      return sum;
    }

    /// <summary>
    /// Подсчет очков у каждого игрока
    /// </summary>
    private void CalcScore()
    {
      for (int j = 0; j < Players.Count; j++)
      {
        Players.ElementAt(j).Score = CalcScoreWinner(Players.ElementAt(j));
      }
    }

    /// <summary>
    /// Подсчет очков у игрока
    /// </summary>
    private int CalcScoreWinner(Player parPlayer)
    {
      int score = 0;

      for (int i = 0; i < parPlayer.Hand.Count; i++)
      {
        if (parPlayer.Hand[i].Value == new int[] { 0, 0 })
        {
          score += 25;
        }
        else if (parPlayer.Hand[i].Value == new int[] { 6, 6 })
        {
          score += 50;
        }
        else
        {
          score += parPlayer.Hand[i].Value[0] + parPlayer.Hand[i].Value[1];
        }
      }

      return score;
    }

    /// <summary>
    /// Передвинуть указатель влево
    /// </summary>
    public void MoveLeft()
    {
      if (Message == WARNING)
      {
        Answer = Player.TurnOnGameTable.StartTable;
        Message = null;
        MoveAccept();

        return;
      }
      int indexSelectedDomino = CurrentTurn.FindSelectedDomino();
      int indexNewSelectedDomino = CurrentTurn.FindCloseAvailableDominoFromLeft(indexSelectedDomino);

      if (Bazaar.SelectBazzar)
      {
        if (indexNewSelectedDomino != -1)
        {
          Bazaar.SelectBazzar = false;
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
      }
      else if (indexNewSelectedDomino != -1)
      {
        CurrentTurn.Hand.ElementAt(indexSelectedDomino).Status = Domino.StatusDomino.Available;

        if (indexNewSelectedDomino < indexSelectedDomino)
        {
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
        else
        {
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
      }
    }

    /// <summary>
    /// Передвинуть указатель вправо
    /// </summary>
    public void MoveRight()
    {
      if (Message == WARNING)
      {
        Answer = Player.TurnOnGameTable.EndTable;
        Message = null;
        MoveAccept();

        return;
      }
      int indexSelectedDomino = CurrentTurn.FindSelectedDomino();
      int indexNewSelectedDomino = CurrentTurn.FindCloseAvailableDominoFromRight(indexSelectedDomino);

      if (Bazaar.SelectBazzar)
      {
        if (indexNewSelectedDomino != -1)
        {
          Bazaar.SelectBazzar = false;
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
      }
      else if (indexNewSelectedDomino != -1)
      {
        CurrentTurn.Hand.ElementAt(indexSelectedDomino).Status = Domino.StatusDomino.Available;

        if (indexNewSelectedDomino > indexSelectedDomino)
        {
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
        else
        {
          CurrentTurn.Hand.ElementAt(indexNewSelectedDomino).Status = Domino.StatusDomino.Selected;
        }
      }
    }

    /// <summary>
    /// Подтверждение действия
    /// </summary>
    public void MoveAccept()
    {
      if (Bazaar.SelectSkip)
      {
        CurrentTurn.SkipTurn = true;
        CheckGameEnd();
        CurrentTurn.BlockTurn();
        ChangeCurrentTurn();
        Turn();

        return;
      }

      if (Bazaar.SelectBazzar)
      {
        CurrentTurn.TakeDominoFromBazzar();
        CurrentTurn.Hand.Last().View = Domino.ViewDomino.Open;
        Bazaar.SelectBazzar = false;
        CurrentTurn.SetValueAvailableInDominoInHand();
        CurrentTurn.SetValueSelectInDominoInHand();
        CheckGameEnd();
      }
      else
      {
        int selectedDomino = CurrentTurn.FindSelectedDomino();
        Player.TurnOnGameTable checkTurn = CurrentTurn.CheckTurnPlayer(CurrentTurn.Hand.ElementAt(selectedDomino));

        if (checkTurn == Player.TurnOnGameTable.Both)
        {
          if (Answer == Player.TurnOnGameTable.StartTable)
          {
            CurrentTurn.MakeTurn(Answer, selectedDomino);
            Answer = Player.TurnOnGameTable.Both;
            Message = null;
          }
          else if (Answer == Player.TurnOnGameTable.EndTable)
          {

            CurrentTurn.MakeTurn(Answer, selectedDomino);
            Answer = Player.TurnOnGameTable.Both;
            Message = null;
          }
          else
          {
            Message = WARNING;

            return;
          }
        }

        if (checkTurn == Player.TurnOnGameTable.StartTable || checkTurn == Player.TurnOnGameTable.EndTable)
        {
          CurrentTurn.MakeTurn(checkTurn, selectedDomino);
        }
        CurrentTurn.BlockTurn();
        ChangeCurrentTurn();
        Turn();
      }
    }

    /// <summary>
    /// Смена хода
    /// </summary>
    public void ChangeCurrentTurn()
    {
      int indexCurrentPlayer = Players.IndexOf(CurrentTurn);
      int countPlayers = Players.Count();

      if (indexCurrentPlayer == countPlayers - 1)
      {
        CurrentTurn = Players.First();
        CurrentTurn.SetValueAvailableInDominoInHand();
        CurrentTurn.SetValueSelectInDominoInHand();
      }
      else
      {
        CurrentTurn = Players.ElementAt(++indexCurrentPlayer);
      }

      CurrentTurn.SkipTurn = false;
    }

    /// <summary>
    /// Создание домино
    /// </summary>
    public List<Domino> CreateDomino()
    {
      List<Domino> domino = new List<Domino>();

      for (int i = 0; i <= 6; i++)
      {
        for (int j = i; j <= 6; j++)
        {
          domino.Add(new Domino(new int[] { i, j }));
        }
      }

      return domino;
    }

    /// <summary>
    /// Устанавливает,кто первый ходит
    /// </summary>
    public void SetCurrentTurn()
    {
      for (int j = 0; j <= 6; j++)
      {
        for (int i = 0; i < Players.Count; i++)
        {
          for (int g = 0; g < Players.ElementAt(i).Hand.Count; g++)
          {
            int[] val = Players.ElementAt(i).Hand.ElementAt(g).Value;

            if (val[0] == j && val[1] == j)
            {
              CurrentTurn = Players.ElementAt(i);

              return;
            }
          }
        }
      }

      for (int i = 0; i <= 6; i++)
      {
        for (int j = i; j <= 6; j++)
        {
          for (int k = 0; k < Players.Count; k++)
          {
            for (int g = 0; g < Players.ElementAt(k).Hand.Count; g++)
            {
              int[] val = Players.ElementAt(k).Hand.ElementAt(g).Value;

              if (val[0] == i && val[1] == j)
              {
                CurrentTurn = Players.ElementAt(k);

                return;
              }
            }
          }
        }
      }
    }
    /// <summary>
    /// Добавление домино в базар
    /// </summary>
    public void AddDominoToBazzar()
    {
      _bazaar.Data = this.CreateDomino();
    }

    /// <summary>
    /// Перемешивание домино в базаре
    /// </summary>
    public void Shuffle()
    {
      this._bazaar.Shuffle();
    }

    /// <summary>
    /// Ход игроков
    /// </summary>
    public void Turn()
    {
      while (!GameIsEnd)
      {
        if (CurrentTurn != Players.First())
        {
          Computer currentTurn = (Computer)CurrentTurn;

          if (currentTurn.MakeTurnComputer() == 1)
          {
            CheckGameEnd();
            currentTurn.BlockTurn();
            CurrentTurn = currentTurn;

            ChangeCurrentTurn();
            if (CurrentTurn == Players.First())
            {
              return;
            }
          }
          else
          {
            currentTurn.TakeDominoFromBazzar();

            CheckGameEnd();
            if (CurrentTurn == Players.First())
            {
              return;
            }
          }
        }
      }
    }

    /// <summary>
    /// Раздача домино игрокам
    /// </summary>
    public void DealCardToPlayer()
    {
      int countDomino = (Players.Count == 2) ? 7 : 5;

      for (int i = 0; i < countDomino; i++)
      {
        for (int j = 0; j < Players.Count; j++)
        {
          Domino domino = _bazaar.Data.Last();

          if (j == 0)
          {
            domino.View = Domino.ViewDomino.Open;
          }
          Players.ElementAt(j).Hand.Add(domino);
          _bazaar.Data.RemoveAt(_bazaar.Data.Count - 1);
        }
      }
    }

  }
}
