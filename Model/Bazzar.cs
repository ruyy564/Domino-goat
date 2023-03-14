using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс базар
  /// </summary>
  public class Bazzar
  {
    /// <summary>
    /// Домино в базаре
    /// </summary>
    private List<Domino> _data;

    /// <summary>
    /// Выбран ли базар
    /// </summary>
    private Boolean _selectBazzar;

    /// <summary>
    /// Выбран пропуск хода
    /// </summary>
    private Boolean _selectSkip;

    /// <summary>
    /// Getter и setter для поля _data
    /// </summary>
    public List<Domino> Data { get => _data; set => _data = value; }

    /// <summary>
    /// Getter и setter для поля _data
    /// </summary>
    public bool SelectBazzar { get => _selectBazzar; set => _selectBazzar = value; }

    /// <summary>
    /// Getter и Setter для поля _selectSkip
    /// </summary>
    public bool SelectSkip { get => _selectSkip; set => _selectSkip = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public Bazzar()
    {
      SelectBazzar = false;
      SelectSkip = false;
      Data = new List<Domino>();
    }

    /// <summary>
    /// Взять домино из базара
    /// </summary>
    /// <returns></returns>
    public Domino TakeDominoFromBazzar()
    {
      Domino domino = this.Data.Last();
      this.Data.RemoveAt(this.Data.Count - 1);

      return domino;
    }

    /// <summary>
    /// Перемешивание домино в базаре
    /// </summary>
    public void Shuffle()
    {
      Domino tmp;
      Random rand = new Random();

      for (int i = this.Data.Count - 1; i >= 1; i--)
      {
        int j = rand.Next(i + 1);
        tmp = this.Data[j];
        this.Data[j] = this.Data[i];
        this.Data[i] = tmp;
      }
    }
  }
}
