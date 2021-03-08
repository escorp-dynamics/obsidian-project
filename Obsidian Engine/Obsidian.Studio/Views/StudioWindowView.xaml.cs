using Fluent;
using Obsidian.Studio.ViewModels;

namespace Obsidian.Studio.Views
{
    public partial class StudioWindowView : IRibbonWindow
    {
        public RibbonTitleBar? TitleBar => (DataContext as StudioWindowViewModel)?.TitleBar;

        public StudioWindowView() => InitializeComponent();
    }
}
