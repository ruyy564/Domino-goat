using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс игрока-компьютера
  /// </summary>
  public class Computer : Player
  {
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parGameTable">Игровой стол</param>
    /// <param name="parBazzar">Базар</param>
    public Computer(GameTable parGameTable, Bazzar parBazzar) : base(parGameTable, parBazzar)
    {
    }

    /// <summary>
    /// Компьютер делает ход
    /// -1 - ход не был сделан
    ///  1 - ход сделан
    /// </summary>
    /// <returns>Результат хода</returns>
    public int MakeTurnComputer()
    {
      TurnOnGameTable resultCheck;
      SetValueAvailableInDominoInHand();
      SetValueSelectInDominoInHand();
      int findDomino = FindSelectedDomino();

      if (findDomino != -1)
      {
        resultCheck = CheckTurnPlayer(Hand.ElementAt(findDomino));

        if (resultCheck == TurnOnGameTable.Both)
        {
          if (new Random().Next() == 0)
          {
            resultCheck = TurnOnGameTable.StartTable;
          }
          else
          {
            resultCheck = TurnOnGameTable.EndTable;
          }
        }
        MakeTurn(resultCheck, findDomino);

        return 1;
      }

      return -1;
    }
  }
}
