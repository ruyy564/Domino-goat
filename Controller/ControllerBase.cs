using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View;
namespace Controller
{
  /// <summary>
  /// Абстрактный класс для контроллеров игры
  /// </summary>
  public abstract class ControllerBase
  {
    /// <summary>
    /// Представление, которым управляет контроллер
    /// </summary>
    private View.ViewBase _view;

    /// <summary>
    /// Getter и Setter для поля _view
    /// </summary>
    public View.ViewBase View { get => _view; set => _view = value; }

    /// <summary>
    /// Обработчик события нажатия на клавишу
    /// </summary>
    /// <param name="parKey">Код клавиши</param>
    public abstract void KeyDown(int parKey);

  }
}
