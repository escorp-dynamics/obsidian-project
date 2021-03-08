using Fluent;
using Gemini.Framework.Services;
using MahApps.Metro.Controls;
using Obsidian.Studio.Views;
using System.ComponentModel.Composition;

namespace Obsidian.Studio.ViewModels
{
    [Export(typeof(IMainWindow))]
    public class StudioWindowViewModel : BaseViewModel
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
            Width = 1366;
            Height = 768;
        }
    }
}
