using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс игровое поле
  /// </summary>
  public class GameTable
  {
    /// <summary>
    /// Домино на игровом поле
    /// </summary>
    private LinkedList<Domino> _data;

    /// <summary>
    /// Getter и Setter поля _data
    /// </summary>
    public LinkedList<Domino> Data { get => _data; set => _data = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public GameTable()
    {
      Data = new LinkedList<Domino>();
    }
  }
}
