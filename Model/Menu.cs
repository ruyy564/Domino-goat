using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс, описывающий работу меню
  /// </summary>
  public class Menu
  {
    /// <summary>
    /// Набор пунктов меню
    /// </summary>
    private List<MenuItem> _items;

    /// <summary>
    /// Gette и Setter для поля _items
    /// </summary>
    public List<MenuItem> Items { get => _items; set => _items = value; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="parMatchItems">Список передаваемых пунктов меню</param>
    /// <param name="parMatchOffsetMenu">Смещение всего меню на экране</param>
    public Menu(List<MenuItem> parMatchItems)
    {
      Items = parMatchItems;
    }
  }
}
