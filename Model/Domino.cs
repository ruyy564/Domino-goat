using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс для домино
  /// </summary>
  public class Domino
  {
    /// <summary>
    /// Статус домино
    /// </summary>
    public enum StatusDomino
    {
      Selected, Available, NotAvailable
    }

    /// <summary>
    /// Вид домино
    /// </summary>
    public enum ViewDomino
    {
      Open, Close
    }

    /// <summary>
    /// Значение домино
    /// </summary>
    private int[] _value;

    /// <summary>
    /// Статус домино
    /// </summary>
    private StatusDomino _status;

    /// <summary>
    /// Вид домино
    /// </summary>
    private ViewDomino _view;

    /// <summary>
    /// Getter и Setter для поля _value
    /// </summary>
    public int[] Value { get => _value; set => _value = value; }

    /// <summary>
    /// Getter и Setter для поля _status
    /// </summary>
    public StatusDomino Status { get => _status; set => _status = value; }

    /// <summary>
    /// Getter и Setter для поля _view
    /// </summary>
    public ViewDomino View { get => _view; set => _view = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Domino(int[] parValue)
    {
      Value = parValue;
      Status = StatusDomino.NotAvailable;
      View = ViewDomino.Close;
    }
  }

}
