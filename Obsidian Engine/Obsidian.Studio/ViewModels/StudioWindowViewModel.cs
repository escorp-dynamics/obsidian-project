using Caliburn.Micro;
using Fluent;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Themes;
using Gemini.Modules.MainWindow.ViewModels;
using Gemini.Modules.Settings.Commands;
using Gemini.Modules.Settings.ViewModels;
using MahApps.Metro.Controls;
using Obsidian.Studio.Properties;
using Obsidian.Studio.Views;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

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

        public StudioWindowViewModel() : base() => IoC.Get<IThemeManager>().SetCurrentTheme(Settings.Default.ThemeName);

        protected override async void OnViewReady(object view)
        {
            base.OnViewReady(view);

            while (TitleBar == null)
            {
                TitleBar = (view as StudioWindowView).FindChild<RibbonTitleBar>("ribbonTitleBar");
                await Task.Delay(10);
            }

            TitleBar.InvalidateArrange();
            TitleBar.UpdateLayout();
        }

        protected virtual async void OnGlobalSettingsShow() => await IoC.Get<IWindowManager>().ShowDialogAsync(IoC.Get<SettingsViewModel>());
    }
}
