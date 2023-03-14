using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTest
{
  /// <summary>
  /// Модульные тесты для класса Game
  /// </summary>
  [TestClass]
  public class ModelUnitTest
  {
    /// <summary>
    /// Проверка, что создается нужное количество игроков
    /// </summary>
    [TestMethod]
    public void CreateGameWithTwoPlayersTest()
    {
      int countPlayer = 2;
      Game game = new Game(countPlayer);
      Assert.AreEqual(countPlayer, game.Players.Count);
    }

    /// <summary>
    /// Проверка, что раздается нужное количество домино при 2 игроках
    /// </summary>
    [TestMethod]
    public void CheckCountDonimoInHandWithTwoPlayersTest()
    {
      int countPlayer = 2;
      int countDominoInHand = 7;

      Game game = new Game(countPlayer);
      Assert.AreEqual(countDominoInHand, game.Players.ElementAt(0).Hand.Count);
    }

    /// <summary>
    /// Проверка, что раздается нужное количество домино при 3 и более игроках
    /// </summary>
    [TestMethod]
    public void CheckCountDonimoInHandWithMoreThenTwoPlayersTest()
    {
      int countPlayer = 3;
      int countDominoInHand = 5;

      Game game = new Game(countPlayer);
      Assert.AreEqual(countDominoInHand, game.Players.ElementAt(0).Hand.Count);
    }

    /// <summary>
    /// Проверка, что раздается нужное количество домино
    /// </summary>
    [TestMethod]
    public void CheckCountDonimoTest()
    {
      int countPlayer = 2;
      int countDomino = 28;

      Game game = new Game(countPlayer);
      int countTakeDomino = game.Players.ElementAt(0).Hand.Count + game.Players.ElementAt(1).Hand.Count + game.Bazaar.Data.Count + game.GameTable.Data.Count;
      Assert.AreEqual(countDomino, countTakeDomino);
    }

    /// <summary>
    /// Проверка, что раздается нужное количество домино
    /// </summary>
    [TestMethod]
    public void CheckCountCreateDonimoTest()
    {
      int countPlayer = 2;
      int countDomino = 28;

      Game game = new Game(countPlayer);
      Assert.AreEqual(countDomino, game.CreateDomino().Count);
    }

    /// <summary>
    /// Проверка, что происходит смена хода
    /// </summary>
    [TestMethod]
    public void CheckAddDominoToBazzarTest()
    {
      int countPlayer = 2;

      Game game = new Game(countPlayer);
      Player currentPlayer = game.CurrentTurn;
      game.ChangeCurrentTurn();
      Player currentPlayer2 = game.CurrentTurn;
      Assert.AreNotEqual(currentPlayer, currentPlayer2);
    }

    /// <summary>
    /// Проверка, что после хода изменяется количество домино в руке
    /// </summary>
    [TestMethod]
    public void CheckChangeCountDominoInHandAfterTurn()
    {
      int countPlayer = 2;

      Game game = new Game(countPlayer);
      int countDonimoBeforeTurn = game.Players.ElementAt(0).Hand.Count;
      game.MoveAccept();
      int countDonimoAfterTurn = game.Players.ElementAt(0).Hand.Count;
      Assert.IsTrue(countDonimoBeforeTurn> countDonimoAfterTurn);
    }

    /// <summary>
    /// Проверка, что можно выбрать другое домино нажав струлку влево
    /// </summary>
    [TestMethod]
    public void CheckChangeSelectDominoMoveLeft()
    {
      int countPlayer = 2;
      Domino domino2=null;
      Domino domino=null;
      Game game = new Game(countPlayer);

      for (int i=0;i< game.Players.ElementAt(0).Hand.Count;i++)
      {
        if (game.Players.ElementAt(0).Hand[i].Status==Domino.StatusDomino.Selected)
        {
          domino = game.Players.ElementAt(0).Hand[i];
        }
      }
      game.MoveLeft();

      for (int i = 0; i < game.Players.ElementAt(0).Hand.Count; i++)
      {
        if (game.Players.ElementAt(0).Hand[i].Status == Domino.StatusDomino.Selected)
        {
          domino2 = game.Players.ElementAt(0).Hand[i];
        }
      }

      Assert.AreNotEqual(domino, domino2);
    }

    /// <summary>
    /// Проверка, что можно выбрать другое домино нажав струлку вправо
    /// </summary>
    [TestMethod]
    public void CheckChangeSelectDominoMoveRight()
    {
      int countPlayer = 2;
      Domino domino2 = null;
      Domino domino = null;
      Game game = new Game(countPlayer);

      for (int i = 0; i < game.Players.ElementAt(0).Hand.Count; i++)
      {
        if (game.Players.ElementAt(0).Hand[i].Status == Domino.StatusDomino.Selected)
        {
          domino = game.Players.ElementAt(0).Hand[i];
        }
      }
      game.MoveRight();

      for (int i = 0; i < game.Players.ElementAt(0).Hand.Count; i++)
      {
        if (game.Players.ElementAt(0).Hand[i].Status == Domino.StatusDomino.Selected)
        {
          domino2 = game.Players.ElementAt(0).Hand[i];
        }
      }

      Assert.AreNotEqual(domino, domino2);
    }

    /// <summary>
    /// Проверка, что можно нужное домино удаляется из руки после хода
    /// </summary>
    [TestMethod]
    public void CheckSelectedDominoRemoveFromHand()
    {
      int countPlayer = 2;
      Domino domino = null;
      Game game = new Game(countPlayer);
      for (int i = 0; i < game.Players.ElementAt(0).Hand.Count; i++)
      {
        if (game.Players.ElementAt(0).Hand[i].Status == Domino.StatusDomino.Selected)
        {
          domino = game.Players.ElementAt(0).Hand[i];
        }
      }
      game.MoveAccept();

      for(int i = 0; i < game.Players.ElementAt(0).Hand.Count; i++)
      {
        if (game.Players.ElementAt(0).Hand[i] == domino)
        {
          Assert.IsTrue(false);
        }
      }
      Assert.IsTrue(true);
    }
  }
}

