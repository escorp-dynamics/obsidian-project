using Fluent;
using Obsidian.Studio.ViewModels;

namespace Obsidian.Studio.Views
{
    /// <summary>
    /// Представляет главное окно приложения.
    /// </summary>
    public partial class StudioWindowView : IRibbonWindow
    {
        /// <summary>
        /// Заголовочный блок окна.
        /// </summary>
        public RibbonTitleBar? TitleBar => (DataContext as StudioWindowViewModel)?.TitleBar;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="StudioWindowView"/>.
        /// </summary>
        public StudioWindowView() => InitializeComponent();
    }
}
