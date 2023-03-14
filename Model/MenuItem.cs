using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  /// <summary>
  /// Класс пункт меню
  /// </summary>
  public class MenuItem
  {
    /// <summary>
    /// Текст пункта меню
    /// </summary>
    private string _textMenuItem;

    /// <summary>
    /// Getter и Setter для поля _textMenuItem
    /// </summary>
    public string TextMenuItem { get => _textMenuItem; set => _textMenuItem = value; }

    /// <summary>
    /// Конструктор пункта меню
    /// </summary>
    public MenuItem(string parText)
    {
      TextMenuItem = parText;
    }
  }
}
