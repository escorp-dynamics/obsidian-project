using Fluent;
using Gemini.Framework.Services;
using Gemini.Modules.MainWindow.ViewModels;
using MahApps.Metro.Controls;
using Obsidian.Studio.Views;
using System.ComponentModel.Composition;

namespace Obsidian.Studio.ViewModels
{
    [Export(typeof(IMainWindow))]
    public class StudioWindowViewModel : MainWindowViewModel
    {
        private RibbonTitleBar? titleBar;

        public RibbonTitleBar? TitleBar
        {
            get => titleBar;

            private set
            {
                titleBar = value;
                NotifyOfPropertyChange(() => TitleBar);
            }
        }

        public StudioWindowViewModel() : base() { }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);

            TitleBar = (view as StudioWindowView).FindChild<RibbonTitleBar>("ribbonTitleBar");
            TitleBar.InvalidateArrange();
            TitleBar.UpdateLayout();
        }
    }
}
